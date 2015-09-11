using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    public interface IDistributorService
    {
        IEnumerable<SlsDistributor> GetAll();       
        SlsDistributor GetById(int Id);
        Operation Save(SlsDistributor obj);
        Operation Delete(SlsDistributor obj);
        Operation Update(SlsDistributor obj);
        
    }
    public class DistributorService : IDistributorService
    {
        private IDistributorRepository _distributorRepository;
        private IUnitOfWork _unitOfWork;


        public DistributorService(IDistributorRepository distributorRepository, IUnitOfWork unitOfWork)
        {
            this._distributorRepository = distributorRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<SlsDistributor> GetAll()
        {
            try
            {
                return _distributorRepository.GetAll();
            }
            catch(Exception ex)
            {                
                return null;
            }
        }

        public SlsDistributor GetById(int Id)
        {
            SlsDistributor obj = _distributorRepository.GetById(Id);
            return obj;
        }
        public Operation Update(SlsDistributor obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _distributorRepository.Update(obj);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;

            }
            return objOperation;
        }

        public Operation Delete(SlsDistributor obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _distributorRepository.Delete(obj);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {

                objOperation.Success = false;
            }
            return objOperation;
        }

        public Operation Save(SlsDistributor obj)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _distributorRepository.AddEntity(obj);
            objOperation.OperationId = Id;

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
            }
            return objOperation;
        }

    }
}
