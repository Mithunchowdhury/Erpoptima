using System;

namespace Optima.Areas.Accounts.ViewModel
{
    public class ReportAnFTrialBalanceDetalisSearchViewModel
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }     

        public int AnFCostCeterId { get; set; }

        public bool? IsPosted { get; set; }
    }

    public class ReportAnFTrialBalanceResultViewModel
    {
        public long Code1 { get; set; }

        public string Part1 { get; set; }

        public long Code2 { get; set; }

        public string Part2 { get; set; }

        public long Code3 { get; set; }

        public string Part3 { get; set; }

        public long Code4 { get; set; }

        public string Part4 { get; set; }

        public long Code5 { get; set; }

        public string Part5 { get; set; }

        public long Code6 { get; set; }

        public string Part6 { get; set; }

        public long Code { get; set; }

        public string particulars { get; set; }

        public decimal PrevDebit { get; set; }

        public decimal PrevCredit { get; set; }

        public decimal TmDebit { get; set; }

        public decimal TmCredit { get; set; }

        public decimal CumDebit { get; set; }

        public decimal CumCredit { get; set; }

        public int depthLevel { get; set; }

        public string Company { get; set; }
      
    }
}