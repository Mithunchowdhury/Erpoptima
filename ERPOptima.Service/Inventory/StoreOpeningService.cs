using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Inventory
{

    #region interface

    public interface IStoreOpeningService
    {      
        IList<InvStoreOpening> Get();

        //IList<InvStoreOpening> GetInvStoreOpeningByInvStoreId(int storeId, int companyId);
        DataTable GetInvStoreOpeningByInvStoreId(int storeId, int companyId);

        InvStoreOpening GetById(long Id);
        void Add(InvStoreOpening objAnFOpeningBalance);
        void Update(InvStoreOpening objAnFOpeningBalance);
        void Remove(InvStoreOpening objAnFOpeningBalance);
        Operation Commit();

        Operation Delete();




    }


    #endregion



    public class StoreOpeningService : IStoreOpeningService
    {

        private IInvStoreOpeningRepository _InvStoreOpeningRepository;
        private IUnitOfWork _UnitOfWork;
        public StoreOpeningService(IInvStoreOpeningRepository openingBalanceRepository, IUnitOfWork unitOfWork)
        {
            this._InvStoreOpeningRepository = openingBalanceRepository;
            this._UnitOfWork = unitOfWork;
        }


       
        public IList<InvStoreOpening> Get()
        {
            throw new NotImplementedException();
        }

        //public IList<InvStoreOpening> GetInvStoreOpeningByInvStoreId(int storeId, int companyId)
        //{            
        //    return _InvStoreOpeningRepository.GetInvStoreOpeningByInvStoreId(storeId, companyId);                      

        //}


        public DataTable GetInvStoreOpeningByInvStoreId(int storeId, int companyId)
        {
            return _InvStoreOpeningRepository.GetInvStoreOpeningByInvStoreId(storeId, companyId);

        }
        public InvStoreOpening GetById(long Id)
        {
            return _InvStoreOpeningRepository.GetById(Id);
        }
        public void Add(InvStoreOpening objAnFOpeningBalance)
        {
            int lastId=_InvStoreOpeningRepository.GetLastId();
            objAnFOpeningBalance.Id = lastId;

            _InvStoreOpeningRepository.Add(objAnFOpeningBalance);
        }

        public void Update(InvStoreOpening objAnFOpeningBalance)
        {

            _InvStoreOpeningRepository.Update(objAnFOpeningBalance);
        
        
        }

        public void Remove(InvStoreOpening objAnFOpeningBalance)
        {
            _InvStoreOpeningRepository.Delete(objAnFOpeningBalance);
        }


        public Operation Delete(InvStoreOpening objAnFOpeningBalance)
        {
            throw new NotImplementedException();
        }


        public Operation Commit()
        {
            Operation objOperation = new Operation { Success = true };

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation = new Operation { Success = false };
                
            }
            return objOperation;
        }


        public Operation Delete()
        {
            throw new NotImplementedException();
        }




    }
}
