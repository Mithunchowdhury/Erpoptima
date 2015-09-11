using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Accounts;
using ERPOptima.Service.Accounts;
using ERPOptima.Web.Filters;
using Optima.Areas.Accounts.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Optima.Areas.Accounts.Controllers
{
    public class MonthLockController : Controller
    {
        //
        // GET: /Accounts/MonthLock/

        private IAnFMonthLockService _AnFMonthLockService;

        public MonthLockController()
        {
            var dbfactory = new DatabaseFactory();
            _AnFMonthLockService = new AnFMonthLockService(new AnFMonthLockRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetByFinancialYearId(int financialYear = 1)
        {
            var list = _AnFMonthLockService.GetByfinancialYearId(financialYear).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetByCurrentFinancialYearId()
        {
            int financialYearId = Convert.ToInt32(Session["financialYear"]);
            var list = _AnFMonthLockService.GetByfinancialYearId(financialYearId).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(AnFMonthLockViewModel objAnFMonthLockViewModel)
        {
            Operation operation = new Operation { Success = false };
            if (ModelState.IsValid)
            {
                var obj = _AnFMonthLockService.GetById(objAnFMonthLockViewModel.Id);
                obj.LockingStatus = objAnFMonthLockViewModel.LockStatus;
                operation = _AnFMonthLockService.Update(obj);
            }

            return Json(operation, JsonRequestBehavior.DenyGet);
        }

        // Save Method
        public ActionResult Save(AnFMonthLock monthLock)
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            int userid = Convert.ToInt32(Session["userId"]);
            int financialYearId = Convert.ToInt32(Session["financialYear"]);

            monthLock.CmnFinancialYearId = financialYearId;
            
            Operation objOperation = new Operation();
            if(ModelState.IsValid)
            {
                if(monthLock.Id == 0)
                {
                    if((bool)Session["Add"])
                    {
                        monthLock.SecConpanyId = companyId;
                        monthLock.CreatedBy = userid;
                     
                        monthLock.CreatedDate = DateTime.Now.Date;
                        objOperation = _AnFMonthLockService.Save(monthLock);
                    }
                    else
                    {
                        objOperation.OperationId = -2;
                        objOperation.Success = false;
                    }
                }
                else
                {
                    if((bool)Session["Edit"])
                    {
                        monthLock.SecConpanyId = companyId;
                        monthLock.ModifiedBy = userid;
                        monthLock.ModifiedDate = DateTime.Now.Date;
                        objOperation = _AnFMonthLockService.Update(monthLock);
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

        // Created by Bably
        [HttpGet]
       
        public ActionResult GetYears()
        {
            var dbfactory = new DatabaseFactory();
            ERPOptima.Data.Common.Repository.CmnFinancialYearRepository fyRep = new ERPOptima.Data.Common.Repository.CmnFinancialYearRepository(dbfactory);
            UnitOfWork uow = new UnitOfWork(dbfactory);
            ERPOptima.Service.Common.ICmnFinancialYearService fyService = new ERPOptima.Service.Common.CmnFinancialYearService(fyRep, uow);

            int financialYearId = Convert.ToInt32(Session["financialYear"]);
            //int financialYearId = 34;
            var years = new List<object>();
            if (financialYearId != null)
            {
               
                ERPOptima.Model.Common.CmnFinancialYear fy = fyService.GetById(financialYearId);
                if(fy!=null)
                {
                    //Add string Year
                    var strYear = fy.OpeningDate.Year;
                    var obj = new { Name = strYear };
                    years.Add(obj);
                    //Add End Year
                    if (fy.ClosingDate.HasValue)
                    {
                        var endYear = fy.ClosingDate.Value.Year;
                        if(!years.Equals(endYear))
                        {
                            obj = new { Name = endYear };
                            years.Add(obj);
                        }
                    }

                }
            }
            return Json(years, JsonRequestBehavior.AllowGet);
        }

        //Delete Method
         [HttpPost]
        public ActionResult Delete(int Id)
        {
            Operation objOperation = new Operation { Success = false };
            if(Id != 0)
            {
                AnFMonthLock obj = _AnFMonthLockService.GetById(Id);
                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _AnFMonthLockService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
         //public int Delete(AnFMonthLock monthLock)
         //{
         //    int ret = 0;
         //    if (monthLock != null)
         //    {
         //        _AnFMonthLockService.Delete(monthLock);

         //    }

         //    return ret;
         //}

    }
}