using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Inventory;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    public interface ISalesReturnService
    {
        IList<SlsSalesReturn> GetAll();
        IList<SlsSalesReturnViewModel> GetAllVM(int companyId);
        Operation Save(SlsSalesReturnViewModel obj, int storedId);
        Operation Update(SlsSalesReturnViewModel obj, int storedId);
        IList<SalesReturnListViewModel> Get(int companyid, DateTime StartDate, DateTime EndDate);
        string GetRefNo(int companyId, string prefix, string offcode);
    }
    public class SalesReturnService : ISalesReturnService
    {
        private ISalesReturnRepository _SalesReturnRepository;
        private IUnitOfWork _UnitOfWork;
        private ISalesReturnDetailRepository _SalesReturnDetailRepository;
        private IStockInRepository _StockInRepository;

        public SalesReturnService(ISalesReturnRepository salesReturnRepository,
            ISalesReturnDetailRepository salesReturnDetailRepository, IStockInRepository stockInRepository,
            IUnitOfWork unitOfWork)
        {
            this._SalesReturnRepository = salesReturnRepository;
            this._SalesReturnDetailRepository = salesReturnDetailRepository;
            this._StockInRepository = stockInRepository;
            this._UnitOfWork = unitOfWork;
        }
        public string GetRefNo(int companyId, string prefix, string offcode)
        {
            string refNo = prefix + "-" + "RTN" + "-" + offcode + "-" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.ToString("MM") + "/" + _SalesReturnRepository.GetLastCode(companyId).ToString();
            return refNo;
        }
        public IList<SlsSalesReturn> GetAll()
        {
            try
            {
                return _SalesReturnRepository.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public IList<SalesReturnListViewModel> Get(int companyid, DateTime StartDate, DateTime EndDate)
        {
            IList<SalesReturnListViewModel> returnList = new List<SalesReturnListViewModel>();
            IList<ERPOptima.Model.Sales.SlsSalesReturn> list = new List<ERPOptima.Model.Sales.SlsSalesReturn>();
            //DataTable dt = new DataTable();

            //SqlParameter[] paramsToStore = new SqlParameter[2];
            //paramsToStore[0] = new SqlParameter("@StartDate", StartDate);
            //paramsToStore[1] = new SqlParameter("@EndDate", EndDate);


            try
            {
                // dt = _SalesReturnRepository.GetFromStoredProcedure(SPList.Report.RptSalesReturnList, paramsToStore);
                list = _SalesReturnRepository.Get(companyid, StartDate, EndDate);
                if (list != null && list.Count > 0)
                {
                    foreach (ERPOptima.Model.Sales.SlsSalesReturn itm in list)
                    {
                        var dbfactory = new DatabaseFactory();
                        SalesReturnListViewModel tmp = new SalesReturnListViewModel();
                        tmp.Id = itm.Id;
                        tmp.RefNo = itm.RefNo;
                        tmp.CreatedDate = itm.CreatedDate;
                        switch (itm.PartyType)
                        {
                            case 1:
                                ERPOptima.Data.Sales.Repository.DistributorRepository rposDistrbutor = new DistributorRepository(dbfactory);
                                tmp.Party = rposDistrbutor.GetById((int)itm.Party).Name;
                                break;
                            case 2:
                                ERPOptima.Data.Sales.Repository.RetailerRepository rposRetailer = new RetailerRepository(dbfactory);
                                tmp.Party = rposRetailer.GetById((int)itm.Party).Name;
                                break;
                            case 3:
                                ERPOptima.Data.Sales.Repository.DealerRepository rposDealer = new DealerRepository(dbfactory);
                                tmp.Party = rposDealer.GetById((int)itm.Party).Name;
                                break;
                            case 4:
                                ERPOptima.Data.Sales.Repository.CorporateClientRepository rposCorporateClient = new CorporateClientRepository(dbfactory);
                                tmp.Party = rposCorporateClient.GetById((int)itm.Party).Name;
                                break;
                        }
                        returnList.Add(tmp);
                    }
                }

            }
            catch (Exception)
            {
            }
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    list = dt.DataTableToList<SalesReturnListViewModel>();
            //}
            return returnList;
        }
        public IList<SlsSalesReturnViewModel> GetAllVM(int companyId)
        {
            try
            {
                IList<SlsSalesReturnViewModel> list = new List<SlsSalesReturnViewModel>();



                var listT = _SalesReturnRepository.GetAll().ToList();
                if (listT != null && listT.Count() > 0)
                {
                    foreach (SlsSalesReturn item in listT)
                    {
                        SlsSalesReturnViewModel obj = SlsSalesReturnMapModelToVM.MapToSlsSalesReturn(item);

                        //
                        IList<SlsProduct> productList = new ChartOfProductService(new ChartOfProductRepository(new DatabaseFactory()),
                    new UnitOfWork(new DatabaseFactory())).GetAll(companyId).ToList();

                        IList<SlsUnit> unitList = new UnitOfMeasurementService(new UnitOfMeasurementRepository(new DatabaseFactory()),
                            new UnitOfWork(new DatabaseFactory())).GetAll().ToList();

                        IList<SlsSalesReturnDetail> detailList = _SalesReturnDetailRepository.GetAll().ToList();

                        //Detail list
                        IList<SlsSalesReturnDetailViewModel> detailsOfObj = GetAllDetails(companyId, obj.Id,
                            productList, unitList, detailList);
                        obj.DetailList = detailsOfObj;

                        list.Add(obj);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public IList<SlsSalesReturnDetailViewModel> GetAllDetails(int companyId, int objId,
            IList<SlsProduct> productList, IList<SlsUnit> unitList, IList<SlsSalesReturnDetail> detailList)
        {
            try
            {
                IList<SlsSalesReturnDetailViewModel> detailsVM = new List<SlsSalesReturnDetailViewModel>();


                IList<SlsSalesReturnDetail> detailsOfObj = detailList.Where(i => i.SlsReturnId == objId).ToList();

                foreach (SlsSalesReturnDetail item in detailsOfObj)
                {
                    SlsSalesReturnDetailViewModel obj = SlsSalesReturnMapModelToVM.MapToSlsSalesReturnDetail(item);
                    if (obj != null)
                    {
                        SlsProduct productObj = productList.Where(i => i.Id == obj.SlsProductId).FirstOrDefault();
                        if (productObj != null)
                        {
                            //obj.SlsProduct = productObj;
                            //obj.SlsProduct = productObj;
                            obj.SlsProductName = productObj.Name;
                        }
                        SlsUnit unitObj = unitList.Where(i => i.Id == obj.SlsUnitId).FirstOrDefault();
                        if (unitObj != null)
                        {
                            //obj.SlsUnit = unitObj;
                            //obj.SlsUnit = unitObj;
                            obj.SlsUnitName = unitObj.Name;
                        }
                        detailsVM.Add(obj);
                    }
                }

                return detailsVM;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Operation Save(SlsSalesReturnViewModel objVM, int storedId)
        {
            Operation objOperation = new Operation { Success = false };

            using (var dbContextTransaction = _SalesReturnRepository.BeginTransaction())
            {
                try
                {
                    objOperation = new Operation { Success = true };                   
                    SlsSalesReturn objT = SlsSalesReturnMapVMToModel.MapToSlsSalesReturn(objVM);

                    int Id = _SalesReturnRepository.AddEntity(objT);
                    _SalesReturnRepository.SaveChanges();
                    objOperation.OperationId = Id;
                    objT.Id = Id;

                    //add or update categories to each item
                    IList<SlsSalesReturnDetail> detailList = new List<SlsSalesReturnDetail>();
                    foreach (SlsSalesReturnDetailViewModel detail in objVM.DetailList)
                    {
                        SlsSalesReturnDetail objD = SlsSalesReturnMapVMToModel.MapToSlsSalesReturnDetail(detail);
                        objD.SlsReturnId = objT.Id;

                        //calculations                        

                        if (objD.Id <= 0)
                            detailList.Add(objD);

                        else
                        {
                            _SalesReturnDetailRepository.Update(objD);
                            ManageStockForSalesReturn(storedId, objT.Id, objT.CreatedDate, objD, "Update");
                        }
                    }
                    //Add detail list - new 
                    if (detailList != null && detailList.Count > 0)
                    {
                        _SalesReturnDetailRepository.AddEntityList(detailList);
                        foreach (var slrDetail in detailList)
                        {
                            ManageStockForSalesReturn(storedId, objT.Id, objT.CreatedDate, slrDetail, "Add");
                        }
                    }

                    _SalesReturnDetailRepository.SaveChanges();


                    try
                    {
                        _SalesReturnRepository.Commit(dbContextTransaction);
                    }
                    catch (Exception ex)
                    {
                        objOperation.Success = false;
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    _SalesReturnRepository.Rollback(dbContextTransaction);
                }
            }
            return objOperation;
        }

        public Operation Update(SlsSalesReturnViewModel objVM, int storedId)
        {
            Operation objOperation = new Operation { Success = false };
            using (var dbContextTransaction = _SalesReturnRepository.BeginTransaction())
            {
                try
                {
                    SlsSalesReturn objT = SlsSalesReturnMapVMToModel.MapToSlsSalesReturn(objVM);
                    objOperation = new Operation { Success = true, OperationId = objT.Id };                    
                    _SalesReturnRepository.Update(objT);
                    _SalesReturnRepository.SaveChanges();

                    //add or update categories to each item
                    IList<SlsSalesReturnDetail> adddOrUpdateList = new List<SlsSalesReturnDetail>();
                    IList<SlsSalesReturnDetail> detailList = new List<SlsSalesReturnDetail>();
                    foreach (SlsSalesReturnDetailViewModel detail in objVM.DetailList)
                    {

                        SlsSalesReturnDetail objD = SlsSalesReturnMapVMToModel.MapToSlsSalesReturnDetail(detail);
                        objD.SlsReturnId = objT.Id;
                        //calculations

                        if (objD.Id <= 0)
                        {
                            detailList.Add(objD);
                            adddOrUpdateList.Add(objD);
                        }

                        else
                        {
                            _SalesReturnDetailRepository.Update(objD);
                            ManageStockForSalesReturn(storedId, objT.Id, objT.CreatedDate, objD, "Update");
                            adddOrUpdateList.Add(objD);
                        }

                    }
                    //Add detail list - new
                    if (detailList != null && detailList.Count > 0)
                    {
                        _SalesReturnDetailRepository.AddEntityList(detailList);
                        foreach (var slrDetail in detailList)
                        {
                            ManageStockForSalesReturn(storedId, objT.Id, objT.CreatedDate, slrDetail, "Add");
                        }
                    }

                    //To delete removed items
                    IList<int> savedItems = _SalesReturnDetailRepository.GetAll().Where(j => j.SlsReturnId == objT.Id).Select(i => i.Id).ToList();
                    foreach (int savedId in savedItems)
                    {
                        var addedOrUpdatedObj = adddOrUpdateList.Where(i => i.Id == savedId).FirstOrDefault();
                        if (addedOrUpdatedObj == null)
                        {
                            //this saved id item removed from UI
                            var removedObj = _SalesReturnDetailRepository.GetById(savedId);
                            _SalesReturnDetailRepository.Delete(removedObj);
                            ManageStockForSalesReturn(storedId, objT.Id, objT.CreatedDate, removedObj, "Delete");
                        }
                    }
                    //end of deletion action                    
                    _SalesReturnDetailRepository.SaveChanges();

                    try
                    {
                        //_unitOfWork.Commit();
                        _SalesReturnRepository.Commit(dbContextTransaction);
                    }
                    catch (Exception ex)
                    {
                        objOperation.Success = false;
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    _SalesReturnRepository.Rollback(dbContextTransaction);
                }
            }
            return objOperation;
        }

        public void ManageStockForSalesReturn(int storedId, int salesReturnId, DateTime date,
            SlsSalesReturnDetail returnObj, string operation)
        {
            //Execute it inside Transaction Scope...            
            InvStockInOut obj = new InvStockInOut();
            obj.InvStoreId = storedId;
            //1=Receive,2=Issue,3-damage,4-transfer,5-return
            obj.TransactionType = 5;
            obj.RefId = salesReturnId;
            obj.SlsProductId = returnObj.SlsProductId;
            obj.Quantity = returnObj.ReturnedQuantity;
            obj.SlsUnitId = returnObj.SlsUnitId;
            //0=Out, 1=In
            obj.Status = 1;
            obj.TransactionDate = date;

            switch (operation)
            {
                case "Add":
                    //_stockInService.SaveInternal(obj);
                    _StockInRepository.AddEntity(obj);
                    break;

                case "Update":
                    {
                        var allStockIns = _StockInRepository.GetAll();
                        //1=Receive,2=Issue,3-damage,4-transfer,5-return
                        try
                        {
                            allStockIns = allStockIns.Where(i => i.TransactionType == 5 && i.RefId == salesReturnId &&
                                i.InvStoreId == storedId && i.TransactionDate == date && i.SlsProductId == returnObj.SlsProductId
                                && i.SlsUnitId == returnObj.SlsUnitId).ToList();
                            if (allStockIns != null && allStockIns.Count() > 0)
                            {
                                obj = allStockIns.First();
                                obj.Quantity = returnObj.ReturnedQuantity;
                                _StockInRepository.Update(obj);
                            }
                            else
                            {
                                //add
                                _StockInRepository.AddEntity(obj);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    break;

                case "Delete":
                    {
                        var allStockIns = _StockInRepository.GetAll();
                        //1=Receive,2=Issue,3-damage,4-transfer,5-return
                        try
                        {
                            allStockIns = allStockIns.Where(i => i.TransactionType == 5 && i.RefId == salesReturnId &&
                                i.InvStoreId == storedId && i.TransactionDate == date && i.SlsProductId == returnObj.SlsProductId
                                && i.SlsUnitId == returnObj.SlsUnitId).ToList();
                            if (allStockIns != null && allStockIns.Count() > 0)
                            {
                                obj = allStockIns.First();
                                _StockInRepository.Delete(obj);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    break;

                default:
                    break;
            }
            _StockInRepository.SaveChanges();
        }

        
    }

    public class SalesReturnListViewModel
    {
        public int Id { get; set; }
        public string RefNo { get; set; }
        public string Party { get; set; }
        public System.DateTime CreatedDate { get; set; }


    }

    public class SlsSalesReturnMapVMToModel
    {
        public static SlsSalesReturn MapToSlsSalesReturn(SlsSalesReturnViewModel obj)
        {
            SlsSalesReturn model = new SlsSalesReturn();

            model.Id = obj.Id;

            model.PartyType = obj.PartyType;
            model.Party = obj.Party;
            model.RefNo = obj.RefNo;
            model.Reason = obj.Reason;

            if (!obj.IsAdjusted)
            {
                //Means no amount to be adjusted
                model.AdjustedAmount = 0;
            }
            else
            {
                model.AdjustedAmount = CalculateAdjustedAmount(obj.DetailList);
            }
            model.SecCompanyId = obj.SecCompanyId;

            model.CreatedBy = obj.CreatedBy;
            model.CreatedDate = obj.CreatedDate;
            model.ModifiedBy = obj.ModifiedBy;
            model.ModifiedDate = obj.ModifiedDate;

            return model;
        }
        public static SlsSalesReturnDetail MapToSlsSalesReturnDetail(SlsSalesReturnDetailViewModel obj)
        {
            SlsSalesReturnDetail model = new SlsSalesReturnDetail();

            model.Id = obj.Id;

            model.SlsReturnId = obj.SlsReturnId;
            model.SlsProductId = obj.SlsProductId;
            model.ReturnedQuantity = obj.ReturnedQuantity;
            model.Rate = obj.Rate;
            model.SlsUnitId = obj.SlsUnitId;

            return model;
        }
        public static decimal? CalculateAdjustedAmount(IList<SlsSalesReturnDetailViewModel> detailList)
        {
            decimal? adjustment = 0;

            try
            {
                foreach (var item in detailList)
                {
                    adjustment += item.Rate * item.ReturnedQuantity;
                }
            }
            catch (Exception ex)
            {

            }

            return adjustment;
        }
    }
    public class SlsSalesReturnMapModelToVM
    {
        public static SlsSalesReturnViewModel MapToSlsSalesReturn(SlsSalesReturn obj)
        {
            SlsSalesReturnViewModel model = new SlsSalesReturnViewModel();

            model.Id = obj.Id;

            model.PartyType = obj.PartyType;
            model.Party = obj.Party;
            model.RefNo = obj.RefNo;
            model.Reason = obj.Reason;
            model.AdjustedAmount = obj.AdjustedAmount;
            model.SecCompanyId = obj.SecCompanyId;

            model.CreatedBy = obj.CreatedBy;
            model.CreatedDate = obj.CreatedDate;
            model.ModifiedBy = obj.ModifiedBy;
            model.ModifiedDate = obj.ModifiedDate;

            //calculative
            if (model.AdjustedAmount != null && model.AdjustedAmount > 0)
            {
                model.IsAdjusted = true;
            }

            return model;
        }
        public static SlsSalesReturnDetailViewModel MapToSlsSalesReturnDetail(SlsSalesReturnDetail obj)
        {
            SlsSalesReturnDetailViewModel model = new SlsSalesReturnDetailViewModel();

            model.Id = obj.Id;

            model.SlsReturnId = obj.SlsReturnId;
            model.SlsProductId = obj.SlsProductId;
            model.ReturnedQuantity = obj.ReturnedQuantity;
            model.SlsUnitId = obj.SlsUnitId;
            model.Rate = obj.Rate;

            return model;
        }

    }

}
