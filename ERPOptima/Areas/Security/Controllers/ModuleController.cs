using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Security.Repository;
using ERPOptima.Service.Security;
using Optima.Areas.Security.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPOptima.Lib.Utilities;
using System.Web.Routing;
using ERPOptima.Model.Security;
using ERPOptima.Lib.Model;
using ERPOptima.Web.Filters;

namespace Optima.Areas.Security.Controllers
{
    public class ModuleController : Controller
    {
        //
        // GET: /Security/Module/

        private ISecModuleService _secModuleService;

        public ModuleController()
        {
            var datafactory = new DatabaseFactory();
            _secModuleService = new SecModuleService(new SecModuleRepository(datafactory), new UnitOfWork(datafactory));
        }
       
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }
        [AuthorizeUser]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSecModules()
        {
            DataTable dt = new DataTable();
            dt = _secModuleService.GetModules();
            List<SecModuleDetails> list = dt.DataTableToList<SecModuleDetails>();
          
            return Json(list,JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetByCompanyId()
         {
             int companyId = Convert.ToInt32(Session["companyId"]);
             var dt = _secModuleService.GetModuleByCompanyId(companyId);
             List<SecModuleDetails> list = dt.DataTableToList<SecModuleDetails>();
             return Json(list, JsonRequestBehavior.AllowGet);
        
        }
        [HttpPost]
        public ActionResult SaveModule(SecModule secModule)
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            secModule.Status = true;
            secModule.CreatedBy = Convert.ToInt32(Session["userId"]);
            secModule.CreatedDate = DateTime.Now;

            Operation objOperation = new Operation { Success = false };
            if (ModelState.IsValid)
            {
                if (secModule.Id == 0)
                {
                    objOperation = _secModuleService.SaveSecModule(secModule);
                }
                else
                {
                    objOperation = _secModuleService.UpdateSecModule(secModule);
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

    }
}
