using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class RptSalesOrderListViewModel
    {
        public string RefNo { get; set; }
        public int Party { get; set; }
        public DateTime PreferredDeliveryDate { get; set; }
        public int TotalAmount { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public int SalesExecutive { get; set; }
    }
}