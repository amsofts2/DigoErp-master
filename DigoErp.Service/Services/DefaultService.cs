using System;
using System.Linq;
using DigoErp.Service.Extentions;
using DigoErp.Service.Models;

namespace DigoErp.Service.Services
{
    public class DefaultService : BaseService
    {
        public Default GetByUserId(long id)
        {
            try
            {
                return UnitOfWork.DefaultRepository.Get(x=>x.CreatedBy == id).FirstOrDefault().MapFrom();
            }
            catch (Exception)
            {
                return new Default();
            }
        }

        public Default GetById(int id)
        {
            try
            {
                return UnitOfWork.DefaultRepository.GetByID(id)?.MapFrom();
            }
            catch (Exception)
            {
                return new Default();
            }
        }

        public void AddOrUpdate(Default @default)
        {
            if (@default.Id > 0)
            {
                UnitOfWork.DefaultRepository.Update(@default.MapFrom());
            }
            else
            {
                UnitOfWork.DefaultRepository.Insert(@default.MapFrom());
            }
            UnitOfWork.Save();
        }

        public void Delete(int id)
        {
            UnitOfWork.DefaultRepository.Delete(id);
            UnitOfWork.Save();
            UnitOfWork.Dispose();
        }
    }
}