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
    public class ProductController : Controller
    {
        //
        // GET: /Sales/Product/
        private IProductService _productService;

        public ProductController()
        {
            var dbfactory = new DatabaseFactory();
            _productService = new ProductService(new ProductRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }

        #region Product

        [HttpGet]
        public ActionResult GetAll()
        {

            var list = _productService.GetAll(int.Parse(Session["companyId"].ToString()));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetById(int Id)
        //{
        //    var list = _productService.GetAll().Where(i => i.Head == Id).ToList();
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}


        
        #endregion

    }
}
