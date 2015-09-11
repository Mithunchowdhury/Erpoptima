using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Accounts.ViewModel
{
    public class ReportAnFVoucherViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ProjectName { get; set; }
        public string CostCenterName { get; set; }

        public float Debit { get; set; }
        public float Credit { get; set; }

        public float TotalAmount { get; set; }

        public string Naration { get; set; }
        public DateTime Date { get; set; }

        public string  VoucherNumber { get; set; }

        public string ShortNarration { get; set; }

        public string VoucherTitle { get; set; }
        
    }
}