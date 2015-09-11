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

    public interface IAnFMonthLockRepository : IRepository<AnFMonthLock>
    {



        int AddEntity(AnFMonthLock monthLoc);


    }
    #endregion
    public class AnFMonthLockRepository : BaseRepository<AnFMonthLock>, IAnFMonthLockRepository
    {
        public AnFMonthLockRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        { 
        
        
        
        }
        public int AddEntity(AnFMonthLock monthLock)
        {
            int Id = 1;
            AnFMonthLock last = DataContext.AnFMonthLocks.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            monthLock.Id = Id;
            base.Add(monthLock);
            return Id;
        }

    }
}
