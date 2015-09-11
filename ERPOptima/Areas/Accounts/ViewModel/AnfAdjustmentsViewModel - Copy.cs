using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Accounts.ViewModel
{
    public class AnfAdjustmentsViewModel
    {
        public int Id { get; set; }
        public int AnFAdvanceId { get; set; }
        public decimal AdjustedAmount { get; set; }
        public System.DateTime Date { get; set; }
        public int CmnFinancialYearId { get; set; }
        public Nullable<int> SecCompanyId { get; set; }
        public Nullable<int> AnFVoucherId { get; set; }
      
        public string AdvanceRefNo { get; set; }
        public string EmployeeName { get; set; }
    }
}