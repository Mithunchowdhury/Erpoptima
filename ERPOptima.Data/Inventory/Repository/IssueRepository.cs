using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Inventory.Repository
{
    #region interface
    public interface IIssueRepository : IRepository<InvIssue>
    {

        List<InvIssue> GetIssueByRequisitionId(int requisitionId);
        int GetLastCode(int companyId);
        int GetLastId(InvIssue objInvIssue);


    }


    #endregion
    public class IssueRepository : BaseRepository<InvIssue>, IIssueRepository
    {
        public IssueRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }

        public int GetLastCode(int companyId)
        {

            int SL = 1;
            InvIssue last = null;
            try
            {
                last = DataContext.InvIssues.Where(r => r.SecCompanyId == companyId).OrderByDescending(x => x.Id).FirstOrDefault();                
            }
            catch(Exception ex)
            {
                
            }
            if (last != null)
            {                
                SL = int.Parse(last.IssueCode.Split('/')[1]) + 1;

            }
            return SL;

        }//end of GetLastCode

        public int GetLastId(InvIssue objInvIssue)
        {
            int Id = 1;
            InvIssue last = DataContext.InvIssues.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            //objInvIssue.Id = Id;
            //base.Add(objInvIssue);
            return Id;

        }
     

        public List<InvIssue> GetIssueByRequisitionId(int requisitionId)
        {
            return DataContext.InvIssues.Where(r => r.InvRequisitionId == requisitionId).ToList();           

        }



    }
}
