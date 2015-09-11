using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPOptima.Data;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Accounts;


namespace ERPOptima.Data.Accounts.Repository
{

    #region Interface

    public interface IAnFDeliveryMethodRepository : IRepository<AnFDeliveryMethod>
    {
        IList<AnFDeliveryMethod> GetAnFDeliveryMethods();
        int AddEntity(AnFDeliveryMethod objAnFDeliveryMethod);
        AnFDeliveryMethod GetAnFDeliveryMethodById(int id);
    }

    #endregion

    public class AnFDeliveryMethodRepository : BaseRepository<AnFDeliveryMethod>, IAnFDeliveryMethodRepository
    {

        public AnFDeliveryMethodRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }

        public IList<AnFDeliveryMethod> GetAnFDeliveryMethods()
        {
            return DataContext.AnFDeliveryMethods.ToList();
        }

        public int AddEntity(AnFDeliveryMethod objAnFDeliveryMethod)
        {
            int Id = 1;
            AnFDeliveryMethod last = DataContext.AnFDeliveryMethods.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            objAnFDeliveryMethod.Id = Id;
            base.Add(objAnFDeliveryMethod);
            return Id;
        }

        public AnFDeliveryMethod GetAnFDeliveryMethodById(int id)
        {            
            return DataContext.AnFDeliveryMethods.Where(x => x.Id == id).FirstOrDefault();
        }
       
    }
    
}
