using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Security.Repository
{
    #region Interface



    public interface ISecRoleRepository : IRepository<SecRole>
    {
        int AddEntity(SecRole objSecRole);
    }

    #endregion
     

    public class SecRoleRepository : BaseRepository<SecRole>, ISecRoleRepository
    {

        public SecRoleRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public int AddEntity(SecRole objSecRole)
        {
            int Id = 1;
            SecRole last = DataContext.SecRoles.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSecRole.Id = Id;
            base.Add(objSecRole);
            return Id;

        }

    }
}
