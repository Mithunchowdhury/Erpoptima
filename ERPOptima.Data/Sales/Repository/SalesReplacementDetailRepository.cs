using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface ISalesReplacementDetailRepository : IRepository<SlsReplacementDetail>
    {
        int GetLastId();
        IList<SlsReplacementDetail> GetByDetailId(int DetailId);
        SlsReplacementDetail GetById(int Id);
        int AddEntity(SlsReplacementDetail objSlsReplacementDetail);
        void AddEntityList(IList<SlsReplacementDetail> list);
        IList<SlsReplacementDetail> GetAll();
    }
    public class SalesReplacementDetailRepository : BaseRepository<SlsReplacementDetail>, ISalesReplacementDetailRepository
    {
        public SalesReplacementDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<SlsReplacementDetail> GetAll()
        {
            return DataContext.SlsReplacementDetails.ToList();
        }

        public int GetLastId()
        {

            int Id = 1;
            SlsReplacementDetail last = DataContext.SlsReplacementDetails.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            return Id;

        }//end of GetLastId
        public int AddEntity(SlsReplacementDetail objSlsReplacementDetail)
        {
            int Id = 1;
            SlsReplacementDetail last = DataContext.SlsReplacementDetails.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsReplacementDetail.Id = Id;
            base.Add(objSlsReplacementDetail);
            return Id;

        }
        public void AddEntityList(IList<SlsReplacementDetail> list)
        {
            int Id = 0;
            SlsReplacementDetail last = DataContext.SlsReplacementDetails.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id;
            }
            foreach (SlsReplacementDetail obj in list)
            {
                if (obj.Id <= 0)
                {
                    Id++;
                    obj.Id = Id;
                    base.Add(obj);
                }
            }
        }

        //public IList<SlsReplacementDetail> GetByDetailId(int DetailId)
        //{


        //    var list = DataContext.SlsReplacementDetails
        //        .Join(DataContext.SlsProducts
        //            , r => r.SlsProductId
        //            , p => p.Id
        //            , (r, p) => new { r, p })
        //        .Join(DataContext.SlsUnits
        //        , rp => rp.r.SlsUnitsId
        //        , u => u.Id
        //        , (rp, u) => new ReqDetail()
        //        {
        //            Id = rp.r.Id,
        //            InvRequisitionId = rp.r.InvDamageId,
        //            SlsProductId = rp.r.SlsProductId,
        //            RequiredQuantity = rp.r.Quantity,
        //            SlsUnitId = rp.r.SlsUnitsId,
        //            ProductName = rp.p.Name,
        //            UnitName = u.Name
        //        })
        //        .Where(req => req.Id == DamageId).ToList();


        //    return list;

        //}



        public IList<SlsReplacementDetail> GetByDetailId(int DetailId)
        {
            throw new NotImplementedException();
        }

        public SlsReplacementDetail GetById(int Id)
        {
            return DataContext.SlsReplacementDetails.Where(p => p.Id == Id).FirstOrDefault();
        }
    }

    public partial class SlsReplacementDetailViewModel
    {
        public int Id { get; set; }
        public Nullable<int> SlsReplacementId { get; set; }
        public Nullable<int> SlsProductId { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<int> SlsUnitId { get; set; }
        public Nullable<decimal> AdjustedAmount { get; set; }
        public string Reason { get; set; }

        public string SlsProductName { get; set; }
        public string SlsUnitName { get; set; }

    }
}
