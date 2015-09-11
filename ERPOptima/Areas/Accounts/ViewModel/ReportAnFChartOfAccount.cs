using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Accounts.ViewModel
{
    public class ReportAnFChartOfAccountViewModel
    {
        public long Clild1 { get; set; }
        public string Particular1 { get; set; }

        public long Schedule1 { get; set; }

        public long Code1 { get; set; }
        public long Code2 { get; set; }
        public long Code3 { get; set; }
        public long Code4 { get; set; }
        public long Code5 { get; set; }
        public long Code6 { get; set; }

        public long Clild2 { get; set; }
        public string Particular2 { get; set; }

        public long Schedule2 { get; set; }



        public long Clild3 { get; set; }
        public string Particular3 { get; set; }

        public long Schedule3 { get; set; }



        public long Clild4 { get; set; }
        public string Particular4 { get; set; }

        public long Schedule4 { get; set; }


        public long Clild5 { get; set; }
        public string Particular5 { get; set; }

        public long Schedule5 { get; set; }
        public long Clild6 { get; set; }
        public string Particular6 { get; set; }

        public long Schedule6 { get; set; }

    }

    public class AnFChartOfAccountWIthOpeningbalanceReportViewModel
    {
        public long CmnProjectId { get; set; }
    
    }

    public class ReportAnFChartOfAccountWIthOpeningbalance
    {
        public long Code { get; set; }
        public string Particular { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public int DepthLevel { get; set; }
        public bool IsTransactionalHead { get; set; }
        public string Company { get; set; }
        public string fyName { get; set; }
    }

}