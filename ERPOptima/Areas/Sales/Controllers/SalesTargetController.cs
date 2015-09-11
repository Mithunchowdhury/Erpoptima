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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class SalesTargetController : Controller
    {
        private ISalesTargetService _SalesTargetService;
        private ISalesTargetDetailService _SalesTargetDetailService;
         private ISecCompanyService _SecCompanyService;
         private IOfficeService _officeService;
         private IHrmEmployeeService _hrmEmployeeService;
         public SalesTargetController()
         {
             var dbfactory = new DatabaseFactory();
             _SalesTargetService = new SalesTargetService(new SalesTargetRepository(dbfactory), new UnitOfWork(dbfactory));
             _SalesTargetDetailService = new SalesTargetDetailService(new SalesTargetDetailRepository(dbfactory), new UnitOfWork(dbfactory));
             _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
             _officeService = new OfficeService(new OfficeRepository(dbfactory), new UnitOfWork(dbfactory));
             _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), new UnitOfWork(dbfactory));
         }
        //
        // GET: /Sales/SalesTarget/
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }
       
        [AuthorizeUser]
        [ResourcePermissionAttribute]
        public ActionResult TargetList()
        {
            return View();
        }


        public string GetRefNo(int companyId, int employeeId)
        {
            SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
            SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
            string refno = _SalesTargetService.GetRefNo(companyId, objCmnCompany.Prefix, office.Code);
            return refno;

        }

        public ActionResult GetTargetList(int companyId)
        {
            var list = _SalesTargetService.GetTargetList(companyId);
            list = list.OrderBy(i => i.Employee).ToList();  //order by employee
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTargetsByYear(int companyId, int month, int year)
        {
            var list = _SalesTargetService.GetTargetsByYear(companyId, month, year).OrderBy(t => t.HrmEmployee).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTargetsByYearNEmployeeId(int companyId, int month, int year, int employeeId)
        {
            var list = _SalesTargetService.GetTargetsByYearNEmployeeId(companyId,month,year,employeeId).OrderByDescending(t=>t.CreatedDate).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTargetDetailByTargetId(int targetId)
        {
            var list = _SalesTargetDetailService.GetTargetDetailByTargetId(targetId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public ActionResult Save(SlsSalesTarget sTarget, List<SlsSalesTargetDetail> sTargetDetail)
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };
            if (ModelState.IsValid && sTargetDetail != null)
            {
                if (sTarget.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        sTarget.SecCompanyId = companyId;
                        sTarget.CreatedBy = userId;
                        sTarget.CreatedDate = DateTime.Now;
                        objOperation = _SalesTargetService.Save(sTarget);

                        int SalesTargetId = Convert.ToInt32(objOperation.OperationId);

                        foreach (var item in sTargetDetail)
                        {
                            SlsSalesTargetDetail objSlsSalesTargetDetail = _SalesTargetDetailService.GetById(item.Id);
                            if (objSlsSalesTargetDetail != null)
                            {
                                objSlsSalesTargetDetail.SlsSalesTargetId = sTarget.Id;
                                objSlsSalesTargetDetail.SlsProductId = item.SlsProductId;
                                objSlsSalesTargetDetail.Quantity = item.Quantity;                                
                                objSlsSalesTargetDetail.SlsUnitId = item.SlsUnitId;
                                _SalesTargetDetailService.Update(objSlsSalesTargetDetail);
                            }
                            else
                            {
                                objSlsSalesTargetDetail = new SlsSalesTargetDetail();                              
                                objSlsSalesTargetDetail.SlsSalesTargetId = SalesTargetId;
                                objSlsSalesTargetDetail.SlsProductId = item.SlsProductId;
                                objSlsSalesTargetDetail.Quantity = item.Quantity;
                                objSlsSalesTargetDetail.SlsUnitId = item.SlsUnitId;
                                _SalesTargetDetailService.Save(objSlsSalesTargetDetail);
                            }

                        }

                    }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        sTarget.SecCompanyId = companyId;
                        sTarget.ModifiedBy = userId;
                        sTarget.ModifiedDate = DateTime.Now;
                        objOperation = _SalesTargetService.Update(sTarget);

                        foreach (var item in sTargetDetail)
                        {
                            SlsSalesTargetDetail objSlsSalesTargetDetail = _SalesTargetDetailService.GetById(item.Id);
                            if (objSlsSalesTargetDetail != null)
                            {
                                objSlsSalesTargetDetail.SlsSalesTargetId = sTarget.Id;
                                objSlsSalesTargetDetail.SlsProductId = item.SlsProductId;
                                objSlsSalesTargetDetail.Quantity = item.Quantity;
                                objSlsSalesTargetDetail.SlsUnitId = item.SlsUnitId;
                                _SalesTargetDetailService.Update(objSlsSalesTargetDetail);
                            }
                            else
                            {
                                objSlsSalesTargetDetail = new SlsSalesTargetDetail();
                                objSlsSalesTargetDetail.SlsSalesTargetId = sTarget.Id;
                                objSlsSalesTargetDetail.SlsProductId = item.SlsProductId;
                                objSlsSalesTargetDetail.Quantity = item.Quantity;
                                objSlsSalesTargetDetail.SlsUnitId = item.SlsUnitId;
                                _SalesTargetDetailService.Save(objSlsSalesTargetDetail);
                            }

                        }





                    }

                }


                objOperation = _SalesTargetService.Commit();


            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Delete(int Id)
        {
            Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                SlsSalesTarget obj = _SalesTargetService.GetById(Id);
                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _SalesTargetService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        public ActionResult DeleteDetail(int Id)
        {
            Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                SlsSalesTargetDetail obj = _SalesTargetDetailService.GetById(Id);
                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _SalesTargetDetailService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }  








    }
}
