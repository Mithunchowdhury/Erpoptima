using ERPOptima.Model.Common;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Security
{
    public partial class SecRole
    {
        public SecRole()
        {
            this.SecDashboardPermissions = new List<SecDashboardPermission>();
            this.SecRolePermissions = new List<SecRolePermission>();
            this.SecUsers = new List<SecUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<SecDashboardPermission> SecDashboardPermissions { get; set; }
        public virtual ICollection<SecRolePermission> SecRolePermissions { get; set; }
        public virtual ICollection<SecUser> SecUsers { get; set; }
    }
}
