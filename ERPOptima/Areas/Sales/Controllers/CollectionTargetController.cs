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
    public class CollectionTargetController : Controller
    {
        //
        // GET: /Sales/CollectionTarget/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Target()
        {
            return View();
        }
        private ICollectionTargetService _collectionTargetService;
        public CollectionTargetController()
        {
            var dbfactory = new DatabaseFactory();
            _collectionTargetService = new CollectionTargetService(new CollectionTargetRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        #region CollectionTarget

        [HttpGet]
        public ActionResult GetAll(int companyId,int monthId,int yearId,int employeeId)
        {
            IList<CollectionTargetViewModel> collectionTarget = null;
            DataTable dt = _collectionTargetService.GetAll(companyId, monthId, yearId, employeeId);
            if (dt != null)
            {
                collectionTarget = new Collection<CollectionTargetViewModel>();
                foreach (DataRow row in dt.Rows)
                {
                    collectionTarget.Add((CollectionTargetViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(CollectionTargetViewModel)));
                }
            }
            collectionTarget = collectionTarget.OrderByDescending(t => t.Year).ToList();
            return Json(collectionTarget, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Save(SlsCollectionTarget CollectionTarget)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            int companyId = Convert.ToInt32(Session["companyId"]);
            Operation objOperation = new Operation { Success = false };            

            if (ModelState.IsValid)
            {
                if (CollectionTarget.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        CollectionTarget.SecCompanyId = companyId;
                        CollectionTarget.CreatedBy = userId;
                        CollectionTarget.CreatedDate = DateTime.Now.Date;                                                
                        objOperation = _collectionTargetService.Save(CollectionTarget);
                    }
                    else { objOperation.OperationId = -1; }

                }
                else
                {
                   if ((bool)Session["Edit"])
                {
                    CollectionTarget.SecCompanyId = companyId;
                    CollectionTarget.ModifiedBy = userId;
                    CollectionTarget.ModifiedDate = DateTime.Now.Date;
                    objOperation = _collectionTargetService.Update(CollectionTarget);
                }
                   else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
        #endregion
    }
}