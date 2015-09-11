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
    public class ThanaController : Controller
    {
        //
        // GET: /Hrm/Department/
        private IThanaService _thanaService;

        public ThanaController()
        {
            var dbfactory = new DatabaseFactory();
            _thanaService = new ThanaService(new ThanaRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }
        #region Thana

        [HttpGet]

        public ActionResult GetAll(int? regionId, int? officeId, int? districtId)
        {
            Collection<ThanaViewModel> thanas = null;
            DataTable dt = _thanaService.GetAll(regionId, officeId, districtId);
            if (dt != null)
            {
                thanas = new Collection<ThanaViewModel>();
                foreach (DataRow row in dt.Rows)
                {
                    thanas.Add((ThanaViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(ThanaViewModel)));
                }
            }

            return Json(thanas, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAllIds(int regionId = 0, int officeId = 0, int districtId = 0, int thanaId = 0)
        {
            if (thanaId > 0)
            {
                var thanas = _thanaService.GetAll();
                if (thanas != null && thanas.Count() > 0)
                    thanas = thanas.Where(i => i.Id == thanaId).ToList();
                var Ids = new HashSet<int>(thanas.Select(x => x.Id));

                return Json(Ids, JsonRequestBehavior.AllowGet);
            }
            else if (districtId > 0)
            {
                var thanas = _thanaService.GetThanasForDistrict(districtId);
                var Ids = new HashSet<int>(thanas.Select(x => x.Id));

                return Json(Ids, JsonRequestBehavior.AllowGet);
            }
            else if (officeId > 0)
            {
                var thanas = _thanaService.GetThanasForOffice(officeId);
                var Ids = new HashSet<int>(thanas.Select(x => x.Id));

                return Json(Ids, JsonRequestBehavior.AllowGet);
            }
            else if (regionId > 0)
            {
                var thanas = _thanaService.GetThanasForRegion(regionId);
                var Ids = new HashSet<int>(thanas.Select(x => x.Id));
                if ((Ids != null && Ids.Count() <= 0) || (Ids == null)) Ids = new HashSet<int>();
                return Json(Ids, JsonRequestBehavior.AllowGet);
            }

            var thanalist = _thanaService.GetAll();
            var thanaIds = new HashSet<int>(thanalist.Select(x => x.Id));

            return Json(thanaIds, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetByDistrictId(int districtId)
        {
            var list = _thanaService.GetAll().Where(i => i.SlsDistrictId == districtId).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetAllThana()
        {
            var list = _thanaService.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetThanaByEmployee(int employeeId, int districtId)
        {
            Collection<AreaConfigurationViewModel> areaConfigurations = null;
            DataTable dt = _thanaService.GetThanaByEmployee(employeeId, districtId);
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
        public ActionResult Save(SlsThana slsThana)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (slsThana.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        slsThana.CreatedBy = userId;
                        slsThana.CreatedDate = DateTime.Now.Date;
                        objOperation = _thanaService.Save(slsThana);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        slsThana.ModifiedBy = userId;
                        slsThana.ModifiedDate = DateTime.Now.Date;
                        objOperation = _thanaService.Update(slsThana);
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
                SlsThana obj = _thanaService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _thanaService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        public ActionResult DistrictIdOfThisThana(int thanaId)
        {
            var list = _thanaService.GetAll().Where(i => i.Id == thanaId).ToList();
            if (list == null || list.Count() == 0)
                return Json(new { result = 0, status = false }, JsonRequestBehavior.AllowGet);
            var distId = list.First().SlsDistrictId;
            return Json(new { result = distId, status = true }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
