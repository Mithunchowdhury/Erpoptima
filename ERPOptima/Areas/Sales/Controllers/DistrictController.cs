using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Service.Sales;
using ERPOptima.Web.Filters;
using Optima.Areas.Sales.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class DistrictController : Controller
    {
        //
        // GET: /Sales/District/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }

        private IDistrictService _districtService;

        public DistrictController()
        {
            var dbfactory = new DatabaseFactory();
            _districtService = new DistrictService(new DistrictRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        #region Districts

        [HttpGet]
        public ActionResult GetAll(int? regionId, int? officeId)
        {
            Collection<DistrictViewModel> districts = null;
            DataTable dt = _districtService.GetAll(regionId,officeId);
            if (dt != null)
            {
                districts = new Collection<DistrictViewModel>();
                foreach (DataRow row in dt.Rows)
                {
                    districts.Add((DistrictViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(DistrictViewModel)));
                }
            }

            return Json(districts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetByOfficeId(int officeId)
        {
            var list = _districtService.GetAll().Where(i => i.SlsOfficeId == officeId).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetAllDistrict()
        {
            var list = _districtService.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDistrictByEmployee(int? employeeId, int officeId)
        {
            Collection<AreaConfigurationViewModel> areaConfigurations = null;
            DataTable dt = _districtService.GetDistrictByEmployee(employeeId, officeId);
            if (dt != null)
            {
                areaConfigurations = new Collection<AreaConfigurationViewModel>();
                foreach (DataRow row in dt.Rows)
                {
                    areaConfigurations.Add((AreaConfigurationViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(AreaConfigurationViewModel)));
                }
            }

            return Json(areaConfigurations, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(SlsDistrict district)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (district.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        SlsDistrict newDistrict = new SlsDistrict();
                        newDistrict.Id = 0;
                        newDistrict.Name = district.Name;
                        newDistrict.Code = district.Code;
                        newDistrict.Remarks = district.Remarks;
                        newDistrict.SlsOfficeId = district.SlsOfficeId;
                        newDistrict.CreatedBy = userId;
                        newDistrict.CreatedDate = DateTime.Now.Date;
                        objOperation = _districtService.Save(newDistrict);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        district.ModifiedBy = userId;
                        district.ModifiedDate = DateTime.Now.Date;
                        objOperation = _districtService.Update(district);
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
                SlsDistrict obj = _districtService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _districtService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        public ActionResult OfficeIdOfThisDistrict(int distId)
        {
            var list = _districtService.GetAll().Where(i => i.Id == distId).ToList();
            if (list == null || list.Count() == 0)
                return Json(new { result = 0, status = false }, JsonRequestBehavior.AllowGet);
            var officeId = list.First().SlsOfficeId;
            return Json(new { result = officeId, status = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
