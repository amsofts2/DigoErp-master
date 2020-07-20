using DigoErp.Repository.Edmx;
using DigoErp.Service.Extentions;
using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DigoErp.Service.Services
{
    public class TransactionService : BaseService
    {
        public DataTableResponse<Transaction> GetResponse(DataTableSearchModel searchModel)
        {
            try
            {
                var take = Take(searchModel, out int skip);

                if (!string.IsNullOrEmpty(searchModel.SearchTerm))
                {
                    System.Linq.Expressions.Expression<Func<Tbl_Transaction, bool>> filter =
                        b =>
                        b.TransactionType.ToString().Contains(searchModel.SearchTerm) ||
                        b.Tbl_Category.Name.ToString().Contains(searchModel.SearchTerm);

                    var tableResponse = UnitOfWork.TransactionRepository.GetPagination<Tbl_Transaction>(take, skip, filter, c => c.OrderBy(o => o.Date));

                    var response = new DataTableResponse<Transaction>
                    {
                        data = tableResponse.data.Select(p => p.MapFrom()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
                else
                {

                    var tableResponse = UnitOfWork.TransactionRepository.GetPagination<Tbl_Transaction>(take, skip, null, c => c.OrderBy(o => o.Date));

                    var response = new DataTableResponse<Transaction>
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
                return new DataTableResponse<Transaction>();
            }
        }

        public List<ReconciliationTransaction> GetTransactionsForReconciliation(Reconciliation reconciliation)
        {
            try
            {

                DateTime startDate = new DateTime(); DateTime endDate = new DateTime();
                if (string.IsNullOrEmpty(reconciliation.StartDate))
                {
                    startDate = DateTime.Now.StartOfMonth();
                }
                else
                {
                    startDate = reconciliation.StartDate.ParseStringDate();
                }
                if (string.IsNullOrEmpty(reconciliation.EndDate))
                {
                    endDate = startDate.EndOfMonth();
                }
                else
                {
                    endDate = reconciliation.EndDate.ParseStringDate();
                }
                System.Linq.Expressions.Expression<Func<Tbl_Transaction, bool>> filter =
                        b =>
                        b.AccountId == reconciliation.AccountId &&
                        b.Created_At >= startDate &&
                        b.Created_At <= endDate;

                var transactions = UnitOfWork.TransactionRepository.Get(filter, x => x.OrderByDescending(y => y.Created_At));
                return transactions.Select(x => x.MapForReconciliation()).ToList();
            }
            catch (Exception)
            {
                return new List<ReconciliationTransaction>();
            }
        }
    }
}
