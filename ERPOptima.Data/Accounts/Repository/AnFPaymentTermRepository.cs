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

    public interface IAnFPaymentTermRepository : IRepository<AnFPaymentTerm>
    {
        IList<AnFPaymentTerm> GetAnFPaymentTerms();
        int AddEntity(AnFPaymentTerm objAnFPaymentTerm);
        AnFPaymentTerm GetAnFPaymentTermById(int id);
    }

    #endregion

    public class AnFPaymentTermRepository : BaseRepository<AnFPaymentTerm>, IAnFPaymentTermRepository
    {

        public AnFPaymentTermRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<AnFPaymentTerm> GetAnFPaymentTerms()
        {
            return DataContext.AnFPaymentTerms.ToList();
        }

        public int AddEntity(AnFPaymentTerm objAnFPaymentTerm)
        {
            int Id = 1;
            AnFPaymentTerm last = DataContext.AnFPaymentTerms.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            objAnFPaymentTerm.Id = Id;
            base.Add(objAnFPaymentTerm);
            return Id;
        }

        public AnFPaymentTerm GetAnFPaymentTermById(int id)
        {
            return DataContext.AnFPaymentTerms.Where(x => x.Id == id).FirstOrDefault();
        }

    }    
}
