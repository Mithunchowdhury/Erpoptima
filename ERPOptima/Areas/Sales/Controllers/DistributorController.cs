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
    public class DistributorController : Controller
    {
        //
        // GET: /Sales/Distributor/
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }
        private IDistributorService _distributorService;
        public DistributorController()
        {
            var dbfactory = new DatabaseFactory();
            _distributorService = new DistributorService(new DistributorRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        #region Distributor

        [HttpGet]        
        public ActionResult GetAll(IList<int> thanaIdList = null, int filter = 0)
        {            
            if (filter > 0)
            {
                if (thanaIdList == null)
                    thanaIdList = new List<int>();

                var result = _distributorService.GetAll().Where(x => thanaIdList.Contains(x.SlsThanaId));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = _distributorService.GetAll();
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            //var records = list.Select(t => new
            //{
            //    Id = t.Id,
            //    SlsThanaId = t.SlsThanaId,                
            //    Code = t.Code,
            //    Name = t.Name,
            //    Description = t.Description,
            //    HeadOfficeId = t.HeadOfficeId,
            //    Address = t.Address,
            //    Phone = t.Phone,                
            //    ResponsiblePerson = t.ResponsiblePerson,
            //    BankName = t.BankName,
            //    BankAccount = t.BankAccount,
            //    CreditLimit = t.CreditLimit,
            //    RateOfCommission = t.RateOfCommission,
            //    SalesTarget = t.SalesTarget,
            //    Remarks = t.Remarks,
            //    SecCompanyId = t.SecCompanyId,
            //    IsAllCompany = t.IsAllCompany,
            //    CreatedBy = t.CreatedBy,
            //    CreatedDate = t.CreatedDate,
            //    ModifiedBy = t.ModifiedBy,
            //    ModifiedDate = t.ModifiedDate
            //});

            
        }

        [HttpPost]
        public ActionResult Save(SlsDistributor distributor)
        {
            int userId = Convert.ToInt32(Session["userId"]);

            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (distributor.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        int companyId = Convert.ToInt32(Session["companyId"]);
                        distributor.SecCompanyId = companyId;
                        distributor.CreatedBy = userId;
                        distributor.CreatedDate = DateTime.Now.Date;
                        objOperation = _distributorService.Save(distributor);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        int companyId = Convert.ToInt32(Session["companyId"]);
                        distributor.SecCompanyId = companyId;
                        distributor.ModifiedBy = userId;
                        distributor.ModifiedDate = DateTime.Now.Date;
                        objOperation = _distributorService.Update(distributor);
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
                SlsDistributor obj = _distributorService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _distributorService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [AllowAnonymous]
        public JsonResult IsCodeAvailable(string distCode)
        {
            var list = _distributorService.GetAll();
            bool status = true;

            if (distCode.Trim().Length > 0 &&
                list != null && list.Count() > 0)
            {
                var existobj = list.Where(i => i.Code == distCode).FirstOrDefault();
                if (existobj != null && existobj.Id > 0)
                    status = false;
            }
            return Json(new { result = status });
        }

        [AllowAnonymous]
        public JsonResult IsNameAvailable(string distName)
        {
            var list = _distributorService.GetAll();
            bool status = true;

            if (distName.Trim().Length > 0 &&
                list != null && list.Count() > 0)
            {
                var existobj = list.Where(i => i.Name == distName).FirstOrDefault();
                if (existobj != null && existobj.Id > 0)
                    status = false;
            }
            return Json(new { result = status });
        }
        #endregion
        [HttpGet]
        public ActionResult GetById(int distId)
        {
            var distributor = _distributorService.GetAll().Where(i => i.Id == distId).First();
            return Json(distributor, JsonRequestBehavior.AllowGet);
        }
    }
}
