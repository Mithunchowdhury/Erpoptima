using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class RptSalesReturnListViewModel
    {
        public DateTime Date { get; set; }
        public string Party { get; set; }
        public string RefNo { get; set; }
        public string Reason { get; set; }
        public decimal AdjustedAmount { get; set; }
        public decimal Quantity { get; set; }
        public string Product { get; set; }
        public string Unit { get; set; }

    }
}