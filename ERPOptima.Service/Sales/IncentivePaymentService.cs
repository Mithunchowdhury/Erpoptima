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
    public interface IIncentivePaymentService
    {
        IList<SlsIncentive> GetAll();
        SlsIncentive GetById(int Id);
        Operation Save(SlsIncentive obj);
        Operation Delete(SlsIncentive obj);
        Operation Update(SlsIncentive obj);
    }

    public class IncentivePaymentService : IIncentivePaymentService
    {
        private IIncentivePaymentRepository _IncentivePaymentRepository;
        private ISalesIncentiveSettingsRepository _ISalesIncentiveSettingsRepository;
        private IUnitOfWork _UnitOfWork;

        public IncentivePaymentService(IIncentivePaymentRepository incentivePaymentRepository,
            ISalesIncentiveSettingsRepository iSalesIncentiveSettingsRepository, IUnitOfWork unitOfWork)
        {
            this._IncentivePaymentRepository = incentivePaymentRepository;
            this._ISalesIncentiveSettingsRepository = iSalesIncentiveSettingsRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<SlsIncentive> GetAll()
        {
            var incentiveList = _IncentivePaymentRepository.GetAll();           

            var result = incentiveList.Select(i => new SlsIncentive()
            {
                Id = i.Id,
                HrmEmployeeId = i.HrmEmployeeId,
                Year = i.Year,
                Month = i.Month,
                Commission = i.Commission,
                AmountPaid = i.AmountPaid,
                PaymentDate = i.PaymentDate,
                Remarks = i.Remarks,
                CreatedBy = i.CreatedBy,
                CreatedDate = i.CreatedDate,
                ModifiedBy = i.ModifiedBy,
                ModifiedDate = i.ModifiedDate,

            }).ToList();



            return result;
        }

        public SlsIncentive GetById(int Id)
        {
            SlsIncentive obj = _IncentivePaymentRepository.GetById(Id);
            return obj;
        }
        public Operation Update(SlsIncentive obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _IncentivePaymentRepository.Update(obj);

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

        public Operation Delete(SlsIncentive obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _IncentivePaymentRepository.Delete(obj);

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

        public Operation Save(SlsIncentive obj)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _IncentivePaymentRepository.AddEntity(obj);
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
