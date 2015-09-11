using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Accounts.ViewModel
{
    public class AnFChequeBookViewModel
    {

        public long Id { get; set; }
        public string ChequeBookNo { get; set; }
        public long AnFChartOfAccountId { get; set; }
        public DateTime IssueDate { get; set; }
        public int NoofPage { get; set; }
        public string StartingPageNo { get; set; }
        public string EndingPageNo { get; set; }
    }
}