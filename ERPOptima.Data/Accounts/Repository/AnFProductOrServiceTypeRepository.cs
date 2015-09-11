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

    public interface IAnFProductOrServiceTypeRepository : IRepository<AnFProductOrServiceType>
    {
        IList<AnFProductOrServiceType> GetProductOrServiceTypes();
        int AddEntity(AnFProductOrServiceType objAnFProductOrServiceType);
        AnFProductOrServiceType GetProductOrServiceTypeById(int nullable);
    }

    #endregion

    public class AnFProductOrServiceTypeRepository : BaseRepository<AnFProductOrServiceType>, IAnFProductOrServiceTypeRepository
    {
        public AnFProductOrServiceTypeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<AnFProductOrServiceType> GetProductOrServiceTypes()
        {
            return DataContext.AnFProductOrServiceTypes.ToList();
        }

        public int AddEntity(AnFProductOrServiceType objAnFProductOrServiceType)
        {
            int Id = 1;
            AnFProductOrServiceType last = DataContext.AnFProductOrServiceTypes.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objAnFProductOrServiceType.Id = Id;
            base.Add(objAnFProductOrServiceType);
            return Id;

        }

        public AnFProductOrServiceType GetProductOrServiceTypeById(int nullable)
        {           
            return DataContext.AnFProductOrServiceTypes.Where( x => x.Id == nullable).FirstOrDefault();
        }

    }

}
