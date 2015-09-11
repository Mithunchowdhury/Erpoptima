using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Security.Repository;
using ERPOptima.Model.Security;
using ERPOptima.Service.Security;
using Optima.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPOptima.Lib.Utilities;
using ERPOptima.Service.Home;
using Optima.Areas.Security.ViewModels;
using ERPOptima.Web.Security.ViewModels;
using System.Collections.ObjectModel;
using ERPOptima.Model.Common;

namespace Optima.Controllers
{
    public class HomeController : Controller
    {
        private IMenuService _menu;
        private ISecModuleService _secModuleService;
        public HomeController()
        {
            var datafactory = new DatabaseFactory();
            _menu = new MenuService(new MenuRepository(datafactory), new UnitOfWork(datafactory));
            _secModuleService = new SecModuleService(new SecModuleRepository(datafactory), new UnitOfWork(datafactory));
        }
        public ActionResult Index(int userId,int companyId)
        {
            

            DataTable dt = _secModuleService.GetSecPermittedModuleByUserId(userId,companyId);
            List<SecPermittedModuleViewModel> list = dt.DataTableToList<SecPermittedModuleViewModel>();

            return View(list);
        }

        public ActionResult ErpUrl()
        {
            return Redirect("http://www.erpoptima.com/");
        }
        public ActionResult Login()
        {
            return View();
        }
        [Authorize]
        public ActionResult SelectCompany()
        {
            return View();
        }

      
        public ActionResult GetMenuByUserIdAndModuleId(int userId,int moduleId)
        {
            //int userId = Convert.ToInt32(Session["userId"].ToString());
            //int moduleId = 2;// Convert.ToInt32(Session["moduleId"].ToString());

            DataTable dt = _menu.GetMenusByUserIdAndModualeId(userId, moduleId);

            List<MenuViewModel> menuList = dt.DataTableToList<MenuViewModel>();

            return Json(menuList, JsonRequestBehavior.AllowGet);
          
        }
        [HttpPost]
        public ActionResult GetMenusForAndriod(int roleId)
        {
            //int userId = Convert.ToInt32(Session["userId"].ToString());
            //int moduleId = 2;// Convert.ToInt32(Session["moduleId"].ToString());
            //Resource -90  is for Sales Order
            //Resource -91  is for Sales Order Approval
            //Resource -115  is for field visit
            var datafactory = new DatabaseFactory();
            SecRolePermissionService rps = new SecRolePermissionService(new SecRolePermissionRepository(datafactory), new UnitOfWork(datafactory));
            Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
            if (rps.IsPermitted(roleId, 90))
            {
                dictionary.Add("Sales", true);
            }
            else
            {
                dictionary.Add("Sales", false);
            }
            if (rps.IsPermitted(roleId, 91))
            {
                dictionary.Add("Approval", true);
            }
            else
            {
                dictionary.Add("Approval", false);
            }
            if (rps.IsPermitted(roleId, 115))
            {
                dictionary.Add("FieldVisit", true);
            }
            else
            {
                dictionary.Add("FieldVisit", false);
            }
            return Json(dictionary, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetProcessLevelMenu(int companyId, int userId, int moduleId)
        {

            Collection<MenuViewModel> menus = new Collection<MenuViewModel>();
            List<CmnProcessLevel> records = new List<CmnProcessLevel>();
            
           
           
            Collection<CmnApprovalProcess> approvalProcesses = new Collection<CmnApprovalProcess>();
           

            DataTable dt = _menu.GetApprovalProcessByModuleId( moduleId);

            List<CmnApprovalProcess> menuList = dt.DataTableToList<CmnApprovalProcess>();
            
            foreach (CmnApprovalProcess row in menuList)
            {
                MenuViewModel objSecResource = new MenuViewModel();
                objSecResource.Id = (int)row.Id;
                objSecResource.Name = row.Url;
                objSecResource.DisplayName = row.Name;

                DataTable dtProcess = _menu.GetApprovalProcessLevelByUserId(userId, (int)row.Id);
                records = dtProcess.DataTableToList<CmnProcessLevel>();
                if (records != null && records.Count > 0)
                {
                    foreach (CmnProcessLevel innerRow in records)
                    {

                        //Check the user is permitted

                        bool isPermitted = false;

                        isPermitted = IsPermitted(userId, (int)row.Id, (int)innerRow.Id);

                        if (isPermitted)
                        {
                            //menus.Contains(objSecResource);
                            var values = from MenuViewModel obj in menus where (obj.Name.ToUpper() == objSecResource.Name.ToUpper()) select obj;
                            if (values != null && values.Count() == 0)
                            {
                                menus.Add(objSecResource);//Add process menu
                            }
                            MenuViewModel objResource = new MenuViewModel();
                            string urlStr = row.Url;
                            urlStr = urlStr.Contains("Pid") == true ? urlStr.Replace("Pid", row.Id.ToString()) : urlStr;
                            urlStr = urlStr.Contains("Lid") == true ? urlStr.Replace("Lid", innerRow.Id.ToString()) : urlStr;
                            objResource.Name = urlStr;
                                //"?Data=" + row.Id + "=" + innerRow.Id + "=" + innerRow.Name;
                            objResource.DisplayName = innerRow.Name;
                            objResource.SecResourcesId = (int)row.Id;

                            menus.Add(objResource);//Add process level menu


                        }
                    }
                }
            }

            return Json(menus, JsonRequestBehavior.AllowGet);
        }


        private bool IsPermitted(int roleId, int processId, int levelId)
        {
            bool ret = false;
            try
            {
                DataTable dt = _menu.IsPermitted(roleId, processId, levelId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int i = 0;
                    int.TryParse(dt.Rows[0][0].ToString(), out i);
                    if (i > 0)
                    {
                        ret = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }



    }
}
