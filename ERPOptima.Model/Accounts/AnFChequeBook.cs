using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Accounts
{
    public partial class AnFChequeBook
    {
        public long Id { get; set; }
        public string ChequeBookNo { get; set; }
        public Nullable<long> AnFChartOfAccountId { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public Nullable<int> NoofPage { get; set; }
        public string StartingPageNo { get; set; }
        public string EndingPageNo { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual AnFChartOfAccount AnFChartOfAccount { get; set; }
    }
}
