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
    public interface IRetailerService
    {
        IEnumerable<SlsRetailer> GetAll();
        SlsRetailer GetById(int Id);
        Operation Save(SlsRetailer obj);
        Operation Delete(SlsRetailer obj);
        Operation Update(SlsRetailer obj);
    }
    public class RetailerService : IRetailerService
    {
        private IRetailerRepository _RetailerRepository;
        private IUnitOfWork _unitOfWork;


        public RetailerService(IRetailerRepository RetailerRepository, IUnitOfWork unitOfWork)
        {
            this._RetailerRepository = RetailerRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<SlsRetailer> GetAll()
        {
            try
            {
                return _RetailerRepository.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SlsRetailer GetById(int Id)
        {
            SlsRetailer obj = _RetailerRepository.GetById(Id);
            return obj;
        }
        public Operation Update(SlsRetailer obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _RetailerRepository.Update(obj);

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

        public Operation Delete(SlsRetailer obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _RetailerRepository.Delete(obj);

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

        public Operation Save(SlsRetailer obj)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _RetailerRepository.AddEntity(obj);
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
