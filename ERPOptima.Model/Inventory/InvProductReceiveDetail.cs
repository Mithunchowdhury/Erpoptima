using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Inventory
{
    public partial class InvProductReceiveDetail
    {
        public int Id { get; set; }
        public Nullable<int> InvProductReceiveId { get; set; }
        public Nullable<int> SlsProductId { get; set; }
        public Nullable<decimal> IssuedQuantity { get; set; }
        public Nullable<decimal> ReceivedQuantity { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> SlsUnitId { get; set; }
        public virtual InvProductReceive InvProductReceive { get; set; }
        public virtual SlsUnit SlsUnit { get; set; }
    }
}
