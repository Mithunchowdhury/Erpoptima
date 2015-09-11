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

    public interface IAreaConfigurationDetailRepository : IRepository<SlsAreaConfigurationDetail>
    {
        int AddEntity(SlsAreaConfigurationDetail obj);
        SlsAreaConfigurationDetail GetById(int nullable);
    }

    #endregion

    public class AreaConfigurationDetailRepository : BaseRepository<SlsAreaConfigurationDetail>, IAreaConfigurationDetailRepository
    {
        public AreaConfigurationDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public int AddEntity(SlsAreaConfigurationDetail objAreaConfigurationDetail)
        {
            int Id = 1;
            SlsAreaConfigurationDetail last = DataContext.SlsAreaConfigurationDetails.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objAreaConfigurationDetail.Id = Id;
            base.Add(objAreaConfigurationDetail);
            return Id;

        }

        public SlsAreaConfigurationDetail GetById(int nullable)
        {
            return DataContext.SlsAreaConfigurationDetails.Where(x => x.Id == nullable).FirstOrDefault();
        }

    }

}
