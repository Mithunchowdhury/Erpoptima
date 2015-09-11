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
    public class UnitOfMeasurementController : Controller
    {
        //
        // GET: /Hrm/Department/
        private IUnitOfMeasurementService _unitOfMeasurementService;

        public UnitOfMeasurementController()
        {
            var dbfactory = new DatabaseFactory();
            _unitOfMeasurementService = new UnitOfMeasurementService(new UnitOfMeasurementRepository(dbfactory), new UnitOfWork(dbfactory));
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
            var list = _unitOfMeasurementService.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUnitByProductRequisition(int requisitionId, int productId)
        {
            var list = _unitOfMeasurementService.GetUnitByProductRequisition(requisitionId, productId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

       

        [HttpPost]
        public ActionResult Save(SlsUnit slsUnit)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (slsUnit.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        slsUnit.CreatedBy = userId;
                        slsUnit.CreatedDate = DateTime.Now.Date;
                        objOperation = _unitOfMeasurementService.Save(slsUnit);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        slsUnit.ModifiedBy = userId;
                        slsUnit.ModifiedDate = DateTime.Now.Date;
                        objOperation = _unitOfMeasurementService.Update(slsUnit);
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
                SlsUnit obj = _unitOfMeasurementService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _unitOfMeasurementService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        #endregion

    }
}
