using ERPOptima.Data.Infrastructure;
using ERPOptima.Lib.Utilities;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
   #region Interface

    public interface IDistributorRepository : IRepository<SlsDistributor>
    {
        IList<SlsDistributor> GetAll();
        int AddEntity(SlsDistributor objSlsOffice);
        SlsDistributor GetById(int id);        
    }

    #endregion

    public class DistributorRepository : BaseRepository<SlsDistributor>, IDistributorRepository
    {
        public DistributorRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsDistributor> GetAll()
        {
            return DataContext.SlsDistributors.ToList();
        }
        public int AddEntity(SlsDistributor objSlsOffice)
        {
            int Id = 1;
            SlsDistributor last = null;
            try
            {
                last = DataContext.SlsDistributors.OrderByDescending(x => x.Id).FirstOrDefault();
            }
            catch(Exception ex)
            {
                //Possibly can occur when no data exists in table.
            }
            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsOffice.Id = Id;
            base.Add(objSlsOffice);
            return Id;

        }

        public SlsDistributor GetById(int id)
        {
            return DataContext.SlsDistributors.Where(x => x.Id == id).FirstOrDefault();
        }


    }
}
