using ERPOptima.Data.Accounts;
using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Helper;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Accounts;
using ERPOptima.Service.Accounts;
using ERPOptima.Web.Accounts.ViewModel;
using ERPOptima.Web.Filters;
using Optima.Areas.Accounts.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Optima.Areas.Accounts.Controllers
{
    public class DepriciationRateController : Controller
    {
        //
        // GET: /Accounts/Depriciation Rate/
        private IAnFCostCenterService _ccService;
        private IChartOfAccountService _AnFChartOfAccountService;
        public DepriciationRateController()
        {
            var dbfactory = new DatabaseFactory();
            _ccService = new AnFCostCenterService(new AnFCostCenterRepository(dbfactory), new UnitOfWork(dbfactory));
            _AnFChartOfAccountService = new ChartOfAccountService(new AnFChartOfAccountRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }

        #region CostCenter

        [HttpGet]
        public ActionResult GetCostCenters()
        {
            int companyId = Convert.ToInt32(Session["companyId"]); 
            //Convert.ToInt32(Session["companyId"]);
            var list = _ccService.GetCostCenters(companyId).ToList();//.Select(ac => new
            //{
            //    Id = ac.Id,
            //    Name = ac.Name,
            //    Code = ac.Code,
            //    Location = ac.Location
            //});

            return Json(list, JsonRequestBehavior.AllowGet);
        }
       
        [HttpGet]
        public ActionResult GetAnfTransactionalHeadForChequeBook()
        {
            int companyId = Convert.ToInt32(Session["CompanyId"]);
            //Convert.ToInt32(Session["companyId"]);
            var list = _ccService.GetCostCenters(companyId).ToList();//.Select(ac => new
            //{
            //    Id = ac.Id,
            //    Name = ac.Name,
            //    Code = ac.Code,
            //    Location = ac.Location
            //});

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveCostCenter(AnFCostCenter anFCostCenter)
        {
            Operation objOperation = new Operation { Success = false };
            if (ModelState.IsValid)
            {                
                if (anFCostCenter.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        anFCostCenter.CmnCompanyId = Convert.ToInt32(Session["companyId"].ToString());
                        objOperation = _ccService.SaveAnFCostCenter(anFCostCenter);
                    }
                    else { objOperation.OperationId = -1; }
                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        anFCostCenter.CmnCompanyId = Convert.ToInt32(Session["companyId"].ToString());
                        objOperation = _ccService.UpdateAnFCostCenter(anFCostCenter);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeleteAnFCostCenter(int Id)
        {
                        Operation objOperation = new Operation { Success = false };

            if (Id != 0)
            {
                AnFCostCenter obj = _ccService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _ccService.DeleteAnFCostCenter(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }       
       
        public ActionResult GetTransactionalHeadForChequeBook()
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
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
            });
            var result = list.Where(x => x.Code.Contains("1020702") && x.IsTransactionalHead == true);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}