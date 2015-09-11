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
    #region interface

    public interface ITransferDetailService
    {

        IList<SlsTransferDetail> GetTransferDetailByTransferId(int transferId);

        Operation Save(SlsTransferDetail objSlsTransferDetail);
        Operation Delete(SlsTransferDetail objSlsTransferDetail);
        SlsTransferDetail GetById(int Id);
        Operation Update(SlsTransferDetail objSlsTransferDetail);


    }


    #endregion
    public class TransferDetailService : ITransferDetailService
    {
        private ITransferDetailRepository _TransferDetailRepository;
        private IUnitOfWork _UnitOfWork;

        public TransferDetailService(ITransferDetailRepository transferDetailRepository, IUnitOfWork unitOfWork)
        {
            this._TransferDetailRepository = transferDetailRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<SlsTransferDetail> GetTransferDetailByTransferId(int transferId)
        {

            return _TransferDetailRepository.GetTransferDetailByTransferId(transferId);


        }
        public SlsTransferDetail GetById(int Id)
        {
            SlsTransferDetail objSlsTransferDetail = _TransferDetailRepository.GetById(Id);
            return objSlsTransferDetail;
        }
        public Operation Save(SlsTransferDetail objSlsTransferDetail)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _TransferDetailRepository.AddEntity(objSlsTransferDetail);
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
        public Operation Update(SlsTransferDetail objSlsTransferDetail)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsTransferDetail.Id };
            _TransferDetailRepository.Update(objSlsTransferDetail);

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
        public Operation Delete(SlsTransferDetail objSlsTransferDetail)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsTransferDetail.Id };
            _TransferDetailRepository.Delete(objSlsTransferDetail);

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









    }
}
