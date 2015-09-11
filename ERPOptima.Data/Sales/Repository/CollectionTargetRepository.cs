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

    public interface ICollectionTargetRepository : IRepository<SlsCollectionTarget>
    {
       // IList<SlsCollectionTarget> GetAll();
        int AddEntity(SlsCollectionTarget obj);
        //SlsCollectionTarget GetById(int id);
    }

    #endregion
    public class CollectionTargetRepository : BaseRepository<SlsCollectionTarget>, ICollectionTargetRepository
    {
        public CollectionTargetRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public int AddEntity(SlsCollectionTarget objCollectionTarget)
        {
            int Id = 1;
            SlsCollectionTarget last = DataContext.SlsCollectionTargets.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objCollectionTarget.Id = Id;
            base.Add(objCollectionTarget);
            return Id;

        }
    }
}
