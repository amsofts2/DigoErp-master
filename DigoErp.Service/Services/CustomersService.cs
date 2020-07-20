using DigoErp.Service.Extentions;
using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DigoErp.Service.Services
{
    public class CustomersService : BaseService
    {
        public DataTableResponse<Customers> GetResponse(DataTableSearchModel searchModel)
        {
            try
            {
                var currentUser = HttpContext.Current.Session["UserDetail"] as User ?? new User();

                var take = Take(searchModel, out int skip);

                if (!string.IsNullOrEmpty(searchModel.SearchTerm))
                {
                    System.Linq.Expressions.Expression<Func<Repository.Edmx.Tbl_Customer, bool>> filter =
                        b =>
                        b.Name.ToString().Contains(searchModel.SearchTerm)
                        || (b.Email.Contains(searchModel.SearchTerm) && b.Email != currentUser.Email);

                    var tableResponse = UnitOfWork.CustomerRepository.GetPagination<Repository.Edmx.Tbl_Customer>(take, skip, filter, c => c.OrderBy(o => o.Name));

                    var response = new DataTableResponse<Customers>
                    {
                        data = tableResponse.data.Select(p => p.MapFrom()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
                else
                {
                    var tableResponse = UnitOfWork.CustomerRepository.GetPagination<Repository.Edmx.Tbl_Customer>(take, skip, x => x.Email != currentUser.Email, c => c.OrderBy(o => o.Name));

                    var response = new DataTableResponse<Customers>
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
                return new DataTableResponse<Customers>();
            }
        }

        public Customers GetById(int batchId)
        {
            try
            {
                return UnitOfWork.CustomerRepository.GetByID(batchId)?.MapFrom();
            }
            catch (Exception)
            {
                return new Customers();
            }
        }

        public void AddOrUpdate(Customers customers)
        {
            if (customers.Id > 0)
            {
                UnitOfWork.CustomerRepository.Update(customers.MapFrom());
            }
            else
            {
                UnitOfWork.CustomerRepository.Insert(customers.MapFrom());
            }
            UnitOfWork.Save();
        }

        public Customers GetByEmail(string email)
        {
            try
            {
                var customer = UnitOfWork.CustomerRepository.Get(x => x.Email == email)?.FirstOrDefault();
                return customer?.MapFrom();
            }
            catch (Exception)
            {
                return new Customers();
            }
        }
    }
}
