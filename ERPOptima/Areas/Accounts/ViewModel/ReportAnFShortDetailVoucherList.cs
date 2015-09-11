using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Accounts.ViewModel
{
    public class ReportAnFShortDetailVoucherList
    {
        
        public long GroupId { get; set; }
        public long GroupDetailsId { get; set; }

        public string Naration { get; set; }

        public string ShortNaration { get; set; }

        public long Debit { get; set; }

        public long Credit { get; set; }

        public string CostCenterName { get; set; }


        public string ProjectName { get; set; }

        public string Name { get; set; }

        public string VoucherNumber { get; set; }

        public DateTime Date { get; set; }



    }
}