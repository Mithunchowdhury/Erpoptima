using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Accounts.ViewModel
{
    public class ReportAnFNoteScheduleProjectViewModel
    {


        public int companyId { get; set; }
        public string project { get; set; }

        public long prevDebit { get; set; }

        public long prevCredit { get; set; }

        public long trnDebit { get; set; }

        public long trnCredit { get; set; }

        public long cumDebit { get; set; }

        public long cumCredit { get; set; }

        public string Company { get; set; }

        public string Part { get; set; }
    }
}