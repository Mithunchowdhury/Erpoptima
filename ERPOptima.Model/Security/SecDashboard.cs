using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Security
{
    public partial class SecDashboard
    {
        public SecDashboard()
        {
            this.SecDashboardPermissions = new List<SecDashboardPermission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int SecModuleId { get; set; }
        public int SecCompanyId { get; set; }
        public bool Status { get; set; }
        public string Tag { get; set; }
        public bool IsActive { get; set; }
        public virtual SecCompany SecCompany { get; set; }
        public virtual ICollection<SecDashboardPermission> SecDashboardPermissions { get; set; }
        public virtual SecModule SecModule { get; set; }
    }

    public class PermittedDashboard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int SecModuleId { get; set; }
        public int SecCompanyId { get; set; }
        public bool Status { get; set; }
        public string Tag { get; set; }
        public bool IsActive { get; set; }
    }




}
