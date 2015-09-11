using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Accounts.ViewModel
{
    public class ReportGeneralLedgerViewModel
    {
        public int Id { get; set; }

        public DateTime vdate { get; set; }
        public string vnum { get; set; }

        public string particular { get; set; }

        public string narration { get; set; }

        public float  debit { get; set; }

        public float credit { get; set; }

        public int vTypeId { get; set; }

        public int vsl { get; set; }

        public string project { get; set; }

        public string costcentre { get; set; }

        public string narr { get; set; }

        public string part2 { get; set; }

        public string shortNarr { get; set; }

        public float opnD { get; set; }

        public float opnC { get; set; }

        public float opn { get; set; }
                
    }
}