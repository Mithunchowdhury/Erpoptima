using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPOptima.Model.Sales
{
    public class SlsTransferDetail
    {
        public int Id { get; set; }
        public Nullable<int> SlsTransferId { get; set; }
        public Nullable<int> SlsProductId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> SlsUnitId { get; set; }
        public virtual SlsProduct SlsProduct { get; set; }
        public virtual SlsTransfer SlsTransfer { get; set; }
        public virtual SlsUnit SlsUnit { get; set; }
    }
}
