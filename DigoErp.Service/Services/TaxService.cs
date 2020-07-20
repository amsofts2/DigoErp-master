using DigoErp.Service.Extentions;
using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using DigoErp.Repository.Edmx;

namespace DigoErp.Service.Services
{
    public class TaxService : BaseService
    {
        public List<Tax> GetAll()
        {
            try
            {
                var tax = UnitOfWork.TaxRepository.Get(c => c.Enabled == null || (c.Enabled != null && (bool)c.Enabled));
                return tax.Select(b => b.MapFrom()).ToList();
            }
            catch (Exception)
            {
                return new List<Tax>();
            }
        }

        public DataTableResponse<Tax> GetResponse(DataTableSearchModel searchModel)
        {
            try
            {

                var take = Take(searchModel, out int skip);

                if (!string.IsNullOrEmpty(searchModel.SearchTerm))
                {
                    System.Linq.Expressions.Expression<Func<Tbl_Tax, bool>> filter =
                        b =>
                        b.Name.ToString().Contains(searchModel.SearchTerm);

                    var tableResponse = UnitOfWork.TaxRepository.GetPagination<Tbl_Tax>(take, skip, filter, c => c.OrderBy(o => o.Name));

                    var response = new DataTableResponse<Tax>
                    {
                        data = tableResponse.data.Select(p => p.MapFrom()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
                else
                {

                    var tableResponse = UnitOfWork.TaxRepository.GetPagination<Tbl_Tax>(take, skip, null, c => c.OrderBy(o => o.Name));

                    var response = new DataTableResponse<Tax>
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
                return new DataTableResponse<Tax>();
            }
        }

        public Tax GetById(int taxId)
        {
            try
            {
                return UnitOfWork.TaxRepository.GetByID(taxId)?.MapFrom();
            }
            catch (Exception)
            {
                return new Tax();
            }
        }

        public void AddOrUpdate(Tax tax)
        {
            if (tax.Id > 0)
            {
                UnitOfWork.TaxRepository.Update(tax.MapFrom());
            }
            else
            {
                UnitOfWork.TaxRepository.Insert(tax.MapFrom());
            }
            UnitOfWork.Save();
        }

        public void Delete(int taxId)
        {
            UnitOfWork.TaxRepository.Delete(taxId);
            UnitOfWork.Save();
            UnitOfWork.Dispose();
        }

        public Tax GetByName(string name)
        {
            try
            {
                var tax = UnitOfWork.TaxRepository.Get(x => x.Name == name)?.FirstOrDefault();
                return tax?.MapFrom();
            }
            catch (Exception)
            {
                return new Tax();
            }
        }
    }
}
