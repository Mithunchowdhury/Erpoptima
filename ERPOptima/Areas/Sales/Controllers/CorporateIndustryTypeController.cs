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
    public class CorporateIndustryTypeController : Controller
    {
        //
        // GET: /Hrm/Department/
        private ICorporateIndustryTypeService _corporateIndustryTypeService;

        public CorporateIndustryTypeController()
        {
            var dbfactory = new DatabaseFactory();
            _corporateIndustryTypeService = new CorporateIndustryTypeService(new CorporateIndustryTypeRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }
        #region Corporate Industry Type

        [HttpGet]
        public ActionResult GetAll()
        {
            var list = _corporateIndustryTypeService.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public ActionResult GetById(int distId)
        //{
        //    var corporateIndustryType = _corporateIndustryTypeService.GetAll().Where(i => i.Id == distId).First();
        //    return Json(corporateIndustryType, JsonRequestBehavior.AllowGet);
        //}
       

        [HttpPost]
        public ActionResult Save(SlsCorporateType slsCorporateType)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (slsCorporateType.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        slsCorporateType.CreatedBy = userId;
                        slsCorporateType.CreatedDate = DateTime.Now.Date;
                        objOperation = _corporateIndustryTypeService.Save(slsCorporateType);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        slsCorporateType.ModifiedBy = userId;
                        slsCorporateType.ModifiedDate = DateTime.Now.Date;
                        objOperation = _corporateIndustryTypeService.Update(slsCorporateType);
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
                SlsCorporateType obj = _corporateIndustryTypeService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _corporateIndustryTypeService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        #endregion

    }
}
