using ERPOptima.Data.Accounts;
using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Accounts;
using ERPOptima.Model.Common;
using ERPOptima.Service.Accounts;
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

namespace Optima.Areas.Accounts.Controllers
{

    public class FiscalYearClosingController : Controller
    {
        //
        // GET: /Accounts/FiscalYear/
        private ICmnFinancialYearService _financialYearService;
        public FiscalYearClosingController()
        {
            var dbfactory = new DatabaseFactory();
            _financialYearService = new CmnFinancialYearService(new CmnFinancialYearRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        [AuthorizeUser]

        public ActionResult Index()
        {
            return View();
        }
        #region Fiscal Year

        //public ActionResult GetFiscalYears()
        //{
        //    int companyId = Convert.ToInt32(Session["companyId"]);
        //    IList<CmnFinancialYear> list = new List<CmnFinancialYear>();
        //    list = _financialYearService.GetCurrentFinancialYearId(); GetFinancialYearsForView(1, 1);//..Where(x => x.AnFChartOfAccountId == companyId).ToList();
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

    }
        #endregion

}