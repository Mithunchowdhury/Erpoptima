using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class SalesCommissionReportViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal CommissionPercentage { get; set; }
        public string BasedOn { get; set; }
        public decimal Commission { get; set; }
        public DateTime Date { get; set; }

    }
}