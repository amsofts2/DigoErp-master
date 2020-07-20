using DigoErp.Repository.Edmx;
using DigoErp.Service.Extentions;
using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigoErp.Service.Services
{
    public class CategoryService :BaseService
    {
        public DataTableResponse<Category> GetResponse(DataTableSearchModel searchModel)
        {
            try
            {
               
                var take = Take(searchModel, out int skip);

                if (!string.IsNullOrEmpty(searchModel.SearchTerm))
                {
                    System.Linq.Expressions.Expression<Func<Tbl_Category, bool>> filter =
                        b =>
                        b.Name.ToString().Contains(searchModel.SearchTerm);

                    var tableResponse = UnitOfWork.CategoryRepository.GetPagination<Tbl_Category>(take, skip, filter, c => c.OrderBy(o => o.Name));

                    var response = new DataTableResponse<Category>
                    {
                        data = tableResponse.data.Select(p => p.MapFrom()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
                else
                {

                    var tableResponse = UnitOfWork.CategoryRepository.GetPagination<Tbl_Category>(take, skip, null, c => c.OrderBy(o => o.Name));

                    var response = new DataTableResponse<Category>
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
                return new DataTableResponse<Category>();
            }
        }

        public Category GetById(int batchId)
        {
            try
            {
                return UnitOfWork.CategoryRepository.GetByID(batchId)?.MapFrom();
            }
            catch (Exception)
            {
                return new Category();
            }
        }

        public void AddOrUpdate(Category category)
        {
            if (category.Id > 0)
            {
                var dbRecord = UnitOfWork.CategoryRepository.GetByIdAsNoTracking(category.Id);
                category.Created_At = dbRecord.Created_At;
                UnitOfWork.CategoryRepository.Update(category.MapFrom());
            }
            else
            {
                UnitOfWork.CategoryRepository.Insert(category.MapFrom());
            }
            UnitOfWork.Save();
        }

        public void Delete(int batchId)
        {
            UnitOfWork.CategoryRepository.Delete(batchId);
            UnitOfWork.Save();
            UnitOfWork.Dispose();
        }

        public Category GetByName(string name)
        {
            try
            {
                var category = UnitOfWork.CategoryRepository.Get(x => x.Name == name)?.FirstOrDefault();
                return category?.MapFrom();
            }
            catch (Exception)
            {
                return new Category();
            }
        }

        public List<Category> GetAll()
        {
            try
            {
                var category = UnitOfWork.CategoryRepository.Get(c => c.Enabled == null || (c.Enabled != null && (bool)c.Enabled));
                return category.Select(b => b.MapFrom()).ToList();
            }
            catch (Exception)
            {
                return new List<Category>();
            }
        }
    }
}
