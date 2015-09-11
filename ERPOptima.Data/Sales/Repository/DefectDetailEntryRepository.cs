using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface IDefectDetailEntryRepository : IRepository<SlsDefectDetail>
    {
        int GetLastId();
        IList<SlsDefectDetail> GetByDefectId(int DefectId);
        SlsDefectDetail GetById(int Id);
        int AddEntity(SlsDefectDetail objSlsDefectDetail);
        void AddEntityList(IList<SlsDefectDetail> list);
    }
    public class DefectDetailEntryRepository : BaseRepository<SlsDefectDetail>, IDefectDetailEntryRepository
    {
        public DefectDetailEntryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public int GetLastId()
        {

            int Id = 1;
            SlsDefectDetail last = DataContext.SlsDefectDetails.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            return Id;

        }//end of GetLastId
        public int AddEntity(SlsDefectDetail objSlsDefectDetail)
        {
            int Id = 1;
            SlsDefectDetail last = DataContext.SlsDefectDetails.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsDefectDetail.Id = Id;
            base.Add(objSlsDefectDetail);
            return Id;

        }
        public void AddEntityList(IList<SlsDefectDetail> list)
        {
            int Id = 0;
            SlsDefectDetail last = DataContext.SlsDefectDetails.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            foreach (SlsDefectDetail obj in list)
            {
                if (obj.Id <= 0)
                {
                    Id++;
                    obj.Id = Id;
                    base.Add(obj);
                }
            }
        }

        //public IList<DamageDetail> GetByDamageId(int DamageId) 
        //{


        //    var list = DataContext.InvDamageDetails
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



        public IList<SlsDefectDetail> GetByDefectId(int DefectId)
        {
            return DataContext.SlsDefectDetails.Where(p => p.SlsDefectId == DefectId).ToList();
        }

        public SlsDefectDetail GetById(int Id)
        {
            return DataContext.SlsDefectDetails.Where(p => p.Id == Id).FirstOrDefault();
        }
    }
}
