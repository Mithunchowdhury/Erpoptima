using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class RptReportInvoiceViewModel
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime Date { get; set; }
        public int PartyName { get; set; }
        public string SoldBy { get; set; }
        public int Rate { get; set; }
        public int GrossAmount { get; set; }
        public int Discount { get; set; }
        public int Net { get; set; }
        public int SalesOrdersNo { get; set; }
        public int Total { get; set; }
        public string InWord { get; set; }
        public string PreparedBy { get; set; }
        public string ApprovedBy { get; set; }
    }
}