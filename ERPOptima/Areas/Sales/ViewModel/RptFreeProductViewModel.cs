using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class RptFreeProductViewModel
    {
      
        public string Code { get; set; }
        public string Product { get; set; }
        public int MQuantity { get; set; }
        public string MUnit { get; set; }
        public int FQuantity { get; set; }
        public string FreeUnit { get; set; }
    }
}