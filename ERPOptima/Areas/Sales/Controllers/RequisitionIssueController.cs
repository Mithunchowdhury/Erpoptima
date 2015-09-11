using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Model.Inventory;
using ERPOptima.Service.Inventory;
using ERPOptima.Service.Security;
using ERPOptima.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class RequisitionIssueController : Controller
    {
        //private IRequisitionService<InvRequisitionApproval, InvRequisitionViewModel> _IRequisitionService;
        //private IRequisitionDetailService _IRequisitionDetailService;
        //private ISecCompanyService _SecCompanyService;

        //public RequisitionIssueController()
        //{
        //    var dbfactory = new DatabaseFactory();
        //    _IRequisitionService = new RequisitionService(new RequisitionRepository(dbfactory),
        //      new RequisitionApprovalRepository(dbfactory), new UnitOfWork(dbfactory));
        //    _IRequisitionDetailService = new RequisitionDetailService(new RequisitionDetailRepository(dbfactory), new UnitOfWork(dbfactory));
        //    _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));

        //}
        //
        // GET: /Sales/RequisitionIssue/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult List()
        {
            return View();
        }
        

    }
}
