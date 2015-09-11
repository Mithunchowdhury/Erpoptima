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
    /// <summary>
    /// Repository Interface for Dealer
    /// </summary>
    public interface IDealerRepository : IRepository<SlsDealer>
    {
        /// <summary>
        /// Get All Dealers
        /// </summary>
        /// <returns>Returns All Dealers</returns>
        IList<SlsDealer> GetAll();
        /// <summary>
        /// Add Dealer in DataContext
        /// </summary>
        /// <param name="obj">Dealer</param>
        /// <returns>Returns Id</returns>
        int AddEntity(SlsDealer obj);
        /// <summary>
        /// Get Dealer by Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Dealer</returns>
        SlsDealer GetById(int id);
    }

    #endregion
    /// <summary>
    /// Repository Implementation for Dealer
    /// </summary>
    public class DealerRepository : BaseRepository<SlsDealer>, IDealerRepository
    {
        public DealerRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        /// <summary>
        /// Get All Dealers
        /// </summary>
        /// <returns>Returns All Dealers</returns>
        public IList<SlsDealer> GetAll()
        {
            return DataContext.SlsDealers.ToList();
        }
        /// <summary>
        /// Add Dealer in DataContext
        /// </summary>
        /// <param name="obj">Dealer</param>
        /// <returns>Returns Id</returns>
        public int AddEntity(SlsDealer obj)
        {
            int Id = 1;
            SlsDealer last = null;
            try
            {
                last = DataContext.SlsDealers.OrderByDescending(x => x.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //Possibly can occur when no data exists in table.
            }
            if (last != null)
            {
                Id = last.Id + 1;

            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }
        /// <summary>
        /// Get Dealer by Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Dealer</returns>
        public SlsDealer GetById(int id)
        {
            return DataContext.SlsDealers.Where(x => x.Id == id).FirstOrDefault();
        }

    }
}
