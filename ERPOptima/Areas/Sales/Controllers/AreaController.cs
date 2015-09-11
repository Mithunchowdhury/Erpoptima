using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.HRM;
using ERPOptima.Model.Sales;
using ERPOptima.Service.Sales;
using ERPOptima.Web.Filters;
using Optima.Areas.Hrm.ViewModel;
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
    public class AreaController : Controller
    {
        //
        // GET: /Sales/Area/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Configuration()
        {
            return View();
        }
        private IAreaService _areaService;
        private IAreaConfigurationService _areaConfigurationService;
        private IAreaConfigurationDetailService _areaConfigurationDetailService;

        public AreaController()
        {
            var dbfactory = new DatabaseFactory();
            _areaService = new AreaService(new AreaRepository(dbfactory), new UnitOfWork(dbfactory));
            _areaConfigurationService = new AreaConfigurationService(new AreaConfigurationRepository(dbfactory), new UnitOfWork(dbfactory));
            _areaConfigurationDetailService = new AreaConfigurationDetailService(new AreaConfigurationDetailRepository(dbfactory), new UnitOfWork(dbfactory));

        }
        #region Area
        [HttpGet]
        public ActionResult GetAllArea()
        {
            var list = _areaService.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]

        public ActionResult GetAll(int? regionId, int? officeId, int? districtId, int? thanaId)
        {
            Collection<AreaViewModel> thanas = null;
            DataTable dt = _areaService.GetAll(regionId, officeId, districtId, thanaId);

            if (dt != null)
            {
                thanas = new Collection<AreaViewModel>();
                foreach (DataRow row in dt.Rows)
                {
                    thanas.Add((AreaViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(AreaViewModel)));
                }
            }

            return Json(thanas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAreaByEmployee(int employeeId, int thanaId)
        {
            Collection<AreaConfigurationViewModel> areaConfigurations = null;
            DataTable dt = _areaService.GetAreaByEmployee(employeeId, thanaId);
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
        public ActionResult Save(SlsArea slsThana)
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
                        objOperation = _areaService.Save(slsThana);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        slsThana.ModifiedBy = userId;
                        slsThana.ModifiedDate = DateTime.Now.Date;
                        objOperation = _areaService.Update(slsThana);
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
                SlsArea obj = _areaService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _areaService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        #endregion

        #region Area Configuration
        [HttpGet]
        public ActionResult GetAreaConfigurationByEmployee(int employeeId)
        {
            Collection<AreaConfigurationViewModel> areaConfigurations = null;
            DataTable dt = _areaConfigurationService.GetByEmployeeId(employeeId);
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
        public ActionResult SaveAreaConfiguration(SlsAreaConfiguration objConfig, Collection<SlsAreaConfigurationDetail> objConfigDetails)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            _areaConfigurationService.DeleteConfiguration((int)objConfig.HrmEmployeeId);
            Operation objOperation = new Operation { Success = false };
            if (ModelState.IsValid)
            {
                if ((bool)Session["Add"] || (bool)Session["Edit"])
                {
                    SlsAreaConfiguration obj = new SlsAreaConfiguration();
                    obj.Id = 0;
                    obj.HrmEmployeeId = (int)objConfig.HrmEmployeeId;
                    obj.IsAreaBased = objConfig.IsAreaBased;
                    obj.IsDistrictBased = objConfig.IsDistrictBased;
                    obj.IsOfficeBased = objConfig.IsOfficeBased;
                    obj.IsThanaBased = objConfig.IsThanaBased;
                    obj.IsRegionBased = objConfig.IsRegionBased;
                    obj.Remarks = "";
                    obj.CreatedBy = userId;
                    obj.CreatedDate = DateTime.Now.Date;

                    objOperation = _areaConfigurationService.Save(obj);
                    _areaConfigurationDetailService.Save(objConfigDetails, (int)objOperation.OperationId);
                }
                else { objOperation.OperationId = -1; }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region Area Configuration new
        public ActionResult GetAreaForConfiguration(int basedOn = 0, int selemployeeId = 0, int selRegionId = 0,
            int selOfficeId = 0, int selDistrictId = 0, int selThanaId = 0)
        {
            //var basedOn = 1;
            //var employeeId = 2;
            //var selRegionId = 1;
            //var selOfficeId = 1;
            //var selDistrictId = 1;
            //var selThanaId = 1;

            var dbfactory = new DatabaseFactory();
            IRegionService _RegionService = new RegionService(new RegionRepository(dbfactory), new UnitOfWork(dbfactory));
            IOfficeService _OfficeService = new OfficeService(new OfficeRepository(dbfactory), new UnitOfWork(dbfactory));
            IDistrictService _DistrictService = new DistrictService(new DistrictRepository(dbfactory), new UnitOfWork(dbfactory));
            IThanaService _ThanaService = new ThanaService(new ThanaRepository(dbfactory), new UnitOfWork(dbfactory));
            IAreaService _AreaService = new AreaService(new AreaRepository(dbfactory), new UnitOfWork(dbfactory));

            IList<int> areaConfiguredForEmp = new List<int>();
            var allAreaConfigurations = _areaConfigurationService.GetAll();
            if (selemployeeId > 0)
            {
                if (allAreaConfigurations != null && allAreaConfigurations.Count() > 0)
                    allAreaConfigurations = allAreaConfigurations.Where(i => i.HrmEmployeeId == selemployeeId).ToList();
                if (allAreaConfigurations != null && allAreaConfigurations.Count() > 0)
                    areaConfiguredForEmp = allAreaConfigurations.Select(i => i.Id).ToList();
            }
            var allAreaConfigurationDetailss = _areaConfigurationDetailService.GetAll();
            if (allAreaConfigurationDetailss != null && allAreaConfigurationDetailss.Count() > 0)
                allAreaConfigurationDetailss = allAreaConfigurationDetailss.Where(i => areaConfiguredForEmp.Contains(i.SlsAreaConfigurationId)).ToList();
            var allRegions = _RegionService.GetAll();
            var allOffices = _OfficeService.GetAll();
            var allDistricts = _DistrictService.GetAll();
            var allThanas = _ThanaService.GetAll();
            var allAreas = _AreaService.GetAll();

            IList<AreaConfigurationViewModel> areaConfigurations = null;

            switch (basedOn)
            {
                case 1:
                    IList<int> regionsConfigured = new List<int>();
                    if (allAreaConfigurationDetailss != null && allAreaConfigurationDetailss.Count() > 0)
                        regionsConfigured = allAreaConfigurationDetailss.Where(i => i.BasedOn == "Region").Select(j => j.RefId).ToList();

                    areaConfigurations = allRegions.Select(i => new AreaConfigurationViewModel()
                    {
                        Id = i.Id,
                        Code = i.Code,
                        Name = i.Name,
                        IsRegionBased = true,
                        Status = regionsConfigured.Contains(i.Id) ? true : false
                    }).Distinct().ToList();
                    break;
                case 2:
                    IList<int> officesConfigured = new List<int>();
                    if (allAreaConfigurationDetailss != null && allAreaConfigurationDetailss.Count() > 0)
                        officesConfigured = allAreaConfigurationDetailss.Where(i => i.BasedOn == "Office").Select(j => j.RefId).ToList();
                    if (selRegionId > 0)
                        allOffices = allOffices.Where(i => i.SlsRegionId == selRegionId).ToList();

                    areaConfigurations = allOffices.Select(i => new AreaConfigurationViewModel()
                    {
                        Id = i.Id,
                        Code = i.Code,
                        Name = i.Name,
                        IsOfficeBased = true,
                        Status = officesConfigured.Contains(i.Id) ? true : false
                    }).Distinct().ToList();
                    break;
                case 3:
                    IList<int> districtsConfigured = new List<int>();
                    if (allAreaConfigurationDetailss != null && allAreaConfigurationDetailss.Count() > 0)
                        districtsConfigured = allAreaConfigurationDetailss.Where(i => i.BasedOn == "District").Select(j => j.RefId).ToList();
                    if (selOfficeId > 0)
                        allDistricts = allDistricts.Where(i => i.SlsOfficeId == selOfficeId).ToList();
                    else if (selRegionId > 0)
                    {
                        IList<int> selOffices = allOffices.Where(i => i.SlsRegionId == selRegionId).Select(j => j.Id).ToList();
                        IList<SlsDistrict> districts = new List<SlsDistrict>();
                        foreach (var item in allDistricts)
                        {
                            if (item.SlsOfficeId != null)
                                districts.Add(item);
                        }
                        allDistricts = districts;
                        allDistricts = allDistricts.Where(i => selOffices.Contains((int)i.SlsOfficeId)).ToList();
                    }

                    areaConfigurations = allDistricts.Select(i => new AreaConfigurationViewModel()
                    {
                        Id = i.Id,
                        Code = i.Code,
                        Name = i.Name,
                        IsOfficeBased = true,
                        Status = districtsConfigured.Contains(i.Id) ? true : false
                    }).Distinct().ToList();
                    break;
                case 4:
                    IList<int> thanasConfigured = new List<int>();
                    if (allAreaConfigurationDetailss != null && allAreaConfigurationDetailss.Count() > 0)
                        thanasConfigured = allAreaConfigurationDetailss.Where(i => i.BasedOn == "Thana").Select(j => j.RefId).ToList();
                    if (selDistrictId > 0)
                        allThanas = allThanas.Where(i => i.SlsDistrictId == selDistrictId).ToList();
                    else if (selOfficeId > 0)
                    {
                        IList<int> selDistricts = allDistricts.Where(i => i.SlsOfficeId == selOfficeId).Select(j => j.Id).ToList();
                        allThanas = allThanas.Where(i => selDistricts.Contains((int)i.SlsDistrictId)).ToList();
                    }
                    else if (selRegionId > 0)
                    {
                        IList<int> selOffices = allOffices.Where(i => i.SlsRegionId == selRegionId).Select(j => j.Id).ToList();
                        IList<SlsDistrict> districts = new List<SlsDistrict>();
                        foreach (var item in allDistricts)
                        {
                            if (item.SlsOfficeId != null)
                                districts.Add(item);
                        }
                        allDistricts = districts;
                        allDistricts = allDistricts.Where(i => selOffices.Contains((int)i.SlsOfficeId)).ToList();
                        if (allDistricts != null)
                        {
                            IList<int> selDistricts = allDistricts.Select(i => i.Id).ToList();
                            allThanas = allThanas.Where(i => selDistricts.Contains((int)i.SlsDistrictId)).ToList();
                        }
                    }

                    areaConfigurations = allThanas.Select(i => new AreaConfigurationViewModel()
                    {
                        Id = i.Id,
                        Code = i.Code,
                        Name = i.Name,
                        IsOfficeBased = true,
                        Status = thanasConfigured.Contains(i.Id) ? true : false
                    }).Distinct().ToList();
                    break;
                case 5:
                    IList<int> areasConfigured = new List<int>();
                    if (allAreaConfigurationDetailss != null && allAreaConfigurationDetailss.Count() > 0)
                        areasConfigured = allAreaConfigurationDetailss.Where(i => i.BasedOn == "Area").Select(j => j.RefId).ToList();

                    if (selThanaId > 0)
                        allAreas = allAreas.Where(i => i.SlsThanaId == selThanaId).ToList();
                    else if (selDistrictId > 0)
                    {
                        IList<int> selThanas = allThanas.Where(i => i.SlsDistrictId == selDistrictId).Select(j => j.Id).ToList();
                        allAreas = allAreas.Where(i => selThanas.Contains((int)i.SlsThanaId)).ToList();
                    }
                    else if (selOfficeId > 0)
                    {
                        IList<int> selDistricts = allDistricts.Where(i => i.SlsOfficeId == selOfficeId).Select(j => j.Id).ToList();
                        allThanas = allThanas.Where(i => selDistricts.Contains((int)i.SlsDistrictId)).ToList();
                        if (allThanas != null)
                        {
                            IList<int> selThanas = allThanas.Select(j => j.Id).ToList();
                            allAreas = allAreas.Where(i => selThanas.Contains((int)i.SlsThanaId)).ToList();
                        }
                    }
                    else if (selRegionId > 0)
                    {
                        IList<int> selOffices = allOffices.Where(i => i.SlsRegionId == selRegionId).Select(j => j.Id).ToList();
                        IList<SlsDistrict> districts = new List<SlsDistrict>();
                        foreach (var item in allDistricts)
                        {
                            if (item.SlsOfficeId != null)
                                districts.Add(item);
                        }
                        allDistricts = districts;
                        allDistricts = allDistricts.Where(i => selOffices.Contains((int)i.SlsOfficeId)).ToList();
                        if (allDistricts != null)
                        {
                            IList<int> selDistricts = allDistricts.Select(i => i.Id).ToList();
                            allThanas = allThanas.Where(i => selDistricts.Contains((int)i.SlsDistrictId)).ToList();
                            if (allThanas != null)
                            {
                                IList<int> selThanas = allThanas.Select(j => j.Id).ToList();
                                allAreas = allAreas.Where(i => selThanas.Contains((int)i.SlsThanaId)).ToList();
                            }
                        }
                    }

                    areaConfigurations = allAreas.Select(i => new AreaConfigurationViewModel()
                    {
                        Id = i.Id,
                        Code = i.Code,
                        Name = i.Name,
                        IsOfficeBased = true,
                        Status = areasConfigured.Contains(i.Id) ? true : false
                    }).Distinct().ToList();
                    break;
                default:
                    areaConfigurations = new Collection<AreaConfigurationViewModel>();
                    break;
            }



            return Json(areaConfigurations, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}