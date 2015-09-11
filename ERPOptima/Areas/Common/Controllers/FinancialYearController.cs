using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Common;
using ERPOptima.Service.Common;
using Optima.Areas.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPOptima.Service;
using ERPOptima.Lib.Utilities;
using System.Web.Routing;
using Optima.Helper;
using ERPOptima.Lib.Model;
using ERPOptima.Web.Filters;
using ERPOptima.Service.Accounts;
using ERPOptima.Data.Accounts.Repository;
namespace Optima.Areas.Common.Controllers
{
    public class FinancialYearController : Controller
    {
        //
        // GET: /Common/FinancialYear/
        private ICmnFinancialYearService _fyService;
        private IAnFMonthLockService _AnFMonthLockService;

        public FinancialYearController()
        {
            var dbfactory = new DatabaseFactory();
            _fyService = new CmnFinancialYearService(new CmnFinancialYearRepository(dbfactory), new UnitOfWork(dbfactory));
            _AnFMonthLockService = new AnFMonthLockService(new AnFMonthLockRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FiscalYearClosing()
        {
            return View();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            Session["financialYear"] = new FinancialYearHelper().SetFinancialYearId(Convert.ToInt32(Session["companyId"].ToString()), Convert.ToInt32(Session["moduleId"].ToString()));
        }


        public ActionResult GetById(int Id = 0)
        {           
            if (Id == 0)
            {
                Id = 216219;//216245;//current financialYearId
            }
            var anfFinancialYear = _fyService.GetById(Id);
            return Json(anfFinancialYear, JsonRequestBehavior.AllowGet);     
        }
        public ActionResult GetCurrentFinancialYear()
        {
            int id = Convert.ToInt32(Session["financialYear"]);
           
            var anfFinancialYear = _fyService.GetById(id);
            
            return Json(anfFinancialYear, JsonRequestBehavior.AllowGet);
        }
        //public int InsertCmnFinantialYear(CmnFinancialYear fy)
        //{

        //    int companyId = Convert.ToInt32(Session["companyId"].ToString());
        //    fy.CmnCompanyId = companyId;
        //    fy.YearClosingStatus = true;
        //    int ret = 0;
        //    if (fy != null)
        //    {
        //        _fyService.Add(fy);
        //        _fyService.Save();

        //    }
            
        //    return ret;
        //}
        public ActionResult Save(CmnFinancialYear fy)
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            int userid = Convert.ToInt32(Session["userId"]);
            int moduleId = Convert.ToInt32(Session["moduleId"]);
            fy.CmnCompanyId = companyId;
            Operation objOperation = new Operation();
            if (ModelState.IsValid)
            {
                if (fy.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        fy.SecModuleId = moduleId;
                        fy.CreatedBy = userid;   //Add by Bably
                        fy.CreatedDate = DateTime.Now.Date; //Add by Bably
                        objOperation = _fyService.Save(fy);
                    }
                    else
                    {
                        objOperation.OperationId = -2;
                        objOperation.Success = false;
                    }
                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        fy.ModifiedBy = userid;  //Add by Bably
                        fy.ModifiedDate = DateTime.Now.Date;  //Add by Bably
                        objOperation = _fyService.Update(fy);
                    }
                    else
                    {
                        objOperation.OperationId = -2;
                        objOperation.Success = false;
                    }
                }
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
        //public int UpdateCmnFinantialYear(CmnFinancialYear fy)
        //{
        //    int companyId = Convert.ToInt32(Session["companyId"].ToString());
        //    fy.CmnCompanyId = companyId;
        //    int ret = 0;
        //    if (fy != null)
        //    {
        //        _fyService.Update(fy);
        //        _fyService.Save();
        //    }
            
        //    return ret;
        //}
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                CmnFinancialYear obj = _fyService.GetById(Id);
                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _fyService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
        public int Delete(CmnFinancialYear fy)
        {
            int ret = 0;
            if (fy != null)
            {
                _fyService.Delete(fy);
             
            }
            
            return ret;
        }

        public ActionResult GetAll()
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            int moduleId = Convert.ToInt32(Session["moduleId"]);  //Add by Bably
            DataTable dt = new DataTable();
            dt = _fyService.GetAll(companyId, moduleId);
            List<CmnFinancialYearsForView> list = new List<CmnFinancialYearsForView>();
            if (dt.Rows.Count > 0)
            {
                list = dt.DataTableToList<CmnFinancialYearsForView>().OrderByDescending(t=>t.Id).ToList(); //Order By Last Entry First
            }

            return Json(list, JsonRequestBehavior.AllowGet);

        }

        
        public ActionResult CheckFinancialYear()
        {
            CmnFinancialYearResultForRange dts = new CmnFinancialYearResultForRange();
            dts.OpeningDate = ""; 
            dts.ClosingDate = "";
            try
            {
                int financialYearId = Convert.ToInt32(Session["financialYear"].ToString());
                int companyId = Convert.ToInt32(Session["companyId"].ToString());
                CmnFinancialYear fy = _fyService.GetFinancialYearDateRange(financialYearId);
                //If year is open
                if (!fy.YearClosingStatus && fy.CmnCompanyId == companyId)
                {
                    dts.OpeningDate = fy.OpeningDate.ToString("dd/MM/yyyy");
                    dts.ClosingDate = (fy.ClosingDate ?? default(DateTime)).ToString("dd/MM/yyyy");
                }                
            }
            catch(Exception ex)
            {

            }
            return Json(dts,JsonRequestBehavior.AllowGet);
        
        }

        public ActionResult IsMonthLocked(DateTime VoucherDate)
        {
            bool result = false;

            try
            {
                int financialYearId = Convert.ToInt32(Session["financialYear"].ToString());
                var months = _AnFMonthLockService.GetByfinancialYearId(financialYearId);
                months = months.Where(i => i.MonthNo == VoucherDate.Month && i.LockingStatus).ToList();
                if(months != null && months.Count > 0)
                {
                    result = true;
                }
            }
            catch(Exception ex)
            {

            }

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        

    }
}
