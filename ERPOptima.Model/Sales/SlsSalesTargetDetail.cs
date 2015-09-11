using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsSalesTargetDetail
    {
        public int Id { get; set; }
        public int SlsSalesTargetId { get; set; }
        public int SlsProductId { get; set; }
        public decimal Quantity { get; set; }
        public int SlsUnitId { get; set; }
        public virtual SlsProduct SlsProduct { get; set; }
        public virtual SlsSalesTarget SlsSalesTarget { get; set; }
        public virtual SlsUnit SlsUnit { get; set; }
    }
}
