using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPOptima.Data;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Accounts;
using ERPOptima.Model.Common;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using ERPOptima.Lib.Utilities;
using ERPOptima.Lib.Model;


namespace ERPOptima.Data.Accounts.Repository
{

    #region Interface
    public interface ICmnApprovalCommentRepository : IRepository<CmnApprovalComment>
    {       
        DataTable GetApprovalComments(int processId, Int64 refId);
        long AddEntity(CmnApprovalComment obj);

        
    }

    #endregion

    public class CmnApprovalCommentRepository : BaseRepository<CmnApprovalComment>, ICmnApprovalCommentRepository
    {
        
        public CmnApprovalCommentRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {
               
            }


        public long AddEntity(CmnApprovalComment obj)
        {
            long Id = 1;
            CmnApprovalComment last = DataContext.CmnApprovalComments.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }

        public DataTable GetApprovalComments(int processId, Int64 refId)
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                SqlParameter[] paramsToStore = new SqlParameter[2];
                paramsToStore[0] = new SqlParameter("@processId", SqlDbType.Int);
                paramsToStore[0].Value = processId;
                paramsToStore[1] = new SqlParameter("@refId", SqlDbType.BigInt);
                paramsToStore[1].Value = refId;

                dt = GetFromStoredProcedure(SPList.CmnApprovalComment.GetCmnApprovalCommentsByProcessAndRefId, paramsToStore);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        
        


    }




    
}
