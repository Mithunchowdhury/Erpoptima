using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Accounts.ViewModel
{
    public class ReportAnFProfitLossViewModel
    {

        public string part3 { get; set; }
        public int notesNo { get; set; }

        public long CurYearAmount { get; set; }

        public long CurMonthAmount { get; set; }

        public long PrevYearAmount { get; set; }

        public string Company { get; set; }

        public string Project { get; set; }

        public string PLHead { get; set; }

        public int SLNo { get; set; }

        public DateTime preDate { get; set; }
    }
}