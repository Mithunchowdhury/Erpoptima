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
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;

namespace ERPOptima.Service.Common
{
    public interface ICmnFinancialYearService
    {

        CmnFinancialYear GetById(int id);
        void Add(CmnFinancialYear fy);
 
      
        Operation Save(CmnFinancialYear objCmnFinancialYear);
        Operation Update(CmnFinancialYear objCmnFinancialYear);
        Operation Delete(CmnFinancialYear objCmnFinancialYear);
        DataTable GetAll(int companyId, int moduleId);



        CmnFinancialYear GetFinancialYearDateRange(int financialYearId);

        int GetCurrentFinancialYearId(int p1, int p2);
    }
    public class CmnFinancialYearService : ICmnFinancialYearService
    {
        private ICmnFinancialYearRepository _cmnFinYearRepository;
        private IUnitOfWork _UnitOfWork;
        public CmnFinancialYearService(ICmnFinancialYearRepository cmnFinYearRepository, IUnitOfWork unitOfWork)
        {
            this._cmnFinYearRepository = cmnFinYearRepository;
            this._UnitOfWork = unitOfWork;
        }

        public CmnFinancialYear GetById(int id)
        {

            return _cmnFinYearRepository.GetById(id);


        }
        public Operation Save(CmnFinancialYear objCmnFinancialYear)
        {
            Operation objOperation = new Operation { Success = true,Message="Saved successfully." };

            long Id = _cmnFinYearRepository.AddEntity(objCmnFinancialYear);
            objOperation.OperationId = Id;

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
                objOperation.Message = "Save not successful.";
            }
            return objOperation;
        }
       
        public Operation Update(CmnFinancialYear objCmnFinancialYear)
        {
            Operation objOperation = new Operation { Success = true, Message = "Saved successfully." };
            _cmnFinYearRepository.Update(objCmnFinancialYear);

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception)
            {
                objOperation.Success = false;
                objOperation.Message = "Save not successful.";
            }
            return objOperation;
        }
        public Operation Delete(CmnFinancialYear objCmnFinancialYear)
        {
            Operation objOperation = new Operation { Success = true, Message = "Deleted successfully." };
            _cmnFinYearRepository.Delete(objCmnFinancialYear);

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception)
            {

                objOperation.Success = false;
                objOperation.Message = "Delete not successful.";
            }
            return objOperation;
        }
        public void Add(CmnFinancialYear fy)
        {
            _cmnFinYearRepository.AddEntity(fy);
        }

        //public void Update(CmnFinancialYear fy)
        //{
        //    _cmnFinYearRepository.UpdateEntity(fy);
        //}

        //public void Delete(CmnFinancialYear fy)
        //{
        //    _cmnFinYearRepository.DeleteEntity(fy);
        //}


       

        public DataTable GetAll(int companyId, int moduleId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@CmnCompanyId", companyId);
            parameters[1] = new SqlParameter("@SecModuleId", moduleId);
            dt = _cmnFinYearRepository.GetFromStoredProcedure(SPList.CmnFinancialYears.GetCmnFinancialYears, parameters);

            return dt;
        }

        public CmnFinancialYear GetFinancialYearDateRange(int financialYearId)
        {

           return  _cmnFinYearRepository.GetFInancialYearById(financialYearId);
        }

        public int GetCurrentFinancialYearId(int company, int module)
        {

            return _cmnFinYearRepository.GetCurrentFinancialYearId(company, module);
        }

   
    }
}
