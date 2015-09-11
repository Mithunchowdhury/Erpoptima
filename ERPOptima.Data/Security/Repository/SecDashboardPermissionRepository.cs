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
    public interface ISecDashboardPermissionRepository : IRepository<SecDashboardPermission>   
    {
        long AddEntity(SecDashboardPermission objSecDashboardPermission);
        List<PermittedDashboard> GetPermittedDashBoard(int companyId, int roleId, int moduleId);
    }

    #endregion
    public class SecDashboardPermissionRepository : BaseRepository<SecDashboardPermission>, ISecDashboardPermissionRepository
    {

        public SecDashboardPermissionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public long AddEntity(SecDashboardPermission objSecDashboardPermission)
        {
            int Id = 1;
            SecDashboardPermission last = DataContext.SecDashboardPermissions.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSecDashboardPermission.Id = Id;
            base.Add(objSecDashboardPermission);
            return Id;

        }

        public List<PermittedDashboard> GetPermittedDashBoard(int companyId, int roleId, int moduleId)
        {
            var list = (from d in DataContext.SecDashboards
                        join dp in DataContext.SecDashboardPermissions on d.Id equals dp.SecDashboardId
                        where d.SecCompanyId == companyId && d.SecModuleId==moduleId && dp.SecRoleId==roleId && dp.IsPermitted==true
                        select new PermittedDashboard
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Url = d.Url,
                            SecModuleId = d.SecModuleId,
                            SecCompanyId = d.SecCompanyId,
                            Status = d.Status,
                            Tag = d.Tag,
                            IsActive = d.IsActive                            
                        }).ToList();           

            return list;


        }







    }
}
