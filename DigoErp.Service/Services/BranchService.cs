using DigoErp.Service.Extentions;
using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigoErp.Service.Services
{
    public class BranchService : BaseService
    {

        public DataTableResponse<Branch> GetResponse(DataTableSearchModel searchModel)
        {
            try
            {
                var take = Take(searchModel, out int skip);

                if (!string.IsNullOrEmpty(searchModel.SearchTerm))
                {
                    System.Linq.Expressions.Expression<Func<Repository.Edmx.Tbl_Branch, bool>> filter =
                        b =>
                        b.Name.ToString().Contains(searchModel.SearchTerm)
                        || b.Email.Contains(searchModel.SearchTerm);

                    var tableResponse = UnitOfWork.BranchRepository.GetPagination<Repository.Edmx.Tbl_Branch>(take, skip, filter, c => c.OrderBy(o => o.Name));

                    var response = new DataTableResponse<Branch>
                    {
                        data = tableResponse.data.Select(p => p.MapFrom()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
                else
                {
                    var tableResponse = UnitOfWork.BranchRepository.GetPagination<Repository.Edmx.Tbl_Branch>(take, skip, null, c => c.OrderBy(o => o.Name));

                    var response = new DataTableResponse<Branch>
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
                return new DataTableResponse<Branch>();
            }
        }

        public void AddOrUpdate(Branch branch)
        {
            if (branch.Id > 0)
            {
                UnitOfWork.BranchRepository.Update(branch.MapFrom());
            }
            else
            {
                UnitOfWork.BranchRepository.Insert(branch.MapFrom());
            }
            UnitOfWork.Save();
        }

        public List<Branch> GetAll()
        {
            try
            {
                var branches = UnitOfWork.BranchRepository.Get();
                return branches.Select(b => b.MapFrom()).ToList();
            }
            catch (Exception)
            {
                return new List<Branch>();
            }
        }

        public void Delete(int branchId)
        {
            UnitOfWork.BranchRepository.Delete(branchId);
            UnitOfWork.Save();
            UnitOfWork.Dispose();
        }

        public Branch GetById(int branchId)
        {
            try
            {
                return UnitOfWork.BranchRepository.GetByID(branchId)?.MapFrom();
            }
            catch (Exception)
            {
                return new Branch();
            }
        }
    }
}
