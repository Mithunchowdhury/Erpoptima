using System.Web.Mvc;

namespace Optima.Areas.Hrm
{
    public class HrmAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Hrm";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Hrm_default",
                "Hrm/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
