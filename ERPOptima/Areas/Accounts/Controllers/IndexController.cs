using ERPOptima.Web.Filters;
using Optima.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Optima.Areas.Accounts.Controllers
{
    
    public class IndexController : Controller
    {
        //
        // GET: /Accounts/Index/

        public ActionResult Index()
        {
            Session["moduleId"] = 2; // do your stuff

            Session["financialYear"] = new FinancialYearHelper().SetFinancialYearId(Convert.ToInt32(Session["companyId"].ToString()), Convert.ToInt32(Session["moduleId"].ToString()));
            return RedirectToAction("Index", "Dashboard", new { area="Accounts"});
        }



    }
}
