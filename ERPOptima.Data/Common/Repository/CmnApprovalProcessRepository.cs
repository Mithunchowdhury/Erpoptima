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

    public interface ICmnApprovalProcessRepository : IRepository<CmnApprovalProcess>
    {
        IList<CmnApprovalProcess> GetCmnApprovalProcesses();
        //CmnBusiness GetLastCode(int companyId);
        int AddEntity(CmnApprovalProcess objCmnApprovalProcess);
        CmnApprovalProcess GetCmnApprovalProcessById(long nullable);
    }

    #endregion interface

    public class CmnApprovalProcessRepository : BaseRepository<CmnApprovalProcess>, ICmnApprovalProcessRepository
    {
        public CmnApprovalProcessRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {


        }
        public IList<CmnApprovalProcess> GetCmnApprovalProcesses()
        {
            return DataContext.CmnApprovalProcesses.ToList();
        }
        public int AddEntity(CmnApprovalProcess objCmnApprovalProcess)
        {
            int Id = 1;
            CmnApprovalProcess last = DataContext.CmnApprovalProcesses.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objCmnApprovalProcess.Id = Id;
            base.Add(objCmnApprovalProcess);
            return Id;

        }

        public CmnApprovalProcess GetCmnApprovalProcessById(long nullable)
        {
            return DataContext.CmnApprovalProcesses.Where(x => x.Id == nullable).FirstOrDefault();
        }
    }

}