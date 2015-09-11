using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
   #region Interface

    public interface IAreaConfigurationRepository : IRepository<SlsAreaConfiguration>
    {
        int AddEntity(SlsAreaConfiguration obj);
        SlsAreaConfiguration GetById(int nullable);
    }

    #endregion

    public class AreaConfigurationRepository : BaseRepository<SlsAreaConfiguration>, IAreaConfigurationRepository
    {
        public AreaConfigurationRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }       
        public int AddEntity(SlsAreaConfiguration objSlsAreaConfiguration)
        {
            int Id = 1;
            SlsAreaConfiguration last = DataContext.SlsAreaConfigurations.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsAreaConfiguration.Id = Id;
            base.Add(objSlsAreaConfiguration);
            return Id;

        }

        public SlsAreaConfiguration GetById(int nullable)
        {            
            return DataContext.SlsAreaConfigurations.Where(x => x.Id == nullable).FirstOrDefault();
        }
        
    }

}
