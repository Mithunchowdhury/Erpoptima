using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Inventory.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Inventory
{
    #region interface
    public interface IRequisitionDetailService
    {

        int GetLastId();
        IList<ReqDetail> GetByRequisitionId(int requisitionId);
        InvRequisitionDetail GetById(int Id);

        void Add(InvRequisitionDetail objInvRequisitionDetail);
        void Update(InvRequisitionDetail objInvRequisitionDetail);
        Operation Delete(InvRequisitionDetail objInvRequisitionDetail);
        Operation Commit();

    }

    #endregion
    public class RequisitionDetailService : IRequisitionDetailService
    {

        private IRequisitionDetailRepository _RequisitionDetailRepository;
        private IUnitOfWork _UnitOfWork;
        public RequisitionDetailService(IRequisitionDetailRepository requisitionDetailRepository, IUnitOfWork unitOfWork)
        {
            this._RequisitionDetailRepository = requisitionDetailRepository;
            this._UnitOfWork = unitOfWork;
        }


        public int GetLastId()
        {
            return _RequisitionDetailRepository.GetLastId();
        }

        public IList<ReqDetail> GetByRequisitionId(int requisitionId)
        {

            return _RequisitionDetailRepository.GetByRequisitionId(requisitionId);
        }

        public InvRequisitionDetail GetById(int Id)
        {
            return _RequisitionDetailRepository.GetById(Id);
        }

        public void Add(InvRequisitionDetail objInvRequisitionDetail)
        {

            _RequisitionDetailRepository.Add(objInvRequisitionDetail);

        }

        public void Update(InvRequisitionDetail objInvRequisitionDetail)
        {

            _RequisitionDetailRepository.Update(objInvRequisitionDetail);


        }
        public Operation Delete(InvRequisitionDetail objInvRequisitionDetail)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objInvRequisitionDetail.Id };
            _RequisitionDetailRepository.Delete(objInvRequisitionDetail);

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





    }
}
