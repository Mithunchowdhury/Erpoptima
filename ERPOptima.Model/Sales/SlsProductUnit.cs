using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsProductUnit
    {
        public int Id { get; set; }
        public int SlsProductId { get; set; }
        public int SlsUnitId { get; set; }
        public Nullable<int> ParentUnitId { get; set; }
        public Nullable<decimal> ConversionRate { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual SlsUnit SlsUnit { get; set; }
        public virtual SlsUnit SlsUnit1 { get; set; }
    }
}
