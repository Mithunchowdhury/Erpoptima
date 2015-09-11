using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsSalesReturnDetail
    {
        public int Id { get; set; }
        public int SlsReturnId { get; set; }
        public int SlsProductId { get; set; }
        public decimal ReturnedQuantity { get; set; }
        public decimal Rate { get; set; }
        public int SlsUnitId { get; set; }
        public virtual SlsProduct SlsProduct { get; set; }
        public virtual SlsSalesReturn SlsSalesReturn { get; set; }
        public virtual SlsUnit SlsUnit { get; set; }
    }
}
