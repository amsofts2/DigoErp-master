using DigoErp.Service.Extentions;
using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using DigoErp.Repository.Edmx;

namespace DigoErp.Service.Services
{
    public class CurrencyService : BaseService
    {
        public List<Currency> GetAll()
        {
            try
            {
                var currency = UnitOfWork.CurrencyRepository.Get(c => c.Enabled == null || (c.Enabled != null && (bool)c.Enabled));
                return currency.Select(b => b.MapFrom()).ToList();
            }
            catch (Exception)
            {
                return new List<Currency>();
            }
        }

        public DataTableResponse<Currency> GetResponse(DataTableSearchModel searchModel)
        {
            try
            {

                var take = Take(searchModel, out int skip);

                if (!string.IsNullOrEmpty(searchModel.SearchTerm))
                {
                    System.Linq.Expressions.Expression<Func<Tbl_Currency, bool>> filter =
                        b => b.Name.Contains(searchModel.SearchTerm)
                        || b.Code.Contains(searchModel.SearchTerm)
                        ;

                    var tableResponse = UnitOfWork.CurrencyRepository.GetPagination<Tbl_Currency>(take, skip, filter, c => c.OrderBy(o => o.Name));

                    var response = new DataTableResponse<Currency>
                    {
                        data = tableResponse.data.Select(p => p.MapFrom()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
                else
                {

                    var tableResponse = UnitOfWork.CurrencyRepository.GetPagination<Tbl_Currency>(take, skip, null, c => c.OrderBy(o => o.Name));

                    var response = new DataTableResponse<Currency>
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
                return new DataTableResponse<Currency>();
            }
        }

        public Currency GetById(long currencyId)
        {
            try
            {
                return UnitOfWork.CurrencyRepository.GetByID(currencyId)?.MapFrom();
            }
            catch (Exception)
            {
                return new Currency();
            }
        }

        public void AddOrUpdate(Currency currency)
        {
            if (currency.Id > 0)
            {
                var dbRecord = UnitOfWork.CurrencyRepository.GetByIdAsNoTracking(currency.Id);
                currency.Created_At = dbRecord.Created_At;
                UnitOfWork.CurrencyRepository.Update(currency.MapFrom());
            }
            else
            {
                UnitOfWork.CurrencyRepository.Insert(currency.MapFrom());
            }
            UnitOfWork.Save();
        }

        public void Delete(int currencyId)
        {
            UnitOfWork.CurrencyRepository.Delete(currencyId);
            UnitOfWork.Save();
            UnitOfWork.Dispose();
        }

        public Currency GetByName(string name)
        {
            try
            {
                var currency = UnitOfWork.CurrencyRepository.Get(x => x.Name == name)?.FirstOrDefault();
                return currency?.MapFrom();
            }
            catch (Exception)
            {
                return new Currency();
            }
        }
    }
}
