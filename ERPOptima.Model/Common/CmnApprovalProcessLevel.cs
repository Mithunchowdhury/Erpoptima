using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Common
{
    public partial class CmnApprovalProcessLevel
    {
        public CmnApprovalProcessLevel()
        {
            this.CmnApprovalUserPermissions = new List<CmnApprovalUserPermission>();
        }

        public int Id { get; set; }
        public Nullable<int> CmnCompanyId { get; set; }
        public Nullable<int> CmnApprovalProcessId { get; set; }
        public int CmnProcessLevelId { get; set; }
        public int Priority { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual CmnApprovalProcess CmnApprovalProcess { get; set; }
        public virtual ICollection<CmnApprovalUserPermission> CmnApprovalUserPermissions { get; set; }
        public virtual CmnProcessLevel CmnProcessLevel { get; set; }
    }
}
