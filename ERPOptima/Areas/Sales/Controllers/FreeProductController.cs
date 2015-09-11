using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Model.ViewModel;
using ERPOptima.Service.Sales;
using ERPOptima.Web.Filters;
using Optima.Areas.Sales.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class FreeProductController : Controller
    {
        private IFreeProductService _FreeProductService;

        public FreeProductController()
        {
            var dbfactory = new DatabaseFactory();
            _FreeProductService = new FreeProductService(new FreeProductRepository(dbfactory),
                new ChartOfProductRepository(dbfactory), new UnitOfMeasurementRepository(dbfactory), new UnitOfWork(dbfactory));            
                      
        }
        //
        // GET: /Sales/FreeProduct/
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            int companyId = Convert.ToInt32(Session["CompanyId"]); 

            var list = _FreeProductService.GetAll(companyId);
            list = list.OrderBy(i => i.SlsProductName).ToList();  //Order by product name

            var result = list.Select(i => new SlsFreeProductsViewModel()
            {
                Id = i.Id,
                SlsProductId = i.SlsProductId,
                StartDate = i.StartDate,
                EndDate = i.EndDate,
                MeasurementQuantity = i.MeasurementQuantity,
                SlsUnitId = i.SlsUnitId,
                FreeQuantity = i.FreeQuantity,
                FreeUnitId = i.FreeUnitId,
                Remarks = i.Remarks,
                SecCompnayId = i.SecCompnayId,
                CreatedBy = i.CreatedBy,
                CreatedDate = i.CreatedDate,
                ModifiedBy = i.ModifiedBy,
                ModifiedDate = i.ModifiedDate,

                SlsProductName = i.SlsProductName,
                SlsUnitName = i.SlsUnitName,
                FreeUnitName = i.FreeUnitName
            }).Distinct().ToList();

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Save(SlsFreeProduct obj)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };
            int companyId = Convert.ToInt32(Session["companyId"]);

            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                                         
                        obj.CreatedBy = userId;
                        obj.CreatedDate = DateTime.Now.Date;
                        obj.SecCompnayId = companyId;
                        objOperation = _FreeProductService.Save(obj);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        obj.ModifiedBy = userId;
                        obj.ModifiedDate = DateTime.Now.Date;
                        obj.SecCompnayId = companyId;
                        objOperation = _FreeProductService.Update(obj);
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
                SlsFreeProduct obj = _FreeProductService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _FreeProductService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
        [HttpGet]
        public ActionResult GetAllFreeProducts(int companyId)
        {
            string cmpny = Session["CompanyId"].ToString();
            var dbfactory = new DatabaseFactory();
            ChartOfProductService _ChartOfProductService = new ChartOfProductService(new ChartOfProductRepository(dbfactory), new UnitOfWork(dbfactory));
            var productlist = _ChartOfProductService.GetProducts(int.Parse(cmpny));

            var list = _FreeProductService.GetAll(int.Parse(cmpny)).ToList();

            List<SlsProduct> records = new List<SlsProduct>();
            foreach (SlsFreeProductsViewModel record in list)
            {
                var rec = productlist.Where(t => t.Id == record.SlsProductId).FirstOrDefault();
                if (rec != null)
                {
                    var isExist = records.Where(t => t.Id == rec.Id).FirstOrDefault();
                    if (isExist == null)
                    {
                        records.Add(rec);
                    }
                }
            }


            return Json(records, JsonRequestBehavior.AllowGet);

        }

    }
}
