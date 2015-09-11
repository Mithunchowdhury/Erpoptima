using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Inventory.Repository
{
    #region interface
    public interface IInvStoreRepository : IRepository<InvStore>
    {
        IList<InvStore> GetStores();
        InvStore GetStoresForOffice(int officeId);
    }

    #endregion
    public class InvStoreRepository : BaseRepository<InvStore>, IInvStoreRepository
    {

        public InvStoreRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {

            }

        public IList<InvStore> GetStores()
        {
            return DataContext.InvStores.ToList();
        }
        public InvStore GetStoresForOffice(int officeId)
        {
            InvStore str = new InvStore();
            str = DataContext.InvStores.Where(t => t.SlsOfficeId == officeId).FirstOrDefault();
            //Model.HRM.HrmEmployee emp = DataContext.HrmEmployees.Where(t => t.Id == user.HrmEmployeeId).FirstOrDefault();
            //if (store != null)
            //{
            //    str = DataContext.SlsOffices.Where(t => t.Id == store.SlsOfficeId).FirstOrDefault();
            //}
            return str;
        }




        


        
    }
}
