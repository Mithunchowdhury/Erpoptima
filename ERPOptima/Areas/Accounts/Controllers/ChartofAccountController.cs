using ERPOptima.Data.Accounts;
using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Accounts;
using ERPOptima.Service.Accounts;
using ERPOptima.Web.Accounts.ViewModel;
using ERPOptima.Web.Filters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Optima.Areas.Accounts.Controllers
{
    
    public class ChartofAccountController : Controller
    {
        private IChartOfAccountService _AnFChartOfAccountService;
        private IAnFOpeningBalanceService _AnFOpeningBalanceService;
        //  private ICmnProjectService _CmnProjectService;

        public ChartofAccountController()
        {
            // Session["ModuleId"] = 2;
            var dbfactory = new DatabaseFactory();
            // _CmnProjectService = new CmnProjectService(new CmnProjectRepository(dbfactory), new UnitOfWork(dbfactory));
            _AnFChartOfAccountService = new ChartOfAccountService(new AnFChartOfAccountRepository(dbfactory), new UnitOfWork(dbfactory));
            _AnFOpeningBalanceService = new AnFOpeningBalanceService(new AnFOpeningBalanceRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        // GET: /Accounts/ChartofAccount/
        public ActionResult Index()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult MappingWithProject()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult ReportHeads()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult ReportAccountHeadMapping()
        {
            return View();
        }

        #region ChartOfAccount

        [HttpPost]
        public ActionResult DeleteChartOfAccount(long Id = 0)
        {
            Operation objOperation = null;

            if (Id != 0)
            {
                AnFChartOfAccount obj = _AnFChartOfAccountService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _AnFChartOfAccountService.DeleteAnfChartOfAccount(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
        
        public String GenerateChartOfAccountChildCode(CodeGenerationViewModel codeGenerationViewModel)
        {
            string result = String.Empty;
            string childcode;
            if (ModelState.IsValid)
            {
               
                childcode = codeGenerationViewModel.ChildCode != null ? codeGenerationViewModel.ChildCode : "";
                

                result = _AnFChartOfAccountService.GenerateChartOfAccountChildCode(codeGenerationViewModel.ParentCode,
                                                                                   childcode,
                                                                                   codeGenerationViewModel.Level + 1,
                                                                                   codeGenerationViewModel.IsLastNode);
            }

            return result;
        }

        [HttpGet]
        public ActionResult GetChartOfAccount(int companyId)
        {
            #region extra

            //var list = _AnFChartOfAccountService.GetParentWithChild(companyId).Select(ac => new  {
            //Name=ac.Name,
            //Id=ac.Id,
            //ParentId=ac.AnFChartOfAccountId
            //}).FirstOrDefault();
            //return Json(list, JsonRequestBehavior.AllowGet);
            //Func<AnFChartOfAccount, object> f = null;
            //f = ac =>
            //{
            //    dynamic t = new
            //    {
            //        Id = ac.Id,
            //        Name = ac.Name,
            //        Code=ac.Code,
            //        IsTransactionalHead=ac.IsTransactionalHead,
            //        Status=ac.Status,
            //        Depthlevel=ac.DepthLevel,
            //        ParentId = ac.AnFChartOfAccountId,
            //        children = ac.AnFChartOfAccounts1.Select(e => f(e))
            //    };
            //    return t;
            //};
            //var r = f(list);
            //return JsonConvert.SerializeObject(r, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            #endregion extra

            var list = _AnFChartOfAccountService.GetParentWithChild(companyId).Select(ac => new
            {
                Id = ac.Id,
                AnFChartOfAccountId = ac.AnFChartOfAccountId,
                Code = ac.Code,
                Name = ac.Name,
                CmnCompanyId = ac.CmnCompanyId,
                ScheduleNo = ac.ScheduleNo,
                DepthLevel = ac.DepthLevel,
                IsTransactionalHead = ac.IsTransactionalHead,
                Status = ac.Status
            }).ToList();
            
            

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AuthorizeUser(Permission = PermissionEnum.Add, ResourceTag = "ChartOfAccountIndex")]
        public ActionResult SaveChartOfAccount(AnFChartOfAccountViewModel anFChartOfAccountViewModel)
        {
            Operation objOperation = new Operation();

            if (ModelState.IsValid)
            {
                AnFChartOfAccount anfChartOfAccount = new AnFChartOfAccount
                {
                    Id = anFChartOfAccountViewModel.Id,
                    Name = anFChartOfAccountViewModel.Name,
                    Code = anFChartOfAccountViewModel.Code,
                    AnFChartOfAccountId = anFChartOfAccountViewModel.AnFChartOfAccountId,
                    CmnCompanyId = anFChartOfAccountViewModel.CmnCompanyId,
                    ScheduleNo = anFChartOfAccountViewModel.ScheduleNo,
                    DepthLevel = anFChartOfAccountViewModel.DepthLevel,
                    IsTransactionalHead = anFChartOfAccountViewModel.IsTransactionalHead,
                    Status = anFChartOfAccountViewModel.Status
                };
                if (anfChartOfAccount.Id == 0)
                {
                    objOperation = _AnFChartOfAccountService.SaveAnFChartOfAccount(anfChartOfAccount);
                }
                else
                {
                    objOperation = _AnFChartOfAccountService.UpdateAnfChartOfAccount(anfChartOfAccount);
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        #endregion ChartOfAccount

        #region MappingWithProject

        public ActionResult GetTransactionalHeadByProjectId(int projectId, int companyId = 5, int financialYear = 216245)
        {
            //DataTable dt = _AnFChartOfAccountService.GetTransactionalHeadByProjectId(projectId, companyId, financialYear);

            //var list = dt.DataTableToList<AnFTransactionalHeadViewModel>();
            Collection<AnFTransactionalHeadViewModel> movements = null;

            DataTable dt = new DataTable();
            dt = _AnFChartOfAccountService.GetTransactionalHeadByProjectId(projectId, companyId, financialYear);
            if (dt != null)
            {
                movements = new Collection<AnFTransactionalHeadViewModel>();
                foreach (DataRow row in dt.Rows)
                {
                    movements.Add((AnFTransactionalHeadViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(AnFTransactionalHeadViewModel)));
                }
            }
            return Json(movements, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult GetTransactionalHeadByCompanyId(int companyId=1)
        {
            List<AnFChartOfAccount> list = _AnFChartOfAccountService.GetTransactionalHeadByCompanyId(companyId).ToList();

            var newVM = list.Select(coa => new
            {
                Id = coa.Id,
                Code = coa.Code,
                Name = coa.Name
            }).ToList();

            return Json(newVM, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetThirdLevel(int companyId = 1)
        {
            List<AnFChartOfAccount> list = _AnFChartOfAccountService.GetThirdLevel(companyId).ToList();

            var newVM = list.Select(coa => new
            {
                Id = coa.Id,
                Code = coa.Code,
                Name = coa.Name
            }).ToList();

            return Json(newVM, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MapTransactionalHead(List<AnFTransactionalHeadViewModel> listAnFTransactionalHeadViewModel, int projectId, int companyId = 5, int financialYear = 216245)
        {
            

            Operation objOperation = new Operation { Success = false };
            if (ModelState.IsValid)
            {
                if (listAnFTransactionalHeadViewModel != null)
                {
                    AnFOpeningBalance objAnFOpeningBalance = null;
                    foreach (var item in listAnFTransactionalHeadViewModel)
                    {
                        if (item.OpeningBalanceId == 0)
                        {
                            objAnFOpeningBalance = new AnFOpeningBalance
                            {
                                CmnCompanyId = companyId,
                                //CmnProjectId = projectId,
                                AnFChartOfAccountId = item.Id,
                                CmnFinancialYearId = financialYear
                            };

                            _AnFOpeningBalanceService.Add(objAnFOpeningBalance);
                            objOperation = _AnFOpeningBalanceService.Commit();

                        }
                        else
                        {
                            objAnFOpeningBalance = _AnFOpeningBalanceService.GetById(item.OpeningBalanceId);
                            if (objAnFOpeningBalance != null)
                            {
                                _AnFOpeningBalanceService.Remove(objAnFOpeningBalance);
                                objOperation = _AnFOpeningBalanceService.Commit();
                            }
                        }
                    }

                }
                else { objOperation.Success = true; }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        #endregion MappingWithProject
    }
}