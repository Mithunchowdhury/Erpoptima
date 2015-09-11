using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Helper;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Common;
using ERPOptima.Service.Common;
using ERPOptima.Web.Accounts.ViewModel;
using ERPOptima.Web.Filters;
using Optima.Areas.Accounts.Controllers;
using Optima.Areas.Accounts.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Optima.Areas.Common.Controllers
{
    public class BusinessController : Controller
    {
        //
        // GET: /Common/Business/
        private ICmnBusinessService _cbService;
        
        
        public BusinessController()
        {
            var dbfactory = new DatabaseFactory();
            //_cbService = new CmnBusinessService(new CmnBusinessRepository(dbfactory), new UnitOfWork(dbfactory));
           
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }
        #region Business Sector

        [HttpGet]
        //public ActionResult GetCmnBusinesses()
        //{            
        //    int companyId = Convert.ToInt32(Session["companyId"]); 
        //    //Convert.ToInt32(Session["companyId"]);

        //    var list = _cbService.GetCmnBusinesses(companyId).ToList();//.Select(ac => new
        //    //{
        //    //    Id = ac.Id,
        //    //    Name = ac.Name,
        //    //    Code = ac.Code,
        //    //    Location = ac.Location
        //    //});
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public ActionResult GetCmnBusinessesIdAndName()
        //{
        //    int companyId = Convert.ToInt32(Session["companyId"]);
        //    //Convert.ToInt32(Session["companyId"]);

        //    var list = _cbService.GetCmnBusinesses(companyId).Select(ac => new
        //    {
        //        Id = ac.Id,
        //        Name = ac.Name
        //    });

        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        public ActionResult SaveCmnBusiness(CmnBusiness business)
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            int userId = Convert.ToInt32(Session["userId"]);
           Operation objOperation = new Operation { Success = false };
            if (ModelState.IsValid)
            {                
                if (business.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        business.CmnCompanyId = companyId;
                        business.CreatedBy = userId;
                        business.CreatedDate = DateTime.Now.Date;
                        objOperation = _cbService.SaveCmnBusiness(business);
                    }
                    else { objOperation.OperationId = -1; }
                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        business.CmnCompanyId = companyId;
                        business.ModifiedBy = userId;
                        business.ModifiedDate = DateTime.Now.Date;
                        objOperation = _cbService.UpdateCmnBusiness(business);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeleteAnFCostCenter(int Id)
        {
            Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                CmnBusiness obj = _cbService.GetById(Id);
                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _cbService.DeleteCmnBusiness(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }        

        #endregion
    }
}
