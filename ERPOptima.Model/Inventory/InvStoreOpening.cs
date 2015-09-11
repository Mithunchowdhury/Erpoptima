using ERPOptima.Model.Inventory;
using ERPOptima.Model.Sales;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Inventory
{
    public partial class InvStoreOpening
    {
        public int Id { get; set; }
        public int InvStoreId { get; set; }
        public Nullable<int> SlsProductId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> SlsUnitId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual InvStore InvStore { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual SlsProduct SlsProduct { get; set; }
        public virtual SlsUnit SlsUnit { get; set; }
    }
}