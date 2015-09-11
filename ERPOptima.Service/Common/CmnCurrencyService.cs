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

namespace ERPOptima.Service.Common
{

    public interface ICmnCurrencyService
    {
        IList<CmnCurrency> GetCmnCurrencies();
        Operation SaveCmnCurrency(CmnCurrency objCmnCurrency);
        Operation DeleteCmnCurrency(CmnCurrency objCmnCurrency);
        CmnCurrency GetById(int Id);
        Operation UpdateCmnCurrency(CmnCurrency objCmnCurrency);   
    }
    public class CmnCurrencyService : ICmnCurrencyService
    {
        private ICmnCurrencyRepository _CmnCurrencyRepository;
        private IUnitOfWork _UnitOfWork;
        public CmnCurrencyService(ICmnCurrencyRepository CmnCurrencyRepository, IUnitOfWork unitOfWork)
        {
            this._CmnCurrencyRepository = CmnCurrencyRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<CmnCurrency> GetCmnCurrencies()
        {
            return _CmnCurrencyRepository.GetCmnCurrencies();
        }

        public CmnCurrency GetById(int Id)
        {
            CmnCurrency objCmnCurrency = _CmnCurrencyRepository.GetById(Id);
            return objCmnCurrency;
        }
        public Operation UpdateCmnCurrency(CmnCurrency objCmnCurrency)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objCmnCurrency.Id };
            _CmnCurrencyRepository.Update(objCmnCurrency);

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
        public Operation DeleteCmnCurrency(CmnCurrency objCmnCurrency)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objCmnCurrency.Id };
            _CmnCurrencyRepository.Delete(objCmnCurrency);

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

        public Operation SaveCmnCurrency(CmnCurrency objCmnCurrency)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _CmnCurrencyRepository.AddEntity(objCmnCurrency);
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

