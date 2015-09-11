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

    public interface IOfficeTypeRepository : IRepository<SlsOfficeType>
    {
        IList<SlsOfficeType> GetAll(int companyId);
        int AddEntity(SlsOfficeType objSlsOfficeType);
        SlsOfficeType GetById(long nullable);
    }

    #endregion

    public class OfficeTypeRepository : BaseRepository<SlsOfficeType>, IOfficeTypeRepository
    {
        public OfficeTypeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsOfficeType> GetAll(int companyId)
        {
            return DataContext.SlsOfficeTypes.ToList();
        }
        public int AddEntity(SlsOfficeType objSlsOfficeType)
        {
            int Id = 1;
            SlsOfficeType last = DataContext.SlsOfficeTypes.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsOfficeType.Id = Id;
            base.Add(objSlsOfficeType);
            return Id;

        }

        public SlsOfficeType GetById(long nullable)
        {
            var a = from v in DataContext.SlsOfficeTypes
                    where v.Id == nullable
                    select v;
            return DataContext.SlsOfficeTypes.Where(x => x.Id == nullable).FirstOrDefault();
        }

    }
}
