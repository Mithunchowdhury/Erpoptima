using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Accounts;
using ERPOptima.Model.Common;
using ERPOptima.Service.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPOptima.Service.Accounts
{
    #region interface

    public interface IAnFVoucherService
    {
        IList<AnFVoucher> Get();

        void Save();

        int GetTotalByCompanyId(int companyId, int financialYearId, int voucherType);

        int GetTotalCancelByCompanyIdAndType(int companyId, int financialYearId, int type);

        int GetTotalPostedByCompanyIdAndType(int companyId, int financialYearId, int type);

        int GetTotalUnPostedByCompanyIdAndType(int companyId, int financialYearId, int type);

        //int GetNoOfUnapproved(int companyId, int processId, int levelId, CmnApprovalProcess approvalProcess);

        DataTable GetTranasactionalHeadsByCompanyId(int companyId);

        string GetNewVoucherNo(int companyId, string prefix, int finyearid, int businessId, int p, DateTime dateTime);

        // For Report

        DataTable GetAnfVoucherList(int cmnFinancialYearId, int cmnCompanyId, int? voucherTypeId, DateTime? dateFrom, DateTime? dateTo, int? anfCostCenterId);

        DataTable GetShortDetailAnFVoucherList(int cmnFinancialYearId, int cmnCompanyId, int? voucherTypeId, DateTime? dateFrom, DateTime? dateTo, int? anfCostCenterId);

        DataTable GetAnfVoucher(long Id);

        int SaveVoucher(AnFVoucher v);
     

        DataTable GetByParameters(int CmnCompanyId, int CmnFinancialYearId, int? nullable1, long? nullable2, DateTime? nullable3, DateTime? nullable4, bool p);

        DataTable GetGeneralLedgerForReport(int cmnFinancialYearId, int cmnCompanyId, string dateFrom, string dateTo, int? CostcenterId, int AnFChartOfAccountId, bool status);
        DataTable GetProjectGeneralLedgerForReport(int cmnFinancialYearId, int cmnCompanyId, string dateFrom, string dateTo, int? businessId, int? projectId, int anFChartOfAccountId, bool status);
        DataTable GetProfitLossForReport(int? CompanyId, int financilaYearId, DateTime dateFrom, DateTime dateTo);
        DataTable GetPaymentVouchers(int cmnFinancialYearId, int cmnCompanyId, int? voucherTypeId, DateTime? dateFrom, DateTime? dateTo, int? anfCostCenterId);
        DataTable GetReceiptVouchers(int cmnFinancialYearId, int cmnCompanyId, int? voucherTypeId, DateTime? dateFrom, DateTime? dateTo, int? anfCostCenterId);
        
        int UpdateVoucher(AnFVoucher v);

        AnFVoucher GetAnfVoucherById(int Id);

        
        Collection<AnFVoucher> GetForApproval(int companyId, int financialYear, int processId, int levelId, DateTime? startDate, DateTime? endDate, int? businessId, int? projectId, Collection<CmnProcessLevel> collection);

        IList<AnFVoucher> SearchVoucher(int companyId, int financialYearId, DateTime? dateFrom, DateTime? toDate, int? type, int? CostcenterId);

        DataTable GetAnFReceivableReport(int financialYearId, int companyId, int? costCenterId);
        DataTable GetAnFPayableReport(int financialYearId, int companyId, int? costCenterId);
    }

    #endregion interface

    public class AnFVoucherService : IAnFVoucherService
    {
        private IAnFVoucherRepository _AnFVoucherRepository;
        private ICmnApprovalProcessService _cmnApprovalProcess;

        private IUnitOfWork _UnitOfWork;

        public AnFVoucherService(IAnFVoucherRepository anFVoucherRepository, IUnitOfWork unitOfWork)
        {
            this._AnFVoucherRepository = anFVoucherRepository;
            this._UnitOfWork = unitOfWork;

        }

        public void Save()
        {
            _UnitOfWork.Commit();
        }

        public int GetTotalByCompanyId(int companyId, int financialYearId, int voucherType)
        {
            return _AnFVoucherRepository.GetTotalByCompanyIdAndType(companyId, financialYearId, voucherType);
        }

        IList<AnFVoucher> IAnFVoucherService.Get()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCancelByCompanyIdAndType(int companyId, int financialYearId, int type)
        {
            return _AnFVoucherRepository.GetTotalCancelByCompanyIdAndType(companyId, financialYearId, type);
        }

        public int GetTotalPostedByCompanyIdAndType(int companyId, int financialYearId, int type)
        {
            return _AnFVoucherRepository.GetTotalPostedByCompanyIdAndType(companyId, financialYearId, type);
        }

        public int GetTotalUnPostedByCompanyIdAndType(int companyId, int financialYearId, int type)
        {
            return _AnFVoucherRepository.GetTotalUnPostedByCompanyIdAndType(companyId, financialYearId, type);
        }

        public DataTable GetTranasactionalHeadsByCompanyId(int companyId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[1];
            paramsToStore[0] = new SqlParameter("@companyId", companyId);
            try
            {
                dt = _AnFVoucherRepository.GetFromStoredProcedure(SPList.AnFChartOfAccounts.GetAnFTransactinalHeadByCmnCompanyId, paramsToStore);
                return dt;
            }
            catch (Exception)
            {
                throw new ArgumentNullException();
            }
        }

        public DataTable GetAnfVoucherList(int cmnFinancialYearId, int cmnCompanyId, int? voucherTypeId, DateTime? dateFrom, DateTime? dateTo, int? anfCostCenterId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[6];
            paramsToStore[0] = new SqlParameter("@CmnFinancialYearId", cmnFinancialYearId);
            paramsToStore[1] = new SqlParameter("@CmnCompanyId", cmnCompanyId);
            paramsToStore[2] = new SqlParameter("@AnFVoucherTypeId", voucherTypeId);
            paramsToStore[3] = new SqlParameter("@StartDate", dateFrom);
            paramsToStore[4] = new SqlParameter("@EndDate", dateTo);
            paramsToStore[5] = new SqlParameter("@AnFCostCenterId", anfCostCenterId);
           
            try
            {
                dt = _AnFVoucherRepository.GetFromStoredProcedure(SPList.Report.RptAnFVoucherList, paramsToStore);
            }
            catch (Exception)
            {
                throw new ArgumentNullException();
            }

            return dt;
        }

        // For Payment Voucher
        public DataTable GetPaymentVouchers(int cmnFinancialYearId, int cmnCompanyId, int? voucherTypeId, DateTime? dateFrom, DateTime? dateTo, int? anfCostCenterId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[6];
            paramsToStore[0] = new SqlParameter("@CmnFinancialYearId", cmnFinancialYearId);
            paramsToStore[1] = new SqlParameter("@CmnCompanyId", cmnCompanyId);
            paramsToStore[2] = new SqlParameter("@AnFVoucherTypeId", voucherTypeId);
            paramsToStore[3] = new SqlParameter("@StartDate", dateFrom);
            paramsToStore[4] = new SqlParameter("@EndDate", dateTo);
            paramsToStore[5] = new SqlParameter("@AnFCostCenterId", anfCostCenterId);

            try
            {
                dt = _AnFVoucherRepository.GetFromStoredProcedure(SPList.Report.RptPaymentVouchers, paramsToStore);
            }
            catch (Exception)
            {
                throw new ArgumentNullException();
            }

            return dt;
        }

        // For Receipt Voucher
        public DataTable GetReceiptVouchers(int cmnFinancialYearId, int cmnCompanyId, int? voucherTypeId, DateTime? dateFrom, DateTime? dateTo, int? anfCostCenterId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[6];
            paramsToStore[0] = new SqlParameter("@CmnFinancialYearId", cmnFinancialYearId);
            paramsToStore[1] = new SqlParameter("@CmnCompanyId", cmnCompanyId);
            paramsToStore[2] = new SqlParameter("@AnFVoucherTypeId", voucherTypeId);
            paramsToStore[3] = new SqlParameter("@StartDate", dateFrom);
            paramsToStore[4] = new SqlParameter("@EndDate", dateTo);
            paramsToStore[5] = new SqlParameter("@AnFCostCenterId", anfCostCenterId);

            try
            {
                dt = _AnFVoucherRepository.GetFromStoredProcedure(SPList.Report.RptReceiptVouchers, paramsToStore);
            }
            catch (Exception)
            {
                throw new ArgumentNullException();
            }

            return dt;
        }
        public DataTable GetShortDetailAnFVoucherList(int cmnFinancialYearId, int cmnCompanyId, int? voucherTypeId, DateTime? dateFrom, DateTime? dateTo, int? anfCostCenterId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[6];
            paramsToStore[0] = new SqlParameter("@CmnFinancialYearId", cmnFinancialYearId);
            paramsToStore[1] = new SqlParameter("@CmnCompanyId", cmnCompanyId);
            paramsToStore[2] = new SqlParameter("@AnFVoucherTypeId", voucherTypeId);
            paramsToStore[3] = new SqlParameter("@StartDate", dateFrom);
            paramsToStore[4] = new SqlParameter("@EndDate", dateTo);
            paramsToStore[5] = new SqlParameter("@AnFCostCenterId", anfCostCenterId);

            try
            {
                dt = _AnFVoucherRepository.GetFromStoredProcedure(SPList.Report.RptAnFVoucherDetailsList, paramsToStore);
            }
            catch (Exception)
            {
            }

            return dt;
        }

        public DataTable GetAnfVoucher(long Id)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[1];
            paramsToStore[0] = new SqlParameter("@Id ", Id);

            try
            {
                dt = _AnFVoucherRepository.GetFromStoredProcedure(SPList.Report.RptAnFVoucherDetailsByVoucherId, paramsToStore);
            }
            catch (Exception)
            {
            }

            return dt;
        }

        public AnFVoucher GetAnfVoucherById(int Id)
        {
            try
            {
                AnFVoucher record = null;
                DataTable dt = GetAnfVoucher(Id);
                if (dt != null && dt.Rows.Count > 0)
                {
                    record = new AnFVoucher();
                    record = (AnFVoucher)Helper.FillTo(dt.Rows[0], typeof(AnFVoucher));
                }
                return record;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetNewVoucherNo(int companyId,string prefix, int finyearid, int businessId,int p, DateTime dateTime)
        {
            DataTable dt = null;

            SqlParameter[] paramsToStore = new SqlParameter[6];

            paramsToStore[0] = new SqlParameter("@vouhcerType", p);
            paramsToStore[1] = new SqlParameter("@Prefix", prefix);
            paramsToStore[2] = new SqlParameter("@companyId", companyId);
            paramsToStore[3] = new SqlParameter("@CmnBusinessId", businessId);
            paramsToStore[4] = new SqlParameter("@financialYearId", finyearid);
            paramsToStore[5] = new SqlParameter("@date", dateTime);

            try
            {
                dt = _AnFVoucherRepository.GetFromStoredProcedure(SPList.AnFVoucher.GetAnFVoucherNo, paramsToStore);

                if (dt.Rows.Count > 0)
                    return dt.Rows[0]["VoucherNumber"] != null ? dt.Rows[0]["VoucherNumber"].ToString() : "";
                else
                    return string.Empty;
            }
            catch (Exception)
            {
                throw new ArgumentNullException();
            }
        }

        //---------------------For Voucher Search---------------------
        public IList<AnFVoucher> SearchVoucher(int companyId, int financialYearId, DateTime? dateFrom, DateTime? toDate, int? type, int? CostcenterId)
        {
            IList<AnFVoucher> list = new List<AnFVoucher>();
            //Logic
            if (dateFrom == null && toDate == null)
            {
                if (type == 0 && CostcenterId == 0)
                {
                    var innerlist = _AnFVoucherRepository.GetAll().ToList();
                    list = innerlist.Where(t => t.CmnCompanyId == companyId && t.CmnFinancialYearId == financialYearId).ToList();
                    
                }
                else
                {
                    var innerlist = _AnFVoucherRepository.GetAll().ToList();
                    list = innerlist.Where(t => t.CmnCompanyId == companyId && t.CmnFinancialYearId == financialYearId && t.Type == type && t.AnFCostCenterId == CostcenterId).ToList();

                }
            }

            else if (dateFrom != null && toDate == null)
            {
                toDate = DateTime.Now;
                var innerlist = _AnFVoucherRepository.GetAll().ToList();
                list = innerlist.Where(t => t.CmnCompanyId == companyId && t.CmnFinancialYearId == financialYearId && t.Type == type && t.AnFCostCenterId == CostcenterId && t.Date >= dateFrom && t.Date <= toDate).ToList();
            }
            else if (dateFrom == null && toDate != null)
            {
                //dateFrom = toDate;
                var innerlist = _AnFVoucherRepository.GetAll().ToList();
                list = innerlist.Where(t => t.CmnCompanyId == companyId && t.CmnFinancialYearId == financialYearId && t.Type == type && t.AnFCostCenterId == CostcenterId && t.Date <= toDate).ToList();
            }
            else
            {
                if (type == 0)
                {
                    var innerlist = _AnFVoucherRepository.GetAll().ToList();
                    list = innerlist.Where(t => t.CmnCompanyId == companyId && t.CmnFinancialYearId == financialYearId && t.AnFCostCenterId == CostcenterId && t.Date >= dateFrom && t.Date <= toDate).ToList();

                }
                else
                {
                    var innerlist = _AnFVoucherRepository.GetAll().ToList();
                    list = innerlist.Where(t => t.CmnCompanyId == companyId && t.CmnFinancialYearId == financialYearId && t.Type == type && t.AnFCostCenterId == CostcenterId && t.Date >= dateFrom && t.Date <= toDate).ToList();

                }
            }

            return list;
        }  // End of Voucher Search
        public int SaveVoucher(AnFVoucher v)
        {

    
            int Id = _AnFVoucherRepository.AddEntity(v);
       
            try
            {
                //  _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
            }
            return Id;
        }

        public int UpdateVoucher(AnFVoucher v) {

            int ret;
            ret= _AnFVoucherRepository.UpdateEntity(v);           
            return ret;
        }

       

        public DataTable GetByParameters(int CmnCompanyId, int CmnFinancialYearId, int? type, long? projectId, DateTime? fromDate, DateTime? toDate, bool p)
        {
            SqlParameter[] paramsToStore = new SqlParameter[7];
            paramsToStore[0] = new SqlParameter("@CmnFinancialYearId", CmnFinancialYearId);
            paramsToStore[1] = new SqlParameter("@CmnCompanyId", CmnCompanyId);
            paramsToStore[2] = new SqlParameter("@Type", type);
            paramsToStore[3] = new SqlParameter("@CmnProjectId", projectId);
            paramsToStore[4] = new SqlParameter("@StartDate", fromDate);
            paramsToStore[5] = new SqlParameter("@EndDate", toDate);
            paramsToStore[6] = new SqlParameter("@IsHeadOffice", p);
            DataTable dt = _AnFVoucherRepository.GetFromStoredProcedure(SPList.AnFVoucher.SearchAnFVouchers, paramsToStore);

            return dt;
        }

        // For Voucher Search
        public DataTable GetVSByParameters(int? CmnCompanyId, int CmnFinancialYearId, int? type, DateTime? dateFrom, DateTime? toDate)
        {
            SqlParameter[] paramsToStore = new SqlParameter[7];
            paramsToStore[0] = new SqlParameter("@CmnFinancialYearId", CmnFinancialYearId);
            paramsToStore[1] = new SqlParameter("@CmnCompanyId", CmnCompanyId);
            paramsToStore[2] = new SqlParameter("@Type", type);
            paramsToStore[4] = new SqlParameter("@StartDate", dateFrom);
            paramsToStore[5] = new SqlParameter("@EndDate", toDate);
            DataTable dt = _AnFVoucherRepository.GetFromStoredProcedure(SPList.AnFVoucher.AnFVoucherSearch, paramsToStore);

            return dt;
        }


        ////

        #region LedgerReport

        public DataTable GetGeneralLedgerForReport(int cmnFinancialYearId, int cmnCompanyId, string dateFrom, string dateTo, int? costcenterId, int anFChartOfAccountId, bool status)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[7];
            paramsToStore[0] = new SqlParameter("@fid", cmnFinancialYearId);
            paramsToStore[1] = new SqlParameter("@companyId", cmnCompanyId);
            //paramsToStore[2] = new SqlParameter("@bsid", businessId);
            //paramsToStore[3] = new SqlParameter("@psid", projectId);
            paramsToStore[2] = new SqlParameter("@Id", anFChartOfAccountId);
            paramsToStore[3] = new SqlParameter("@costcentreid", costcenterId);
            paramsToStore[4] = new SqlParameter("@datefrom", dateFrom);
            paramsToStore[5] = new SqlParameter("@dateto", dateTo);
            paramsToStore[6] = new SqlParameter("@PStatus", status);

            try
            {
                dt = _AnFVoucherRepository.GetFromStoredProcedure(SPList.Report.RptAnFGeneralLedger, paramsToStore);
            }
            catch (Exception ex)
            {
            }

            return dt;
        }

        public DataTable GetProjectGeneralLedgerForReport(int cmnFinancialYearId, int cmnCompanyId, string dateFrom, string dateTo, int? businessId, int? projectId, int anFChartOfAccountId, bool status)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[7];
            paramsToStore[0] = new SqlParameter("@fid", cmnFinancialYearId);
            paramsToStore[1] = new SqlParameter("@companyId", cmnCompanyId);
            paramsToStore[2] = new SqlParameter("@bsid", businessId);
            paramsToStore[3] = new SqlParameter("@psid", projectId);
            paramsToStore[4] = new SqlParameter("@Id", anFChartOfAccountId);
            paramsToStore[5] = new SqlParameter("@datefrom", dateFrom);
            paramsToStore[6] = new SqlParameter("@dateto", dateTo);
            

            try
            {
                dt = _AnFVoucherRepository.GetFromStoredProcedure(SPList.Report.RptAnFGeneralLedgerProject, paramsToStore);
            }
            catch (Exception ex)
            {
            }

            return dt;
        }

        #endregion LedgerReport

        #region Report Profit Loss

        public DataTable GetProfitLossForReport(int? CompanyId, int financilaYearId, DateTime dateFrom, DateTime dateTo)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[4];
            paramsToStore[0] = new SqlParameter("@companyId", CompanyId);
            paramsToStore[1] = new SqlParameter("@fid", financilaYearId);
            paramsToStore[2] = new SqlParameter("@datefrom", dateFrom);
            paramsToStore[3] = new SqlParameter("@dateto", dateTo);
            
            try
            {
                dt = _AnFVoucherRepository.GetFromStoredProcedure(SPList.Report.RptAnFProfitLoss, paramsToStore);
            }
            catch (Exception ex)
            {
               
            }

            return dt;
        }


        public Collection<AnFVoucher> GetForApproval(int companyId, int financialYear, int processId, int levelId, DateTime? startDate, DateTime? endDate, int? businessId, int? segmentId, Collection<CmnProcessLevel> records)
        //public Collection<AnFVoucher> GetForApproval(int companyId, int yr, int processId, int levelId, DateTime startDate, DateTime endDate, int businessId, int segmentId, Collection<CmnProcessLevel> records)
        {

            //Collection<CmnProcessLevel> records = bll.GetAll(companyId, EnumClass.DepartmentId, processId);
            IList<int> pre = new List<int>();
            IList<int> post = new List<int>();
            int index = 0;
            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].Id == levelId)
                {
                    index = i;
                }
            }
            for (int i = 0; i < records.Count; i++)
            {
                if (index > i)
                {
                    pre.Add((int)records[i].Id);
                }
                else if (index < i)
                {
                    post.Add((int)records[i].Id);
                }
            }
            //int segmentId;
            string sqlText = string.Empty;
            //if (segment == "")
            //{
            //    segmentId = 0;
            //}
            //else
            //{
            //    sqlText += "SELECT Id FROM CmnProjectSegments WHERE Name = " + "'" + segment + "'";
            //    sqlText += "AND CmnBusinessSectorId IN (";
            //    sqlText += "SELECT Id From CmnBusinessSectors WHERE Name = " + "'" + sector + "'";
            //    sqlText += "AND CmnCompanyId = " + companyId.ToString() + ")";
            //    DataTable dtsegment = AnFVoucherDLL.ExecuteSql(sqlText);
            //    segmentId = int.Parse(dtsegment.Rows[0][0].ToString());
            //}

            sqlText = string.Empty;
            if (segmentId != 0)
            {
                sqlText += "SELECT  RefId FROM CmnApprovals ";
                sqlText += "WHERE ((Value IS NULL) OR  (Value = 0)) AND (CmnCompanyId = " + companyId.ToString() + ") AND (CmnProcessLevelId = " + levelId.ToString() + ") AND (CmnApprovalProcessId = " + processId.ToString() + ") ";
                foreach (int i in pre)
                {
                    string lvlId = "pre" + i.ToString();
                    sqlText += "OR 1=";
                    sqlText += "(";
                    sqlText += "SELECT Value FROM CmnApprovals as " + lvlId + " ";
                    sqlText += "WHERE (" + lvlId + ".CmnApprovalProcessId = " + processId.ToString() + ") AND (" + lvlId + ".CmnProcessLevelId = " + i.ToString() + ") AND (" + lvlId + ".CmnCompanyId = " + companyId.ToString() + ") AND (Value = 1) AND " + lvlId + ".RefId=CmnApprovals.RefId";
                    sqlText += ") ";
                }
                foreach (int i in post)
                {
                    string lvlId = "post" + i.ToString();
                    sqlText += "OR 0=";
                    sqlText += "(";
                    sqlText += "SELECT Value FROM CmnApprovals as " + lvlId + " ";
                    sqlText += "WHERE (" + lvlId + ".CmnApprovalProcessId = " + processId.ToString() + ") AND (" + lvlId + ".CmnProcessLevelId = " + i.ToString() + ") AND (" + lvlId + ".CmnCompanyId = " + companyId.ToString() + ") AND (Value = 0) AND " + lvlId + ".RefId=CmnApprovals.RefId";
                    sqlText += ") ";
                }

                sqlText = "SELECT DISTINCT AnFVouchers.*,convert(int,SUBSTRING(dbo.AnFVouchers.VoucherNumber,8,LEN(dbo.AnFVouchers.VoucherNumber)-13)) as vnum FROM AnFVouchers,(" + sqlText + ") AS Approvals " + " WHERE Approvals.RefId = AnFVouchers.Id AND AnFVouchers.IsPosted = 0 AND AnFVouchers.IsCancel = 0 AND AnFVouchers.CmnCompanyId=" + companyId.ToString();
                sqlText += " AND CmnFinancialYearId= " + financialYear.ToString();
                if (startDate != null && endDate != null)
                {
                    sqlText += " AND convert(date,AnFVouchers.Date,101) BETWEEN  '" + startDate.Value.ToString("yyyy-MM-dd") + "'" + " AND '" + endDate.Value.ToString("yyyy-MM-dd") + "'";
                }
                //else
                //{
                //    sqlText += " AND convert(date,AnFVouchers.Date,101) BETWEEN  null  AND null";
                //}
                if (segmentId == null)
                {
                    sqlText += "  AND AnFVouchers.Id In (Select AnFVoucherId From AnFVoucherDetails) order by vnum";

                }
                else
                {
                    sqlText += "  AND AnFVouchers.Id In (Select AnFVoucherId From AnFVoucherDetails Where CmnProjectId=" + segmentId + ") order by vnum";
                }
            }
            else
            {
                sqlText += "SELECT  RefId FROM CmnApprovals ";
                sqlText += "WHERE ((Value IS NULL) OR  (Value = 0)) AND (CmnCompanyId = " + companyId.ToString() + ") AND (CmnProcessLevelId = " + levelId.ToString() + ") AND (CmnApprovalProcessId = " + processId.ToString() + ") ";
                foreach (int i in pre)
                {
                    string lvlId = "pre" + i.ToString();
                    sqlText += "OR 1=";
                    sqlText += "(";
                    sqlText += "SELECT Value FROM CmnApprovals as " + lvlId + " ";
                    sqlText += "WHERE (" + lvlId + ".CmnApprovalProcessId = " + processId.ToString() + ") AND (" + lvlId + ".CmnProcessLevelId = " + i.ToString() + ") AND (" + lvlId + ".CmnCompanyId = " + companyId.ToString() + ") AND (Value = 1) AND " + lvlId + ".RefId=CmnApprovals.RefId";
                    sqlText += ") ";
                }
                foreach (int i in post)
                {
                    string lvlId = "post" + i.ToString();
                    sqlText += "OR 0=";
                    sqlText += "(";
                    sqlText += "SELECT Value FROM CmnApprovals as " + lvlId + " ";
                    sqlText += "WHERE (" + lvlId + ".CmnApprovalProcessId = " + processId.ToString() + ") AND (" + lvlId + ".CmnProcessLevelId = " + i.ToString() + ") AND (" + lvlId + ".CmnCompanyId = " + companyId.ToString() + ") AND (Value = 0) AND " + lvlId + ".RefId=CmnApprovals.RefId";
                    sqlText += ") ";
                }

                sqlText = "SELECT DISTINCT AnFVouchers.*,convert(int,SUBSTRING(dbo.AnFVouchers.VoucherNumber,8,LEN(dbo.AnFVouchers.VoucherNumber)-13)) as vnum FROM AnFVouchers,(" + sqlText + ") AS Approvals " + " WHERE Approvals.RefId = AnFVouchers.Id AND AnFVouchers.IsPosted = 0 AND AnFVouchers.IsCancel = 0 AND AnFVouchers.CmnCompanyId=" + companyId.ToString();
                sqlText += " AND CmnFinancialYearId= " + financialYear.ToString()  ;
                if (startDate != null && endDate != null)
                {
                    sqlText += " AND convert(date,AnFVouchers.Date,101) BETWEEN  '" + startDate.Value.ToString("yyyy-MM-dd") + "'" + " AND '" + endDate.Value.ToString("yyyy-MM-dd") + "'";
                }
                //else
                //{
                //    sqlText += " AND convert(date,AnFVouchers.Date,101) BETWEEN  null  AND null";
                //}
                sqlText += " order by vnum";
                
            }
            Collection<AnFVoucher> vouchers = null;
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[1];

                paramsToStore[0] = new SqlParameter("@sql", sqlText);
                
                DataTable dt = _AnFVoucherRepository.GetFromStoredProcedure(SPList.AnFVoucher.ExecuteSqlText, paramsToStore);
                if (dt != null)
                {
                    vouchers = new Collection<AnFVoucher>();
                    foreach (DataRow row in dt.Rows)
                    {
                        vouchers.Add((AnFVoucher)Helper.FillTo(row, typeof(AnFVoucher)));
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vouchers;
        }




        #endregion

        #region ReceivableReport
        public DataTable GetAnFReceivableReport(int cmnFinancialYearId, int cmnCompanyId, int? anfCostCenterId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[3];
            paramsToStore[0] = new SqlParameter("@CmnFinancialYearId", cmnFinancialYearId);
            paramsToStore[1] = new SqlParameter("@CmnCompanyId", cmnCompanyId);
            paramsToStore[2] = new SqlParameter("@AnFCostCenterId", anfCostCenterId);

            try
            {
                dt = _AnFVoucherRepository.GetFromStoredProcedure(SPList.Report.RptReceivableReport, paramsToStore);
            }
            catch (Exception)
            {
                throw new ArgumentNullException();
            }

            return dt;
        }
        #endregion ReceivableReport

        #region PayableReport
        public DataTable GetAnFPayableReport(int cmnFinancialYearId, int cmnCompanyId, int? anfCostCenterId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[3];
            paramsToStore[0] = new SqlParameter("@CmnFinancialYearId", cmnFinancialYearId);
            paramsToStore[1] = new SqlParameter("@CmnCompanyId", cmnCompanyId);
            paramsToStore[2] = new SqlParameter("@AnFCostCenterId", anfCostCenterId);

            try
            {
                dt = _AnFVoucherRepository.GetFromStoredProcedure(SPList.Report.RptPayableReport, paramsToStore);
            }
            catch (Exception)
            {
                throw new ArgumentNullException();
            }

            return dt;
        }
        #endregion PayableReport
    }
}