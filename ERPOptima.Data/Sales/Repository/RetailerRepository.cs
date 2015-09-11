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

    public interface IRetailerRepository : IRepository<SlsRetailer>
    {
        IList<SlsRetailer> GetAll();
        int AddEntity(SlsRetailer objSlsRetailer);
        SlsRetailer GetById(int Id);
    }

    #endregion

    public class RetailerRepository : BaseRepository<SlsRetailer>, IRetailerRepository
    {
        public RetailerRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsRetailer> GetAll()
        {
            return DataContext.SlsRetailers.ToList();
        }
        public int AddEntity(SlsRetailer objSlsRetailer)
        {
            int Id = 1;
            SlsRetailer last = null;
            try
            {
                last = DataContext.SlsRetailers.OrderByDescending(x => x.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //Possibly can occur when no data exists in table.
            }
            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsRetailer.Id = Id;
            base.Add(objSlsRetailer);
            return Id;

        }

        public SlsRetailer GetById(int id)
        {
            return DataContext.SlsRetailers.Where(x => x.Id == id).FirstOrDefault();
        }

    }
}
