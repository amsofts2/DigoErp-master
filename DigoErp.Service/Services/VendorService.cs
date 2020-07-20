
using DigoErp.Repository.Edmx;
using DigoErp.Service.Extentions;
using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigoErp.Service.Services
{
    public class VendorService : BaseService
    {
        public DataTableResponse<Vendor> GetResponse(DataTableSearchModel searchModel)
        {
            try
            {
                var currentUser = HttpContext.Current.Session["UserDetail"] as User ?? new User();

                var take = Take(searchModel, out int skip);

                if (!string.IsNullOrEmpty(searchModel.SearchTerm))
                {
                    System.Linq.Expressions.Expression<Func<Tbl_Vendor, bool>> filter =
                        b =>
                        b.Name.ToString().Contains(searchModel.SearchTerm);

                    var tableResponse = UnitOfWork.VendorRepository.GetPagination<Tbl_Vendor>(take, skip, filter, c => c.OrderBy(o => o.Name));

                    var response = new DataTableResponse<Vendor>
                    {
                        data = tableResponse.data.Select(p => p.MapFrom()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
                else
                {

                    var tableResponse = UnitOfWork.VendorRepository.GetPagination<Tbl_Vendor>(take, skip, null, c => c.OrderBy(o => o.Name));

                    var response = new DataTableResponse<Vendor>
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
                return new DataTableResponse<Vendor>();
            }
        }

        public Vendor GetById(long vendorId)
        {
            try
            {
                return UnitOfWork.VendorRepository.GetByID(vendorId)?.MapFrom();
            }
            catch (Exception)
            {
                return new Vendor();
            }
        }

        public void AddOrUpdate(Vendor vendor)
        {
            if (vendor.Id > 0)
            {
                var dbVendor = UnitOfWork.VendorRepository.GetByIdAsNoTracking(vendor.Id);
                vendor.Created_At = dbVendor.Created_At;
                UnitOfWork.VendorRepository.Update(vendor.MapFrom());
            }
            else
            {
                UnitOfWork.VendorRepository.Insert(vendor.MapFrom());
            }
            UnitOfWork.Save();
        }

        public List<Vendor> GetAll()
        {
            try
            {
                var vendors = UnitOfWork.VendorRepository.Get();
                return vendors.Select(b => b.MapFrom()).ToList();
            }
            catch (Exception)
            {
                return new List<Vendor>();
            }
        }

        public void Delete(int batchId)
        {
            UnitOfWork.VendorRepository.Delete(batchId);
            UnitOfWork.Save();
            UnitOfWork.Dispose();
        }

        public Vendor GetByEmail(string email)
        {
            try
            {
                var user = UnitOfWork.VendorRepository.Get(x => x.Email == email)?.FirstOrDefault();
                return user?.MapFrom();
            }
            catch (Exception)
            {
                return new Vendor();
            }
        }
    }
}
