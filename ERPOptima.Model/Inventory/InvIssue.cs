using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Inventory
{
    public partial class InvIssue
    {
        public InvIssue()
        {
            this.InvIssueDetails = new List<InvIssueDetail>();
            this.InvProductReceives = new List<InvProductReceive>();
            this.InvProductSendDetails = new List<InvProductSendDetail>();
        }

        public int Id { get; set; }
        public string IssueCode { get; set; }
        public Nullable<int> InvRequisitionId { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<int> InvStoreId { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> SecCompanyId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<InvIssueDetail> InvIssueDetails { get; set; }
        public virtual ICollection<InvProductReceive> InvProductReceives { get; set; }
        public virtual ICollection<InvProductSendDetail> InvProductSendDetails { get; set; }
        public virtual InvRequisition InvRequisition { get; set; }
        public virtual InvStore InvStore { get; set; }
        public virtual SecCompany SecCompany { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }


    }
}
