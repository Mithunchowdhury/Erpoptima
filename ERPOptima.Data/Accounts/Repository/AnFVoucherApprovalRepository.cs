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
    public interface IAnFVoucherApprovalRepository : IRepository<CmnApprovalComment>
    {       
       
    }

    #endregion

    public class AnFVoucherApprovalRepository : BaseRepository<CmnApprovalComment>, IAnFVoucherApprovalRepository
    {
        
        public AnFVoucherApprovalRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {
               
            }
        

    }
    
}
