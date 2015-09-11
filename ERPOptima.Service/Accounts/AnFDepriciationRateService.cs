using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Common.Repository;
using System.Data;
using System.Data.SqlClient;
using ERPOptima.Model.Accounts;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;

namespace ERPOptima.Service.Accounts
{
    public interface IAnFDepreciationRateService
    {
        IList<AnFDepreciationRate> GetDepreciationRates();
        Operation SaveAnFDepreciationRate(AnFDepreciationRate objAnFDepreciationRate);
        Operation DeleteAnFDepreciationRate(AnFDepreciationRate objAnFDepreciationRate);
        AnFDepreciationRate GetById(int Id);
        Operation UpdateAnFDepreciationRate(AnFDepreciationRate objAnFDepreciationRate);
    }
    public class AnFDepreciationRateService : IAnFDepreciationRateService
    {
        private IAnFDepreciationRateRepository _AnFDepreciationRateRepository;
        private IUnitOfWork _UnitOfWork;
        public AnFDepreciationRateService(IAnFDepreciationRateRepository AnFDepreciationRateRepository, IUnitOfWork unitOfWork)
        {
            this._AnFDepreciationRateRepository = AnFDepreciationRateRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<AnFDepreciationRate> GetDepreciationRates()
        {
            return _AnFDepreciationRateRepository.GetDepreciationRates();
        }

        public AnFDepreciationRate GetById(int Id)
        {
            AnFDepreciationRate objAnFDepreciationRate = _AnFDepreciationRateRepository.GetById(Id);
            return objAnFDepreciationRate;
        }
        public Operation UpdateAnFDepreciationRate(AnFDepreciationRate objAnFDepreciationRate)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFDepreciationRate.Id };
            _AnFDepreciationRateRepository.Update(objAnFDepreciationRate);

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
        public Operation DeleteAnFDepreciationRate(AnFDepreciationRate objAnFDepreciationRate)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFDepreciationRate.Id };
            _AnFDepreciationRateRepository.Delete(objAnFDepreciationRate);

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

        public Operation SaveAnFDepreciationRate(AnFDepreciationRate objAnFDepreciationRate)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _AnFDepreciationRateRepository.AddEntity(objAnFDepreciationRate);
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
