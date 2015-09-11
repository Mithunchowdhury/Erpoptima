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
using ERPOptima.Web.Filters;

namespace Optima.Areas.Common.Controllers
{
    public class ApprovalProcessController : Controller
    {
        //
        // GET: /Common/ApprovalProcess/
        private ICmnApprovalProcessService _approvalProcessService;
        private ICmnApprovalProcessLevelService _cmnApprovalProcessLevelService;
        
         public ApprovalProcessController()
        {
            var dbfactory = new DatabaseFactory();
            _approvalProcessService = new CmnApprovalProcessService(new CmnApprovalProcessRepository(dbfactory), new UnitOfWork(dbfactory));
           

        }
               [AuthorizeUser]

        public ActionResult Index()
        {
            return View();
        }
        #region Approval Process
        [HttpGet]
        public ActionResult GetApprovalProcesses()
        {
            IList<CmnApprovalProcess> list = new List<CmnApprovalProcess>();
            list = _approvalProcessService.GetCmnApprovalProcesses().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        
        [HttpPost]
        public ActionResult SaveApprovalProcess(CmnApprovalProcess approvalProcess)
        {
            int userId = Convert.ToInt32(Session["userId"]);
                        Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                CmnApprovalProcess apvlProcess = new CmnApprovalProcess();
                apvlProcess.Id = approvalProcess.Id;
                apvlProcess.SecModuleId = approvalProcess.SecModuleId;
                apvlProcess.Name = approvalProcess.Name;
                apvlProcess.ShortName = approvalProcess.ShortName;
                apvlProcess.Status = approvalProcess.Status;
                if (apvlProcess.Id == 0)
                {
                    apvlProcess.CreatedBy = userId;
                    apvlProcess.CreatedDate = DateTime.Now.Date;
                    objOperation = _approvalProcessService.SaveCmnApprovalProcess(apvlProcess);
                }
                else
                {
                    apvlProcess.ModifiedBy = userId;
                    apvlProcess.ModifiedDate = DateTime.Now.Date;
                    objOperation = _approvalProcessService.UpdateCmnApprovalProcess(apvlProcess);
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeleteApprovalProcess(int Id = 0)
        {
            Operation objOperation = new Operation { Success = false };

            if (Id != 0)
            {
                CmnApprovalProcess  obj = _approvalProcessService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;

                }
                else
                {

                    objOperation = _approvalProcessService.DeleteCmnApprovalProcess(obj);
                }
                return Json(objOperation, JsonRequestBehavior.DenyGet);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }


        public ActionResult GetByModuleId( int SecModuleId)
        {
            var list = _approvalProcessService.GetByModuleId(SecModuleId).Select(ap=>new {

                Id=ap.Id,
                Name=ap.Name
            
            }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        
        }



        #endregion
    }
}
