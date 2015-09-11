using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class StockBalanceReportViewModel
    {

         public int Id { get; set; }
         public int InvStoreId { get; set; }
         public string StoreName { get; set; }
         public string Code { get; set; }
         public string Product { get; set; }
         public decimal Quantity { get; set; }
         public string Unit { get; set; }
         public int UnitId { get; set; }
         public decimal UnitPrice { get; set; }
         public decimal TotalPrice { get; set; }
        

    }
}