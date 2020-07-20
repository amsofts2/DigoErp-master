
using DigoErp.Repository.Edmx;
using DigoErp.Service.Extentions;
using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DigoErp.Service.Enums;

namespace DigoErp.Service.Services
{
    public class BillService : BaseService
    {
        public DataTableResponse<Bill> GetResponse(DataTableSearchModel searchModel)
        {
            try
            {
                var currentUser = HttpContext.Current.Session["UserDetail"] as User ?? new User();

                var take = Take(searchModel, out int skip);

                if (!string.IsNullOrEmpty(searchModel.SearchTerm))
                {
                    System.Linq.Expressions.Expression<Func<Tbl_Bill, bool>> filter =
                        b =>
                        b.Number.ToString().Contains(searchModel.SearchTerm);

                    var tableResponse = UnitOfWork.BillRepository.GetPagination<Tbl_Bill>(take, skip, filter, c => c.OrderBy(o => o.Number));

                    var response = new DataTableResponse<Bill>
                    {
                        data = tableResponse.data.Select(p => p.MapFrom()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
                else
                {
                    
                    var tableResponse = UnitOfWork.BillRepository.GetPagination<Tbl_Bill>(take, skip, null, c => c.OrderBy(o => o.Number));

                    var response = new DataTableResponse<Bill>
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
                return new DataTableResponse<Bill>();
            }
        }

        public Bill GetById(long bill_Id)
        {
            try
            {
                return UnitOfWork.BillRepository.GetByID(bill_Id)?.MapFrom();
            }
            catch (Exception)
            {
                return new Bill();
            }
        }

        public string GenerateBillNumber()
        {
            var maxId = UnitOfWork.BillRepository.GetMaxId(x => x.Id);
            if (maxId > 0)
            {
                var invoice = UnitOfWork.BillRepository.GetByID(maxId);
                var number = string.Empty;
                try
                {
                    number = (long.Parse(invoice.Number.Split('-')[1]) + 1).ToString("D5");
                }
                catch (Exception)
                {
                }
                invoice.Number = "BIL-" + number;
                return invoice.Number;
            }
            else
            {
                return "BIL-00001";
            }
        }

        public void AddOrUpdate(Bill bill)
        {
            if (bill.Id > 0)
            {
                var dbRecord = UnitOfWork.BillRepository.GetByIdAsNoTracking(bill.Id);
                var billItems = UnitOfWork.Bill_ItemRepository.Get(x => x.Bill_Id == bill.Id);
                foreach (var billItem in billItems)
                {
                    UnitOfWork.Bill_ItemRepository.Delete(billItem);
                    UnitOfWork.Save();
                }
                foreach (var item in bill.Bill_Items)
                {
                    UnitOfWork.Bill_ItemRepository.Insert(item.MapFrom(bill.Id));
                    UnitOfWork.Save();
                }
                bill.Bill_Items = null;
                bill.Created_At = dbRecord.Created_At;
                UnitOfWork.BillRepository.Update(bill.MapFrom());
            }
            else
            {
                UnitOfWork.BillRepository.Insert(bill.MapFrom());
            }
            UnitOfWork.Save();
        }

        public Bill GetByBillNumber(string number)
        {
            try
            {
                var bill = UnitOfWork.BillRepository.Get(x => x.Number == number)?.FirstOrDefault();
                return bill?.MapFrom();
            }
            catch (Exception)
            {
                return new Bill();
            }
        }

        public List<Bill> GetAll()
        {
            try
            {
                var bill = UnitOfWork.BillRepository.Get();
                return bill.Select(b => b.MapFrom()).ToList();
            }
            catch (Exception)
            {
                return new List<Bill>();
            }
        }

        public void Delete(int batchId)
        {
            UnitOfWork.BillRepository.Delete(batchId);
            UnitOfWork.Save();
            UnitOfWork.Dispose();
        }

        public bool UpdateStatus(long bill_Id, string status)
        {
            UnitOfWork.BillRepository.RunSqlQuery("Update Tbl_Bill set Status='" + status + "' where Id=" + bill_Id);
            if (status == InvoiceStatus.PAID.ToString())
            {
                MoveBillToExpense(bill_Id);
            }
            return true;
        }
        private void MoveBillToExpense(long bill_Id)
        {
            try
            {
                var invoice = UnitOfWork.BillRepository.GetByID(bill_Id);
                UnitOfWork.PaymentRepository.Insert(new Tbl_Transaction
                {
                    Date = DateTime.Now.ToString("dd-MM-yyyy"),
                    Amount = invoice.GrandTotal,
                    VendorId = invoice.VendorId,
                    CategoryId = invoice.CategoryId,
                    Recurring = invoice.Recurring,
                    TransactionType = (int?)TransactionType.Expense
                });
                UnitOfWork.Save();
            }
            catch (Exception)
            {

            }
        }
    }
}
