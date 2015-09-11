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
    //class DamageDetailService
    //{
    //}

    #region interface
    public interface IDamageDetailService
    {

        int GetLastId();
        IList<DamageDetail> GetByDamageId(int damageId); 
        InvDamageDetail GetById(int Id);

        void Add(InvDamageDetail objInvDamageDetail);
        Operation Save(InvDamageDetail objInvDamageDetail);
        void Update(InvDamageDetail objInvDamageDetail);
        Operation Delete(InvDamageDetail objInvDamageDetail);
        Operation Commit();

    }

      #endregion
    public class DamageDetailService : IDamageDetailService
    {

        private InvDamageDetailRepository _DamageDetailRepository;
        private IUnitOfWork _UnitOfWork;
        public DamageDetailService(InvDamageDetailRepository requisitionDetailRepository, IUnitOfWork unitOfWork)
        {
            this._DamageDetailRepository = requisitionDetailRepository;
            this._UnitOfWork = unitOfWork;
        }


        public int GetLastId()
        {
            return _DamageDetailRepository.GetLastId();
        }



        public IList<DamageDetail> GetByDamageId(int damageId)
        {
            return _DamageDetailRepository.GetByDamageId(damageId);           


        }

        public InvDamageDetail GetById(int Id)
        {
            InvDamageDetail objDamageDetail = _DamageDetailRepository.GetById(Id);
            return objDamageDetail;
        }

        public void Add(InvDamageDetail objInvDamageDetail)
        {
            _DamageDetailRepository.Add(objInvDamageDetail);

        }

        public void Update(InvDamageDetail objInvDamageDetail)
        {
            throw new NotImplementedException();
        }

        public Operation Delete(InvDamageDetail objInvDamageDetail)
        {
            throw new NotImplementedException();
        }
        public Operation Save(InvDamageDetail objInvIssueDetail)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _DamageDetailRepository.AddEntity(objInvIssueDetail);
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
