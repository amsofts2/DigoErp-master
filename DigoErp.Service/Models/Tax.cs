namespace DigoErp.Service.Models
{
    public class Tax
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal? Rate { get; set; }
        public string Type { get; set; }
        public bool? Enabled { get; set; }
    }
}
