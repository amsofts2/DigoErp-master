using DigoErp.Repository.Edmx;
using DigoErp.Service.Extentions;
using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigoErp.Service.Services
{
    public class TransferService : BaseService
    {
        public DataTableResponse<Transfer> GetResponse(DataTableSearchModel searchModel)
        {
            try
            {
                var take = Take(searchModel, out int skip);

                if (!string.IsNullOrEmpty(searchModel.SearchTerm))
                {
                    System.Linq.Expressions.Expression<Func<Tbl_Transfer, bool>> filter =
                        b =>
                        b.FromAccount.ToString().Contains(searchModel.SearchTerm) ||
                        b.ToAccount.ToString().Contains(searchModel.SearchTerm);

                    var tableResponse = UnitOfWork.TransfersRepository.GetPagination<Tbl_Transfer>(take, skip, filter, c => c.OrderBy(o => o.FromAccount));

                    var response = new DataTableResponse<Transfer>
                    {
                        data = tableResponse.data.Select(p => p.MapFrom()).ToList(),
                        recordsFiltered = tableResponse.recordsFiltered,
                        recordsTotal = tableResponse.recordsTotal
                    };
                    return response;
                }
                else
                {

                    var tableResponse = UnitOfWork.TransfersRepository.GetPagination<Tbl_Transfer>(take, skip, null, c => c.OrderBy(o => o.FromAccount));

                    var response = new DataTableResponse<Transfer>
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
                return new DataTableResponse<Transfer>();
            }
        }

        public Transfer GetById(int batchId)
        {
            try
            {
                return UnitOfWork.TransfersRepository.GetByID(batchId)?.MapFrom();
            }
            catch (Exception)
            {
                return new Transfer();
            }
        }
        public void AddOrUpdate(Transfer transfer)
        {
            if (transfer.Id > 0)
            {
                var dbRecord = UnitOfWork.TransfersRepository.GetByIdAsNoTracking(transfer.Id);
                transfer.Created_At = dbRecord.Created_At;
                UnitOfWork.TransfersRepository.Update(transfer.MapFrom());
            }
            else
            {
                UnitOfWork.TransfersRepository.Insert(transfer.MapFrom());
            }
            UnitOfWork.Save();
        }

        public List<Transfer> GetAll()
        {
            try
            {
                var transfer = UnitOfWork.TransfersRepository.Get();
                return transfer.Select(b => b.MapFrom()).ToList();
            }
            catch (Exception)
            {
                return new List<Transfer>();
            }
        }

        public void Delete(int batchId)
        {
            UnitOfWork.TransfersRepository.Delete(batchId);
            UnitOfWork.Save();
            UnitOfWork.Dispose();
        }

    }
}
