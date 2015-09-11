using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Sales;
using ERPOptima.Model.ViewModel;
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
    
    public interface ICollectionEntryService
    {       
        string getAutoNumber(int companyId, string prefix, string offcode);

        IEnumerable<SlsCollectionViewModel> GetAll();
        SlsCollectionViewModel GetById(int Id);
        Operation Save(SlsCollectionViewModel obj);
        Operation Update(SlsCollectionViewModel obj);

        DataTable GetCollectionMoneyReceiptReport(int companyId, int CollectionEntryID); 
  
    }

    public class CollectionEntryService : ICollectionEntryService
    {

        private ICollectionEntryRepository _repository;
        private IUnitOfWork _unitOfWork;


        public CollectionEntryService(ICollectionEntryRepository repository, IUnitOfWork unitOfWork)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
        }

        //public IList<SlsCollection> GetAll()
        //{
        //    return _repository.GetAll();
        //}
      
        //t

        public IEnumerable<SlsCollectionViewModel> GetAll()
        {
            return GetAllCollection(0);
        }
        /// <summary>
        /// Both GetAll and GetById will reuse this helper
        /// </summary>
        /// <param name="collectionId">offerId=0 GetAll, offerId!=0 GetById</param>
        /// <returns></returns>
        public IEnumerable<SlsCollectionViewModel> GetAllCollection(int collectionId)
        {
            try
            {
                Collection<SlsCollectionViewModel> list = null;
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@CollectionId", collectionId);       //GetAllSpSlsCollections
                DataTable dt = _repository.GetFromStoredProcedure(SPList.CollectionData.GetAllSpSlsCollections, paramsToStore);

                if (dt != null)
                {
                    list = new Collection<SlsCollectionViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((SlsCollectionViewModel)ERPOptima.Lib.Utilities.Helper.FillTo(row, typeof(SlsCollectionViewModel)));
                    }
                }

                ////to get categories offered for each offer
                //foreach (SlsCollectionViewModel item in list)
                //{
                //    //attach detail list to item
                //  ////  item.OfferCategories = GetAllcollectionDetails(item.Id).ToList();
                //}

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public SlsCollectionViewModel GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Operation Save(SlsCollectionViewModel obj)
        {
          
            Operation objOperation = new Operation { Success = false };
            using (var dbContextTransaction = _repository.BeginTransaction())
            {
                try
                {
                    objOperation = new Operation { Success = true };

                    SlsCollection collect = SlsCollectionMapVMToModel.MapToSlsCollection(obj);

                    int Id = _repository.AddEntity(collect);
                    _repository.SaveChanges();
                    objOperation.OperationId = Id;
                    collect.Id = Id;
                    try
                    {
                        //_unitOfWork.Commit();
                        _repository.Commit(dbContextTransaction);
                    }
                    catch (Exception ex)
                    {
                        objOperation.Success = false;
                        throw ex;
                    }
                }
                catch(Exception ex)
                {
                    _repository.Rollback(dbContextTransaction);
                }
            }
            return objOperation;
        
        }

        public Operation Update(SlsCollectionViewModel obj)
        {
            Operation objOperation = new Operation { Success = false };
            using (var dbContextTransaction = _repository.BeginTransaction())
            {
                try
                {
                    objOperation = new Operation { Success = true, OperationId = obj.Id };
                    SlsCollection collect = SlsCollectionMapVMToModel.MapToSlsCollection(obj);
                    _repository.Update(collect);
                    _repository.SaveChanges();


                    try
                    {
                        //_unitOfWork.Commit();
                        _repository.Commit(dbContextTransaction);
                    }
                    catch (Exception ex)
                    {
                        objOperation.Success = false;
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    _repository.Rollback(dbContextTransaction);
                }
            }
            return objOperation;
        }


        public class SlsCollectionMapVMToModel
        {
            public static SlsCollection MapToSlsCollection(SlsCollectionViewModel obj)
            {
                SlsCollection model = new SlsCollection();

                model.Id = obj.Id;
                model.Amount = obj.Amount;
                model.BankName = obj.BankName;
                model.CollectedFrom = obj.CollectedFrom;
                model.PaymentMode = obj.PaymentMode;
                model.PartyType = obj.PartyType;

                model.RefNo = obj.RefNo;
                model.TransactionRefNo = obj.TransactionRefNo;
                model.TransactionType = obj.TransactionType;
                model.CreatedBy = obj.CreatedBy;
                model.CreatedDate = obj.CreatedDate;
                model.ModifiedBy = obj.ModifiedBy;
                model.ModifiedDate = obj.ModifiedDate;

                model.CollectionDate = obj.CollectionDate;
                model.HrmEmployeeId = obj.HrmEmployeeId;
                model.SecCompanyId = obj.SecCompanyId;

                return model;
            }
        }


        public string getAutoNumber(int companyId, string prefix, string offcode)
        {
            string refNo = prefix + "-" + "COL" + "-" + offcode + "-" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.ToString("MM") + "/" + _repository.getAutoNumber(companyId).ToString();
            return refNo;
        }




        public DataTable GetCollectionMoneyReceiptReport(int companyId, int CollectionEntryID)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[2];
            paramsToStore[0] = new SqlParameter("@SecCompanyId", companyId);
            paramsToStore[1] = new SqlParameter("@CollectionId", CollectionEntryID);

            try
            {
                dt = _repository.GetFromStoredProcedure(SPList.Report.RptMoneyReceipt, paramsToStore);
            }
            catch (Exception)
            {
            }

            return dt;
        }
    }
}
