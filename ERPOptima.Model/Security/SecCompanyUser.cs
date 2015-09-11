using ERPOptima.Model.Common;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Security
{
    public partial class SecCompanyUser
    {
        public int Id { get; set; }
        public int SecCompanyId { get; set; }
        public int SecUserId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SecCompany SecCompany { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual SecUser SecUser2 { get; set; }
    }
}
