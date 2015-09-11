using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Service.Sales;
using ERPOptima.Web.Filters;
using Optima.Areas.Sales.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Optima.Areas.Sales.Controllers
{
    public class ProductPriceController : Controller
    {
        //
        // GET: /Sales/ProductPrice/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }

        private IProductPriceService _productPriceService;

        public ProductPriceController()
        {
            var dbfactory = new DatabaseFactory();
            _productPriceService = new ProductPriceService(new ProductPriceRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        #region ProductPrices

        [HttpGet]
        public ActionResult GetAll(int productId = 0, int unitId = 0)
        {
            IList<ProductPriceViewModel> productPrices = null;
            DataTable dt = _productPriceService.GetAll(productId,unitId);
            if (dt != null)
            {
                productPrices = new List<ProductPriceViewModel>();
                foreach (DataRow row in dt.Rows)
                {
                    productPrices.Add((ProductPriceViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(ProductPriceViewModel)));
                }
            }
            productPrices = productPrices.OrderBy(t => t.Code).ToList();
            productPrices = productPrices.OrderByDescending(t => t.DeclarationDate).ToList();  //order by declarationDate
            return Json(productPrices, JsonRequestBehavior.AllowGet);
        //    //var list = _productPriceService.GetAll();
        //    //return Json(list, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetByRegionId(int regionId)
        //{
        //    var list = _officeService.GetAll().Where(i=>i.SlsRegionId == regionId).ToList();
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult GetOfficeByEmployee(int? employeeId,int regionId) 
        //{
        //    Collection<AreaConfigurationViewModel> areaConfigurations = null;
        //    DataTable dt = _officeService.GetOfficeByEmployee(employeeId,regionId);
        //    if (dt != null)
        //    {
        //        areaConfigurations = new Collection<AreaConfigurationViewModel>();
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            areaConfigurations.Add((AreaConfigurationViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(AreaConfigurationViewModel)));
        //        }
        //    }

        //    return Json(areaConfigurations, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public ActionResult Save(SlsProductPrice productPrice)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                if (productPrice.Id == 0)   
                {
                    if ((bool)Session["Add"])
                    {                        
                        productPrice.CreatedBy = userId;
                        productPrice.CreatedDate = DateTime.Now.Date;
                        productPrice.DeclarationDate = DateTime.Now.Date;                        
                        objOperation = _productPriceService.Save(productPrice);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        productPrice.ModifiedBy = userId;
                        productPrice.ModifiedDate = DateTime.Now.Date;
                        objOperation = _productPriceService.Update(productPrice);
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
                SlsProductPrice obj = _productPriceService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _productPriceService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

       // [AllowAnonymous]
       // public JsonResult IsOfficeCodeAvailable(string officeCode)
       // {
        //    var list = _officeService.GetAll();
        //    bool status = true;
            
        //    if (officeCode.Trim().Length > 0 &&
        //        list != null && list.Count() > 0)
        //    {
        //        var existobj = list.Where(i => i.Code == officeCode).FirstOrDefault();
        //        if (existobj != null && existobj.Id > 0)
        //            status = false;
        //    }
        //    return Json(new { result = status});
       // }

        //public ActionResult RegionIdOfThisOffice(int officeId)
        //{
        //    var list = _officeService.GetAll().Where(i => i.Id == officeId).ToList();
        //    if (list == null || list.Count() == 0)
        //        return Json(new { result = 0, status = false }, JsonRequestBehavior.AllowGet);
        //    var regionId = list.First().SlsRegionId;
        //    return Json(new { result = regionId, status = true }, JsonRequestBehavior.AllowGet);
        //}

        #endregion

    }
}
