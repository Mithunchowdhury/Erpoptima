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

    public interface ITransferService
    {

        IList<SlsTransfer> GetAll(int companyId);
        string GetRefNo(int companyId, string prefix, string offcode);
        Operation Save(SlsTransfer objSlsTransfer);
        Operation Delete(SlsTransfer objSlsTransfer);
        SlsTransfer GetById(int Id);
        Operation Update(SlsTransfer objSlsTransfer);
        Operation Commit();

    }


    #endregion
    public class TransferService : ITransferService
    {

        private ITransferRepository _TransferRepository;
        private IUnitOfWork _UnitOfWork;

        public TransferService(ITransferRepository transferRepository, IUnitOfWork unitOfWork)
        {
            this._TransferRepository = transferRepository;
            this._UnitOfWork = unitOfWork;
        }


        public IList<SlsTransfer> GetAll(int companyId)
        {

            return _TransferRepository.GetAll(companyId);


        }
        public string GetRefNo(int companyId, string prefix, string offcode)
        {
            string refNo = prefix + "-" + "TRN" + "-" + offcode + "-" + DateTime.Now.ToString("yy") + "-" + DateTime.Now.ToString("MM") + "/" + _TransferRepository.GetRefNo(companyId).ToString();
            return refNo;
        }

        public SlsTransfer GetById(int Id)
        {
            SlsTransfer objSlsTransfer = _TransferRepository.GetById(Id);
            return objSlsTransfer;
        }

        public Operation Save(SlsTransfer objSlsTransfer)
        {
            Operation objOperation = new Operation { Success = true };

            int lastId = _TransferRepository.GetLastId(objSlsTransfer);
            objSlsTransfer.Id = lastId;
            objOperation.OperationId = lastId;

            _TransferRepository.Add(objSlsTransfer);
            return objOperation;
        }
        public Operation Update(SlsTransfer objSlsTransfer)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsTransfer.Id };
            _TransferRepository.Update(objSlsTransfer);
            return objOperation;

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
        public Operation Delete(SlsTransfer objSlsTransfer)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsTransfer.Id };
            _TransferRepository.Delete(objSlsTransfer);

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
