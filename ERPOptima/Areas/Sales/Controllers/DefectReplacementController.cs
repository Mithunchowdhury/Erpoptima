using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Service.Sales;
using ERPOptima.Service.Security;
using ERPOptima.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Optima.Areas.Sales.Controllers
{
    public class DefectReplacementController : Controller
    {
        private IDefectEntryService _IDefectReplacementService;
        private IDefectDetailEntryService _IDefectDetailService;
        private ISecCompanyService _SecCompanyService;
        private IChartOfProductService _ProductService;
        private IUnitOfMeasurementService _UnitService;
        public DefectReplacementController()
        {
               var dbfactory = new DatabaseFactory();
               _IDefectReplacementService = new DefectEntryService(new DefectEntryRepository(dbfactory), new UnitOfWork(dbfactory));
            _IDefectDetailService = new DefectDetailEntryService(new DefectDetailEntryRepository(dbfactory), new UnitOfWork(dbfactory));
            _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
            _ProductService = new ChartOfProductService(new ChartOfProductRepository(dbfactory), new UnitOfWork(dbfactory));
            _UnitService = new UnitOfMeasurementService(new UnitOfMeasurementRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        //
        // GET: /Sales/DefectReplacement/
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetDetailsByDefectId(int defectId)
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            var productList = _ProductService.GetAll(companyId);
            var unitList = _UnitService.GetAll();

            var list = _IDefectDetailService.GetByDefectId(defectId);
            List<SlsDefectDetailViewModel> defectDetail = new List<SlsDefectDetailViewModel>();

            if (list != null && list.Count() > 0)
            {

                defectDetail = list.Select(o => new SlsDefectDetailViewModel
                {
                    Id = o.Id,
                    SlsDefectId = o.SlsDefectId,
                    SlsProductId = o.SlsProductId,
                    SlsProductName = productList.Where(i=>i.Id == o.SlsProductId).FirstOrDefault().Name,
                    Quantity = o.Quantity,
                    SlsUnitId = o.SlsUnitId,
                    SlsUnitName = unitList.Where(i => i.Id == o.SlsUnitId).FirstOrDefault().Name,
                    Reason = o.Reason,
                    ReplacedQuantity = o.ReplacedQuantity,
                    AdjustedAmount = o.AdjustedAmount,

                }).ToList();


            }

            return Json(defectDetail, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAllDefects()
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            var list = _IDefectReplacementService.GetAll(companyId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(IList<SlsDefectDetail> list)
        {
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                foreach (var item in list)
                {
                    if (item.Id == 0)
                    {
                        if ((bool)Session["Add"])
                        {
                            //possibly this will not happen, all items will be modified.
                            objOperation = _IDefectDetailService.Save(item);
                        }
                        else { objOperation.OperationId = -1; }

                    }
                    else
                    {
                        if ((bool)Session["Edit"])
                        {
                            objOperation = _IDefectDetailService.Update(item);
                        }
                        else { objOperation.OperationId = -2; }
                    }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

    }
}
