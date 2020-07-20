namespace DigoErp.Service.Models
{
    public class DataTableSearchModel
    {
        public DataTableSearchModel()
        {
            RowsPerPage = RowsPerPage ?? 10;
        }
        public int Page { get; set; }
        public int? RowsPerPage { get; set; }
        public int SortByColumn { get; set; }
        public string SortDir { get; set; }
        public string SearchTerm { get; set; }
        public long? Created_By { get; set; }
    }
}
