using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Security
{
    public partial class SecUserRole
    {
        public int Id { get; set; }
        public int SecUserId { get; set; }
        public int SecRoleId { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SecRole SecRole { get; set; }
        public virtual SecUser SecUser { get; set; }
    }
}
