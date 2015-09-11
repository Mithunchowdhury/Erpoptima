using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Helper;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Service.Sales;
using ERPOptima.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class CommissionPackageController : Controller
    {
        //
        // GET: /Sales/CommissionPackage/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }

        private ICommissionPackageService _CommissionPackageService;

        public CommissionPackageController()
        {
            var dbfactory = new DatabaseFactory();
            _CommissionPackageService = new CommissionPackageService(new CommissionPackageRepository(dbfactory), new UnitOfWork(dbfactory));            
        }

        [HttpGet]
        public ActionResult GetYears()
        {
            IEnumerable<int> yearList = Enumerable.Range(DateTime.Now.Year, 10);
            var years = yearList.Select(i => new { Id = i, Name = i }).Distinct().ToList();
            return Json(years, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetMonths()
        {
            IList<MMonth> monthList = YearMonth.GetMonths();
            return Json(monthList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            var list = _CommissionPackageService.GetAll();
            list = list.OrderByDescending(i => i.Year).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Save(SlsCommissionPackage objT)
        {
            int userId = Convert.ToInt32(Session["userId"]);            
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (objT.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        objT.CreatedBy = userId;
                        objT.CreatedDate = DateTime.Now.Date;                        
                        objOperation = _CommissionPackageService.Save(objT);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        objT.ModifiedBy = userId;
                        objT.ModifiedDate = DateTime.Now.Date;
                        objOperation = _CommissionPackageService.Update(objT);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public ActionResult Delete(SlsCommissionPackage objT)
        {
            Operation objOperation = new Operation { Success = false };

            if (objT!= null && objT.Id != 0)
            {
                objOperation = _CommissionPackageService.Delete(objT);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

    }
}
