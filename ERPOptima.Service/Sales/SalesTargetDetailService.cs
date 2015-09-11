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

    public interface ISalesTargetDetailService
    {

        IList<SlsSalesTargetDetail> GetTargetDetailByTargetId(int targetId);

        Operation Save(SlsSalesTargetDetail objSlsSalesTargetDetail);
        Operation Delete(SlsSalesTargetDetail objSlsSalesTargetDetail);
        SlsSalesTargetDetail GetById(int Id);
        Operation Update(SlsSalesTargetDetail objSlsSalesTargetDetail);


    }


    #endregion
    public class SalesTargetDetailService : ISalesTargetDetailService
    {

        private ISalesTargetDetailRepository _SalesTargetDetailRepository;
        private IUnitOfWork _UnitOfWork;

        public SalesTargetDetailService(ISalesTargetDetailRepository salesTargetDetailRepository, IUnitOfWork unitOfWork)
        {
            this._SalesTargetDetailRepository = salesTargetDetailRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<SlsSalesTargetDetail> GetTargetDetailByTargetId(int targetId)
        {

            return _SalesTargetDetailRepository.GetTargetDetailByTargetId(targetId);


        }
        public SlsSalesTargetDetail GetById(int Id)
        {
            SlsSalesTargetDetail objSlsSalesTargetDetail = _SalesTargetDetailRepository.GetById(Id);
            return objSlsSalesTargetDetail;
        }
        public Operation Save(SlsSalesTargetDetail objSlsSalesTargetDetail)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _SalesTargetDetailRepository.AddEntity(objSlsSalesTargetDetail);
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
        public Operation Update(SlsSalesTargetDetail objSlsSalesTargetDetail)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsSalesTargetDetail.Id };
            _SalesTargetDetailRepository.Update(objSlsSalesTargetDetail);

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
        public Operation Delete(SlsSalesTargetDetail objSlsSalesTargetDetail)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSlsSalesTargetDetail.Id };
            _SalesTargetDetailRepository.Delete(objSlsSalesTargetDetail);

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
