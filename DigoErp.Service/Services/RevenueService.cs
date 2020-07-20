
using DigoErp.Repository.Edmx;
using DigoErp.Service.Enums;
using DigoErp.Service.Extentions;
using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigoErp.Service.Services
{
    public class RevenueService : BaseService
    {
        public DataTableResponse<Revenue> GetResponse(DataTableSearchModel searchModel)
        {
            try
            {
                var take = Take(searchModel, out int skip);

                if (!string.IsNullOrEmpty(searchModel.SearchTerm))
                {
                    System.Linq.Expressions.Expression<Func<Tbl_Transaction, bool>> filter =
                        b => b.TransactionType== (int)TransactionType.Income && (
                        b.Date.ToString().Contains(searchModel.SearchTerm) ||
                        b.Tbl_Account.AccountName.ToString().Contains(searchModel.SearchTerm));

                    var tableResponse = UnitOfWork.TransactionRepository.GetPagination<Tbl_Transaction>(take, skip, filter, c => c.OrderBy(o => o.Date));

                    var response = new DataTableResponse<Revenue>
                    {
                        data = tableResponse.data.Select(p => p.MapForRevenue()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
                else
                {
                    System.Linq.Expressions.Expression<Func<Tbl_Transaction, bool>> filter =
                      b => b.TransactionType == (int)TransactionType.Income;
                    var tableResponse = UnitOfWork.TransactionRepository.GetPagination<Tbl_Transaction>(take, skip, filter, c => c.OrderBy(o => o.Date));

                    var response = new DataTableResponse<Revenue>
                    {
                        data = tableResponse.data.Select(p => p.MapForRevenue()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new DataTableResponse<Revenue>();
            }
        }

        public Revenue GetById(int batchId)
        {
            try
            {
                return UnitOfWork.RevenueRepository.GetByID(batchId)?.MapForRevenue();
            }
            catch (Exception)
            {
                return new Revenue();
            }
        }

        public void AddOrUpdate(Revenue revenue)
        {
            if (revenue.Id > 0)
            {
                var dbRecord = UnitOfWork.RevenueRepository.GetByIdAsNoTracking(revenue.Id);
                revenue.Created_At = dbRecord.Created_At;
                UnitOfWork.RevenueRepository.Update(revenue.MapFrom());
            }
            else
            {
                UnitOfWork.RevenueRepository.Insert(revenue.MapFrom());
            }
            UnitOfWork.Save();
        }

        public List<Revenue> GetAll()
        {
            try
            {
                var revenue = UnitOfWork.RevenueRepository.Get();
                return revenue.Select(b => b.MapForRevenue()).ToList();
            }
            catch (Exception)
            {
                return new List<Revenue>();
            }
        }

        public void Delete(int batchId)
        {
            UnitOfWork.RevenueRepository.Delete(batchId);
            UnitOfWork.Save();
            UnitOfWork.Dispose();
        }
    }
}
