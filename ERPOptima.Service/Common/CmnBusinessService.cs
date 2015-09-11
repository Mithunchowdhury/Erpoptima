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

    public interface ICmnBusinessService
    {

        //IList<CmnBusiness> GetCmnBusinesses(int companyId);
        // string GenerateChartOfAccountChildCode(string parentCode, string childCode, int Level, bool isLastNode);
        Operation SaveCmnBusiness(CmnBusiness objCmnBusiness);
        Operation DeleteCmnBusiness(CmnBusiness objCmnBusiness);
        CmnBusiness GetById(int Id);
        Operation UpdateCmnBusiness(CmnBusiness objCmnBusiness);
        //DataTable GetTransactionalHeadByProjectId(int projectId, int companyId, int financialYearId);
       // DataTable GetCmnBusinessByCompanyId(int CompanyId);
    }
    public class CmnBusinessService : ICmnBusinessService
    {
        private ICmnBusinessRepository _CmnBusinessRepository;
        private IUnitOfWork _UnitOfWork;
        public CmnBusinessService(ICmnBusinessRepository cmnBusinessRepository, IUnitOfWork unitOfWork)
        {
            this._CmnBusinessRepository = cmnBusinessRepository;
            this._UnitOfWork = unitOfWork;
        }

        //public IList<CmnBusiness> GetCmnBusinesses(int companyId)
        //{
        //    return _CmnBusinessRepository.GetCmnBusinesses(companyId);
        //}

        public CmnBusiness GetById(int Id)
        {
            CmnBusiness objCmnBusiness = _CmnBusinessRepository.GetById(Id);
            return objCmnBusiness;
        }
        public Operation UpdateCmnBusiness(CmnBusiness objCmnBusiness)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objCmnBusiness.Id };
            _CmnBusinessRepository.Update(objCmnBusiness);

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
        public Operation DeleteCmnBusiness(CmnBusiness objCmnBusiness)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objCmnBusiness.Id };
            _CmnBusinessRepository.Delete(objCmnBusiness);

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

        public Operation SaveCmnBusiness(CmnBusiness objCmnBusiness)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _CmnBusinessRepository.AddEntity(objCmnBusiness);
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

       //public DataTable GetTrialBalanceDetailsReport(int CompanyId, long financilaYearId, DateTime fromDate, DateTime toDate, long projectId, long costCenterId, bool yearClosingStatus)
        //{
        //    try
        //    {
        //        SqlParameter[] parameters = new SqlParameter[7];
        //        parameters[0] = new SqlParameter("@companyId", CompanyId);
        //        parameters[1] = new SqlParameter("@fid", financilaYearId);
        //        parameters[2] = new SqlParameter("@datefrom", fromDate);
        //        parameters[3] = new SqlParameter("@dateto", toDate);
        //        parameters[4] = new SqlParameter("@psid", projectId);
        //        parameters[5] = new SqlParameter("@costcentreid", null);
        //        parameters[6] = new SqlParameter("@pstatus", false);

        //        DataTable dt = _CmnBusinessRepository.GetFromStoredProcedure("RptAnFTrialBalanceDetails", parameters);
        //        return dt;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        //public DataTable GetTrialBalanceProjectWiseReport(int CompanyId, long financilaYearId, DateTime dateTime1, DateTime dateTime2)
        //{
        //    try
        //    {
        //        SqlParameter[] parameters = new SqlParameter[4];
        //        parameters[0] = new SqlParameter("@companyId", CompanyId);
        //        parameters[1] = new SqlParameter("@fid", financilaYearId);
        //        parameters[2] = new SqlParameter("@datefrom", dateTime1);
        //        parameters[3] = new SqlParameter("@dateto", dateTime2);


        //        DataTable dt = _CmnBusinessRepository.GetFromStoredProcedure("RptAnFTrialBalanceProjectWise", parameters);
        //        return dt;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

    }
}

