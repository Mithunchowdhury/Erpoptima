using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Security
{
    public partial class SecRolePermission
    {
        public int Id { get; set; }
        public int SecRoleId { get; set; }
        public int SecResourceId { get; set; }
        public Nullable<bool> Add { get; set; }
        public Nullable<bool> ReadOnly { get; set; }
        public Nullable<bool> Edit { get; set; }
        public Nullable<bool> Delete { get; set; }
        public Nullable<bool> Print { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SecResource SecResource { get; set; }
        public virtual SecRole SecRole { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
    }
}
