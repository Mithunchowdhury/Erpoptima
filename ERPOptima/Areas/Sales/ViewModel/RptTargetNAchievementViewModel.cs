using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.ViewModel
{
    public class RptTargetNAchievementViewModel
    {
        public int HrmEmployeeId { get; set; }
        public string SalesPerson { get; set; }
        public int SlsProductId { get; set; }
        public string Code { get; set; }
        public string Product { get; set; }
        public decimal TargetQuantity { get; set; }
        public string TargetUnit { get; set; }
        public decimal AchievedQuantity { get; set; }
        public decimal TargetSalesAmount { get; set; }
        public decimal AchievedAmount { get; set; }
        public decimal CollectionTargetAmount { get; set; }
        public decimal CollectionAmount { get; set; }

       
       
    }
}
