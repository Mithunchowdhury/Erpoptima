using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Helper;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Accounts;
using ERPOptima.Service.Accounts;
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
    public class CostCenterController : Controller
    {
        //
        // GET: /Accounts/CostCenter/
        private IAnFCostCenterService _ccService;

        public CostCenterController()
        {
            var dbfactory = new DatabaseFactory();
            _ccService = new AnFCostCenterService(new AnFCostCenterRepository(dbfactory), new UnitOfWork(dbfactory));
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

        private string GetReportPath(string fileName)
        {
            string basePath = HostingEnvironment.MapPath("~/Areas/Accounts/CrystalReport");

            return Path.Combine(basePath, fileName);
        }

        public ActionResult GetAnFCostCenterReport(ReportAnFCostCentreViewModel obj)
        {
            DataTable dt = null;
            string filepath = string.Empty;

            int CompanyId = 5;// Convert.ToInt32(Session["companyId"].ToString());

            IList<AnFCostCenter> collection = new List<AnFCostCenter>();
            List<ReportParameter> paramList = new List<ReportParameter>();
            if (ModelState.IsValid)
            {
                dt = new DataTable();
                dt = _ccService.GetCostCentersByCompanyId(CompanyId);

                filepath = GetReportPath("AnFCostCenter.rpt");

                if (dt != null && filepath != string.Empty)
                {
                    return new CrystalReportResult(filepath, dt, paramList);
                }
                else
                {
                    return RedirectToAction("CostCenterReport");
                }
            }
            else
            {
                return RedirectToAction("CostCenterReport");
            }
        }

        #endregion CostCenter
    }
}