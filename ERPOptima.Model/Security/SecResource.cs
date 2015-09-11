using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Security
{
    public partial class SecResource
    {
        public SecResource()
        {
            this.SecResources1 = new List<SecResource>();
            this.SecRolePermissions = new List<SecRolePermission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public Nullable<int> SecResourcesId { get; set; }
        public int SecModuleId { get; set; }
        public bool Status { get; set; }
        public string Tag { get; set; }
        public int SerialNo { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SecModule SecModule { get; set; }
        public virtual ICollection<SecResource> SecResources1 { get; set; }
        public virtual SecResource SecResource1 { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual ICollection<SecRolePermission> SecRolePermissions { get; set; }
    }
}
