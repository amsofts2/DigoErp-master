using DigoErp.Repository.Edmx;
using DigoErp.Service.Extentions;
using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using DigoErp.Service.Enums;

namespace DigoErp.Service.Services
{
    public class InvoiceService : BaseService
    {
        public DataTableResponse<Invoice> GetResponse(DataTableSearchModel searchModel)
        {
            try
            {
                var take = Take(searchModel, out int skip);

                if (!string.IsNullOrEmpty(searchModel.SearchTerm))
                {
                    System.Linq.Expressions.Expression<Func<Tbl_Invoice, bool>> filter =

                        b => b.Tbl_Customer.Name.ToString().Contains(searchModel.SearchTerm) ||
                        b.InvoiceNumber.ToString().Contains(searchModel.SearchTerm) ||
                        b.InvoiceDate.ToString().Contains(searchModel.SearchTerm) ||
                        b.DueDate.ToString().Contains(searchModel.SearchTerm);

                    var tableResponse = UnitOfWork.InvoiceRepository.GetPagination<Tbl_Invoice>(take, skip, filter, c => c.OrderByDescending(o => o.InvoiceNumber));

                    var response = new DataTableResponse<Invoice>
                    {
                        data = tableResponse.data.Select(p => p.MapFrom()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
                else
                {

                    var tableResponse = UnitOfWork.InvoiceRepository.GetPagination<Tbl_Invoice>(take, skip, null, c => c.OrderByDescending(o => o.InvoiceNumber));

                    var response = new DataTableResponse<Invoice>
                    {
                        data = tableResponse.data.Select(p => p.MapFrom()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new DataTableResponse<Invoice>();
            }
        }

        public string GenerateInvoiceNumber()
        {
            var maxId = UnitOfWork.InvoiceRepository.GetMaxId(x => x.Id);
            if (maxId > 0)
            {
                var invoice = UnitOfWork.InvoiceRepository.GetByID(maxId);
                var number = string.Empty;
                try
                {
                    number = (long.Parse(invoice.InvoiceNumber.Split('-')[1]) + 1).ToString("D5");
                }
                catch (Exception)
                {
                }
                invoice.InvoiceNumber = "INV-" + number;
                return invoice.InvoiceNumber;
            }
            else
            {
                return "INV-00001";
            }
        }

        public Invoice GetById(long invoiceId)
        {
            try
            {
                return UnitOfWork.InvoiceRepository.GetByID(invoiceId)?.MapFrom();
            }
            catch (Exception)
            {
                return new Invoice();
            }
        }
        public void AddOrUpdate(Invoice invoice)
        {
            if (invoice.Id > 0)
            {
                var dbRecord = UnitOfWork.InvoiceRepository.GetByIdAsNoTracking(invoice.Id);
                var invoiceItems = UnitOfWork.InvoiceItemRepository.Get(x => x.InvoiceId == invoice.Id);
                foreach (var tblInvoiceItem in invoiceItems)
                {
                    UnitOfWork.InvoiceItemRepository.Delete(tblInvoiceItem);
                    UnitOfWork.Save();
                }
                foreach (var item in invoice.InvoiceItems)
                {
                    UnitOfWork.InvoiceItemRepository.Insert(item.MapFrom(invoice.Id));
                    UnitOfWork.Save();
                }
                invoice.InvoiceItems = null;
                invoice.Created_At = dbRecord.Created_At;
                UnitOfWork.InvoiceRepository.Update(invoice.MapFrom());
            }
            else
            {
                UnitOfWork.InvoiceRepository.Insert(invoice.MapFrom());
            }
            UnitOfWork.Save();
        }

        public Invoice GetByInvoiceNumber(string number)
        {
            try
            {
                var invoice = UnitOfWork.InvoiceRepository.Get(x => x.InvoiceNumber == number)?.FirstOrDefault();
                return invoice?.MapFrom();
            }
            catch (Exception)
            {
                return new Invoice();
            }
        }

        public List<Invoice> GetAll()
        {
            try
            {
                var invoice = UnitOfWork.InvoiceRepository.Get();
                return invoice.Select(b => b.MapFrom()).ToList();
            }
            catch (Exception)
            {
                return new List<Invoice>();
            }
        }

        public void Delete(long invoiceId)
        {
            var invoiceItems = UnitOfWork.InvoiceItemRepository.Get(x => x.InvoiceId == invoiceId);
            foreach (var item in invoiceItems)
            {
                UnitOfWork.InvoiceItemRepository.Delete(item.Id);
            }
            UnitOfWork.InvoiceRepository.Delete(invoiceId);
            UnitOfWork.Save();
            UnitOfWork.Dispose();
        }

        public bool UpdateStatus(long invoiceId, string status)
        {
            UnitOfWork.InvoiceRepository.RunSqlQuery("Update Tbl_Invoice set Status='" + status + "' where Id=" + invoiceId);
            if (status == InvoiceStatus.PAID.ToString())
            {
                MoveInvoiceToRevenue(invoiceId);
            }
            return true;
        }

        private void MoveInvoiceToRevenue(long invoiceId)
        {
            try
            {
                var invoice = UnitOfWork.InvoiceRepository.GetByID(invoiceId);
                UnitOfWork.RevenueRepository.Insert(new Tbl_Transaction
                {
                    Date = DateTime.Now.ToString("dd-MM-yyyy"),
                    Amount = invoice.GrandTotal,
                    CustomerId = invoice.CustomerId,
                    CategoryId = invoice.CategoryId,
                    Recurring = invoice.Recurring,
                    TransactionType = (int?) TransactionType.Income
                });
                UnitOfWork.Save();
            }
            catch (Exception)
            {

            }
        }
    }
}
