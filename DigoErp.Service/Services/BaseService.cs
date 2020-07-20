using DigoErp.Repository.UnitOFWork;
using DigoErp.Service.Models;

namespace DigoErp.Service.Services
{
    public class BaseService
    {
        protected readonly UnitOfWork UnitOfWork;
        protected BaseService()
        {
            UnitOfWork = new UnitOfWork();
        }

        private int Skip(DataTableSearchModel searchModel)
        {
            var skip = searchModel.Page * searchModel.RowsPerPage - searchModel.RowsPerPage;
            return skip ?? 0;
        }

        protected int Take(DataTableSearchModel searchModel, out int skip)
        {
            skip = Skip(searchModel);
            int? take = searchModel.Page * searchModel.RowsPerPage - skip;
            return take ?? 0; ;
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
