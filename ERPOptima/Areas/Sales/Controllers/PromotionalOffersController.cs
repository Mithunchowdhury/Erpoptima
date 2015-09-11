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
    public class PromotionalOffersController : Controller
    {
        //
        // GET: /Sales/PromotionalOffers/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }

        private IPromotionalOfferService _promotionalOfferService;       
        public PromotionalOffersController()
        {
            var dbfactory = new DatabaseFactory();
            _promotionalOfferService = new PromotionalOfferService(new PromotionalOfferRepository(dbfactory),
                new PromotionalOfferDetailRepository(dbfactory),
                new UnitOfWork(dbfactory));
        }


        [HttpGet]
        public ActionResult GetAll()
        {
            var list = _promotionalOfferService.GetAll();
            list = list.OrderByDescending(i => i.CreatedDate).ToList();  //order by createdDate

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAllCategoriesForOffer()
        {
            var list = _promotionalOfferService.GetAllCategoriesForOffer();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Save(SlsPromotionalOfferViewModel obj)
        {
            int userId = Convert.ToInt32(Session["userId"]);

            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        obj.CreatedBy = userId;
                        obj.CreatedDate = DateTime.Now.Date;
                        objOperation = _promotionalOfferService.Save(obj);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        obj.ModifiedBy = userId;
                        obj.ModifiedDate = DateTime.Now.Date;
                        objOperation = _promotionalOfferService.Update(obj);
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
                SlsPromotionalOfferViewModel obj = _promotionalOfferService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _promotionalOfferService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }


    }
}
