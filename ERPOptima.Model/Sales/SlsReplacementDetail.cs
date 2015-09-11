using ERPOptima.Model.Inventory;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsReplacementDetail
    {
        public int Id { get; set; }
        public Nullable<int> SlsReplacementId { get; set; }
        public Nullable<int> SlsProductId { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<int> SlsUnitId { get; set; }
        public Nullable<decimal> AdjustedAmount { get; set; }
        public string Reason { get; set; }
        public virtual SlsProduct SlsProduct { get; set; }
        public virtual SlsReplacement SlsReplacement { get; set; }
        public virtual SlsUnit SlsUnit { get; set; }
    }
}
