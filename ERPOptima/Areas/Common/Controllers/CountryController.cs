using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Helper;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Common;
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

namespace Optima.Areas.Common.Controllers
{
    public class CountryController : Controller
    {
        //
        // GET: /Common/Business/
        private ICmnCountryService _ccService;

        public CountryController()
        {
            var dbfactory = new DatabaseFactory();
            _ccService = new CmnCountryService(new CmnCountryRepository(dbfactory), new UnitOfWork(dbfactory));
           
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }
        #region Country

        [HttpGet]
        public ActionResult GetCmnCountries()
        {
            var list = _ccService.GetCmnCountries().ToList();         
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCmnCountriesIdAndName()
        {
            var list = _ccService.GetCmnCountries().Select(ac => new
            {
                Id = ac.Id,
                Name = ac.Name
            });

            return Json(list, JsonRequestBehavior.AllowGet);
        }
           
        [HttpPost]
        public ActionResult SaveCmnCountry(CmnCountry country)
        {
           Operation objOperation = new Operation { Success = false };
            if (ModelState.IsValid)
            {
                int userId = Convert.ToInt32(Session["userId"].ToString());
                country.CreatedBy = userId;

                if (country.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        objOperation = _ccService.SaveCmnCountry(country);
                    }
                    else { objOperation.OperationId = -1; }
                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        country.ModifiedBy = userId;
                        objOperation = _ccService.UpdateCmnCountry(country);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeleteCmnCountry(int Id)
        {
           Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                CmnCountry obj = _ccService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _ccService.DeleteCmnCountry(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }        

        #endregion
    }
}
