using ERPOptima.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class ManagementDashboardController : Controller
    {
        //
        // GET: /Sales/ManagementDashboard/
        //[AuthorizeUser]
        //[ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }

    }
}
