using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsFreeProduct
    {
        public int Id { get; set; }
        public int SlsProductId { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public int MeasurementQuantity { get; set; }
        public int SlsUnitId { get; set; }
        public int FreeQuantity { get; set; }
        public int FreeUnitId { get; set; }
        public string Remarks { get; set; }
        public int SecCompnayId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual SlsProduct SlsProduct { get; set; }
        public virtual SlsUnit SlsUnit { get; set; }
        public virtual SlsUnit SlsUnit1 { get; set; }
    }
}
