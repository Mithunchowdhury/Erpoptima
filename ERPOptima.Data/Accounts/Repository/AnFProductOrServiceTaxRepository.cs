using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Accounts.Repository
{

    #region Interface

    public interface IAnFProductOrServiceTaxRepository : IRepository<AnFProductOrServiceTax>
    {
        IList<AnFProductOrServiceTax> GetProductOrServiceTaxes();
        int AddEntity(AnFProductOrServiceTax objAnFProductOrServiceTax);
        AnFProductOrServiceTax GetProductOrServiceTaxById(int nullable);
    }

    #endregion

    public class AnFProductOrServiceTaxRepository : BaseRepository<AnFProductOrServiceTax>, IAnFProductOrServiceTaxRepository
    {
        public AnFProductOrServiceTaxRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<AnFProductOrServiceTax> GetProductOrServiceTaxes()
        {
            return DataContext.AnFProductOrServiceTaxes.ToList();
        }

        public int AddEntity(AnFProductOrServiceTax objAnFProductOrServiceTax)
        {
            int Id = 1;
            AnFProductOrServiceTax last = DataContext.AnFProductOrServiceTaxes.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objAnFProductOrServiceTax.Id = Id;
            base.Add(objAnFProductOrServiceTax);
            return Id;

        }

        public AnFProductOrServiceTax GetProductOrServiceTaxById(int nullable)
        {
            return DataContext.AnFProductOrServiceTaxes.Where(x => x.Id == nullable).FirstOrDefault();
        }

    }
}
