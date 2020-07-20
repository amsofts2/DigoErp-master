using DigoErp.App_Start;
using DigoErp.Areas.Banking.Models;
using DigoErp.Controllers;
using DigoErp.Service.Models;
using DigoErp.Service.Services;
using System.Web.Mvc;

namespace DigoErp.Areas.Banking.Controllers
{
    [CustomAuthorize("Super Admin", "Admin")]
    public class TransactionsController : BaseController
    {
        private readonly TransactionService transactionService;

        public TransactionsController()
        {
            transactionService = new TransactionService();
        }
        // GET: Banking/Transactions
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(DataTableSearchModel searchModel)
        {
            DataTableResponse<Transaction> response = transactionService.GetResponse(searchModel);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetTransactionsForReconciliation(Reconciliation searchModel)
        {
           
            if(searchModel.AccountId == null || searchModel.AccountId <= 0)
            {
                var defualtSetting = Session["Default_Settings"] as Default ?? new Default();
                searchModel.AccountId = defualtSetting.AccountId;
            }

            if (searchModel.ClosingBalance  ==  null || searchModel.ClosingBalance <= 0)
            {
                searchModel.ClosingBalance = 0.00M;
            }
            TransactionReponseModel reponseModel = new TransactionReponseModel
            {
                StartDate = searchModel.StartDate,
                EndDate = searchModel.EndDate,
                AccountId = searchModel.AccountId,
                Transactions = transactionService.GetTransactionsForReconciliation(searchModel)
            };
            return Json(reponseModel, JsonRequestBehavior.AllowGet);
        }
    }
}