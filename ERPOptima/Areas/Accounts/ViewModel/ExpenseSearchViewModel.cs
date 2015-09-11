using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Accounts.ViewModel
{
    public class ExpenseSearchViewModel
    {
        public DateTime DateFrom { get; set; }

        public DateTime ToDate { get; set; }

        public int? CostcenterId { get; set; }     
    }
}