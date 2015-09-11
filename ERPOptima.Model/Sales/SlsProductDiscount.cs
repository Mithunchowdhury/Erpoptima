using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsProductDiscount
    {
        public int Id { get; set; }
        public int SlsRegionId { get; set; }
        public int SlsProuctId { get; set; }
        public decimal Discount { get; set; }
        public System.DateTime Date { get; set; }
        public string Remarks { get; set; }
        public virtual SlsProduct SlsProduct { get; set; }
        public virtual SlsRegion SlsRegion { get; set; }
    }
}
