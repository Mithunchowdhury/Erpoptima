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

    public interface IAnFCostCenterService
    {

        IList<AnFCostCenter> GetCostCenters(int companyId);

        Operation SaveAnFCostCenter(AnFCostCenter objAnFCostCenter);
        Operation DeleteAnFCostCenter(AnFCostCenter objAnFCostCenter);
        AnFCostCenter GetById(int Id);
        Operation UpdateAnFCostCenter(AnFCostCenter objAnFCostCenter);

        DataTable GetCostCentersByCompanyId(int CompanyId);

        DataTable GetCostOfGoodSoldReport(int? CompanyId, int financilaYearId, DateTime dateFrom, DateTime dateTo);
        
        AnFCostCenter GetCostCenterById(long nullable);

        DataTable GetTrialBalanceDetailsReport(int CompanyId, int financilaYearId, DateTime fromDate, DateTime toDate, int costCenterId, bool yearClosingStatus);

        DataTable GetTrialBalanceProjectWiseReport(int CompanyId, long financilaYearId, DateTime dateTime1, DateTime dateTime2,int? businessId);
    }
    public class AnFCostCenterService: IAnFCostCenterService
    {       

        private IAnFCostCenterRepository _AnFCostCenterRepository;
        private IUnitOfWork _UnitOfWork;
        public AnFCostCenterService(IAnFCostCenterRepository anFCostCenterRepository, IUnitOfWork unitOfWork)
        {
            this._AnFCostCenterRepository = anFCostCenterRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<AnFCostCenter> GetCostCenters(int companyId)
        {
            return _AnFCostCenterRepository.GetCostCenters(companyId);
        }
       
        public AnFCostCenter GetById(int Id) {


            AnFCostCenter objAnFCostCenter = _AnFCostCenterRepository.GetById(Id);
            return objAnFCostCenter;
        
        }


        public Operation UpdateAnFCostCenter(AnFCostCenter objAnFCostCenter)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFCostCenter.Id };
            _AnFCostCenterRepository.Update(objAnFCostCenter);

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

        public Operation DeleteAnFCostCenter(AnFCostCenter objAnFCostCenter)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFCostCenter.Id };
            _AnFCostCenterRepository.Delete(objAnFCostCenter);

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

        public Operation SaveAnFCostCenter(AnFCostCenter objAnFCostCenter)
        {
            Operation objOperation = new Operation { Success=true};

            long Id = _AnFCostCenterRepository.AddEntity(objAnFCostCenter);
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
        public DataTable GetCostCentersByCompanyId(int CompanyId)
        {
            try
            {
                SqlParameter [] parameers = new SqlParameter[1];
                parameers[0] = new SqlParameter("@companyId",CompanyId);

                DataTable dt = _AnFCostCenterRepository.GetFromStoredProcedure(SPList.Report.RptAnFCostCentre, parameers);
                 return dt;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public DataTable GetCostOfGoodSoldReport(int? CompanyId, int financilaYearId, DateTime dateFrom, DateTime dateTo)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[4];
            paramsToStore[0] = new SqlParameter("@companyId", CompanyId);
            paramsToStore[1] = new SqlParameter("@fid", financilaYearId);
            paramsToStore[2] = new SqlParameter("@datefrom", dateFrom);
            paramsToStore[3] = new SqlParameter("@dateto", dateTo);

            try
            {
                dt = _AnFCostCenterRepository.GetFromStoredProcedure(SPList.Report.RptAnFCostOfGoodsSold, paramsToStore);
            }
            catch (Exception ex)
            {
                                
            }

            return dt;
        }

        public AnFCostCenter GetCostCenterById(long nullable)
        {
            return _AnFCostCenterRepository.GetCostCenterById(nullable);
        }
        public DataTable GetTrialBalanceDetailsReport(int CompanyId, int financilaYearId, DateTime fromDate, DateTime toDate, int costCenterId, bool yearClosingStatus)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@companyId", CompanyId);
                parameters[1] = new SqlParameter("@fid", financilaYearId);
                parameters[2] = new SqlParameter("@datefrom", fromDate);
                parameters[3] = new SqlParameter("@dateto", toDate);                
                parameters[4] = new SqlParameter("@costcentreid", null);
                parameters[5] = new SqlParameter("@pstatus", false);

                DataTable dt = _AnFCostCenterRepository.GetFromStoredProcedure(SPList.Report.RptAnFTrialBalanceDetails, parameters);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        
        }

        public DataTable GetTrialBalanceProjectWiseReport(int CompanyId, long financilaYearId, DateTime dateTime1, DateTime dateTime2,int? businessId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@companyId", CompanyId);
                parameters[1] = new SqlParameter("@fid", financilaYearId);
                parameters[2] = new SqlParameter("@datefrom", dateTime1);
                parameters[3] = new SqlParameter("@dateto", dateTime2);
                parameters[4] = new SqlParameter("@bsid", businessId);

                DataTable dt = _AnFCostCenterRepository.GetFromStoredProcedure(SPList.Report.RptAnFTrialBalanceProjectWise, parameters);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        
        }

    }
}
