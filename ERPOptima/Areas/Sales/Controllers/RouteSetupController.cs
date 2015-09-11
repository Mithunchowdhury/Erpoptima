using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Model.ViewModel;
using ERPOptima.Service.Sales;
using ERPOptima.Web.Filters;
using Optima.Areas.Common.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class RouteSetupController : Controller
    {
        private IRouteSetupService _service;
        private IRoutePlanService<SlsRoutePlanApproval, SlsRoutePlan> _routePlanService;
        private ApprovalController<SlsRoutePlanApproval, SlsRoutePlan> _approvalController;
        private IDistributorService _distributorservice;
        private IRetailerService _retailerservice;
        private IDealerService _delearservice;
        private ICorporateClientService _corporateclientservice;

        //
        // GET: /Sales/RouteSetup/
        public RouteSetupController()
        {
            var dbfactory = new DatabaseFactory();
            var unitOfWork = new UnitOfWork(dbfactory);
            _service = new RouteSetupService(new RouteSetupRepository(dbfactory), 
                new RouteSetupDetailRepository(dbfactory), new UnitOfWork(dbfactory));
            _routePlanService = new RoutePlanService(new RoutePlanRepository(dbfactory), new RoutePlanDetailRepository(dbfactory), 
                new RoutePlanApprovalRepository(dbfactory), new NotificationRepository(dbfactory), 
                new NotificationDetailRepository(dbfactory), new UnitOfWork(dbfactory));
            _approvalController = new ApprovalController<SlsRoutePlanApproval, SlsRoutePlan>(_routePlanService);
            _distributorservice = new DistributorService(new DistributorRepository(dbfactory), unitOfWork);
            _retailerservice = new RetailerService(new RetailerRepository(dbfactory), unitOfWork);
            _delearservice = new DealerService(new DealerRepository(dbfactory), unitOfWork);
            _corporateclientservice = new CorporateClientService(new CorporateClientRepository(dbfactory), unitOfWork);
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult RoutePlan()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Approval()
        {
            int? id = null;
            if (TempData["RoutePlanNumber"] != null)
            {
                id = (int)TempData["RoutePlanNumber"];
                ViewData["RoutePlanNumber"] = id;
            }

            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Test()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult GetAll()
        {
            var list = _service.GetAll();


            return Json(list, JsonRequestBehavior.AllowGet);
            //return null;
        }

        public ActionResult GetByRoutePlanId(int RoutePlanId)
        {
            var list = _routePlanService.GetByRoutePlanId(RoutePlanId).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult GetAllWeeks()
        {

            var dt = new DateTime(DateTime.Today.Year, 1, 1);
            for (; ; )
            {
                if (dt.DayOfWeek == DayOfWeek.Saturday)
                {
                    break;
                }
                else
                {
                    dt = dt.AddDays(1);
                }
            }

            //beware different cultures, see other answers
            var startOfFirstWeek = dt;
            var weeks =
                Enumerable
                    .Range(0, 54)
                    .Select(i => new
                    {
                        weekStart = startOfFirstWeek.AddDays(i * 7)
                    })
                    .TakeWhile(x => x.weekStart.Year <= dt.Year)
                    .Select(x => new
                    {
                        x.weekStart,
                        weekFinish = x.weekStart.AddDays(5)
                    })
                    .SkipWhile(x => x.weekFinish < dt.AddDays(1))
                    .Select((x, i) => new
                    {
                        x.weekStart,
                        x.weekFinish,
                        weekNum = i + 1
                    });
            //var list = _service.GetAll();
            var wk = weeks.Select(i => new
            {
                WeekNo = i.weekNum,
                Start=i.weekStart.ToString("dd-MMM-yyyy"),
                End = i.weekFinish.ToString("dd-MMM-yyyy")
            });
            return Json(wk.ToList(), JsonRequestBehavior.AllowGet);
            //return null;
        }
        [HttpGet]
        public ActionResult GetAllByOfficeId(int officeId)
        {
            var list = _service.GetAll(officeId);
            return Json(list, JsonRequestBehavior.AllowGet);
            //return null;
        }
        [HttpGet]
        public ActionResult GetAllPlansByEmployeeId(int employeeId)
        {
            var dbfactory = new DatabaseFactory();
            var list = _routePlanService.GetAll(employeeId);
            list = list.OrderByDescending(i => i.CreatedDate).ToList();  // Bbb

            var plans = list.Select(i => new
            {
                Id = i.Id,
                HrmEmplyeeId=i.HrmEmployeeId,
                Title=i.Title,
                WeekNo = i.WeekNo,
                Start = ((DateTime)i.DateFrom).ToString("dd-MMM-yyyy"),
                End = ((DateTime)i.DateTo).ToString("dd-MMM-yyyy")
            });
            return Json(plans.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetRouteSetupDetails(int routesetupId)
        {
            var list = _service.GetRouteSetupDetails(routesetupId);
            var distributorList = _distributorservice.GetAll();
            var retailerList = _retailerservice.GetAll();
            var delearList = _delearservice.GetAll();
            var corporateclientList = _corporateclientservice.GetAll();

            var details = list.Select(i => new SlsRouteDetailViewModel()
            {
                Id = i.Id,
                PartyType = i.PartyType,
                SlsCorporateClientId = i.SlsCorporateClientId,
                SlsDealerId = i.SlsDealerId,
                SlsDistributorId = i.SlsDistributorId,
                SlsRetailerId = i.SlsRetailerId,
                SlsRouteId = i.SlsRouteId,
                Name = "",
                Code = ""
            }).ToList();

            foreach (var item in details)
            {
                if(item.PartyType == 1)
                {
                    if (item.SlsDistributorId != null)
                    {
                        SlsDistributor obj = _distributorservice.GetById((int)item.SlsDistributorId);
                        item.Name = obj.Name;
                        item.Code = obj.Code;
                    }
                }
                else if(item.PartyType == 2)
                {
                    if (item.SlsRetailerId != null)
                    {
                        SlsRetailer obj = _retailerservice.GetById((int)item.SlsRetailerId);
                        item.Name = obj.Name;
                        item.Code = obj.Code;
                    }
                }
                else if (item.PartyType == 3)
                {
                    if (item.SlsDealerId != null)
                    {
                        SlsDealer obj = _delearservice.GetById((int)item.SlsDealerId);
                        item.Name = obj.Name;
                        item.Code = obj.Code;
                    }
                }
                else if (item.PartyType == 4)
                {
                    if (item.SlsCorporateClientId != null)
                    {
                        SlsCorporateClient obj = _corporateclientservice.GetById((int)item.SlsCorporateClientId);
                        item.Name = obj.Name;
                        item.Code = obj.Code;
                    }
                }                
            }

            return Json(details, JsonRequestBehavior.AllowGet);            
        }
        //Save
        [HttpPost]
        public ActionResult Save(SlsRoute record, IList<SlsRouteDetail> detailRecords)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid && record != null && detailRecords != null && detailRecords.Count > 0)
            {
                foreach (SlsRouteDetail item in detailRecords)
                {
                    if (item.SlsDistributorId == 0)
                        item.SlsDistributorId = null;
                    if (item.SlsRetailerId == 0)
                        item.SlsRetailerId = null;
                    if (item.SlsDealerId == 0)
                        item.SlsDealerId = null;
                    if (item.SlsCorporateClientId == 0)
                        item.SlsCorporateClientId = null;
                }

                if (record.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {

                        record.CreatedBy = userId;
                        record.CreatedDate = DateTime.Now.Date;
                        objOperation = _service.Save(record, detailRecords);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        record.ModifiedBy = userId;
                        record.ModifiedDate = DateTime.Now.Date;
                        objOperation = _service.Update(record, detailRecords);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }


            return Json(objOperation, JsonRequestBehavior.AllowGet);
            //return null;
        }
        public ActionResult SaveRoutePlan(SlsRoutePlan record)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };
            var dbfactory = new DatabaseFactory();            
            if (ModelState.IsValid)
            {
                if (record.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {

                        record.CreatedBy = userId;
                        record.CreatedDate = DateTime.Now.Date;
                        objOperation = _routePlanService.Save(record);
                    }
                    else
                    {
                        objOperation.OperationId = -1;
                    }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        record.ModifiedBy = userId;
                        record.ModifiedDate = DateTime.Now.Date;
                        objOperation = _routePlanService.Update(record);
                    }
                    else
                    {
                        objOperation.OperationId = -2;
                    }
                }
            }


            return Json(objOperation, JsonRequestBehavior.AllowGet);
            //return null;
        }

        #region Approval
        [HttpGet]
        public ActionResult GetAllForApproval()
        {
            int userId = Convert.ToInt32(Session["userId"]);
            var list = _approvalController.GetAllForApproval(userId);
            list = list.OrderByDescending(i => i.CreatedDate).ToList();
            var result = list.Select(i => new
            {
                Id = i.Id,
                WeekNo = i.WeekNo,
                DateFrom = i.DateFrom,
                DateTo = i.DateTo,
                HrmEmployeeId = i.HrmEmployeeId,
                Title = i.Title,
                Status = i.Status,
                ApprovalStatus = i.ApprovalStatus,
                CreatedBy = i.CreatedBy,
                CreatedDate = i.CreatedDate,
                ModifiedBy = i.ModifiedBy,
                ModifiedDate = i.ModifiedDate
            }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetAllForApprovalByDate(DateTime fromDate, DateTime toDate)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            int employeeId = Convert.ToInt32(Session["employeeId"]);
            var list = _approvalController.GetAllForApprovalByDate(userId, fromDate, toDate);
            
            var result = list.Select(i => new
            {
                Id = i.Id,
                WeekNo = i.WeekNo,
                DateFrom = i.DateFrom,
                DateTo = i.DateTo,
                HrmEmployeeId = i.HrmEmployeeId,
                Title = i.Title,
                Status = i.Status,
                ApprovalStatus = i.ApprovalStatus,
                CreatedBy = i.CreatedBy,
                CreatedDate = i.CreatedDate,
                ModifiedBy = i.ModifiedBy,
                ModifiedDate = i.ModifiedDate
            }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetApprovalById(int id)
        {
            var obj = _approvalController.GetApprovalById(id);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Approve(SlsRoutePlanApproval obj, string newComment)
        {
            //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
            return UpdateSalesOrderApproval(obj, 2, newComment);
        }

        [HttpPost]
        public ActionResult Reject(SlsRoutePlanApproval obj, string newComment)
        {
            //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
            return UpdateSalesOrderApproval(obj, 4, newComment);
        }
        [HttpPost]
        public ActionResult Pass(SlsRoutePlanApproval obj, string newComment)
        {
            //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
            return UpdateSalesOrderApproval(obj, 3, newComment);
        }
        public ActionResult UpdateSalesOrderApproval(SlsRoutePlanApproval obj, int action, string newComment)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        //Sales Order Approval is created internally during sales order creation.
                        objOperation.Success = false;
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        //According to DB column description: 1=New,2=Approve,3=Pass,4=Reject
                        obj.Action = action;
                        obj.ModifiedBy = userId;
                        obj.ModifiedDate = DateTime.Now.Date;
                        objOperation = _approvalController.UpdateApproval(obj, newComment);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        public ActionResult ApprovalById(int number)
        {
            TempData["RoutePlanNumber"] = number;
            return RedirectToAction("Approval");
        }
        #endregion


        public ActionResult DeleteRoutePlanById(int Id)
        {
            Operation objOperation = new Operation { Success = false };
          
            if (Id != 0)
            {
                SlsRoutePlan obj = _routePlanService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _routePlanService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }


    }
}


    