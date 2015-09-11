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
    public class OfficeTypeController : Controller
    {
        //
        // GET: /Hrm/Department/
        private IOfficeTypeService _officeTypeService;

        public OfficeTypeController()
        {
            var dbfactory = new DatabaseFactory();
            _officeTypeService = new OfficeTypeService(new OfficeTypeRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }
        #region OfficeType

        [HttpGet]
        public ActionResult GetAll()
        {
            var list = _officeTypeService.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

       

        [HttpPost]
        public ActionResult Save(SlsOfficeType slsOfficeType)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (slsOfficeType.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        slsOfficeType.CreatedBy = userId;
                        slsOfficeType.CreatedDate = DateTime.Now.Date;
                        objOperation = _officeTypeService.Save(slsOfficeType);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        slsOfficeType.ModifiedBy = userId;
                        slsOfficeType.ModifiedDate = DateTime.Now.Date;
                        objOperation = _officeTypeService.Update(slsOfficeType);
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
                SlsOfficeType obj = _officeTypeService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _officeTypeService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        #endregion

    }
}
