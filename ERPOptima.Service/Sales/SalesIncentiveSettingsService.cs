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
    public interface ISalesIncentiveSettingsService
    {
        IEnumerable<SlsIncentiveSetting> GetAll();

        SlsIncentiveSetting GetById(int Id);

        Operation Save(SlsIncentiveSetting obj);

        Operation Delete(SlsIncentiveSetting obj);

        Operation Update(SlsIncentiveSetting obj);
        decimal GetIncentiveRate(decimal totalsalespermonth);

    }
    public class SalesIncentiveSettingsService : ISalesIncentiveSettingsService
    {
        private ISalesIncentiveSettingsRepository _salesIncentiveRepository;
        private IUnitOfWork _unitOfWork;


        public SalesIncentiveSettingsService(ISalesIncentiveSettingsRepository salesIncentiveRepository, IUnitOfWork unitOfWork)
        {
            this._salesIncentiveRepository = salesIncentiveRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<SlsIncentiveSetting> GetAll()
        {
            try
            {
                return _salesIncentiveRepository.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public SlsIncentiveSetting GetById(int Id)
        {
            SlsIncentiveSetting obj = _salesIncentiveRepository.GetById(Id);
            return obj;
        }
        public Operation Update(SlsIncentiveSetting obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _salesIncentiveRepository.Update(obj);

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

        public Operation Delete(SlsIncentiveSetting obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            _salesIncentiveRepository.Delete(obj);

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


        public Operation Save(SlsIncentiveSetting obj)
        {
            Operation objOperation = new Operation { Success = true };

            int Id = _salesIncentiveRepository.AddEntity(obj);
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

        public decimal GetIncentiveRate(decimal totalsalespermonth)
        {
            return _salesIncentiveRepository.GetIncentiveRate(totalsalespermonth);
        }

    }
     

}
