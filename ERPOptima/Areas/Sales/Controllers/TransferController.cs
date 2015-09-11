using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Hrm.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using ERPOptima.Model.Security;
using ERPOptima.Service.Hrm;
using ERPOptima.Service.Inventory;
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
    public class TransferController : Controller
    {
         private ITransferService _TransferService;
         private ITransferDetailService _TransferDetailService;
         private ISecCompanyService _SecCompanyService;
         private IOfficeService _officeService;
         private IStoreService _StoreService;
         private IHrmEmployeeService _hrmEmployeeService;
         public TransferController()
         {
             var dbfactory = new DatabaseFactory();
             _TransferService = new TransferService(new TransferRepository(dbfactory), new UnitOfWork(dbfactory));
             _TransferDetailService = new TransferDetailService(new TransferDetailRepository(dbfactory), new UnitOfWork(dbfactory));
             _SecCompanyService = new SecCompanyService(new SecCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
             _officeService = new OfficeService(new OfficeRepository(dbfactory), new UnitOfWork(dbfactory));
             _StoreService = new StoreService(new InvStoreRepository(dbfactory), new UnitOfWork(dbfactory));
             _hrmEmployeeService = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), new UnitOfWork(dbfactory));
         }

        //
        // GET: /Sales/Transfer/

         [AuthorizeUser]
         [ResourcePermissionAttribute]
        public ActionResult Index()
        {
            return View();
        }


         [HttpGet]
         public string GetRefNo(int companyId, int employeeId)
         {
             SecCompany objCmnCompany = _SecCompanyService.GetById(companyId);
             SlsOffice office = _officeService.GetById((int)_hrmEmployeeService.GetById(employeeId).SlsOfficeId);
             string refNo = _TransferService.GetRefNo(companyId, objCmnCompany.Prefix, office.Code);
             return refNo;

         }

        public ActionResult GetAll(int companyId)
        {
            //var list = _TransferService.GetAll(companyId);

            var list = _TransferService.GetAll(companyId).Select(t => new
            {
                Id = t.Id,
                RefNo = t.RefNo,
                From = t.From,
                FromOffice = _officeService.GetById((int)t.From).Name,
                FromInvStoreId = t.FromInvStoreId,
                FromStore = _StoreService.GetById((int)t.FromInvStoreId).Name,
                To = t.To,
                ToOffice = _officeService.GetById((int)t.To).Name,
                ToInvStoreId = t.ToInvStoreId,
                ToStore = _StoreService.GetById((int)t.ToInvStoreId).Name,
                Date = t.Date,
                VehicleNo = t.VehicleNo,
                ChallenNo = t.ChallenNo,
                GatepassNo = t.GatepassNo,
                SecCompanyId = t.SecCompanyId,
                Remarks = t.Remarks,
                CreatedBy=t.CreatedBy,
                CreatedDate=t.CreatedDate
               
            }).ToList();

            
            list = list.OrderByDescending(i => i.CreatedDate).ToList();  //order by created date

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTransferDetailByTransferId(int transferId)
        {
            var list = _TransferDetailService.GetTransferDetailByTransferId(transferId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public ActionResult Save(SlsTransfer transfer, List<SlsTransferDetail> transferDetail)
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            int userId = Convert.ToInt32(Session["userId"]);
            Operation objOperation = new Operation { Success = false };
            if (ModelState.IsValid && transferDetail != null)
            {
                if (transfer.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        transfer.SecCompanyId = companyId;
                        transfer.CreatedBy = userId;
                        transfer.CreatedDate = DateTime.Now;
                        objOperation = _TransferService.Save(transfer);

                        int transferId = Convert.ToInt32(objOperation.OperationId);

                        foreach (var item in transferDetail)
                        {
                            SlsTransferDetail objSlsTransferDetail = _TransferDetailService.GetById(item.Id);
                            if (objSlsTransferDetail != null)
                            {
                                objSlsTransferDetail.SlsTransferId = transfer.Id;
                                objSlsTransferDetail.SlsProductId = item.SlsProductId;
                                objSlsTransferDetail.Quantity = item.Quantity;                               
                                objSlsTransferDetail.SlsUnitId = item.SlsUnitId;
                                _TransferDetailService.Update(objSlsTransferDetail);
                            }
                            else
                            {
                                objSlsTransferDetail = new SlsTransferDetail();
                                objSlsTransferDetail.SlsTransferId = transferId;
                                objSlsTransferDetail.SlsProductId = item.SlsProductId;
                                objSlsTransferDetail.Quantity = item.Quantity;   
                                objSlsTransferDetail.SlsUnitId = item.SlsUnitId;
                                _TransferDetailService.Save(objSlsTransferDetail);
                            }

                        }

                    }

                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        transfer.SecCompanyId = companyId;
                        transfer.ModifiedBy = userId;
                        transfer.ModifiedDate = DateTime.Now;
                        objOperation = _TransferService.Update(transfer);

                        foreach (var item in transferDetail)
                        {
                            SlsTransferDetail objSlsTransferDetail = _TransferDetailService.GetById(item.Id);
                            if (objSlsTransferDetail != null)
                            {
                                objSlsTransferDetail.SlsTransferId = transfer.Id;
                                objSlsTransferDetail.SlsProductId = item.SlsProductId;
                                objSlsTransferDetail.Quantity = item.Quantity;
                                objSlsTransferDetail.SlsUnitId = item.SlsUnitId;                              
                                _TransferDetailService.Update(objSlsTransferDetail);
                            }
                            else
                            {
                                objSlsTransferDetail = new SlsTransferDetail();
                                objSlsTransferDetail.SlsTransferId = transfer.Id;
                                objSlsTransferDetail.SlsProductId = item.SlsProductId;
                                objSlsTransferDetail.Quantity = item.Quantity;
                                objSlsTransferDetail.SlsUnitId = item.SlsUnitId; 
                                _TransferDetailService.Save(objSlsTransferDetail);
                            }

                        }





                    }

                }


                objOperation = _TransferService.Commit();


            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Delete(int Id)
        {
            Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                SlsTransfer obj = _TransferService.GetById(Id);
                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _TransferService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        public ActionResult DeleteDetail(int Id)
        {
            Operation objOperation = new Operation { Success = false };
            if (Id != 0)
            {
                SlsTransferDetail obj = _TransferDetailService.GetById(Id);
                if (obj == null)
                {
                    objOperation.Success = false;
                    return Json(objOperation, JsonRequestBehavior.DenyGet);
                }
                objOperation = _TransferDetailService.Delete(obj);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        } 






















    }
}
