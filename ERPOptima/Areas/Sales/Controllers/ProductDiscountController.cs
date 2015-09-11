using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Service.Sales;
using ERPOptima.Web.Filters;
using Optima.Areas.Sales.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class ProductDiscountController : Controller
    {

        private IProductDiscountService _ProductDiscountService;
        private IUnitOfMeasurementService _unitOfMeasurementService;
        public ProductDiscountController()
        {
            var dbfactory = new DatabaseFactory();
            _ProductDiscountService = new ProductDiscountService(new ProductDiscountRepository(dbfactory), new UnitOfWork(dbfactory));
            _unitOfMeasurementService = new UnitOfMeasurementService(new UnitOfMeasurementRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        //
        // GET: /Sales/ProductDiscount/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetProductDiscount(int categoryId, int regionId, int companyId = 1)
        {

            if (ModelState.IsValid)
            {
                List<ProductDiscountViewModel> list = null;

                DataTable dt = _ProductDiscountService.GetProductDiscount(categoryId, regionId, companyId);
                if (dt != null)
                {
                    list = new List<ProductDiscountViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((ProductDiscountViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(ProductDiscountViewModel)));
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
        public ActionResult Save(List<SlsProductDiscount> discountList, List<SlsProductDiscount> removeDiscountList)
        {
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid && discountList != null)
            {
                int Id = _ProductDiscountService.GetLastId();
                foreach (var item in discountList)
                {
                    SlsProductDiscount objSlsProductDiscount = _ProductDiscountService.GetById(item.Id);                  
                    if (objSlsProductDiscount != null)
                    {                                             
                        objSlsProductDiscount.SlsRegionId = item.SlsRegionId;
                        objSlsProductDiscount.SlsProuctId = item.SlsProuctId;
                        objSlsProductDiscount.Discount = item.Discount;
                        objSlsProductDiscount.Date = DateTime.Now;
                        objSlsProductDiscount.Remarks = item.Remarks;
                        _ProductDiscountService.Update(objSlsProductDiscount);
                    }
                    else
                    {
                        objSlsProductDiscount = new SlsProductDiscount();
                        objSlsProductDiscount.Id = Id;
                        objSlsProductDiscount.SlsRegionId = item.SlsRegionId;
                        objSlsProductDiscount.SlsProuctId = item.SlsProuctId;
                        objSlsProductDiscount.Discount = item.Discount;
                        objSlsProductDiscount.Date = DateTime.Now;
                        objSlsProductDiscount.Remarks = item.Remarks;
                        _ProductDiscountService.Add(objSlsProductDiscount);
                        Id = Id + 1;
                    }

                }

                //Delete modified product discount
                if (removeDiscountList != null && removeDiscountList.Count > 0)
                {
                    foreach (var item in removeDiscountList)
                    {
                        SlsProductDiscount objSlsProductDiscount = _ProductDiscountService.GetById(item.Id);
                        if (objSlsProductDiscount != null)
                        {
                            _ProductDiscountService.Delete(objSlsProductDiscount);
                        }
                        else
                        {
                            //While removing existing product discount, this is not possible.
                        }
                    }
                }
                objOperation = _ProductDiscountService.Commit();
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }










    }
}
