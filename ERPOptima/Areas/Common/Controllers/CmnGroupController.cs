using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Helper;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Common;
using ERPOptima.Model.Security;
using ERPOptima.Service.Common;
using ERPOptima.Web.Accounts.ViewModel;
using ERPOptima.Web.Filters;
using Optima.Areas.Accounts.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Optima.Areas.Common.Controllers
{
    public class GroupController : Controller
    {
        //
        // GET: /Common/Business/
        private ISecGroupService _ccService;

        public GroupController()
        {
            var dbfactory = new DatabaseFactory();
            _ccService = new SecGroupService(new SecGroupRepository(dbfactory), new UnitOfWork(dbfactory));

        }
        [AuthorizeUser]
        public ActionResult Index()
        {
            return View();
        }
        #region Group

        [HttpGet]
        public ActionResult GetGroups()
        {
            var list = _ccService.GetSecGroups().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveSecGroup(SecGroup Group)
        {
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                int userId = Convert.ToInt32(Session["userId"].ToString());
                Group.CreatedBy = userId;
                if (Group.Id == 0)
                {
                    objOperation = _ccService.SaveSecGroup(Group);
                }
                else
                {
                    Group.ModifiedBy = userId;
                    objOperation = _ccService.UpdateSecGroup(Group);
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeleteSecGroup(int Id)
        {
            Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                SecGroup obj = _ccService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _ccService.DeleteSecGroup(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        #endregion
    }
}