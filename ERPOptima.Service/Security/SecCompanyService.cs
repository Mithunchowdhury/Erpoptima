using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Service.Security
{
    public interface ISecCompanyService
    {
        Dictionary<Int32, string> GetModuleIdAndName();
        DataTable GetCompanyByUserId(int userId);
        SecCompany GetById(int Id);
        IList<SecCompany> GetCmnCompanies();
        Operation SaveCmnCompany(SecCompany objCmnCompany);
        Operation DeleteCmnCompany(SecCompany objCmnCompany);
        Operation UpdateCmnCompany(SecCompany objCmnCompany);
        IEnumerable<SecCompany> GetAll();
    }

    public class SecCompanyService : ISecCompanyService
    {
        private ISecCompanyRepository _CmnCompanyRepository;
        private IUnitOfWork _UnitOfWork;

        public SecCompanyService(ISecCompanyRepository cmnCompanyRepository, IUnitOfWork unitOfWork)
        {
            this._CmnCompanyRepository = cmnCompanyRepository;
            this._UnitOfWork = unitOfWork;
        }

        public Dictionary<Int32, string> GetModuleIdAndName()
        {

            Dictionary<Int32, string> cm = _CmnCompanyRepository.GetCompaniesIdAndName();

            return cm;
        }
        public DataTable GetCompanyByUserId(int userId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SecUserId", userId);
                DataTable dt = _CmnCompanyRepository.GetFromStoredProcedure(SPList.SecCompanyUser.GetSecCompanyUsersBySecUserId, parameters);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public SecCompany GetById(int Id)
        {


            return _CmnCompanyRepository.GetById(Id);

        }

        public IList<SecCompany> GetCmnCompanies()
        {
            return _CmnCompanyRepository.GetCmnCompanies();
        }
        public Operation UpdateCmnCompany(SecCompany objCmnCompany)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objCmnCompany.Id };
            _CmnCompanyRepository.Update(objCmnCompany);

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
        public Operation DeleteCmnCompany(SecCompany objCmnCompany)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objCmnCompany.Id };
            _CmnCompanyRepository.Delete(objCmnCompany);

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

        public Operation SaveCmnCompany(SecCompany objCmnCompany)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _CmnCompanyRepository.AddEntity(objCmnCompany);
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

        public IEnumerable<SecCompany> GetAll()
        {
            try
            {
                return _CmnCompanyRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetPrifix(int companyId)
        {
            string ret = string.Empty;
            SecCompany com = this.GetById(companyId);
           
            ret=com.Prefix;
            return ret;
        }
    }
}
