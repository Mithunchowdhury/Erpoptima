using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    #region interface
    public interface ISalesTargetDetailRepository : IRepository<SlsSalesTargetDetail>
    {

        List<SlsSalesTargetDetail> GetTargetDetailByTargetId(int targetId);

        int AddEntity(SlsSalesTargetDetail objInvIssueDetail);


    }


    #endregion
    public class SalesTargetDetailRepository : BaseRepository<SlsSalesTargetDetail>, ISalesTargetDetailRepository
    {

        public SalesTargetDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }


        public int AddEntity(SlsSalesTargetDetail objSlsSalesTargetDetail)
        {
            int Id = 1;
            SlsSalesTargetDetail last = DataContext.SlsSalesTargetDetails.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsSalesTargetDetail.Id = Id;
            base.Add(objSlsSalesTargetDetail);
            return Id;

        }


        public List<SlsSalesTargetDetail> GetTargetDetailByTargetId(int targetId)
        {
            return DataContext.SlsSalesTargetDetails.Where(r => r.SlsSalesTargetId == targetId).ToList();
           
        }







    }
}
