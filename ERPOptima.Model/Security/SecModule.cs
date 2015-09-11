using ERPOptima.Model.Common;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Security
{
    public partial class SecModule
    {
        public SecModule()
        {
            this.CmnApprovalProcesses = new List<CmnApprovalProcess>();
            this.CmnFinancialYears = new List<CmnFinancialYear>();
            this.SecDashboards = new List<SecDashboard>();
            this.SecResources = new List<SecResource>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Url { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> Status { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<CmnApprovalProcess> CmnApprovalProcesses { get; set; }
        public virtual ICollection<CmnFinancialYear> CmnFinancialYears { get; set; }
        public virtual ICollection<SecDashboard> SecDashboards { get; set; }
        public virtual ICollection<SecResource> SecResources { get; set; }
    }
}
