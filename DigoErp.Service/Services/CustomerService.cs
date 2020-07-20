using DigoErp.Service.Extentions;
using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigoErp.Service.Services
{
    public class CustomersService : BaseService
    {
        public DataTableResponse<Customer> GetResponse(DataTableSearchModel searchModel)
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

                    var response = new DataTableResponse<Customer>
                    {
                        data = tableResponse.data.Select(p => p.MapFrom()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
                else
                {
                    var tableResponse = UnitOfWork.CustomerRepository.GetPagination<Repository.Edmx.Tbl_Customer>(take, skip, null, c => c.OrderBy(o => o.Name));

                    var response = new DataTableResponse<Customer>
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
                return new DataTableResponse<Customer>();
            }
        }

        public Customer GetById(long customerId)
        {
            try
            {
                return UnitOfWork.CustomerRepository.GetByID(customerId)?.MapFrom();
            }
            catch (Exception)
            {
                return new Customer();
            }
        }

        public void AddOrUpdate(Customer customer)
        {
            if (customer.Id > 0)
            {
                var dbCustomer = UnitOfWork.CustomerRepository.GetByIdAsNoTracking(customer.Id);
                customer.Created_At = dbCustomer.Created_At;
                UnitOfWork.CustomerRepository.Update(customer.MapFrom());
            }
            else
            {
                customer.Created_At = DateTime.Now;
                UnitOfWork.CustomerRepository.Insert(customer.MapFrom());
            }
            UnitOfWork.Save();
        }

        public Customer GetByEmail(string email)
        {
            try
            {
                var customer = UnitOfWork.CustomerRepository.Get(x => x.Email == email)?.FirstOrDefault();
                return customer?.MapFrom();
            }
            catch (Exception)
            {
                return new Customer();
            }
        }
        public void Delete(int customerId)
        {
            UnitOfWork.CustomerRepository.Delete(customerId);
            UnitOfWork.Save();
            UnitOfWork.Dispose();
        }

        public List<Customer> GetAll()
        {
            try
            {
                var customer = UnitOfWork.CustomerRepository.Get(c => c.IsEnabled == null || (c.IsEnabled != null && (bool)c.IsEnabled));
                return customer.Select(b => b.MapFrom()).ToList();
            }
            catch (Exception)
            {
                return new List<Customer>();
            }
        }
    }
}