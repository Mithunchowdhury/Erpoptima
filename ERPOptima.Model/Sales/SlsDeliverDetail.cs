using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Sales
{
    public partial class SlsDeliverDetail
    {
        public int Id { get; set; }
        public int SlsDeliveryId { get; set; }
        public int SlsProductId { get; set; }
        public decimal Quantity { get; set; }
        public int SlsUnitId { get; set; }
        public decimal Rate { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public virtual SlsDelivery SlsDelivery { get; set; }
    }
}
