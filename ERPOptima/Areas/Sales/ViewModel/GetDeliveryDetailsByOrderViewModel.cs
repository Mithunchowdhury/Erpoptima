using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class GetDeliveryDetailsByOrderViewModel
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
        public decimal SalesOrderQuantity { get; set; }
        public string PName { get; set; }
        public string UName { get; set; }
      

    }
}