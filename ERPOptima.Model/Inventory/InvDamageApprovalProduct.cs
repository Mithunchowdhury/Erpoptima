using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Inventory
{
    public partial class InvDamageApprovalProduct
    {
        public int Id { get; set; }
        public int InvDamageId { get; set; }
        public decimal DamagedQuantity { get; set; }
        public int SlsUnitId { get; set; }
        public string Reason { get; set; }
        public decimal ApprovedQuantity { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual InvDamage InvDamage { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
    }
}
