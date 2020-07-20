using DigoErp.Repository.Edmx;
using DigoErp.Service.Enums;
using DigoErp.Service.Extentions;
using DigoErp.Service.Models;
using System;
using System.Linq;

namespace DigoErp.Service.Services
{
    public class PaymentService : BaseService
    {
        public DataTableResponse<Payment> GetResponse(DataTableSearchModel searchModel)
        {
            try
            {
                var take = Take(searchModel, out int skip);

                if (!string.IsNullOrEmpty(searchModel.SearchTerm))
                {
                    System.Linq.Expressions.Expression<Func<Tbl_Transaction, bool>> filter =
                        b => b.TransactionType == (int)TransactionType.Expense && (
                        b.Date.ToString().Contains(searchModel.SearchTerm)
                        || b.Tbl_Account.AccountName.Contains(searchModel.SearchTerm)
                        || b.Tbl_Vendor.Name.Contains(searchModel.SearchTerm)
                        || b.Tbl_Category.Name.Contains(searchModel.SearchTerm));

                    var tableResponse = UnitOfWork.TransactionRepository.GetPagination<Tbl_Transaction>(take, skip, filter, c => c.OrderBy(o => o.Date));

                    var response = new DataTableResponse<Payment>
                    {
                        data = tableResponse.data.Select(p => p.MapForPayment()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
                else
                {
                    System.Linq.Expressions.Expression<Func<Tbl_Transaction, bool>> filter =
                       b => b.TransactionType == (int)TransactionType.Expense;
                    var tableResponse = UnitOfWork.TransactionRepository.GetPagination<Tbl_Transaction>(take, skip, filter, c => c.OrderBy(o => o.Date));

                    var response = new DataTableResponse<Payment>
                    {
                        data = tableResponse.data.Select(p => p.MapForPayment()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new DataTableResponse<Payment>();
            }
        }

        public Payment GetById(int paymentId)
        {
            try
            {
                return UnitOfWork.PaymentRepository.GetByID(paymentId)?.MapForPayment();
            }
            catch (Exception)
            {
                return new Payment();
            }
        }
        public void AddOrUpdatePayment(Payment payment)
        {
            if (payment.Id > 0)
            {
               var dbRecord = UnitOfWork.PaymentRepository.GetByIdAsNoTracking(payment.Id);
                payment.Created_At = dbRecord.Created_At;
                UnitOfWork.PaymentRepository.Update(payment.MapFrom());
            }
            else
            {
                UnitOfWork.PaymentRepository.Insert(payment.MapFrom());
            }
            UnitOfWork.Save();
        }

        public void Delete(int paymentId)
        {
            UnitOfWork.PaymentRepository.Delete(paymentId);
            UnitOfWork.Save();
            UnitOfWork.Dispose();
        }
    }
}
