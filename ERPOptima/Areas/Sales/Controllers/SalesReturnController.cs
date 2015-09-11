using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Inventory;
using ERPOptima.Model.Sales;
using ERPOptima.Model.Security;
using ERPOptima.Service.Hrm;
using ERPOptima.Service.Inventory;
using ERPOptima.Service.Sales;
using ERPOptima.Service.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class SalesReturnController : Controller
    {
        private ISalesReturnService _SalesReturnService;
        private ISecCompanyService _SecCompanyService;
        private IOfficeService _officeService;
        private IHrmEmployeeService _hrmEmployeeService;
        public SalesReturnController()
        {
            var dbfactory = new DatabaseFactory();
            _SalesReturnService = new SalesReturnService(new SalesReturnRepository(dbfactory),
                new SalesReturnDetailRepository(dbfactory), new StockInRepository(dbfactory), new UnitOfWork(dbfactory));
            _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
            _officeService = new OfficeService(new OfficeRepository(dbfactory), new UnitOfWork(dbfactory));
            _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        [HttpGet]
        public ActionResult GetRefNo(int companyId, int employeeId)
        {
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
            string refNo = _SalesReturnService.GetRefNo(companyId, objCmnCompany.Prefix, office.Code);            
            return Json(new { result = refNo }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            var list = _SalesReturnService.GetAllVM(companyId);
            list = list.OrderByDescending(i => i.CreatedDate).ToList();  //order by created date
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Save(SlsSalesReturnViewModel objT)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            int companyId = Convert.ToInt32(Session["companyId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {

                //Get store id of logged in user.
                var dbfactory = new DatabaseFactory();
                IOfficeService offservice = new OfficeService(new OfficeRepository(dbfactory), new UnitOfWork(dbfactory));
                SlsOffice off = offservice.GetUserOffice(userId);
                int officeId = off.Id;
                IStoreService storeservice = new StoreService(new InvStoreRepository(dbfactory), new UnitOfWork(dbfactory));
                InvStore store = storeservice.GetStoresForOffice(officeId);
                int storeId = store.Id;


                if (objT.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        objT.CreatedBy = userId;
                        objT.CreatedDate = DateTime.Now.Date;
                        objT.SecCompanyId = companyId;
                        objOperation = _SalesReturnService.Save(objT, storeId);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        objT.ModifiedBy = userId;
                        objT.ModifiedDate = DateTime.Now.Date;
                        objOperation = _SalesReturnService.Update(objT, storeId);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
    }
}
