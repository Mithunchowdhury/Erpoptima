using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsSalesOrderDetail
    {
        public int Id { get; set; }
        public int SlsSalesOrderId { get; set; }
        public int SlsProductId { get; set; }
        public decimal SalesOrderQuantity { get; set; }
        public int SlsUnitId { get; set; }
        public decimal Rate { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public virtual SlsProduct SlsProduct { get; set; }
        public virtual SlsSalesOrder SlsSalesOrder { get; set; }
        public virtual SlsUnit SlsUnit { get; set; }
    }


    public partial class SalesOrderDetail
    {
        public int Id { get; set; }
        public int SlsSalesOrderId { get; set; }
        public int SlsProductId { get; set; }
        public decimal SalesOrderQuantity { get; set; }
        public int SlsUnitId { get; set; }
        public decimal Rate { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public string ProductName { get; set; }
        public string UnitName { get; set; }
    }







}
