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
    public class ChartOfProductController : Controller
    {
        private IChartOfProductService _ChartOfProductService;
        private IProductPriceService _ProductPriceService;
        int userId = 0;
        public ChartOfProductController()
        {          
            var dbfactory = new DatabaseFactory();
            _ChartOfProductService = new ChartOfProductService(new ChartOfProductRepository(dbfactory), new UnitOfWork(dbfactory));
            _ProductPriceService = new ProductPriceService(new ProductPriceRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        //
        // GET: /Sales/ChartOfProduct/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetAll(int companyId = 1)
        {
            companyId = Convert.ToInt32(Session["companyId"]);
            var list = _ChartOfProductService.GetAll(companyId).Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                Code = p.Code,
                IsProduct = p.IsProduct,
                NoCredit = p.NoCredit,
                SlsProductId = p.SlsProductId,
                Level = p.Level,
                Description = p.Description,
                SecCompanyId = p.SecCompanyId,
                CreatedBy=p.CreatedBy,
                ModifiedBy=p.ModifiedBy
            }).ToList();
           
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public SlsProduct GetById(int Id)
        {
            SlsProduct list = _ChartOfProductService.GetById(Id);
            return list;
        }

        public ActionResult GetProducts(int companyId = 1)
        {
            var list = _ChartOfProductService.GetProducts(companyId).Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                Code = p.Code,
                IsProduct = p.IsProduct,
                NoCredit = p.NoCredit,
                SlsProductId = p.SlsProductId,
                Level = p.Level,
                Description = p.Description,
                SecCompanyId = p.SecCompanyId,
                CreatedBy = p.CreatedBy,
                ModifiedBy = p.ModifiedBy
            }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
           
        }

        public ActionResult GetProductsConfigured(int companyId = 1)
        {
            IList<int> productsConfigured = _ProductPriceService.GetAllProductPrices().Select(i=>i.SlsProductId).Distinct().ToList();
            var list = _ChartOfProductService.GetProducts(companyId).Where(i=> productsConfigured.Contains(i.Id) && i.IsProduct).Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                Code = p.Code,
                IsProduct = p.IsProduct,
                NoCredit = p.NoCredit,
                SlsProductId = p.SlsProductId,
                Level = p.Level,
                Description = p.Description,
                SecCompanyId = p.SecCompanyId,
                CreatedBy = p.CreatedBy,
                ModifiedBy = p.ModifiedBy
            }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetProductsByReqId(int requisitionId, int companyId = 1)
        {
            var list = _ChartOfProductService.GetProductsByReqId(requisitionId,companyId).Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                Code = p.Code,
                IsProduct = p.IsProduct,
                NoCredit = p.NoCredit,
                SlsProductId = p.SlsProductId,
                Level = p.Level,
                Description = p.Description,
                SecCompanyId = p.SecCompanyId,
                CreatedBy = p.CreatedBy,
                ModifiedBy = p.ModifiedBy
            }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetCategories(int companyId = 1)
        {
            var list = _ChartOfProductService.GetCategories(companyId).Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                Code = p.Code,
                IsProduct = p.IsProduct,
                NoCredit = p.NoCredit,
                SlsProductId = p.SlsProductId,
                Level = p.Level,
                Description = p.Description,
                SecCompanyId = p.SecCompanyId,
                CreatedBy = p.CreatedBy,
                ModifiedBy = p.ModifiedBy
            }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRootCategories(int companyId = 1)
        {
            var list = _ChartOfProductService.GetAll(companyId).Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                Code = p.Code,
                IsProduct = p.IsProduct,
                NoCredit = p.NoCredit,
                SlsProductId = p.SlsProductId,
                Level = p.Level,
                Description = p.Description,
                SecCompanyId = p.SecCompanyId,
                CreatedBy = p.CreatedBy,
                ModifiedBy = p.ModifiedBy
            }).Where(p => p.Level==0).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubCategories(int categoryId, int companyId = 1)
        {
            var list = _ChartOfProductService.GetSubCategories(categoryId,companyId).Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                Code = p.Code,
                IsProduct = p.IsProduct,
                NoCredit = p.NoCredit,
                SlsProductId = p.SlsProductId,
                Level = p.Level,
                Description = p.Description,
                SecCompanyId = p.SecCompanyId,
                CreatedBy = p.CreatedBy,
                ModifiedBy = p.ModifiedBy
            }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBySlsProductId(int categoryId)
        {
            var list = _ChartOfProductService.GetBySlsProductId(categoryId).Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                Code = p.Code,
                IsProduct = p.IsProduct,
                NoCredit = p.NoCredit,
                SlsProductId = p.SlsProductId,
                Level = p.Level,
                Description = p.Description,
                SecCompanyId = p.SecCompanyId,
                CreatedBy = p.CreatedBy,
                ModifiedBy = p.ModifiedBy
            }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AuthorizeUser(Permission = PermissionEnum.Add, ResourceTag = "ChartofProductIndex")]
        public ActionResult Save(ChartOfProductViewModel objChartOfProductViewModel, int companyId = 1)
        {
            Operation objOperation = new Operation();
            userId = Convert.ToInt32(Session["userId"]);

            if (ModelState.IsValid)
            {
                SlsProduct objSlsProduct = new SlsProduct
                {
                    Id = objChartOfProductViewModel.Id,
                    Name = objChartOfProductViewModel.Name,
                    Code = objChartOfProductViewModel.Code,
                    IsProduct = objChartOfProductViewModel.IsProduct,
                    NoCredit = objChartOfProductViewModel.NoCredit,
                    SlsProductId = objChartOfProductViewModel.SlsProductId,
                    Level = objChartOfProductViewModel.Level,
                    Description = objChartOfProductViewModel.Description,
                    SecCompanyId = companyId,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = userId,
                    ModifiedDate = DateTime.Now
                };
                if (objSlsProduct.Id == 0)
                {
                    objOperation = _ChartOfProductService.Save(objSlsProduct);
                }
                else
                {
                    objOperation = _ChartOfProductService.Update(objSlsProduct);
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Delete(int Id = 0)
        {
            Operation objOperation = null;

            if (Id != 0)
            {
                SlsProduct obj = _ChartOfProductService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _ChartOfProductService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }





    }
}
