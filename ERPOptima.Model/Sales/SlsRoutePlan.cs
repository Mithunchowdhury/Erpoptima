using ERPOptima.Model.HRM;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsRoutePlan
    {
        public SlsRoutePlan()
        {
            this.SlsRoutePlanApprovals = new List<SlsRoutePlanApproval>();
            this.SlsRoutePlanDetails = new List<SlsRoutePlanDetail>();
        }

        public int Id { get; set; }
        public int WeekNo { get; set; }
        public Nullable<System.DateTime> DateFrom { get; set; }
        public Nullable<System.DateTime> DateTo { get; set; }
        public int HrmEmployeeId { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public Nullable<int> ApprovalStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual HrmEmployee HrmEmployee { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual ICollection<SlsRoutePlanApproval> SlsRoutePlanApprovals { get; set; }
        public virtual ICollection<SlsRoutePlanDetail> SlsRoutePlanDetails { get; set; }
    }
}
