using System;
using DigoErp.Repository.Edmx;
using DigoErp.Service.Models;

namespace DigoErp.Service.Extentions
{
    public static class ReconciliationExtentions
    {
        public static Tbl_Reconciliation MapFrom(this Reconciliation reconciliation)
        {
            return new Tbl_Reconciliation
            {
                Id = reconciliation.Id,
                AccountId = reconciliation.AccountId,
                StartDate = reconciliation.StartDate,
                EndDate = reconciliation.EndDate,
                ClosingBalance = reconciliation.ClosingBalance,
                Status = reconciliation.Status,
                Created_By = reconciliation.Created_By,
                Created_At = reconciliation.Id > 0 ? reconciliation.Created_At : DateTime.Now,
                Updated_At = reconciliation.Id  > 0 ? reconciliation.Updated_At:default(DateTime?)
            };
        }

        public static Reconciliation MapFrom(this Tbl_Reconciliation reconciliation)
        {
            return new Reconciliation
            {
                Id = reconciliation.Id,
                AccountId = reconciliation.AccountId,
                StartDate = reconciliation.StartDate,
                EndDate = reconciliation.EndDate,
                ClosingBalance = reconciliation.ClosingBalance,
                Status = reconciliation.Status,
                Created_By = reconciliation.Created_By,
                Created_At = reconciliation.Created_At,
                Updated_At = reconciliation.Updated_At,
                AccountName = reconciliation.Tbl_Account?.AccountName
            };
        }
    }
}
