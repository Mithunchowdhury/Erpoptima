using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Service.Sales;
using ERPOptima.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class RetailerController : Controller
    {
        //
        // GET: /Sales/Retailer/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }

        private IRetailerService _retailerService;
        public RetailerController()
        {
            var dbfactory = new DatabaseFactory();
            _retailerService = new RetailerService(new RetailerRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        #region Retailer

        [HttpGet]
        public ActionResult GetAll()
        {
            var list = _retailerService.GetAll();
            //var records = list.Select(t => new
            //{
            //    Id = t.Id,
            //    SlsDistributorId = t.SlsDistributorId,
            //    SlsOfficeId = t.SlsOfficeId,
            //    Code = t.Code,
            //    Name = t.Name,
            //    Description = t.Description,
            //    Address = t.Address,
            //    Phone = t.Phone,
            //    ResponsiblePerson = t.ResponsiblePerson,
            //    BankName = t.BankName,
            //    BankAccount = t.BankAccount,
            //    CreditLimit = t.CreditLimit,
            //    SecCompanyId = t.SecCompanyId,
            //    IsAllCompany = t.IsAllCompany,
            //    CreatedBy = t.CreatedBy,
            //    CreatedDate = t.CreatedDate,
            //    ModifiedBy = t.ModifiedBy,
            //    ModifiedDate = t.ModifiedDate
            //});
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(SlsRetailer reatiler)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (reatiler.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        int companyId = Convert.ToInt32(Session["companyId"]);
                        reatiler.SecCompanyId = companyId;
                        reatiler.CreatedBy = userId;
                        reatiler.CreatedDate = DateTime.Now.Date;
                        if (reatiler.SlsOfficeId == 0)
                        {
                            reatiler.SlsOfficeId = null;
                        }
                        if(reatiler.SlsDistributorId==0)
                        {
                            reatiler.SlsDistributorId = null;
                        }
                        objOperation = _retailerService.Save(reatiler);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        reatiler.ModifiedBy = userId;
                        reatiler.ModifiedDate = DateTime.Now.Date;
                        objOperation = _retailerService.Update(reatiler);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            Operation objOperation = new Operation { Success = false };

            if (Id != 0)
            {
                SlsRetailer obj = _retailerService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _retailerService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [AllowAnonymous]
        public JsonResult IsCodeAvailable(string retailerCode)
        {
            var list = _retailerService.GetAll();
            bool status = true;

            if (retailerCode.Trim().Length > 0 &&
                list != null && list.Count() > 0)
            {
                var existobj = list.Where(i => i.Code == retailerCode).FirstOrDefault();
                if (existobj != null && existobj.Id > 0)
                    status = false;
            }
            return Json(new { result = status });
        }

        [AllowAnonymous]
        public JsonResult IsNameAvailable(string retailerName)
        {
            var list = _retailerService.GetAll();
            bool status = true;

            if (retailerName.Trim().Length > 0 &&
                list != null && list.Count() > 0)
            {
                var existobj = list.Where(i => i.Name == retailerName).FirstOrDefault();
                if (existobj != null && existobj.Id > 0)
                    status = false;
            }
            return Json(new { result = status });
        }
        #endregion
         [HttpGet]
        public ActionResult GetById(int distId)
        {
            var Retailer = _retailerService.GetAll().Where(i => i.Id == distId).First();
            return Json(Retailer, JsonRequestBehavior.AllowGet);
        }

    }
}
