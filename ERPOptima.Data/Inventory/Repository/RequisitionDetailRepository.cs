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
    public interface IRequisitionDetailRepository : IRepository<InvRequisitionDetail>
    {

        int GetLastId();
        IList<ReqDetail> GetByRequisitionId(int requisitionId);
        InvRequisitionDetail GetById(int Id);
    }

    #endregion
    public class RequisitionDetailRepository : BaseRepository<InvRequisitionDetail>, IRequisitionDetailRepository
    {
        public RequisitionDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }
        public int GetLastId()
        {

            int Id = 1;
            InvRequisitionDetail last = DataContext.InvRequisitionDetails.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            return Id;

        }//end of GetLastId




        public IList<ReqDetail> GetByRequisitionId(int requisitionId)
        {           

            var  list = DataContext.InvRequisitionDetails
                .Join(DataContext.SlsProducts
                    , r => r.SlsProductId
                    , p => p.Id
                    , (r, p) => new { r, p })
                .Join(DataContext.SlsUnits
                , rp => rp.r.SlsUnitId
                , u => u.Id
                , (rp, u) => new ReqDetail()
                {
                    Id = rp.r.Id,
                    InvRequisitionId = rp.r.InvRequisitionId,
                    SlsProductId = rp.r.SlsProductId,
                    RequiredQuantity = rp.r.RequiredQuantity,
                    SlsUnitId = rp.r.SlsUnitId,
                    ProductName = rp.p.Name,
                    UnitName = u.ShortName
                })
                .Where(req => req.InvRequisitionId == requisitionId).ToList();


                       
            return list;           

           
        }

        public InvRequisitionDetail GetById(int Id)
        {
            return DataContext.InvRequisitionDetails.Where(p => p.Id == Id).FirstOrDefault();
        }






    }
}
