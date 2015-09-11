using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class PartyCreditReportViewModel
    {

        public string Name { get; set; }
        public string Type { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal CurrentCredit { get; set; }
      


    }
}