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

    public interface IAnFPaymentMethodRepository : IRepository<AnFPaymentMethod>
    {
        IList<AnFPaymentMethod> GetAnFPaymentMethods();
        int AddEntity(AnFPaymentMethod objAnFPaymentMethod);
        AnFPaymentMethod GetAnFPaymentMethodById(int id);
    }

    #endregion

    public class AnFPaymentMethodRepository : BaseRepository<AnFPaymentMethod>, IAnFPaymentMethodRepository
    {

        public AnFPaymentMethodRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<AnFPaymentMethod> GetAnFPaymentMethods()
        {
            return DataContext.AnFPaymentMethods.ToList();
        }

        public int AddEntity(AnFPaymentMethod objAnFPaymentMethod)
        {
            int Id = 1;
            AnFPaymentMethod last = DataContext.AnFPaymentMethods.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            objAnFPaymentMethod.Id = Id;
            base.Add(objAnFPaymentMethod);
            return Id;
        }

        public AnFPaymentMethod GetAnFPaymentMethodById(int id)
        {
            return DataContext.AnFPaymentMethods.Where(x => x.Id == id).FirstOrDefault();
        }

    }    
}
