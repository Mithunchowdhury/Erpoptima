using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class RptPriceListViewModel
    {
       
        public string Category { get; set; } 
        public string Product { get; set; }
        public decimal FactoryCost { get; set; }
        public string Unit { get; set; }        
        public string Code { get; set; }        
        public decimal MRP { get; set; }
       
    }
}