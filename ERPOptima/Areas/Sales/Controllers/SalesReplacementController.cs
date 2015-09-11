using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Model.Security;
using ERPOptima.Service.Hrm;
using ERPOptima.Service.Sales;
using ERPOptima.Service.Security;
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
    public class SalesReplacementController : Controller
    {
        private ISalesReplacementService _SalesReplacementService;
        private ISalesReplacementDetailService _SalesReplacementDetailService; 
        private ISecCompanyService _SecCompanyService;
        private IChartOfProductService _ChartOfProductService;
        private IUnitOfMeasurementService _unitOfMeasurementService;
        private IOfficeService _officeService;
        private IHrmEmployeeService _hrmEmployeeService;

        int userId = 0;
        int companyId = 0;

        public SalesReplacementController()
        {
            var dbfactory = new DatabaseFactory();
            _SalesReplacementService = new SalesReplacementService(new SalesReplacementRepository(dbfactory), new UnitOfWork(dbfactory));

            _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
            _SalesReplacementDetailService = new SalesReplacementDetailService(new SalesReplacementDetailRepository(dbfactory), new UnitOfWork(dbfactory));
            _ChartOfProductService = new ChartOfProductService(new ChartOfProductRepository(dbfactory), new UnitOfWork(dbfactory));
            _unitOfMeasurementService = new UnitOfMeasurementService(new UnitOfMeasurementRepository(dbfactory), new UnitOfWork(dbfactory));
            _officeService = new OfficeService(new OfficeRepository(dbfactory), new UnitOfWork(dbfactory));
            _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), new UnitOfWork(dbfactory));
        }
        //
        // GET: /Sales/SalesReplacement/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAutoNumber(int companyId, int employeeId)
        {
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
            var autoNumber = _SalesReplacementService.GetLastCode(companyId, objCmnCompany.Prefix, office.Code);
            return Json(new { Refno = autoNumber }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            //VM List 
            IList<SlsReplacementViewModel> result = new List<SlsReplacementViewModel>();
            int companyId = Convert.ToInt32(Session["companyId"]);
            var list = _SalesReplacementService.GetAll(companyId);
            var detailList = _SalesReplacementDetailService.GetAll();
            var productList = _ChartOfProductService.GetAll(companyId);
            var unitList = _unitOfMeasurementService.GetAll();
            list = list.OrderByDescending(i => i.CreatedDate).ToList(); //order by created date
            if (list != null && list.Count() > 0)
            {
                foreach (var item in list)
                {
                    //make VM from model
                    var itemVM = SlsReplacementMapModelToVM.MapToSlsReplacement(item);

                    //initialize VM detail list
                    IList<SlsReplacementDetailViewModel> resultDetail = new List<SlsReplacementDetailViewModel>();

                    //get detail model list for this model
                    var detListOfReplacement = detailList.Where(i => i.SlsReplacementId == item.Id).ToList();
                    foreach(var detail in detListOfReplacement)
                    {
                        //make VMDetail from Detail model
                        var detailVM = SlsReplacementDetailMapModelToVM.MapToSlsReplacementDetail(detail);
                        if(detail.SlsProductId != null && detail.SlsProductId > 0)
                        {
                            //assign product name
                            string productName = productList.Where(i => i.Id == detail.SlsProductId).FirstOrDefault().Name;
                            detailVM.SlsProductName = productName;
                        }
                        if (detail.SlsUnitId != null && detail.SlsUnitId > 0)
                        {
                            //assign unit name
                            string unitName = unitList.Where(i => i.Id == detail.SlsUnitId).FirstOrDefault().Name;
                            detailVM.SlsUnitName = unitName;
                        }

                        //add prepared detailVM to VM detail list
                        resultDetail.Add(detailVM);
                    }

                    //copy VM detail list to VM's detail VM list
                    itemVM.SlsReplacementDetailVMs = resultDetail;

                    //add prepared VM to VM list
                    result.Add(itemVM);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBySalesId(int salesId)
        {

            SlsReplacement obj = _SalesReplacementService.GetBySales(salesId);

            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        //[HttpGet]
        //public ActionResult GetAllSalesReplacementDetails(int SlsReplacementId)
        //{
        //    //DatabaseFactory dbfactory = new DatabaseFactory();
        //    //var list = new SalesOrderService(new SalesOrderRepository(dbfactory),
        //    //    new SalesOrderDetailRepository(dbfactory),
        //    //    new SalesOrderApprovalRepository(dbfactory),
        //    //    new UnitOfWork(dbfactory)).GetAll();
        //    var list = _SalesReplacementDetailService.GetAll();
        //    if(list != null && list.Count() > 0)
        //    {
        //        list = list.Where(i => i.SlsReplacementId == SlsReplacementId).ToList();
        //    }
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult Save(SlsReplacementViewModel slsReplacement)
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            int userId = Convert.ToInt32(Session["userId"]);
            var dbfactory = new DatabaseFactory();

            Operation objOperation = new Operation { Success = false };
            if (ModelState.IsValid)
            {
                if (slsReplacement.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        slsReplacement.SecCompanyId = companyId;
                        slsReplacement.CreatedBy = userId;
                        slsReplacement.CreatedDate = DateTime.Now;
                        //invDamage.InvDamageDetails = null;
                        objOperation = _SalesReplacementService.Save(slsReplacement);

                        //int SalesReplacementId = Convert.ToInt32(objOperation.OperationId);
                        //List<SlsReplacementDetail> slsReplacementDetail = slsReplacement.SlsReplacementDetails.ToList();
                        ////slsReplacement.SalesOrderDetails;
                        //foreach (var item in slsReplacementDetail)
                        //{
                        //    SlsReplacementDetail objSlsReplacementDetail = _SalesReplacementDetailService.GetById(item.Id);
                        //    if (objSlsReplacementDetail != null)
                        //    {
                        //        objSlsReplacementDetail.SlsReplacementId= slsReplacement.Id;
                        //        objSlsReplacementDetail.SlsProductId = item.SlsProductId;
                        //        objSlsReplacementDetail.Quantity = item.Quantity;
                        //        objSlsReplacementDetail.Reason = item.Reason;
                        //        objSlsReplacementDetail.SlsUnitId = item.SlsUnitId;
                        //        _SalesReplacementDetailService.Update(objSlsReplacementDetail);
                        //    }
                        //    else
                        //    {
                        //        objSlsReplacementDetail = new SlsReplacementDetail();
                        //        objSlsReplacementDetail.SlsReplacementId = SalesReplacementId;
                        //        objSlsReplacementDetail.SlsProductId = item.SlsProductId;
                        //        objSlsReplacementDetail.Quantity = item.Quantity;
                        //        objSlsReplacementDetail.Reason = item.Reason;
                        //        objSlsReplacementDetail.SlsUnitId = item.SlsUnitId;
                        //        _SalesReplacementDetailService.Save(objSlsReplacementDetail);
                        //    }

                        //}

                    }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        slsReplacement.SecCompanyId = companyId;
                        slsReplacement.ModifiedBy = userId;
                        slsReplacement.ModifiedDate = DateTime.Now;
                        objOperation = _SalesReplacementService.Update(slsReplacement);

                        List<SlsReplacementDetail> slsReplacementDetail = new List<SlsReplacementDetail>();
                        if (slsReplacement.SlsReplacementDetailVMs != null && slsReplacement.SlsReplacementDetailVMs.Count() > 0)
                        {
                            foreach (var detailVM in slsReplacement.SlsReplacementDetailVMs)
                            {
                                //make VMDetail from Detail model
                                var detail = SlsReplacementDetailMapVMToModel.MapToSlsReplacementDetail(detailVM);
                                slsReplacementDetail.Add(detail);
                            }
                        }
                        
                        //slsReplacement.SalesOrderDetails;
                        foreach (var item in slsReplacementDetail)
                        {
                            SlsReplacementDetail objSlsReplacementDetail = _SalesReplacementDetailService.GetById(item.Id);
                            if (objSlsReplacementDetail != null)
                            {
                                objSlsReplacementDetail.SlsReplacementId = slsReplacement.Id;
                                objSlsReplacementDetail.SlsProductId = item.SlsProductId;
                                objSlsReplacementDetail.AdjustedAmount= item.AdjustedAmount;
                                objSlsReplacementDetail.Quantity = item.Quantity;
                                objSlsReplacementDetail.Reason = item.Reason;
                                objSlsReplacementDetail.SlsUnitId = item.SlsUnitId;
                                _SalesReplacementDetailService.Update(objSlsReplacementDetail);
                            }
                            else
                            {
                                objSlsReplacementDetail = new SlsReplacementDetail();
                                objSlsReplacementDetail.SlsReplacementId = slsReplacement.Id;
                                objSlsReplacementDetail.SlsProductId = item.SlsProductId;
                                objSlsReplacementDetail.AdjustedAmount = item.AdjustedAmount;
                                objSlsReplacementDetail.Quantity = item.Quantity;
                                objSlsReplacementDetail.Reason = item.Reason;
                                objSlsReplacementDetail.SlsUnitId = item.SlsUnitId;
                                _SalesReplacementDetailService.Save(objSlsReplacementDetail);
                            }

                        }

                    }

                }

                //objOperation = _IDamageService.Commit();
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }
      
       
    }
}
