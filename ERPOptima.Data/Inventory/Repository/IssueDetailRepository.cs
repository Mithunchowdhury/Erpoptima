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
    public interface IIssueDetailRepository : IRepository<InvIssueDetail>
    {

        List<InvIssueDetails> GetIssueDetailByIssueId(int issueId);

        int AddEntity(InvIssueDetail objInvIssueDetail);


    }


    #endregion
    public class IssueDetailRepository : BaseRepository<InvIssueDetail>, IIssueDetailRepository
    {
        public IssueDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }


        public int AddEntity(InvIssueDetail objInvIssueDetail)
        {
            int Id = 1;
            InvIssueDetail last = DataContext.InvIssueDetails.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objInvIssueDetail.Id = Id;
            base.Add(objInvIssueDetail);
            return Id;

        }


        public List<InvIssueDetails> GetIssueDetailByIssueId(int issueId)
        {
            //return DataContext.InvIssueDetails.Where(r => r.InvIssueId == issueId).ToList();

            var list = DataContext.InvIssueDetails
                .Join(DataContext.SlsProducts
                    , r => r.SlsProductId
                    , p => p.Id
                    , (r, p) => new { r, p })
                .Join(DataContext.SlsUnits
                , rp => rp.r.SlsUnitId
                , u => u.Id
                , (rp, u) => new InvIssueDetails()
                {
                    Id = rp.r.Id,
                    InvIssueId = rp.r.InvIssueId,
                    SlsProductId = rp.r.SlsProductId,
                    RequiredQuantity = rp.r.RequiredQuantity,
                    IssuedQuantity=rp.r.IssuedQuantity,
                    SlsUnitId = rp.r.SlsUnitId,
                    ProductName = rp.p.Name,
                    UnitName = u.ShortName
                })
                .Where(req => req.InvIssueId == issueId).ToList();


            return list;      

        }









    }
}
