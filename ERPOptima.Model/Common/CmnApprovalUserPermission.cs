using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Common
{
    public partial class CmnApprovalUserPermission
    {
        public int Id { get; set; }
        public Nullable<int> CmnApprovalProcessLevelId { get; set; }
        public int SecUserId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual CmnApprovalProcessLevel CmnApprovalProcessLevel { get; set; }
    }
}
