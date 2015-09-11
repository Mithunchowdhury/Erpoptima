using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Common;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Common.Repository
{

    #region Interface

    public interface ISecGroupRepository : IRepository<SecGroup>
    {
        IList<SecGroup> GetSecGroups();        
        int AddEntity(SecGroup objSecGroup);
        SecGroup GetSecGroupById(int id);
    }
    #endregion
    public class SecGroupRepository : BaseRepository<SecGroup>, ISecGroupRepository
    {
        public SecGroupRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SecGroup> GetSecGroups()
        {
            return DataContext.SecGroups.ToList();
        }

        public int AddEntity(SecGroup objSecGroup)
        {
            int Id = 1;
            SecGroup last = DataContext.SecGroups.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            objSecGroup.Id = Id;
            base.Add(objSecGroup);
            return Id;
        }

        public SecGroup GetSecGroupById(int id)
        {
            //var a = from v in DataContext.AnFCostCenters
            //        where v.Id == id
            //        select v;
            return DataContext.SecGroups.Where(x => x.Id == id).FirstOrDefault();
        }

    }
}

