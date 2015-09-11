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

    public interface ICorporateClientRepository : IRepository<SlsCorporateClient>
    {
        IEnumerable<SlsCorporateClient> GetAll(int companyId);
        IEnumerable<SlsCorporateClient> GetCorporateName(int companyId);
        int AddEntity(SlsCorporateClient objCorporateClient);
        SlsCorporateClient GetById(long id);
    }

    #endregion

    public class CorporateClientRepository : BaseRepository<SlsCorporateClient>, ICorporateClientRepository
    {
        public CorporateClientRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IEnumerable<SlsCorporateClient> GetAll(int companyId)
        {
            return DataContext.SlsCorporateClients.ToList();
        }
        public IEnumerable<SlsCorporateClient> GetCorporateName(int companyId)
        {
            return DataContext.SlsCorporateClients.ToList();
        }
        public int AddEntity(SlsCorporateClient objCorporateClient)
        {
            int Id = 1;
            SlsCorporateClient last = DataContext.SlsCorporateClients.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objCorporateClient.Id = Id;
            base.Add(objCorporateClient);
            return Id;

        }

        public SlsCorporateClient GetById(long id)
        {
            var a = from v in DataContext.SlsCorporateClients
                    where v.Id == id
                    select v;
            return DataContext.SlsCorporateClients.Where(x => x.Id == id).FirstOrDefault();
        }

    }
}
