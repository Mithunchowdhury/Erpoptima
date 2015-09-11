using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class RptSalesTargetViewModel
    {
        public string EmployeeName { get; set; }
        public string ProductName { get; set; }
        public string UnitName { get; set; }
        //public DateTime CreatedDate { get; set; }
        public decimal TargetQuantity { get; set; }
        public string Code { get; set; }
        public decimal TargetCollectionAmount { get; set; }
        public decimal TargetSalesAmount { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }


    }
}