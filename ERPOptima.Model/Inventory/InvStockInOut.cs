using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Inventory
{
    public partial class InvStockInOut
    {
        public int Id { get; set; }
        public Nullable<int> InvStoreId { get; set; }
        public int TransactionType { get; set; }
        public int? RefId { get; set; }
        public int SlsProductId { get; set; }
        public decimal Quantity { get; set; }       
        public int Status { get; set; }
        public Nullable<int> SlsUnitId { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }

        public virtual InvStore InvStore { get; set; }
        public virtual SlsProduct SlsProduct { get; set; }
        public virtual SlsUnit SlsUnit { get; set; } 
       
    }
}
