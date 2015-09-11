using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
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
    public class GeneralDiscountController : Controller
    {
        //
        // GET: /Sales/GeneralDiscount/
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }

        private IGeneralDiscountService _generalDiscountService;

        public GeneralDiscountController()
        {
            var dbfactory = new DatabaseFactory();
            _generalDiscountService = new GeneralDiscountService(new GeneralDiscountRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            var list = _generalDiscountService.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Save(SlsGeneralDiscount generalDiscount)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation {  Success  = false };

            if (ModelState.IsValid)
            {
                if (generalDiscount.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        SlsGeneralDiscount newGeneralDiscount = new SlsGeneralDiscount();
                        newGeneralDiscount.Id = 0;
                        newGeneralDiscount.SlsRegionId = generalDiscount.SlsRegionId;
                        newGeneralDiscount.Discount = generalDiscount.Discount;
                        newGeneralDiscount.CreatedBy = userId;
                        newGeneralDiscount.CreatedDate = DateTime.Now.Date;
                        objOperation = _generalDiscountService.Save(newGeneralDiscount);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        generalDiscount.ModifiedBy = userId;
                        generalDiscount.ModifiedDate = DateTime.Now.Date;
                        objOperation = _generalDiscountService.Update(generalDiscount);
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
                SlsGeneralDiscount obj = _generalDiscountService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _generalDiscountService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
    }
}
