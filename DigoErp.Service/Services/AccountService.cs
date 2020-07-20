using DigoErp.Repository.Edmx;
using DigoErp.Service.Extentions;
using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigoErp.Service.Services
{
    public class AccountService : BaseService
    {
        public DataTableResponse<Account> GetResponse(DataTableSearchModel searchModel)
        {
            try
            {
                var currentUser = HttpContext.Current.Session["UserDetail"] as User ?? new User();

                var take = Take(searchModel, out int skip);

                if (!string.IsNullOrEmpty(searchModel.SearchTerm))
                {
                    System.Linq.Expressions.Expression<Func<Tbl_Account, bool>> filter =
                        b =>
                        b.Number.ToString().Contains(searchModel.SearchTerm) ||
                        b.AccountName.ToString().Contains(searchModel.SearchTerm);

                    var tableResponse = UnitOfWork.AccountRepository.GetPagination<Tbl_Account>(take, skip, filter, c => c.OrderBy(o => o.Number));

                    var response = new DataTableResponse<Account>
                    {
                        data = tableResponse.data.Select(p => p.MapFrom()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
                else
                {

                    var tableResponse = UnitOfWork.AccountRepository.GetPagination<Tbl_Account>(take, skip, null, c => c.OrderBy(o => o.Number));

                    var response = new DataTableResponse<Account>
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
                return new DataTableResponse<Account>();
            }
        }

        public Account GetById(int batchId)
        {
            try
            {
                return UnitOfWork.AccountRepository.GetByID(batchId)?.MapFrom();
            }
            catch (Exception)
            {
                return new Account();
            }
        }
        public void AddOrUpdateAccount(Account account)
        {
            if (account.Id > 0)
            {
                var dbRecord = UnitOfWork.AccountRepository.GetByIdAsNoTracking(account.Id);
                account.Created_At = dbRecord.Created_At;
                UnitOfWork.AccountRepository.Update(account.MapFrom());
            }
            else
            {
                UnitOfWork.AccountRepository.Insert(account.MapFrom());
            }
            UnitOfWork.Save();
        }

        public List<Account> GetAll()
        {
            try
            {
                var account = UnitOfWork.AccountRepository.Get(a => a.IsEnabled == null || (a.IsEnabled != null && (bool)a.IsEnabled));
                return account.Select(b => b.MapFrom()).ToList();
            }
            catch (Exception)
            {
                return new List<Account>();
            }
        }

        public Account GetByAccountName(string number)
        {
            try
            {
                var account = UnitOfWork.AccountRepository.Get(x => x.Number == number)?.FirstOrDefault();
                return account?.MapFrom();
            }
            catch (Exception)
            {
                return new Account();
            }
        }

        public void Delete(int batchId)
        {
            UnitOfWork.AccountRepository.Delete(batchId);
            UnitOfWork.Save();
            UnitOfWork.Dispose();
        }

    }
}
