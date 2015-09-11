using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Accounts.ViewModel
{
    public class ReportAnFExpenseViewModel
    {
        public int Id { get; set; }
        public string RefNo { get; set; }
        public string Particular { get; set; }
        public long AnFChartOfAccountId { get; set; }
        public int AnFCostCenterId { get; set; }
        public System.DateTime Date { get; set; }
        public string BillNo { get; set; }
        public int Mode { get; set; }
        public string Narration { get; set; }
        public string FileLocation { get; set; }
        public int CmnFinancialYearId { get; set; }
        public int SecCompanyId { get; set; }
        public int AnFVoucherId { get; set; }
        public decimal Amount { get; set; }
        public bool IsPosted { get; set; }
          
      
    }
}