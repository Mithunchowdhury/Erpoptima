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
    public class DealerController : Controller
    {
        //
        // GET: /Sales/Dealer/
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }

        private IDealerService _dealerService;
        public DealerController()
        {
            var dbfactory = new DatabaseFactory();
            _dealerService = new DealerService(new DealerRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        #region Dealer
      
        [HttpGet]
        public ActionResult GetAll()
        {
            var list = _dealerService.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

     
        //[HttpGet]
        //public ActionResult GetAll(IList<int> thanaIdList = null, int filter = 0)
        //{
        //    if (filter > 0)
        //    {
        //        if (thanaIdList == null)
        //            thanaIdList = new List<int>();

        //        var list = _dealerService.GetAll().Where(x => thanaIdList.Contains(x.SlsThanaId)).ToList();
        //        return Json(list, JsonRequestBehavior.AllowGet);
        //    }

        //    var listall = _dealerService.GetAll();
        //    return Json(listall, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public ActionResult Save(SlsDealer dealer)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (dealer.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        int companyId = Convert.ToInt32(Session["companyId"]);
                        dealer.SecCompanyId = companyId;
                        dealer.CreatedBy = userId;
                        dealer.CreatedDate = DateTime.Now.Date;
                        objOperation = _dealerService.Save(dealer);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        dealer.ModifiedBy = userId;
                        dealer.ModifiedDate = DateTime.Now.Date;
                        objOperation = _dealerService.Update(dealer);
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
                SlsDealer obj = _dealerService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _dealerService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [AllowAnonymous]
        public JsonResult IsCodeAvailable(string dealerCode)
        {
            var list = _dealerService.GetAll();
            bool status = true;

            if (dealerCode.Trim().Length > 0 &&
                list != null && list.Count() > 0)
            {
                var existobj = list.Where(i => i.Code == dealerCode).FirstOrDefault();
                if (existobj != null && existobj.Id > 0)
                    status = false;
            }
            return Json(new { result = status });
        }

        [AllowAnonymous]
        public JsonResult IsNameAvailable(string dealerName)
        {
            var list = _dealerService.GetAll();
            bool status = true;

            if (dealerName.Trim().Length > 0 &&
                list != null && list.Count() > 0)
            {
                var existobj = list.Where(i => i.Name == dealerName).FirstOrDefault();
                if (existobj != null && existobj.Id > 0)
                    status = false;
            }
            return Json(new { result = status });
        }
        #endregion

        [HttpGet]
        public ActionResult GetById(int distId)
        {
            var Dealer = _dealerService.GetAll().Where(i => i.Id == distId).First();
            return Json(Dealer, JsonRequestBehavior.AllowGet);
        }

    }
}
