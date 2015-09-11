using ERPOptima.Data.Accounts;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Model;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ERPOptima.Service.Accounts
{

    #region Interface
    public interface IChartOfAccountService
    {
        IList<AnFChartOfAccount> GetParentWithChild(int companyId);
 

        IEnumerable<AnFChartOfAccount> GetThirdLevel(int companyId);

        string GenerateChartOfAccountChildCode(string parentCode, string childCode, int Level, bool isLastNode);

        Operation SaveAnFChartOfAccount(AnFChartOfAccount anfChartOfAccount);

        Operation DeleteAnfChartOfAccount(AnFChartOfAccount objAnFChartOfAccount);

        AnFChartOfAccount GetById(long Id);

        Operation UpdateAnfChartOfAccount(AnFChartOfAccount objAnFChartOfAccount);

        DataTable GetTransactionalHeadByProjectId(int projectId, int companyId, int financialYearId);

        IEnumerable<AnFChartOfAccount> GetTransactionalHeadByCompanyId(int companyId);

        List<AnFChartOfAccount> GetByCompanyId(int CompanyId);

        DataTable GetAnFChartAccountsForReportByCompanyId(int CompanyId);

        DataTable GetAnFChartAccountsWithOpeningBalanceForReportByCompanyId(int CompanyId, int? financilaYearId);

        DataTable GetReportForNoteSchedule(int cmnFinancialYearId, int cmnCompanyId, string dateFrom, string dateTo, int? costCenterId, int anFChartOfAccountId);
        DataTable GetReportForNoteScheduleProject(int cmnFinancialYearId, int cmnCompanyId, int? businessId, string dateFrom, string dateTo, int anFChartOfAccountId);

        //DataTable GetAnFBalanceSheetReport(int CompanyId, long financilaYearId, DateTime dateTime1, DateTime dateTime2,int? b, long? p);
        DataTable GetAnFBalanceSheetReport(int? CompanyId, int financilaYearId, DateTime dateTime1, DateTime dateTime2);
    }
    #endregion


    #region Member
    public class ChartOfAccountService : IChartOfAccountService
    {
        private IChartOfAccountRepository _ChartOfAccountRepository;
        private IUnitOfWork _UnitOfWork;

        public ChartOfAccountService(IChartOfAccountRepository anfChartOfAccountRepository, IUnitOfWork unitOfWork)
        {
            this._ChartOfAccountRepository = anfChartOfAccountRepository;
            this._UnitOfWork = unitOfWork;
        }

        public IList<AnFChartOfAccount> GetParentWithChild(int companyId)
        {
            return _ChartOfAccountRepository.GetParentWithChild(companyId);
        }

        public IEnumerable<AnFChartOfAccount> GetThirdLevel(int companyId)
        {
            return _ChartOfAccountRepository.GetMany(coa => coa.CmnCompanyId == companyId && coa.DepthLevel == 3);
        
        }

        public AnFChartOfAccount GetById(long Id)
        {
            AnFChartOfAccount objAnFChartOfAccount = _ChartOfAccountRepository.GetById(Id);
            return objAnFChartOfAccount;
        }

        public Operation UpdateAnfChartOfAccount(AnFChartOfAccount objAnFChartOfAccount)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFChartOfAccount.Id };
            _ChartOfAccountRepository.Update(objAnFChartOfAccount);

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

        public Operation DeleteAnfChartOfAccount(AnFChartOfAccount objAnFChartOfAccount)
        {
            Operation objOperation = new Operation { Success = true, OperationId = objAnFChartOfAccount.Id };
            _ChartOfAccountRepository.Delete(objAnFChartOfAccount);

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

        public Operation SaveAnFChartOfAccount(AnFChartOfAccount objAnFChartOfAccount)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = _ChartOfAccountRepository.AddEntity(objAnFChartOfAccount);
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

        public DataTable GetTransactionalHeadByProjectId(int projectId, int companyId, int financialYearId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[3];

            paramsToStore[0] = new SqlParameter("@companyId", companyId);
            paramsToStore[1] = new SqlParameter("@projectID", projectId);
            paramsToStore[2] = new SqlParameter("@YearID", financialYearId);

            dt = _ChartOfAccountRepository.GetFromStoredProcedure(SPList.Common.GetAnFTransactinalHeadForMappingByProjectId, paramsToStore);

            return dt;
        }

        public IEnumerable<AnFChartOfAccount> GetTransactionalHeadByCompanyId(int companyId)
        {
            return _ChartOfAccountRepository.GetMany(coa => coa.CmnCompanyId == companyId && coa.IsTransactionalHead == true);
        }
        
        #region ChildCodeGeneration
        public string GenerateChartOfAccountChildCode(string parentCode, string childCode, int Level, bool isLastNode)
        {
            string format = "";
            int code;
            try
            {
                long number;
                if (childCode != "")
                    number = long.Parse(childCode);
                else
                    number = 0;

                if (Level == 0)
                {
                    if (string.IsNullOrEmpty(childCode))
                    {
                        format = String.Format("{0}01", parentCode);
                    }
                    else if ((!string.IsNullOrEmpty(childCode)) && (childCode.Substring(1, 2).CompareTo("99") < 0))
                    {
                        number = Convert.ToInt64(childCode);
                        number = number + 1;
                        format = Convert.ToString(number);
                    }
                }
                if (Level == 1)
                {
                    if (string.IsNullOrEmpty(childCode))
                    {
                        //format = "1";
                        code = 1;
                        //format = parentCode + "." + code.ToString();
                    }
                    else //if ((!string.IsNullOrEmpty(childCode)) && (childCode.Substring(0, 1).CompareTo("9") < 0))
                    {
                        //number = number + 10000;
                        //format = Convert.ToString(number);
                        //format = childCode + 1;

                        code = Convert.ToInt32(childCode) + 1;
                        format = code.ToString();
                    }
                }
                else if (Level == 2)
                {
                    if (string.IsNullOrEmpty(childCode))
                    {
                        if (Convert.ToInt32(parentCode) < 9)
                        {
                            format = parentCode.Substring(0, 1).ToString() + "00100";
                        }
                        else
                        {
                            format = parentCode.Substring(0, 1).ToString() + "0100";
                        }
                    }
                    else if ((!string.IsNullOrEmpty(childCode)) && (childCode.Substring(1, 2).CompareTo("999") < 0))
                    {
                        number = Convert.ToInt64(childCode.Substring(1, 2));
                        number = number + 1;
                        if (number < 10) format = parentCode.Substring(0, 2).ToString() + Convert.ToString(number) + "00";
                        else format = parentCode.Substring(0, 2).ToString() + Convert.ToString(number) + "00";
                    }
                }
                else if (Level == 3)
                {
                    if (string.IsNullOrEmpty(childCode))
                    {
                        format = parentCode.Substring(0, 3) + "01";
                    }
                    else if ((!string.IsNullOrEmpty(childCode)) && (childCode.Substring(3, 2).CompareTo("999") < 0))
                    {
                        number = Convert.ToInt64(childCode.Substring(3, 2));
                        number = number + 1;
                        if (number < 10) format = parentCode.Substring(0, 3) + "0" + Convert.ToString(number);
                        else format = parentCode.Substring(0, 3) + Convert.ToString(number);
                    }
                }
                else if (Level == 4)
                {
                    if (string.IsNullOrEmpty(childCode) && isLastNode == false)
                    {
                        format = String.Format("{0}01", parentCode);
                    }
                    else if (string.IsNullOrEmpty(childCode) && isLastNode == true)
                    {
                        format = String.Format("{0}0000001", parentCode);
                    }
                    else if (!string.IsNullOrEmpty(childCode) && (isLastNode == false))
                    {
                        if (childCode.Substring(5, 2).CompareTo("99") < 0)
                        {
                            number = number + 1;
                            format = Convert.ToString(number);
                        }
                    }
                    else if (!string.IsNullOrEmpty(childCode) && (isLastNode == true))
                    {
                        if (childCode.Length == 12 && childCode.Substring(5, 7).CompareTo("99999") < 0)
                        {
                            number = number + 1;
                            format = Convert.ToString(number);
                        }
                        else /////
                        {
                            number = number + 1;
                            format = Convert.ToString(number);
                        }
                    }
                }
                else if (Level == 5)
                {
                    if (string.IsNullOrEmpty(childCode) && isLastNode == false)
                    {
                        format = String.Format("{0}01", parentCode);
                    }
                    else if (string.IsNullOrEmpty(childCode) && isLastNode == true)
                    {
                        format = String.Format("{0}00001", parentCode);
                    }
                    else if ((!string.IsNullOrEmpty(childCode)) && (isLastNode == false))
                    {
                        if (childCode.Substring(7, 2).CompareTo("99") < 0)
                        {
                            number = number + 1;
                            format = Convert.ToString(number);
                        }
                    }
                    else if ((!string.IsNullOrEmpty(childCode)) && (isLastNode == true))
                    {
                        if (childCode.Length == 12 && childCode.Substring(7, 5).CompareTo("99999") < 0)
                        {
                            number = number + 1;
                            format = Convert.ToString(number);
                        }
                        else /////
                        {
                            number = number + 1;
                            format = Convert.ToString(number);
                        }
                    }
                }
                else if (Level == 6)
                {
                    if (string.IsNullOrEmpty(childCode))
                    {
                        format = String.Format("{0}001", parentCode);
                    }
                    else if ((!string.IsNullOrEmpty(childCode)) && (childCode.Substring(9, 3).CompareTo("999") < 0))
                    {
                        number = number + 1;
                        format = Convert.ToString(number);
                    }
                }

                return format;
            }
            catch (Exception ex)
            {
                return format;
            }
        }

        #endregion

        public List<AnFChartOfAccount> GetByCompanyId(int CompanyId)
        {
            return _ChartOfAccountRepository.GetByCompanyId(CompanyId);
        }

        public DataTable GetAnFChartAccountsForReportByCompanyId(int CompanyId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[1];

            paramsToStore[0] = new SqlParameter("@companyId", CompanyId);

            dt = _ChartOfAccountRepository.GetFromStoredProcedure(SPList.Report.RptAnFChartsofAccount, paramsToStore);

            return dt;
        }

        public DataTable GetAnFChartAccountsWithOpeningBalanceForReportByCompanyId(int CompanyId, int? financilaYearId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[2];

            paramsToStore[0] = new SqlParameter("@companyId", CompanyId);
            paramsToStore[1] = new SqlParameter("@fid", financilaYearId);


            dt = _ChartOfAccountRepository.GetFromStoredProcedure(SPList.Report.RptAnFOpenningBalance, paramsToStore);

            return dt;
        }

        #region  Report NoteSchedule

        public DataTable GetReportForNoteSchedule(int cmnFinancialYearId, int cmnCompanyId, string dateFrom, string dateTo, int? costCenterId, int anFChartOfAccountId)
        {

            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[6];
            paramsToStore[0] = new SqlParameter("@fid", cmnFinancialYearId);
            paramsToStore[1] = new SqlParameter("@companyId", cmnCompanyId);        
            paramsToStore[2] = new SqlParameter("@Id", anFChartOfAccountId);
            paramsToStore[3] = new SqlParameter("@datefrom", dateFrom);
            paramsToStore[4] = new SqlParameter("@dateto", dateTo);
            paramsToStore[5] = new SqlParameter("@costcentreid", costCenterId);


            try
            {
                dt = _ChartOfAccountRepository.GetFromStoredProcedure(SPList.Report.RptAnFNoteSchedule, paramsToStore);
            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;

        }

        public DataTable GetReportForNoteScheduleProject(int cmnFinancialYearId, int cmnCompanyId,int? businessId, string dateFrom, string dateTo, int anFChartOfAccountId)
        {

            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[6];
            paramsToStore[0] = new SqlParameter("@fid", cmnFinancialYearId);
            paramsToStore[1] = new SqlParameter("@companyId", cmnCompanyId);
            paramsToStore[2] = new SqlParameter("@bsid", businessId);
            paramsToStore[3] = new SqlParameter("@Id", anFChartOfAccountId);
            paramsToStore[4] = new SqlParameter("@datefrom", dateFrom);
            paramsToStore[5] = new SqlParameter("@dateto", dateTo);


            try
            {
                dt = _ChartOfAccountRepository.GetFromStoredProcedure(SPList.Report.RptAnFNoteScheduleProject, paramsToStore);
            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;

        }

        //public DataTable GetAnFBalanceSheetReport(int CompanyId, long financilaYearId, DateTime dateTime1, DateTime dateTime2, int? b,long? p)
        public DataTable GetAnFBalanceSheetReport(int? CompanyId, int financilaYearId, DateTime dateTime1, DateTime dateTime2)
        {

            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[4];
            paramsToStore[0] = new SqlParameter("@companyId", CompanyId);
            paramsToStore[1] = new SqlParameter("@fid", financilaYearId);
            paramsToStore[2] = new SqlParameter("@datefrom", dateTime1);
            paramsToStore[3] = new SqlParameter("@dateto", dateTime2);
            
            try
            {
                dt = _ChartOfAccountRepository.GetFromStoredProcedure(SPList.Report.RptAnFBalanceSheet, paramsToStore);
            }
            catch (Exception ex)
            {
                //dt = null;
            }

            return dt;
        
        }

        #endregion

    }

    #endregion
}
