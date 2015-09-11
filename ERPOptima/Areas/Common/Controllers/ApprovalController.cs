using ERPOptima.Lib.Model;
using ERPOptima.Model.ViewModel;
using ERPOptima.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Common.Controllers
{
    public class ApprovalController<TObj, TObjVM>
    {
        private IApprovalService<TObj, TObjVM> _approvalService;
        public ApprovalController(IApprovalService<TObj, TObjVM> approvalService)
        {
            _approvalService = approvalService;
        }
        public IEnumerable<TObjVM> GetAllForApproval(int userId)
        {
            var list = _approvalService.GetAllForApproval(userId);
            return list;
        }
        public IEnumerable<TObjVM> GetAllForApprovalByDate(int userId, DateTime fromDate, DateTime toDate)
        {
            var list = _approvalService.GetAllForApprovalByDate(fromDate, toDate, userId);


            return list;
        }
        public TObj GetApprovalById(int objId)
        {
            var obj = _approvalService.GetApprovalById(objId);
            return obj;
        }
        public Operation UpdateApproval(TObj o, string newComment)
        {
            var obj = _approvalService.UpdateApproval(o, newComment);
            return obj;
        }


        

    }

    public class ApprovalDataController : Controller
    {
        [HttpGet]
        public ActionResult GetApprovalDictionary()
        {
            var list = GetApprovalMasterData();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public IList<ApprovalStatus> GetApprovalMasterData()
        {
            //In DB mentioned-1=New,2=Approve,3=Pass,4=Reject
            IList<ApprovalStatus> list = new List<ApprovalStatus>();

            list.Add(new ApprovalStatus() { Id = 1, Name = "New" });
            list.Add(new ApprovalStatus() { Id = 2, Name = "Approved" });
            list.Add(new ApprovalStatus() { Id = 3, Name = "Passed" });
            list.Add(new ApprovalStatus() { Id = 4, Name = "Rejected" });

            return list;
        }
    }
}