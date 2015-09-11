using System;

namespace Optima.Areas.Accounts.ViewModel
{
    public class ReportAnFTrialbalanceProjectWiseSearchViewModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int? BusinessId { get; set; }
        public int? CmnProjectId { get; set; }

    }

    public class ReportAnFTrialbalanceProjectWiseResultViewModel
    {
        public int companyId { get; set; }

        public long pojectId { get; set; }

        public string project { get; set; }

        public decimal prevDebit { get; set; }

        public decimal prevCredit { get; set; }

        public decimal trnDebit { get; set; }

        public decimal trnCredit { get; set; }

        public decimal cumDebit { get; set; }

        public decimal cumCredit { get; set; }

        public string company { get; set; }
    }
}