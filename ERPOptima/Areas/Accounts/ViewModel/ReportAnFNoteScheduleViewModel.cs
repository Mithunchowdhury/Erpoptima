using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Accounts.ViewModel
{
    public class ReportAnFNoteScheduleViewModel
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

        public long PrevDebit { get; set; }

        public long PrevCredit { get; set; }

        public long TrnDebit { get; set; }

        public long TrnCredit { get; set; }

        public long CumDebit { get; set; }

        public long CumCredit { get; set; }

        public int depthLevel { get; set; }

        public int note { get; set; }

        public string Company { get; set; }

        public string Project { get; set; }

    }
}