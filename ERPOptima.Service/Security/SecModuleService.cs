using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Security.Repository;
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

    public interface ISecModuleService
    {
        void Add(SecModule sc);
        void Save();
        DataTable GetModules();

       DataTable GetModuleByCompanyId(int companyId);
       DataTable GetSecPermittedModuleByUserId(int userId, int companyId);

       Operation SaveSecModule(SecModule objSecModule);
       Operation DeleteSecModule(SecModule objSecModule);
       Operation UpdateSecModule(SecModule objSecModule);
    }
    public class SecModuleService:ISecModuleService
    {
        private ISecModuleRepository _secModuleRepository;
        private IUnitOfWork _UnitOfWork;
        public SecModuleService(ISecModuleRepository secModuleRepository, IUnitOfWork unitOfWork)
        {
            this._secModuleRepository = secModuleRepository;
            this._UnitOfWork = unitOfWork;
        }


        public void Add(SecModule sm)
        {
            _secModuleRepository.Add(sm);
        }


        public void Save()
        {
            _UnitOfWork.Commit();
        }

        public DataTable GetModules()
        {
           DataTable dt = new DataTable();
           SqlParameter[] parameters = new SqlParameter[0];
        

           dt = _secModuleRepository.GetFromStoredProcedure(SPList.SecModule.GetSecModules, parameters);

         return dt;
        }
        public Operation SaveSecModule(SecModule objSecModule)
        {
            Operation objOperation = new Operation { Success = true };

            int Id = _secModuleRepository.AddEntity(objSecModule);
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

        public Operation UpdateSecModule(SecModule objSecModule)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSecModule.Id };
            _secModuleRepository.Update(objSecModule);

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
        public Operation DeleteSecModule(SecModule objSecModule)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objSecModule.Id };
            _secModuleRepository.Delete(objSecModule);

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

        public DataTable GetModuleByCompanyId(int companyId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@CompanyId", companyId);
            dt = _secModuleRepository.GetFromStoredProcedure(SPList.SecModule.GetSecModulesByCompanyId, parameters);
            return dt;
        }
        public DataTable GetSecPermittedModuleByUserId(int userId, int companyId) 
        {


            DataTable dt = new DataTable();
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SecUserId", userId);
            parameters[1] = new SqlParameter("@CmnCompanyId", companyId);
            dt = _secModuleRepository.GetFromStoredProcedure(SPList.SecModule.GetSecPermittedModuleByUserId, parameters);
            return dt;
        
        
        }
    }
}
