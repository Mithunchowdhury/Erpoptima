using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Security;
using System.Collections.Generic;
using System.Linq;

namespace ERPOptima.Data.Security.Repository
{
    #region Interface

    public interface ISecModuleRepository : IRepository<SecModule>
    {
        int AddEntity(SecModule objSecModule);
        SecModule GetSecModuleById(int nullable);
    }

    #endregion Interface

    public class SecModuleRepository : BaseRepository<SecModule>, ISecModuleRepository
    {
        public SecModuleRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public int AddEntity(SecModule objSecModule)
        {
            int Id = 1;
            SecModule last = DataContext.SecModules.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSecModule.Id = Id;
            base.Add(objSecModule);
            return Id;

        }

        public SecModule GetSecModuleById(int nullable)
        {
            var a = from v in DataContext.AnFCostCenters
                    where v.Id == nullable
                    select v;
            return DataContext.SecModules.Where(x => x.Id == nullable).FirstOrDefault();
        }
    }
}