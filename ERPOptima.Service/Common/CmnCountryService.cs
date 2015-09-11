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

    public interface ICmnCountryService
    {
        IList<CmnCountry> GetCmnCountries();
        Operation SaveCmnCountry(CmnCountry objCmnCountry);
        Operation DeleteCmnCountry(CmnCountry objCmnCountry);
        CmnCountry GetById(int Id);
        Operation UpdateCmnCountry(CmnCountry objCmnCountry);   
    }
    public class CmnCountryService : ICmnCountryService
    {
        private ICmnCountryRepository _CmnCountryRepository;
        private IUnitOfWork _UnitOfWork;
        public CmnCountryService(ICmnCountryRepository cmnCountryRepository, IUnitOfWork unitOfWork)
        {
            this._CmnCountryRepository = cmnCountryRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<CmnCountry> GetCmnCountries()
        {
            return _CmnCountryRepository.GetCmnCountries();
        }

        public CmnCountry GetById(int Id)
        {
            CmnCountry objCmnCountry = _CmnCountryRepository.GetById(Id);
            return objCmnCountry;
        }
        public Operation UpdateCmnCountry(CmnCountry objCmnCountry)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objCmnCountry.Id };
            _CmnCountryRepository.Update(objCmnCountry);

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
        public Operation DeleteCmnCountry(CmnCountry objCmnCountry)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objCmnCountry.Id };
            _CmnCountryRepository.Delete(objCmnCountry);

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

        public Operation SaveCmnCountry(CmnCountry objCmnCountry)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _CmnCountryRepository.AddEntity(objCmnCountry);
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

