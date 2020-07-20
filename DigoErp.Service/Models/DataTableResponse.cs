using System.Collections.Generic;

namespace DigoErp.Service.Models
{
    /// <summary>
    /// Generic data table Response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataTableResponse<T> where T : class
    {
        public DataTableResponse()
        {
            data = new List<T>();
            recordsTotal = 0;
            recordsFiltered = 0;
        }
        public List<T> data { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
    }
}
