using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class RptSlsDailySalesViewModel
    {
        public string Code { get; set; }
        public string Product { get; set; }
        public string Name  { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public decimal Total { get; set; }
        
    }
}