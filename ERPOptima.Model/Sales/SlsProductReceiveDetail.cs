using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Sales
{
    public partial class SlsProductReceiveDetail
    {
        public int Id { get; set; }
        public int SlsProductReceiveId { get; set; }
        public int SlsProductId { get; set; }
        public int SlsUnitId { get; set; }
        public Nullable<decimal> DeliveryQuantity { get; set; }
        public Nullable<decimal> ReceivedQuantity { get; set; }
        public string Remarks { get; set; }

        public virtual SlsProductReceive SlsProductReceive { get; set; }
        public virtual SlsProduct SlsProduct { get; set; }
        public virtual SlsUnit SlsUnit { get; set; }
    }
}
