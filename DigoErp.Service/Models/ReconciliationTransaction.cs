namespace DigoErp.Service.Models
{
    public class ReconciliationTransaction
    {
        public long Id { get; set; }
        public long? ReconciliationId { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Contact { get; set; }
        public decimal? Deposit { get; set; }
        public decimal? Withdrawal { get; set; }
        public bool? IsClear { get; set; }
        public string CurrencySymbol { get; set; }
        public string SymbolPosition { get; set; }
        public int? TransactionType { get; set; }
    }
}
