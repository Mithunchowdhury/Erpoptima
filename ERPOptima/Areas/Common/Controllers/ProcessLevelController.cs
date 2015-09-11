using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Common;
using ERPOptima.Service.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPOptima.Lib.Utilities;
using ERPOptima.Web.Common.ViewModel;
using Microsoft.VisualStudio.QualityTools.UnitTestFramework;
using ERPOptima.Web.Filters;

namespace Optima.Areas.Common.Controllers
{
    public class ProcessLevelController : Controller
    {
        //
        // GET: /Common/ProcessLevel/
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }

        private ICmnProcessLevelService _CmnProcessLevelService;
        private ICmnApprovalProcessLevelService _cmnApprovalProcessLevelService;

        public ProcessLevelController()
        {
            var dbfactory = new DatabaseFactory();
            _CmnProcessLevelService = new CmnProcessLevelService(new CmnProcessLevelRepository(dbfactory), new UnitOfWork(dbfactory));
            _cmnApprovalProcessLevelService = new CmnApprovalProcessLevelService(new CmnApprovalProcessLevelRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        [HttpGet]
        public ActionResult GetProcessLevels()
        {
            var list = _CmnProcessLevelService.GetProcessLevel().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(CmnProcessLevel objCmnProcessLevel)
        {
                        Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {

                if (objCmnProcessLevel.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        objOperation = _CmnProcessLevelService.Save(objCmnProcessLevel);
                    }
                    else { objOperation.OperationId = -1; }
                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        objOperation = _CmnProcessLevelService.Update(objCmnProcessLevel);
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
                CmnProcessLevel objCmnProcessLevel = _CmnProcessLevelService.GetById(Id);

                if (objCmnProcessLevel == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _CmnProcessLevelService.Delete(objCmnProcessLevel);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }


        public ActionResult GetByModuleId(int approvalProcessId,int companyId=1 )
        {

            DataTable dt = _cmnApprovalProcessLevelService.GetByCompanyModuleApprovalProcessId(companyId, approvalProcessId);

            var list = dt.DataTableToList<CmnApprovalProcessLevelMappingViewModel>();

            return Json(list, JsonRequestBehavior.AllowGet);

        }




    }
}
