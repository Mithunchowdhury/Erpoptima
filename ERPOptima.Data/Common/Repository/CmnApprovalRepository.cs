using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ERPOptima.Data.Common.Repository
{
    #region interface

    public interface ICmnApprovalRepository : IRepository<CmnApproval>
    {
        long AddEntity(CmnApproval ap);

        long GetLastId();

        int GetCountByParameters(int _companyId, int p, long processLevelId, long _refId);


        DataTable GetApproval(long refId, int processId, int levelId);

    }

    #endregion interface

    public class CmnApprovalRepository : BaseRepository<CmnApproval>, ICmnApprovalRepository
    {
        public CmnApprovalRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }


        public long AddEntity(CmnApproval ap)
        {
            long Id = 1;

            Id=ap.Id;
            base.Add(ap);
            return Id;
        }

        public long GetLastId()
        {
            long Id=0;
            CmnApproval last = DataContext.CmnApprovals.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            else
            {
                Id = 1;
            }
            return Id;
        }


        public int GetCountByParameters(int _companyId, int p, long processLevelId, long _refId)
        {

            return DataContext.CmnApprovals.Where(r => r.CmnCompanyId == _companyId && r.RefId == _refId && r.CmnApprovalProcessId == p && r.CmnProcessLevelId == processLevelId).Count();
        }

        public DataTable GetApproval(long refId, int processId, int levelId)
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();

                SqlParameter[] paramsToStore = new SqlParameter[3];
                paramsToStore[0] = new SqlParameter("@refId", SqlDbType.BigInt);
                paramsToStore[0].Value = refId;
                paramsToStore[1] = new SqlParameter("@processId", SqlDbType.VarChar);
                paramsToStore[1].Value = processId;
                paramsToStore[2] = new SqlParameter("@levelId", SqlDbType.VarChar);
                paramsToStore[2].Value = levelId;
                dt = GetFromStoredProcedure(SPList.CmnApproval.GetCmnApprovalsByRefAndProcessAndLevelId, paramsToStore);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

    }
}