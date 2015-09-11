using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Service.Sales;
using ERPOptima.Web.Filters;
using Optima.Areas.Sales.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class ProductUnitsController : Controller
    {
         private IProductUnitService _ProductUnitService;
         private IUnitOfMeasurementService _unitOfMeasurementService;
         private IProductPriceService _ProductPriceService;

         public ProductUnitsController()
        {
            var dbfactory = new DatabaseFactory();
            _ProductUnitService = new ProductUnitService(new ProductUnitRepository(dbfactory), new UnitOfWork(dbfactory));
            _unitOfMeasurementService = new UnitOfMeasurementService(new UnitOfMeasurementRepository(dbfactory), new UnitOfWork(dbfactory));
            _ProductPriceService = new ProductPriceService(new ProductPriceRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        //
        // GET: /Sales/ProductUnits/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }

         [HttpGet]
        public ActionResult GetSlsProductUnitsBySlsProductId(int productId)
        {

            if (ModelState.IsValid)
            {
                List<SlsProductUnit> list = _ProductUnitService.GetSlsProductUnitsBySlsProductId(productId).ToList();

                List<SlsProductUnitViewModel> viewModelList = new List<SlsProductUnitViewModel>();

                SlsProductUnitViewModel objSlsProductUnitViewModel = null;

                list.ForEach(op =>
                {
                    objSlsProductUnitViewModel = new SlsProductUnitViewModel();
                    objSlsProductUnitViewModel.Id = op.Id;
                    objSlsProductUnitViewModel.SlsProductId = op.SlsProductId;
                    objSlsProductUnitViewModel.SlsUnitId = op.SlsUnitId;
                    objSlsProductUnitViewModel.Unit = op.SlsUnit.ShortName;
                    objSlsProductUnitViewModel.ParentUnitId = op.ParentUnitId;
                    if (op.ParentUnitId != null)
                    {
                        objSlsProductUnitViewModel.ParentUnit = _unitOfMeasurementService.GetById((int)op.ParentUnitId).ShortName;
                    }
                    else
                    {
                        objSlsProductUnitViewModel.ParentUnit = "";
                    }
                    objSlsProductUnitViewModel.ConversionRate = op.ConversionRate;
                    objSlsProductUnitViewModel.Remarks = op.Remarks;
                    objSlsProductUnitViewModel.CreatedBy = op.CreatedBy;
                    objSlsProductUnitViewModel.CreatedDate = op.CreatedDate;
                    objSlsProductUnitViewModel.ModifiedBy = op.ModifiedBy;
                    objSlsProductUnitViewModel.ModifiedDate = op.ModifiedDate;
                    viewModelList.Add(objSlsProductUnitViewModel);
                });
                return Json(viewModelList, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }
        

         [HttpPost]
         public ActionResult Save(SlsProductUnit pu)
         {           
             int userId = Convert.ToInt32(Session["userId"]);
             Operation objOperation = new Operation { Success = false };
             if (ModelState.IsValid)
             {
                 if (pu.Id == 0)
                 {
                     if ((bool)Session["Add"])
                     {
                         pu.CreatedBy = userId;                       
                         pu.CreatedDate = DateTime.Now;
                         objOperation = _ProductUnitService.Save(pu);
                     }
                     else { objOperation.OperationId = -1; }
                 }
                 else
                 {
                     if ((bool)Session["Edit"])
                     {                        
                         pu.ModifiedBy = userId;
                         pu.ModifiedDate = DateTime.Now;
                         objOperation = _ProductUnitService.Update(pu);
                     }
                     else { objOperation.OperationId = -2; }
                 }
             }

             return Json(objOperation, JsonRequestBehavior.DenyGet);
         }

         public ActionResult Delete(int Id)
         {
             Operation objOperation = new Operation { Success = false };
             if (Id != 0)
             {
                 SlsProductUnit obj = _ProductUnitService.GetById(Id);
                 if (obj == null)
                 {
                     objOperation.Success = false;
                     return Json(objOperation, JsonRequestBehavior.DenyGet);
                 }
                 objOperation = _ProductUnitService.Delete(obj);
             }
             return Json(objOperation, JsonRequestBehavior.DenyGet);
         }



         [HttpGet]
         public ActionResult GetByProductId(int UId)
         {
             var productUnit = _ProductUnitService.GetAll().Where(i => i.Id == UId).First();
             return Json(productUnit, JsonRequestBehavior.AllowGet);
         }



         [HttpGet]
         public ActionResult GetUnitsConfigured(int productId)
         {
             IList<int> unitsInPP = _ProductPriceService.GetAllProductPrices().Where(i=>i.SlsProductId == productId).Select(i => i.SlsUnitId).Distinct().ToList();
             IList<int> unitsConfigured = _ProductUnitService.GetAll().Where(i => i.SlsProductId == productId).Select(j => j.SlsUnitId).ToList();
             var units = _unitOfMeasurementService.GetAll().Where(i => unitsConfigured.Contains(i.Id) && unitsInPP.Contains(i.Id)).ToList();
             var result = units.Select(i => new
             {
                 Id = i.Id,
                 ShortName = i.ShortName,
                 Name = i.Name
                 

             }).Distinct().ToList();
             return Json(result, JsonRequestBehavior.AllowGet);
         }



    }
}
