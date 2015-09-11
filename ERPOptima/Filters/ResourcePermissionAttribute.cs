using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Security.Repository;
using ERPOptima.Service.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPOptima.Web.Filters
{
    public class ResourcePermissionAttribute : ActionFilterAttribute
    {
        private ISecResourceService _secResourceService;
        public ResourcePermissionAttribute()
        {
            var dbFactory = new DatabaseFactory();
            _secResourceService = new SecResourceService(new SecResourceRepository(dbFactory),new UnitOfWork(dbFactory));
        
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                string actionName = filterContext.ActionDescriptor.ActionName;
                string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                string tag = controllerName + actionName;

                int userId =  Convert.ToInt32(filterContext.HttpContext.Session["userId"]);
                int moduleId = Convert.ToInt32(filterContext.HttpContext.Session["moduleId"]);
                DataTable dt = _secResourceService.GetResourcePermissionByUserId(tag, userId, moduleId);
                if (dt.Rows.Count>0)
                {
                    filterContext.Controller.ViewData.Add("ReadOnly", dt.Rows[0][0]);
                    filterContext.Controller.ViewData.Add("Add", dt.Rows[0][1]);
                    filterContext.Controller.ViewData.Add("Edit", dt.Rows[0][2]);
                    filterContext.Controller.ViewData.Add("Delete", dt.Rows[0][3]);
                    filterContext.Controller.ViewData.Add("Print", dt.Rows[0][4]);
                    HttpContext.Current.Session["ReadOnly"] = dt.Rows[0][0];
                    HttpContext.Current.Session["Add"] = dt.Rows[0][1];
                    HttpContext.Current.Session["Edit"] = dt.Rows[0][2];
                    HttpContext.Current.Session["Delete"] = dt.Rows[0][3];
                    HttpContext.Current.Session["Print"] = dt.Rows[0][4];
                }              
            }        
        }
    }
}