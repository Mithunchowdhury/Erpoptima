using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Accounts.ViewModel
{
    public class ReportAnfAdvanceAdjustmentViewModel
    {
        public decimal AdjustedAmount { get; set; }
        public System.DateTime Date { get; set; }
        public string Remarks { get; set; }
    }
}