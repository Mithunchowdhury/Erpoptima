using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Security
{
    public partial class SecDashboardPermission
    {
        public int Id { get; set; }
        public int SecRoleId { get; set; }
        public int SecDashboardId { get; set; }
        public bool IsPermitted { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SecDashboard SecDashboard { get; set; }
        public virtual SecRole SecRole { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
    }
}
