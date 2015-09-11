using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface ISalesIncentiveSettingsRepository : IRepository<SlsIncentiveSetting>
    {
        decimal GetIncentiveRate(decimal totalsalespermonth);

        int AddEntity(SlsIncentiveSetting objSlsIncentiv);

        SlsIncentiveSetting GetById(int id);
    }

    public class SalesIncentiveSettingsRepository : BaseRepository<SlsIncentiveSetting>, ISalesIncentiveSettingsRepository
    {
        public SalesIncentiveSettingsRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        

        public int AddEntity(SlsIncentiveSetting objSlsIncentiv)
        {
            int Id = 1;
            SlsIncentiveSetting last = null;
            try
            {
                last = DataContext.SlsIncentiveSettings.OrderByDescending(x => x.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //Possibly can occur when no data exists in table.
            }
            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsIncentiv.Id = Id;
            base.Add(objSlsIncentiv);
            return Id;

        }
        public SlsIncentiveSetting GetById(int id)
        {
            return DataContext.SlsIncentiveSettings.Where(x => x.Id == id).FirstOrDefault();
        }

        public decimal GetIncentiveRate(decimal totalsalespermonth)
        {
            decimal result = 0;
            try
            {
                var list = DataContext.SlsIncentiveSettings.ToList();
                if (list != null && list.Count() > 0)
                {
                    list = list.Where(i => (i.LowerLimit != null || i.UpperLimit != null) &&
                        (i.LowerLimit == null || (i.LowerLimit != null && i.LowerLimit <= totalsalespermonth)) &&
                        (i.UpperLimit == null || (i.UpperLimit != null && totalsalespermonth <= i.UpperLimit))).ToList();
                    result = list.FirstOrDefault().CommissionPercentage;
                }
            }
            catch(Exception ex)
            {

            }

            return result;
        }

    }
}
