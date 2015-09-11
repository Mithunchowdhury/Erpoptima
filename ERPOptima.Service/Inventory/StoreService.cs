using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Inventory
{
    #region interface

    public interface IStoreService
    {

        IList<InvStore> GetAll();
        InvStore GetById(int Id);
        InvStore GetStoresForOffice(int officeId);
    }


    #endregion
    public class StoreService:IStoreService
    {

        private IInvStoreRepository _InvStoreRepository;
        //private IInvStoreRepository _storeRepository;
        private IUnitOfWork _UnitOfWork;
        public StoreService(IInvStoreRepository invStoreRepository, IUnitOfWork unitOfWork)
        {
            this._InvStoreRepository = invStoreRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<InvStore> GetAll()
        {
            return _InvStoreRepository.GetStores();
        }

        public InvStore GetById(int Id)
        {
            InvStore objInvStore = _InvStoreRepository.GetById(Id);
            return objInvStore;
        }
        public InvStore GetStoresForOffice(int officeId)
        {
            InvStore obj = _InvStoreRepository.GetStoresForOffice(officeId);
            return obj;
        }








    }
}
