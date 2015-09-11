using ERPOptima.Data.Common.Repository;
using ERPOptima.Lib.Utilities;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Service.Common;
using Optima.Areas.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ERPOptima.Model.Common;
using ERPOptima.Lib.Model;

namespace Optima.Areas.Common.Controllers
{
    public class CalendarSetupController : Controller
    {
        //
        public CalendarSetupController()
        {
        }
        public ActionResult Index()
        {
            return View();
        }
       
    }
}