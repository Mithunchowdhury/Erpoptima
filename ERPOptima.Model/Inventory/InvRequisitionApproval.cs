using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Inventory
{
    public partial class InvRequisitionApproval
    {
        public int Id { get; set; }
        public Nullable<int> InvRequisitionsId { get; set; }
        public Nullable<int> From { get; set; }
        public Nullable<int> To { get; set; }
        public Nullable<int> Action { get; set; }
        public string Comment { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual InvRequisition InvRequisition { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
    }
}
