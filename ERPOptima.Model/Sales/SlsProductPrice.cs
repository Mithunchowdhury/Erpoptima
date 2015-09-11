using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsProductPrice
    {
        public int Id { get; set; }
        public int SlsProductId { get; set; }
        public int SlsUnitId { get; set; }
        public decimal FactoryCost { get; set; }
        public decimal MRP { get; set; }
        public decimal DistributorCommission { get; set; }
        public decimal RetailCommission { get; set; }
        public System.DateTime DeclarationDate { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual SlsProduct SlsProduct { get; set; }
        public virtual SlsUnit SlsUnit { get; set; }
    }
}
