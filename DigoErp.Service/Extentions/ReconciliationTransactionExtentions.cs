using DigoErp.Repository.Edmx;
using DigoErp.Service.Models;

namespace DigoErp.Service.Extentions
{
    public static class ReconciliationTransactionExtentions
    {
        public static ReconciliationTransaction MapFrom(this Tbl_Reconciliation_Transaction transaction)
        {
            return new ReconciliationTransaction
            {
                Id = transaction.Id,
                ReconciliationId = transaction.ReconciliationId,
                Date = transaction.Date,
                Description = transaction.Description,
                Contact = transaction.Contact,
                Deposit = transaction.Deposit,
                Withdrawal = transaction.Withdrawal,
                IsClear = transaction.IsClear
            };
        }

        public static Tbl_Reconciliation_Transaction MapFrom(this ReconciliationTransaction transaction,long? reconciliationId = 0)
        {
            return new Tbl_Reconciliation_Transaction
            {
                Id = transaction.Id,
                ReconciliationId = reconciliationId > 0 ? reconciliationId : transaction.ReconciliationId,
                Date = transaction.Date,
                Description = transaction.Description,
                Contact = transaction.Contact,
                Deposit = transaction.Deposit,
                Withdrawal = transaction.Withdrawal,
                IsClear = transaction.IsClear
            };
        }
    }
}
