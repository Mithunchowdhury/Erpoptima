using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Inventory
{
    public partial class InvRequisition
    {
        public InvRequisition()
        {
            this.InvIssues = new List<InvIssue>();
            this.InvProductSendDetails = new List<InvProductSendDetail>();
            this.InvRequisitionApprovals = new List<InvRequisitionApproval>();
            this.InvRequisitionDetails = new List<InvRequisitionDetail>();
        }

        public int Id { get; set; }
        public string RequisitionCode { get; set; }
        public System.DateTime PreferredDeliveryDate { get; set; }
        public Nullable<int> SecCompanyId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> ApprovalStatus { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<InvIssue> InvIssues { get; set; }
        public virtual ICollection<InvProductSendDetail> InvProductSendDetails { get; set; }
        public virtual ICollection<InvRequisitionApproval> InvRequisitionApprovals { get; set; }
        public virtual ICollection<InvRequisitionDetail> InvRequisitionDetails { get; set; }
        public virtual SecCompany SecCompany { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
    }
}
