using ERPOptima.Data.Infrastructure;
using ERPOptima.Service.Sales;
using ERPOptima.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Service.Sales;
using ERPOptima.Web.Filters;
using Optima.Areas.Common.Controllers;

namespace Optima.Areas.Sales.Controllers
{
    public class SalesDiscountSettingController : Controller
    {
       
        //
        // GET: /Sales/SalesDiscountSetting/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }
        private ISalesDiscountSettingService _salesDiscountSettingService;
        public SalesDiscountSettingController()
        {
            var dbfactory = new DatabaseFactory();
             ISalesDiscountSettingRepository rpos=new SalesDiscountSettingRepository(dbfactory);
             UnitOfWork unit=new UnitOfWork(dbfactory);
            _salesDiscountSettingService = new SalesDiscountSettingService(rpos, unit);
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            var list = _salesDiscountSettingService.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Save(SlsDiscountSetting discountSetting)// store to value for save
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (discountSetting.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        discountSetting.Id = 0;//new value input
                        discountSetting.CreatedBy = userId;
                        discountSetting.CreatedDate = DateTime.Now.Date;
                        objOperation = _salesDiscountSettingService.Save(discountSetting);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        discountSetting.ModifiedBy = userId;
                        discountSetting.ModifiedDate = DateTime.Now.Date;
                        objOperation = _salesDiscountSettingService.Update(discountSetting);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            Operation objOperation = new Operation { Success = false };

            if (Id != 0)
            {
                SlsDiscountSetting obj = _salesDiscountSettingService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _salesDiscountSettingService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [AllowAnonymous]
        public JsonResult IsTitleAvailable(string title)
        {
            var list = _salesDiscountSettingService.GetAll();
            bool status = true;

            if (title.Trim().Length > 0 &&
                list != null && list.Count() > 0)
            {
                var existobj = list.Where(i => i.Title == title).FirstOrDefault();
                if (existobj != null && existobj.Id > 0)
                    status = false;
            }
            return Json(new { result = status });
        }

    }
}
