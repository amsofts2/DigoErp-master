using DigoErp.Repository.Edmx;
using DigoErp.Service.Extentions;
using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigoErp.Service.Services
{
    public class RoleService :BaseService
    {
        public DataTableResponse<Role> GetResponse(DataTableSearchModel searchModel)
        {
            try
            {

                var take = Take(searchModel, out int skip);

                if (!string.IsNullOrEmpty(searchModel.SearchTerm))
                {
                    System.Linq.Expressions.Expression<Func<Tbl_Role, bool>> filter =
                        b =>
                        b.Name.ToString().Contains(searchModel.SearchTerm) ||
                        b.Code.ToString().Contains(searchModel.SearchTerm);

                    var tableResponse = UnitOfWork.RoleRepository.GetPagination<Tbl_Role>(take, skip, filter, c => c.OrderBy(o => o.Name));

                    var response = new DataTableResponse<Role>
                    {
                        data = tableResponse.data.Select(p => p.MapFrom()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
                else
                {

                    var tableResponse = UnitOfWork.RoleRepository.GetPagination<Tbl_Role>(take, skip, null, c => c.OrderBy(o => o.Name));

                    var response = new DataTableResponse<Role>
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
                return new DataTableResponse<Role>();
            }
        }

        public Role GetById(int batchId)
        {
            try
            {
                return UnitOfWork.RoleRepository.GetByID(batchId)?.MapFrom();
            }
            catch (Exception)
            {
                return new Role();
            }
        }
        public void AddOrUpdate(Role role)
        {
            if (role.Id > 0)
            {

                UnitOfWork.RoleRepository.Update(role.MapFrom());

            }
            else
            {
                UnitOfWork.RoleRepository.Insert(role.MapFrom());

            }
            UnitOfWork.Save();
        }

        public List<Role> GetAll()
        {
            try
            {
                var role = UnitOfWork.RoleRepository.Get();
                return role.Select(b => b.MapFrom()).ToList();
            }
            catch (Exception)
            {
                return new List<Role>();
            }
        }

        public Role GetByRoleName(string name)
        {
            try
            {
                var role = UnitOfWork.RoleRepository.Get(x => x.Name == name)?.FirstOrDefault();
                return role?.MapFrom();
            }
            catch (Exception)
            {
                return new Role();
            }
        }

        public void Delete(int batchId)
        {
            UnitOfWork.RoleRepository.Delete(batchId);
            UnitOfWork.Save();
            UnitOfWork.Dispose();
        }
    }
}
