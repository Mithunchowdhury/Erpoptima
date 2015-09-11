using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Inventory.Repository
{
    //class InvDamageDetailRepository
    //{
    //}

    #region interface
    public interface IInvDamageDetailRepository : IRepository<InvDamageDetail>
    {

        int GetLastId();
        IList<DamageDetail> GetByDamageId(int DamageId);
        InvDamageDetail GetById(int Id);
        int AddEntity(InvDamageDetail objInvDamageDetail);
        void AddEntityList(IList<InvDamageDetail> list);
    }

    #endregion

    public class InvDamageDetailRepository : BaseRepository<InvDamageDetail>, IInvDamageDetailRepository
    {
        public InvDamageDetailRepository (IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public int GetLastId()
        {

            int Id = 1;
            InvDamageDetail last = DataContext.InvDamageDetails.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            return Id;

        }//end of GetLastId
        public int AddEntity(InvDamageDetail objInvDamageDetail)
        {
            int Id = 1;
            InvDamageDetail last = DataContext.InvDamageDetails.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objInvDamageDetail.Id = Id;
            base.Add(objInvDamageDetail);
            return Id;

        }
        public void AddEntityList(IList<InvDamageDetail> list)
        {
            int Id = 0;
            InvDamageDetail last = DataContext.InvDamageDetails.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id+1;
            }
            foreach (InvDamageDetail obj in list)
            {
                if (obj.Id <= 0)
                {
                    Id++;
                    obj.Id = Id;
                    base.Add(obj);
                }
            }
        }

        public IList<DamageDetail> GetByDamageId(int DamageId)
        {


            var list = DataContext.InvDamageDetails
                .Join(DataContext.SlsProducts
                    , r => r.SlsProductId
                    , p => p.Id
                    , (r, p) => new { r, p })
                .Join(DataContext.SlsUnits
                , rp => rp.r.SlsUnitsId
                , u => u.Id
                , (rp, u) => new DamageDetail()
                {
                    Id = rp.r.Id,
                    InvDamageId = rp.r.InvDamageId,
                    SlsProductId = rp.r.SlsProductId,
                    Quantity = rp.r.Quantity,
                    SlsUnitsId = rp.r.SlsUnitsId,
                    ProductName = rp.p.Name,
                    UnitName = u.ShortName,
                    Reason=rp.r.Reason
                })
                .Where(req => req.InvDamageId == DamageId).ToList();

            


            return list;

        }




        //public IList<DamageDetail> GetByDamageId(int DamageId)
        //{
        //    throw new NotImplementedException();
        //}

        public InvDamageDetail GetById(int Id)
        {
            return DataContext.InvDamageDetails.Where(p => p.Id == Id).FirstOrDefault();
        }
    }

 

}

