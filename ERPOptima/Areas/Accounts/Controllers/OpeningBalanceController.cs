using ERPOptima.Data.Accounts;
using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Accounts;
using ERPOptima.Service.Accounts;
using ERPOptima.Web.Accounts.ViewModel;
using ERPOptima.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Optima.Areas.Accounts.Controllers
{
    public class OpeningBalanceController : Controller
    {
        private IAnFOpeningBalanceService _AnFOpeningBalanceService;
        private IChartOfAccountService _coaService;

        public OpeningBalanceController()
        {
            var dbfactory = new DatabaseFactory();
            _AnFOpeningBalanceService = new AnFOpeningBalanceService(new AnFOpeningBalanceRepository(dbfactory), new UnitOfWork(dbfactory));
            _coaService = new ChartOfAccountService(new AnFChartOfAccountRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        //
        // GET: /Accounts/OpeningBalance/
        [AuthorizeUser]

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetByProjectId(int projectId, int companyId = 1, int financialYear = 1)
        {

            if (ModelState.IsValid)
            {
                List<AnFOpeningBalance> list = _AnFOpeningBalanceService.GetByProjectId(projectId, companyId, financialYear).ToList();

                List<OpeningBalanceViewModel> viewModelList = new List<OpeningBalanceViewModel>();

                OpeningBalanceViewModel objOpeningBalanceViewModel = null;
                list.ForEach(op =>
                {
                    objOpeningBalanceViewModel = new OpeningBalanceViewModel();
                    objOpeningBalanceViewModel.Id = op.Id;
                    objOpeningBalanceViewModel.Name = op.AnFChartOfAccount.Name;
                    objOpeningBalanceViewModel.Code = op.AnFChartOfAccount.Code;
                    objOpeningBalanceViewModel.AnFChartOfAccountId = op.AnFChartOfAccountId;
                    objOpeningBalanceViewModel.Status = op.Status;
                    objOpeningBalanceViewModel.Debit = op.Debit;
                    objOpeningBalanceViewModel.Credit = op.Credit;
                    objOpeningBalanceViewModel.IsEditable = op.IsEditable;
                    viewModelList.Add(objOpeningBalanceViewModel);
                });
                return Json(viewModelList, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }


        // Code to load grid
        public ActionResult GetByFinancialYearId(int financialYearId, int companyId)
        {
            if (ModelState.IsValid)
            {
                //List<AnFOpeningBalance> List = _AnFOpeningBalanceService.GetByFinancialYearId(financialYearId, companyId).ToList();

                List<AnFOpeningBalance> List = new List<AnFOpeningBalance>();

                List<OpeningBalanceViewModel> viewModelList = new List<OpeningBalanceViewModel>();

                List = _AnFOpeningBalanceService.GetByFinancialYearId(financialYearId, companyId).ToList();

                OpeningBalanceViewModel objOpeningBalanceViewModel = null;

                List.ForEach(op =>
                {
                    objOpeningBalanceViewModel = new OpeningBalanceViewModel();
                    objOpeningBalanceViewModel.Id = op.Id;
                    objOpeningBalanceViewModel.Name = op.AnFChartOfAccount == null ? _coaService.GetById(op.AnFChartOfAccountId).Name : op.AnFChartOfAccount.Name;
                    objOpeningBalanceViewModel.Code = op.AnFChartOfAccount == null ? _coaService.GetById(op.AnFChartOfAccountId).Code: op.AnFChartOfAccount.Code;
                    objOpeningBalanceViewModel.AnFChartOfAccountId = op.AnFChartOfAccountId;
                    objOpeningBalanceViewModel.Status = op.Status;
                    objOpeningBalanceViewModel.Debit = op.Debit;
                    objOpeningBalanceViewModel.Credit = op.Credit;
                    objOpeningBalanceViewModel.IsEditable = op.IsEditable;
                    viewModelList.Add(objOpeningBalanceViewModel);

                });

                return Json(viewModelList, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }  //End Opening Balance Code 


        [HttpPost]


        public ActionResult Update(List<OpeningBalanceViewModel> viewModelList)
        {
            Operation objOperation = new Operation { Success = false };

            int financialYearId = Convert.ToInt32(Session["financialYear"].ToString());
            int companyId = Convert.ToInt32(Session["companyId"].ToString());
            int userID = Convert.ToInt32(Session["userId"]);

            if (ModelState.IsValid && viewModelList != null)
            {
                foreach (var item in viewModelList)
                {
                    if (item != null)
                    {
                        AnFOpeningBalance objAnFOpeningBalance = _AnFOpeningBalanceService.GetById(item.Id);
                        if (objAnFOpeningBalance == null)
                        {

                            objAnFOpeningBalance = new AnFOpeningBalance();
                            objAnFOpeningBalance.AnFChartOfAccountId = item.AnFChartOfAccountId;
                            objAnFOpeningBalance.Debit = item.Debit;
                            objAnFOpeningBalance.Credit = item.Credit;
                            objAnFOpeningBalance.IsEditable = true;
                            objAnFOpeningBalance.Status = true;
                            objAnFOpeningBalance.CmnFinancialYearId = financialYearId;
                            objAnFOpeningBalance.CmnCompanyId = companyId;
                            objAnFOpeningBalance.CreatedBy = userID;

                            _AnFOpeningBalanceService.Save(objAnFOpeningBalance);

                        }

                        else
                        {
                            objAnFOpeningBalance.Debit = item.Debit;
                            objAnFOpeningBalance.Credit = item.Credit;
                            _AnFOpeningBalanceService.Update(objAnFOpeningBalance);

                        }
                    }

                }

                objOperation = _AnFOpeningBalanceService.Commit();
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }  //End of save


    }
}