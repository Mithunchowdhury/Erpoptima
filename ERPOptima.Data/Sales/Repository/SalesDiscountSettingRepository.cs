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

    public interface ISalesDiscountSettingRepository : IRepository<SlsDiscountSetting>
    {
        IList<SlsDiscountSetting> GetAll();

        int AddEntity(SlsDiscountSetting objSlsDiscount);

        SlsDiscountSetting GetById(int id);
    }

    #endregion

    public class SalesDiscountSettingRepository : BaseRepository<SlsDiscountSetting>, ISalesDiscountSettingRepository
    {
        public SalesDiscountSettingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsDiscountSetting> GetAll()
        {
            return DataContext.SlsDiscountSettings.ToList();
        }

        public int AddEntity(SlsDiscountSetting objSlsDiscount)
        {
            int Id = 1;
            SlsDiscountSetting last = null;
            try
            {
                last = DataContext.SlsDiscountSettings.OrderByDescending(x => x.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //Possibly can occur when no data exists in table.
            }
            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsDiscount.Id = Id;
            base.Add(objSlsDiscount);
            return Id;

        }
        public SlsDiscountSetting GetById(int id)
        {
            return DataContext.SlsDiscountSettings.Where(x => x.Id == id).FirstOrDefault();
        }

    }
}
