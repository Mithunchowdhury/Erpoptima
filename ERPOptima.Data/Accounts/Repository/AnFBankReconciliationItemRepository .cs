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

    public interface IAnFBankReconciliationItemRepository : IRepository<AnFBankReconciliationItem>
    {
        IList<AnFBankReconciliationItem> GetAnFBankReconciliationItems();
        int AddEntity(AnFBankReconciliationItem objAnFBankReconciliationItem);
        AnFBankReconciliationItem GetAnFBankReconciliationItemById(int id);
    }

    #endregion

    public class AnFBankReconciliationItemRepository : BaseRepository<AnFBankReconciliationItem>, IAnFBankReconciliationItemRepository
    {

        public AnFBankReconciliationItemRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<AnFBankReconciliationItem> GetAnFBankReconciliationItems()
        {
            return DataContext.AnFBankReconciliationItems.ToList();
        }

        public int AddEntity(AnFBankReconciliationItem objAnFBankReconciliationItem)
        {
            int Id = 1;
            AnFBankReconciliationItem last = DataContext.AnFBankReconciliationItems.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            objAnFBankReconciliationItem.Id = Id;
            base.Add(objAnFBankReconciliationItem);
            return Id;
        }

        public AnFBankReconciliationItem GetAnFBankReconciliationItemById(int id)
        {
            return DataContext.AnFBankReconciliationItems.Where(x => x.Id == id).FirstOrDefault();
        }

    }    
}
