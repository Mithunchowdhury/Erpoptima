using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Sales
{
    /// <summary>
    /// Service Interface for Dealer
    /// </summary>
    public interface IDealerService
    {
        IEnumerable<SlsDealer> GetAll();
        SlsDealer GetById(int Id);
        Operation Save(SlsDealer obj);
        Operation Delete(SlsDealer obj);
        Operation Update(SlsDealer obj);
    }
    /// <summary>
    /// Service Implementation for Dealer
    /// </summary>
    public class DealerService : IDealerService
    {
        private IDealerRepository _DealerRepository;
        private IUnitOfWork _unitOfWork;


        public DealerService(IDealerRepository DealerRepository, IUnitOfWork unitOfWork)
        {
            this._DealerRepository = DealerRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<SlsDealer> GetAll()
        {
            try
            {
                return _DealerRepository.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SlsDealer GetById(int Id)
        {
            SlsDealer obj = _DealerRepository.GetById(Id);
            return obj;
        }
        public Operation Update(SlsDealer obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _DealerRepository.Update(obj);

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

        public Operation Delete(SlsDealer obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _DealerRepository.Delete(obj);

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

        public Operation Save(SlsDealer obj)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _DealerRepository.AddEntity(obj);
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
