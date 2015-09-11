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

    public interface ICorporateIndustryTypeRepository : IRepository<SlsCorporateType>
    {
        IList<SlsCorporateType> GetAll(int companyId);
        int AddEntity(SlsCorporateType objSlsCorporateType);
        SlsCorporateType GetById(long nullable);
    }

    #endregion

    public class CorporateIndustryTypeRepository : BaseRepository<SlsCorporateType>, ICorporateIndustryTypeRepository
    {
        public CorporateIndustryTypeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsCorporateType> GetAll(int companyId)
        {
            return DataContext.SlsCorporateTypes.ToList();
        }
        public int AddEntity(SlsCorporateType objSlsCorporateType)
        {
            int Id = 1;
            SlsCorporateType last = DataContext.SlsCorporateTypes.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsCorporateType.Id = Id;
            base.Add(objSlsCorporateType);
            return Id;

        }

        public SlsCorporateType GetById(long nullable)
        {
            var a = from v in DataContext.SlsCorporateTypes
                    where v.Id == nullable
                    select v;
            return DataContext.SlsCorporateTypes.Where(x => x.Id == nullable).FirstOrDefault();
        }

    }
}