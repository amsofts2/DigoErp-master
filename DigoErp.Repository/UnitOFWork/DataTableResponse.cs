using System.Collections.Generic;

namespace DigoErp.Repository.UnitOFWork
{
    public class DataTableResponse<TEntity> where TEntity : class
    {
        public DataTableResponse()
        {
            data = new List<TEntity>();
            recordsTotal = 0;
            recordsFiltered = 0;
        }
        public List<TEntity> data { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
    }
}