using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Common
{
    public partial class CmnProcessLevel
    {
        public CmnProcessLevel()
        {
            this.CmnApprovalProcessLevels = new List<CmnApprovalProcessLevel>();
            this.CmnApprovals = new List<CmnApproval>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<CmnApprovalProcessLevel> CmnApprovalProcessLevels { get; set; }
        public virtual ICollection<CmnApproval> CmnApprovals { get; set; }
    }
}
