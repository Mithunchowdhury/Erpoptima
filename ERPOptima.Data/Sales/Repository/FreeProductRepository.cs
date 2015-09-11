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

    public interface IFreeProductRepository : IRepository<SlsFreeProduct>
    {
        IList<SlsFreeProduct> GetAll(int companyId);
        int AddEntity(SlsFreeProduct objSlsOffice);
        SlsFreeProduct GetById(int id); 

    }



    #endregion
    public class FreeProductRepository : BaseRepository<SlsFreeProduct>, IFreeProductRepository
    {

        public FreeProductRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }

        public IList<SlsFreeProduct> GetAll(int companyId)
        {

            return DataContext.SlsFreeProducts.Where(i => i.SecCompnayId == companyId).ToList();
          
        }

        public int AddEntity(SlsFreeProduct obj)
        {
            int Id = 1;
            SlsFreeProduct last = null;
            try
            {
                last = DataContext.SlsFreeProducts.OrderByDescending(x => x.Id).FirstOrDefault();
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

        public SlsFreeProduct GetById(int id)
        {
            return DataContext.SlsFreeProducts.Where(x => x.Id == id).FirstOrDefault();
        }



    }

    
}