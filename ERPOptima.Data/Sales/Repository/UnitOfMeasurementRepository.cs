using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    #region Interface

    public interface IUnitOfMeasurementRepository : IRepository<SlsUnit>
    {
        IList<SlsUnit> GetAll(int Id);
        int AddEntity(SlsUnit objSlsUnit);
        SlsUnit GetById(long nullable);
        SlsUnit GetByName(string name);

        IList<SlsUnits> GetUnitByProductRequisition(int requisitionId,int productId);
    }

    #endregion

    public class UnitOfMeasurementRepository : BaseRepository<SlsUnit>, IUnitOfMeasurementRepository
    {
        public UnitOfMeasurementRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsUnit> GetAll(int Id)
        {
            return DataContext.SlsUnits.ToList();
        }
        public int AddEntity(SlsUnit objSlsUnit)
        {
            int Id = 1;
            SlsUnit last = DataContext.SlsUnits.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsUnit.Id = Id;
            base.Add(objSlsUnit);
            return Id;

        }

        public SlsUnit GetById(long nullable)
        {
            var a = from v in DataContext.SlsUnits
                    where v.Id == nullable
                    select v;
            return DataContext.SlsUnits.Where(x => x.Id == nullable).FirstOrDefault();
        }
        public SlsUnit GetByName(string name)
        {
            return DataContext.SlsUnits.Where(x => x.ShortName == name).FirstOrDefault();
        }

        public IList<SlsUnits> GetUnitByProductRequisition(int requisitionId, int productId)
        {
            var list = (from u in DataContext.SlsUnits
                        join r in DataContext.InvRequisitionDetails on u.Id equals r.SlsUnitId
                        where r.SlsProductId == productId && r.InvRequisitionId == requisitionId
                        select new SlsUnits
                        {
                            Id = u.Id,
                            Name = u.Name,
                            ShortName=u.ShortName,                           
                            CreatedBy = u.CreatedBy,
                            CreatedDate = u.CreatedDate,
                            ModifiedBy = u.ModifiedBy,
                            ModifiedDate = u.ModifiedDate
                        }).ToList();
            return list;

        }



    }

}
