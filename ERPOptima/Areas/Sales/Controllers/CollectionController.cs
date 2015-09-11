using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Model.Security;
using ERPOptima.Model.ViewModel;
using ERPOptima.Service.Hrm;
using ERPOptima.Service.Sales;
using ERPOptima.Service.Security;
using ERPOptima.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class CollectionController : Controller
    {
        private ICollectionEntryService _collectionEntryService;
        private ISecCompanyService _SecCompanyService;
        private IOfficeService _officeService;
        private IHrmEmployeeService _hrmEmployeeService;
         public CollectionController()
        {
            var dbfactory = new DatabaseFactory();
            _collectionEntryService = new CollectionEntryService(new CollectionEntryRepository(dbfactory), new UnitOfWork(dbfactory));
            _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
            _officeService = new OfficeService(new OfficeRepository(dbfactory), new UnitOfWork(dbfactory));
            _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        //
        // GET: /Sales/Collection/
       

         [HttpGet]
         public ActionResult GetAutoNumber(int companyId, int employeeId)
         {
             SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
             SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
             var autoNumber = _collectionEntryService.getAutoNumber(companyId, objCmnCompany.Prefix, office.Code);
             return Json(new { Refno = autoNumber }, JsonRequestBehavior.AllowGet);
         }

        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Report()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Target()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Entry()
        {
            return View();
        }
     
        [HttpGet]
        public ActionResult GetAll()
        {
            var list = _collectionEntryService.GetAll();

            list = list.OrderByDescending(i => i.CreatedDate).ToList(); //order by createdDate

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //[HttpGet]
        //public ActionResult GetAllBycollectedFromId(int collectedFromId)
        //{
        //    var list = _collectionEntryService.GetAll(collectedFromId);
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //    //return null;
        //}

        [HttpPost]
        public ActionResult Save(SlsCollectionViewModel collectionEntry)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (collectionEntry.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        collectionEntry.CreatedBy = userId;

                        collectionEntry.CreatedDate = DateTime.Now.Date;

                        objOperation = _collectionEntryService.Save(collectionEntry);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        collectionEntry.ModifiedBy = userId;
                        collectionEntry.ModifiedDate = DateTime.Now.Date;
                        objOperation = _collectionEntryService.Update(collectionEntry);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }


    }
}
