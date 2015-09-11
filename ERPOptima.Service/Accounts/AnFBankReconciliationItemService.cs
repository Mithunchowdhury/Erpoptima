using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Common.Repository;
using System.Data;
using System.Data.SqlClient;
using ERPOptima.Model.Accounts;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;

namespace ERPOptima.Service.Accounts
{
    public interface IAnFBankReconciliationItemService
    {
        IList<AnFBankReconciliationItem> GetAnFBankReconciliationItems();
        Operation SaveAnFBankReconciliationItem(AnFBankReconciliationItem objAnFBankReconciliationItem);
        Operation DeleteAnFBankReconciliationItem(AnFBankReconciliationItem objAnFBankReconciliationItem);
        AnFBankReconciliationItem GetById(int Id);
        Operation UpdateAnFBankReconciliationItem(AnFBankReconciliationItem objAnFBankReconciliationItem);
    }
    public class AnFBankReconciliationItemService : IAnFBankReconciliationItemService
    {
        private IAnFBankReconciliationItemRepository _AnFBankReconciliationItemRepository;
        private IUnitOfWork _UnitOfWork;
        public AnFBankReconciliationItemService(IAnFBankReconciliationItemRepository AnFBankReconciliationItemRepository, IUnitOfWork unitOfWork)
        {
            this._AnFBankReconciliationItemRepository = AnFBankReconciliationItemRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<AnFBankReconciliationItem> GetAnFBankReconciliationItems()
        {
            return _AnFBankReconciliationItemRepository.GetAnFBankReconciliationItems();
        }

        public AnFBankReconciliationItem GetById(int Id)
        {
            AnFBankReconciliationItem objAnFBankReconciliationItem = _AnFBankReconciliationItemRepository.GetById(Id);
            return objAnFBankReconciliationItem;
        }
        public Operation UpdateAnFBankReconciliationItem(AnFBankReconciliationItem objAnFBankReconciliationItem)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFBankReconciliationItem.Id };
            _AnFBankReconciliationItemRepository.Update(objAnFBankReconciliationItem);

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception)
            {
                objOperation.Success = false;

            }
            return objOperation;
        }
        public Operation DeleteAnFBankReconciliationItem(AnFBankReconciliationItem objAnFBankReconciliationItem)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFBankReconciliationItem.Id };
            _AnFBankReconciliationItemRepository.Delete(objAnFBankReconciliationItem);

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception)
            {

                objOperation.Success = false;
            }
            return objOperation;
        }

        public Operation SaveAnFBankReconciliationItem(AnFBankReconciliationItem objAnFBankReconciliationItem)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _AnFBankReconciliationItemRepository.AddEntity(objAnFBankReconciliationItem);
            objOperation.OperationId = Id;

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
            }
            return objOperation;
        }
    }
}
