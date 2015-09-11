using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Accounts.ViewModel
{
    public class ReportAnFBalanceSheetSearchViewModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int SecCompanyId { get; set; }
        
    }

    public class ReportAnFBalanceSheetResultViewModel
    {
        public long Code1 { get; set; }
        public long Code2 { get; set; }
        public long Code3 { get; set; }

        public string Part1 { get; set; }
        public string Part2 { get; set; }
        public string Part3 { get; set; }

        public int ScheduleNo { get; set; }

        public decimal CurYearAmount { get; set; }
        public decimal PrevYearAmount { get; set; }

        public string Company { get; set; }
       

        public DateTime preDate { get; set; }

        public string dep { get; set; }
        public decimal curdep { get; set; }
        public decimal prevdep { get; set; }

        public int cid { get; set; }

    }


}