using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Service.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Inventory.Controllers
{
    public class StoreController : Controller
    {
        private IStoreService _StoreService;
        public StoreController()
        {
            var dbfactory = new DatabaseFactory();
            _StoreService = new StoreService(new InvStoreRepository(dbfactory), new UnitOfWork(dbfactory));
            
        }
        //
        // GET: /Inventory/Store/

        public ActionResult Index()
        {
            return View();
        }




        public ActionResult GetAll()
        {
            var list = _StoreService.GetAll().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStores()
        {
            var list = _StoreService.GetAll().Select(store => new { Id = store.Id, Name = store.Name }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
       


        public ActionResult GetStoresForOffice(int officeId)
        {
            var list = _StoreService.GetStoresForOffice(officeId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }




    }
}
