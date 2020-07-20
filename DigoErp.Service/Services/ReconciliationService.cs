using DigoErp.Repository.Edmx;
using DigoErp.Service.Enums;
using DigoErp.Service.Extentions;
using DigoErp.Service.Models;
using System;
using System.Linq;

namespace DigoErp.Service.Services
{
    public class ReconciliationService : BaseService
    {
        public DataTableResponse<Reconciliation> GetResponse(DataTableSearchModel searchModel)
        {
            try
            {
                var take = Take(searchModel, out int skip);

                if (!string.IsNullOrEmpty(searchModel.SearchTerm))
                {
                    System.Linq.Expressions.Expression<Func<Tbl_Reconciliation, bool>> filter =

                        b => b.Created_By == searchModel.Created_By && ( b.Tbl_Account.AccountName.ToString().Contains(searchModel.SearchTerm) ||
                        b.StartDate.ToString().Contains(searchModel.SearchTerm) ||
                        b.EndDate.ToString().Contains(searchModel.SearchTerm));

                    var tableResponse = UnitOfWork.ReconciliationRepository.GetPagination<Tbl_Reconciliation>(take, skip, filter, c => c.OrderByDescending(o => o.Created_At));

                    var response = new DataTableResponse<Reconciliation>
                    {
                        data = tableResponse.data.Select(p => p.MapFrom()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
                else
                {

                    System.Linq.Expressions.Expression<Func<Tbl_Reconciliation, bool>> filter =
                        b => b.Created_By == searchModel.Created_By;

                    var tableResponse = UnitOfWork.ReconciliationRepository.GetPagination<Tbl_Reconciliation>(take, skip, filter, c => c.OrderByDescending(o => o.Created_At));

                    var response = new DataTableResponse<Reconciliation>
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
                return new DataTableResponse<Reconciliation>();
            }
        }

        public Reconciliation GetById(long reconciliationId)
        {
            try
            {
                return UnitOfWork.ReconciliationRepository.GetByID(reconciliationId)?.MapFrom();
            }
            catch (Exception)
            {
                return new Reconciliation();
            }
        }

        public void AddOrUpdate(Reconciliation reconciliation)
        {
            if (reconciliation.Id > 0)
            {
                var dbRecord = UnitOfWork.ReconciliationRepository.GetByIdAsNoTracking(reconciliation.Id);
                reconciliation.Created_At = dbRecord.Created_At;
                reconciliation.Status = dbRecord.Status;

                var reconciliation_Transactions = UnitOfWork.ReconciliationTransactionRepository.Get(x => x.ReconciliationId == reconciliation.Id);
                foreach (var reconciliation_Transaction in reconciliation_Transactions)
                {
                    UnitOfWork.ReconciliationTransactionRepository.Delete(reconciliation_Transaction);
                    UnitOfWork.Save();
                }
                foreach (var item in reconciliation.Transactions)
                {
                    UnitOfWork.ReconciliationTransactionRepository.Insert(item.MapFrom(reconciliation.Id));
                    UnitOfWork.Save();
                }
                reconciliation.Transactions = null;
                UnitOfWork.ReconciliationRepository.Update(reconciliation.MapFrom());
            }
            else
            {
                reconciliation.Status = ReconciliationStatus.Unreconciled.ToString();
                UnitOfWork.ReconciliationRepository.Insert(reconciliation.MapFrom());
            }
            UnitOfWork.Save();
        }

        public void Delete(long reconciliationId)
        {
            UnitOfWork.ReconciliationRepository.Delete(reconciliationId);
            UnitOfWork.Save();
            UnitOfWork.Dispose();
        }
    }
}