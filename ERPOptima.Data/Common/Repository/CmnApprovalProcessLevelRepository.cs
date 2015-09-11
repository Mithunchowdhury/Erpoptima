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

    public interface ICmnApprovalProcessLevelRepository : IRepository<CmnApprovalProcessLevel>
    {
        int GetLastId();

    }

    #endregion interface
    public class CmnApprovalProcessLevelRepository : BaseRepository<CmnApprovalProcessLevel>, ICmnApprovalProcessLevelRepository
    {

        public CmnApprovalProcessLevelRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {


        }

        public int GetLastId()
        {

            int Id = 1;
            CmnApprovalProcessLevel last = DataContext.CmnApprovalProcessLevels.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            return Id;

        }//end 
    }
}
