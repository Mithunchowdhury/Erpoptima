using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Inventory;
using ERPOptima.Service.Inventory;
using ERPOptima.Web.Filters;
using Optima.Areas.Inventory.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPOptima.Lib.Utilities;
using ERPOptima.Service.Sales;
using ERPOptima.Model.Sales;
using ERPOptima.Data.Sales.Repository;


namespace Optima.Areas.Inventory.Controllers
{
    public class StoreOpeningController : Controller
    {

        private IStoreOpeningService _StoreOpeningService;
        private IUnitOfMeasurementService _unitOfMeasurementService;
        public StoreOpeningController()
        {
            var dbfactory = new DatabaseFactory();
            _StoreOpeningService = new StoreOpeningService(new InvStoreOpeningRepository(dbfactory), new UnitOfWork(dbfactory));
            _unitOfMeasurementService = new UnitOfMeasurementService(new UnitOfMeasurementRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        //
        // GET: /Inventory/StoreOpening/
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }


       

        public ActionResult GetInvStoreOpeningByInvStoreId(int storeId, int companyId = 1)
        {
            
            if (ModelState.IsValid)
            {
                List<InvStoreOpeningViewModel> list = null;
                    
                  DataTable dt=_StoreOpeningService.GetInvStoreOpeningByInvStoreId(storeId, companyId);
                  if (dt != null)
                  {
                      list = new List<InvStoreOpeningViewModel>();
                      foreach (DataRow row in dt.Rows)
                      {
                          list.Add((InvStoreOpeningViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(InvStoreOpeningViewModel)));
                      }
                  }

               
                return Json(list, JsonRequestBehavior.AllowGet);                               
                
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
           
        }



        [HttpPost]
        public ActionResult Update(List<InvStoreOpeningViewModel> viewModelList)
        {
            Operation objOperation = new Operation { Success = false };
            int userId = Convert.ToInt32(Session["userId"]);
            if (ModelState.IsValid && viewModelList != null)
            {
                foreach (var item in viewModelList)
                {
                    InvStoreOpening objInvStoreOpening = _StoreOpeningService.GetById(item.Id);
                    SlsUnit objSlsUnit = _unitOfMeasurementService.GetByName(item.Unit);
                    if (objInvStoreOpening != null)
                    {
                        objInvStoreOpening.Quantity = item.Quantity;
                        objInvStoreOpening.SlsUnitId = objSlsUnit.Id;
                        objInvStoreOpening.ModifiedBy = userId;
                        objInvStoreOpening.ModifiedDate = DateTime.Now;
                        _StoreOpeningService.Update(objInvStoreOpening);
                    }
                    else
                    {
                        objInvStoreOpening = new InvStoreOpening();
                        objInvStoreOpening.InvStoreId = item.InvStoreId;
                        objInvStoreOpening.SlsProductId = item.SlsProductId;
                        objInvStoreOpening.Quantity = item.Quantity;
                        objInvStoreOpening.SlsUnitId = objSlsUnit.Id;
                        objInvStoreOpening.CreatedBy = userId;
                        objInvStoreOpening.CreatedDate = DateTime.Now;
                        _StoreOpeningService.Add(objInvStoreOpening);
                    }

                }

                objOperation = _StoreOpeningService.Commit();
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }





    }
}
