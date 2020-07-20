
using DigoErp.Repository.Edmx;
using DigoErp.Service.Extentions;
using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigoErp.Service.Services
{
    public class ItemService  :BaseService
    {
        public DataTableResponse<Item> GetResponse(DataTableSearchModel searchModel)
        {
            try
            {
                var currentUser = HttpContext.Current.Session["UserDetail"] as User ?? new User();

                var take = Take(searchModel, out int skip);

                if (!string.IsNullOrEmpty(searchModel.SearchTerm))
                {
                    System.Linq.Expressions.Expression<Func<Repository.Edmx.Tbl_Item, bool>> filter =
                        b =>
                        b.Name.ToString().Contains(searchModel.SearchTerm);

                    var tableResponse = UnitOfWork.ItemRepository.GetPagination<Tbl_Item>(take, skip, filter, c => c.OrderBy(o => o.Name));

                    var response = new DataTableResponse<Item>
                    {
                        data = tableResponse.data.Select(p => p.MapFrom()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
                else
                {
                    var item=UnitOfWork.GetItem();
                    var tableResponse = UnitOfWork.ItemRepository.GetPagination<Tbl_Item>(take, skip, null, c => c.OrderBy(o => o.Name));

                    var response = new DataTableResponse<Item>
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
                return new DataTableResponse<Item>();
            }
        }

        public Item GetById(int batchId)
        {
            try
            {
                return UnitOfWork.ItemRepository.GetByID(batchId)?.MapFrom();
            }
            catch (Exception)
            {
                return new Item();
            }
        }

        public void AddOrUpdate(Item item)
        {
            if (item.Id > 0)
            {
                var dbRecord = UnitOfWork.ItemRepository.GetByIdAsNoTracking(item.Id);
                item.Created_At = dbRecord.Created_At;
                UnitOfWork.ItemRepository.Update(item.MapFrom());
            }
            else
            {
                UnitOfWork.ItemRepository.Insert(item.MapFrom());
            }
            UnitOfWork.Save();
        }



        public Item GetByName(string name)
        {
            try
            {
                var item = UnitOfWork.ItemRepository.Get(x => x.Name == name)?.FirstOrDefault();
                return item?.MapFrom();
            }
            catch (Exception)
            {
                return new Item();
            }
        }

        public void Delete(int batchId)
        {
            UnitOfWork.ItemRepository.Delete(batchId);
            UnitOfWork.Save();
            UnitOfWork.Dispose();
        }

        public List<Item> GetAll()
        {
            try
            {
                var item = UnitOfWork.ItemRepository.Get(c => c.IsEnabled == null || (c.IsEnabled != null && (bool)c.IsEnabled));
                return item.Select(b => b.MapFrom()).ToList();
            }
            catch (Exception)
            {
                return new List<Item>();
            }
        }

    }
}
