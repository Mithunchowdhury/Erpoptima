using ERPOptima.Data.Accounts;
using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Accounts;
using ERPOptima.Service.Accounts;
using ERPOptima.Web.Accounts.ViewModel;
using ERPOptima.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using ERPOptima.Lib.Model;


namespace Optima.Areas.Accounts.Controllers
{
    public class ClosingBalanceController : Controller
    {
        private IAnFClosingBalanceService _anfClosingBalanceService;
        private IChartOfAccountService _coaService;
        public ClosingBalanceController()
        {
            var dbfactory = new DatabaseFactory();
            _anfClosingBalanceService = new AnFClosingBalanceService(new AnFClosingBalanceRepository(dbfactory), new UnitOfWork(dbfactory));
            _coaService = new ChartOfAccountService(new AnFChartOfAccountRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        //
        // GET: /Accounts/ClosingBalance/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult InventoryClosingBalance()
        {
            return View();
        }

       

        #region Inventory Closing Balance


        //public ActionResult GetClosingBalanceByFinancialYearId(int financialYearId)
        //{
        //    int companyId = Convert.ToInt32(Session["companyId"].ToString());
        //    List<AnfClosingBlanceViewModel> list = new List<AnfClosingBlanceViewModel>();

        //    DataTable dt = _anfClosingBalanceService.GetClosingBalanceByFinancialYearId(financialYearId, companyId);

        //    list = dt.DataTableToList<AnfClosingBlanceViewModel>();

        //    return Json(list, JsonRequestBehavior.AllowGet);

        //}

        //---------------------------Load Grid---------------------------------
        public ActionResult GetClosingBalanceByFinancialYearId(int financialYearId, int companyId)
        {
            if (ModelState.IsValid)
            {

                List<AnFClosingBalance> List = new List<AnFClosingBalance>();
                
                List<AnfClosingBlanceViewModel> viewModelList = new List<AnfClosingBlanceViewModel>();

                List = _anfClosingBalanceService.GetClosingBalanceByFinancialYearId(financialYearId, companyId).ToList();

                AnfClosingBlanceViewModel objAnfClosingBlanceViewModel = null;

                List.ForEach(op =>
                {
                    objAnfClosingBlanceViewModel = new AnfClosingBlanceViewModel();
                    objAnfClosingBlanceViewModel.Id = op.Id;
                    objAnfClosingBlanceViewModel.Name = op.AnFChartOfAccount == null ? _coaService.GetById(op.AnfChartOfAccountId).Name : op.AnFChartOfAccount.Name;
                    objAnfClosingBlanceViewModel.Code = op.AnFChartOfAccount == null ? _coaService.GetById(op.AnfChartOfAccountId).Code : op.AnFChartOfAccount.Code;
                    objAnfClosingBlanceViewModel.AnFChartOfAccountId = op.AnfChartOfAccountId;
                    objAnfClosingBlanceViewModel.Status = op.Status;
                    objAnfClosingBlanceViewModel.Debit = op.Debit;
                    objAnfClosingBlanceViewModel.Credit = op.Credit;
                    objAnfClosingBlanceViewModel.IsEditable = op.IsEditable;
                    viewModelList.Add(objAnfClosingBlanceViewModel);

                });

                return Json(viewModelList, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }  //End of Load grid

        //------------------------------Save method-----------------------------------

        [HttpPost]
        public ActionResult Update(List<AnfClosingBlanceViewModel> viewModelList)
        {
            ERPOptima.Lib.Model.Operation objOperation = new ERPOptima.Lib.Model.Operation { Success = false };

            int financialYearId = Convert.ToInt32(Session["financialYear"].ToString());
            int companyId = Convert.ToInt32(Session["companyId"].ToString());
            int userID = Convert.ToInt32(Session["userId"]);

            if (ModelState.IsValid && viewModelList != null)
            {
                foreach (var item in viewModelList)
                {
                    if (item != null)
                    {
                        AnFClosingBalance objAnFClosingBalance = _anfClosingBalanceService.GetById(item.Id);
                        if (objAnFClosingBalance == null)
                        {
                            
                                objAnFClosingBalance = new AnFClosingBalance();
                                objAnFClosingBalance.AnfChartOfAccountId = item.AnFChartOfAccountId;
                                objAnFClosingBalance.Debit = item.Debit;
                                objAnFClosingBalance.Credit = item.Credit;
                                objAnFClosingBalance.IsEditable = true;
                                objAnFClosingBalance.Status = true;
                                objAnFClosingBalance.CmnFinancialYearId = financialYearId;
                                objAnFClosingBalance.CmnCompanyId = companyId;
                                objAnFClosingBalance.CreatedBy = userID;

                                _anfClosingBalanceService.Save(objAnFClosingBalance);

                       }

                       else
                        {
                                objAnFClosingBalance.Debit = item.Debit;
                                objAnFClosingBalance.Credit = item.Credit;
                                _anfClosingBalanceService.Update(objAnFClosingBalance);
                          
                        }
                    }

                }

                objOperation = _anfClosingBalanceService.Commit();
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }


        #endregion

    }
}
