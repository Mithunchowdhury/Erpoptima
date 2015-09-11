using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Sales/Index/

        public ActionResult Index()
        {
            Session["moduleId"] = 5;
            return View();
        }
      

    }
}
