using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Common.Repository
{

   

    #region interface

    public interface ICmnApprovalUserPermissionRepository : IRepository<CmnApprovalUserPermission>
    {

        int GetLastId();

    }

    #endregion 
    public class CmnApprovalUserPermissionRepository:BaseRepository<CmnApprovalUserPermission>, ICmnApprovalUserPermissionRepository
    {

        public CmnApprovalUserPermissionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public int GetLastId()
        {
            int Id = 0;
            CmnApprovalUserPermission last = DataContext.CmnApprovalUserPermissions.OrderByDescending(x => x.Id).FirstOrDefault();

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
    }
}
