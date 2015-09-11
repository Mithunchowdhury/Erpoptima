using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Security.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Security;
using ERPOptima.Service.Security;
using ERPOptima.Web.Filters;
using ERPOptima.Web.Security.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Optima.Areas.Security.Controllers
{
    public class MenuController : Controller
    {
        private ISecResourceService _secResourceService;

        public MenuController()
        {
            var dbfactory = new DatabaseFactory();
            _secResourceService = new SecResourceService(new SecResourceRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        //
        // GET: /Security/Menu/
        [AuthorizeUser]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMenuByModuleId(int secModuleId)
        {
            var list = _secResourceService.GetByModuleId(secModuleId).Select(sr => new
            {
                Id = sr.Id,
                DisplayName = sr.DisplayName,
                SerialNo = sr.SerialNo,
                SecResourcesId = sr.SecResourcesId

            }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateMenu(List<SecMenuViewModel> objMenus)
        {
            Operation operation = new Operation { Success = false };
            if (ModelState.IsValid)
            {
                int serial = 1;
                Action<SecMenuViewModel,int?> f = null;
                f = (sm,value) =>
                {
                    SecResource sr = _secResourceService.GetById(sm.Id);
                    sr.SerialNo = serial;
                    _secResourceService.Update(sr);
                    serial++;
                    if (sm.items != null)
                    {
                       
                        foreach (SecMenuViewModel inneritem in sm.items)
                        {
                            f(inneritem, sm.Id);
                        }
                    }
                };

                foreach (SecMenuViewModel item in objMenus)
                {
                    
                    f(item,null);
                }

                operation = _secResourceService.Commit();
            }
            return Json(operation, JsonRequestBehavior.AllowGet);
        }
    }
}