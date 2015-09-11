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
    public interface ISecRolePermissionRepository : IRepository<SecRolePermission>
    {
        long AddEntity(SecRolePermission objSecRolePermission);
    }

    #endregion
    public class SecRolePermissionRepository : BaseRepository<SecRolePermission>, ISecRolePermissionRepository
    {

        public SecRolePermissionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public long AddEntity(SecRolePermission objSecRolePermission)
        {
            int Id = 1;
            SecRolePermission last = DataContext.SecRolePermissions.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSecRolePermission.Id = Id;
            base.Add(objSecRolePermission);
            return Id;

        }

    }
}
