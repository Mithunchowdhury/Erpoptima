using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Inventory
{
    public partial class InvProductReceive
    {
        public InvProductReceive()
        {
            this.InvProductReceiveDetails = new List<InvProductReceiveDetail>();
        }

        public int Id { get; set; }
        public int InvStoreId { get; set; }
        public int InvIssueId { get; set; }
        public string ChallanNo { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual InvIssue InvIssue { get; set; }
        public virtual ICollection<InvProductReceiveDetail> InvProductReceiveDetails { get; set; }
        public virtual InvStore InvStore { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }


       
    }
}
