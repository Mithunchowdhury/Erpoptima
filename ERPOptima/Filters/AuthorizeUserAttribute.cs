using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Security.Repository;
using ERPOptima.Service.Security;
using System;
using System.Data;
using System.Web;
using System.Web.Mvc;

namespace ERPOptima.Web.Filters
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        // Custom property
        private PermissionEnum permission;
        private bool ispermissionset=false;
        public PermissionEnum Permission {
            get {
                return permission;
            } 
            set { 
                ispermissionset=true;
                permission = value;
            } }

        public string ResourceTag { get; set; }

        private ISecResourceService _secResourceService;

        public AuthorizeUserAttribute()
        {
            var dbFactory = new DatabaseFactory();
            _secResourceService = new SecResourceService(new SecResourceRepository(dbFactory), new UnitOfWork(dbFactory));
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isAuthorized = base.AuthorizeCore(httpContext);
            
            int userId = Convert.ToInt32(httpContext.Session["userId"]);
            int moduleId = Convert.ToInt32(httpContext.Session["moduleId"]);
            if (!isAuthorized || userId == 0 || moduleId==0)
            {
                return false;
            }
            if (string.IsNullOrEmpty(ResourceTag))
            {
                string controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
                string actionName = httpContext.Request.RequestContext.RouteData.Values["action"].ToString();
                ResourceTag = controllerName + actionName;
            }
            DataTable dt = _secResourceService.GetResourcePermissionByUserId(ResourceTag, userId, moduleId);
            if (dt.Rows.Count > 0)
            {
                if (ispermissionset)
                {
                    if (!(bool)dt.Rows[0][Permission.ToString()])
                    {
                        
                        return false;
                    }
                }
                
            }
            else
            {
                isAuthorized = false;
            }
            return isAuthorized;
        }
    }
}