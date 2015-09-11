using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    public interface IStockInService
    {
        IEnumerable<InvStockInOut> GetAll();
        //string GetRefNo(int companyId, string prefix);
        Operation Save(InvStockInOut obj);
        Operation Delete(InvStockInOut obj);
        InvStockInOut GetById(int Id);
        InvStore GetStoreName(string name);
        Operation Update(InvStockInOut obj);
        //Operation Commit();
    }
    public class StockInService : IStockInService
    {
        private IStockInRepository _IStockInRepository;
        private IUnitOfWork _UnitOfWork;
        public StockInService(IStockInRepository StockInRepository, IUnitOfWork unitOfWork)
        {
            this._IStockInRepository = StockInRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IEnumerable<InvStockInOut> GetAll()
        {
            return _IStockInRepository.GetAllStockIns();
        }
        //public string GetRefNo(int companyId, string prefix)
        //{

        //    string refNo = prefix + "-" + "STO" + "-" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + "-" + _IStockInRepository.GetRefNo(companyId).ToString();
        //    return refNo;
        //}

        public InvStockInOut GetById(int Id)
        {
            InvStockInOut obj = _IStockInRepository.GetById(Id);
            return obj;
        }
        public InvStore GetStoreName(string name)
        {
            InvStore objInvStore = _IStockInRepository.GetStoreName(name);
            return objInvStore;
        }
        public Operation Update(InvStockInOut obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _IStockInRepository.Update(obj);

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
        public Operation Delete(InvStockInOut obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _IStockInRepository.Delete(obj);

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

        public Operation Save(InvStockInOut obj)
        {
            Operation objOperation = new Operation { Success = true };

            //For now - as Stock In
            //0=Out, 1=In
            obj.Status = 1;
            //1=Receive,2=Issue,3-damage,4-transfer,5-return
            obj.TransactionType = 1;

            long Id = _IStockInRepository.AddEntity(obj);
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
