using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Common;
using ERPOptima.Service.Common;
using ERPOptima.Web.Security.ViewModel;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using ERPOptima.Lib.Utilities;
using ERPOptima.Web.Filters;

namespace Optima.Areas.Security.Controllers
{
    public class ApprovalController : Controller
    {
        private ICmnApprovalProcessLevelService _cmnApprovalProcessLevelService;
        private ICmnApprovalUserPermissionService _cmnApprovalUserPermissionService;
        public ApprovalController()
        {
            var dbfactory = new DatabaseFactory();
            _cmnApprovalProcessLevelService = new CmnApprovalProcessLevelService(new CmnApprovalProcessLevelRepository(dbfactory), new UnitOfWork(dbfactory));
            _cmnApprovalUserPermissionService = new CmnApprovalUserPermissionService(new CmnApprovalUserPermissionRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        //
        // GET: /Security/Approval/
        [AuthorizeUser]
        public ActionResult Index()
        {
            return View();
        }
        [AuthorizeUser]
        public ActionResult ApprovalProcessLevelMapping()
        {
            return View();
        }
        [AuthorizeUser]
        public ActionResult ApprovalPermission()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveApprovalProcessMapping(List<CmnApprovalProcessLevelViewModel> objApprovalProcessLevels, int cmnCompanyId = 1)
        {
            Operation operation = new Operation { Success = false };
            if (ModelState.IsValid)
            {
                CmnApprovalProcessLevel obj = null;
                int lastId = 0;
                lastId = _cmnApprovalProcessLevelService.GetLastId();
                foreach (CmnApprovalProcessLevelViewModel item in objApprovalProcessLevels)
                {
                    if (item.Id == 0)
                    {
                        obj = new CmnApprovalProcessLevel();
                        obj.Id = lastId;
                        obj.Priority = item.Priority;
                        obj.CmnProcessLevelId = item.CmnProcessLevelId;
                        obj.CmnApprovalProcessId = item.CmnApprovalProcessId;
                        obj.CmnCompanyId = cmnCompanyId;

                        _cmnApprovalProcessLevelService.Save(obj);
                        lastId++;
                    }
                    else
                    {
                        obj = _cmnApprovalProcessLevelService.GetById(item.Id);
                        if (item.Mapped)
                        {
                            obj.Priority = item.Priority;
                            obj.CmnProcessLevelId = item.CmnProcessLevelId;
                            obj.CmnApprovalProcessId = item.CmnApprovalProcessId;
                            _cmnApprovalProcessLevelService.Update(obj);
                        }
                        else
                        {
                            _cmnApprovalProcessLevelService.Delete(obj);
                        }
                    }
                }

                operation = _cmnApprovalProcessLevelService.Commit();
            }
            return Json(operation, JsonRequestBehavior.DenyGet);
        }

        public ActionResult GetApprovalPermission(int SecModuleId, int SecUserId, int companyId = 1)
        {

            DataTable dt = _cmnApprovalUserPermissionService.GetApprovalPermission(SecModuleId, companyId, SecUserId);

            var list = dt.DataTableToList<ApprovalPermissionViewModel>();

            return Json(list, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult SaveApprovalPermission(List<CmnApprovalUserPermissionViewModel> obj)
        {
            Operation operation = new Operation { Success = false };
            if (ModelState.IsValid)
            {
                CmnApprovalUserPermission cmnApprovalUserPermission;
                int lastId = _cmnApprovalUserPermissionService.GetLastId();
                if (obj != null)
                {
                    foreach (CmnApprovalUserPermissionViewModel item in obj)
                    {
                        if (item.Id == 0)
                        {
                            cmnApprovalUserPermission = new CmnApprovalUserPermission();
                            cmnApprovalUserPermission.Id = lastId;
                            cmnApprovalUserPermission.CmnApprovalProcessLevelId = item.CmnApprovalProcessLevelId;
                            cmnApprovalUserPermission.SecUserId = item.SecUserId;
                            _cmnApprovalUserPermissionService.Save(cmnApprovalUserPermission);
                            lastId++;

                        }
                        else if (item.Id != 0 && item.Mapped == false)
                        {
                            cmnApprovalUserPermission = _cmnApprovalUserPermissionService.GetById(item.Id);
                            _cmnApprovalUserPermissionService.Delete(cmnApprovalUserPermission);
                        }
                    }
                    operation = _cmnApprovalUserPermissionService.Commit();

                }
                else { operation.Success = true; }
            }
            return Json(operation, JsonRequestBehavior.DenyGet);
        }
    }

}