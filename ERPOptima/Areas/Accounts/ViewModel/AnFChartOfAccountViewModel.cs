using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Accounts.ViewModel
{
    public class AnFChartOfAccountViewModel
    {

        public long Id { get; set; }
        public Nullable<long> AnFChartOfAccountId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int CmnCompanyId { get; set; }
        public string ScheduleNo { get; set; }
        public Nullable<int> DepthLevel { get; set; }
        public bool IsTransactionalHead { get; set; }
        public bool Status { get; set; }

    }

 
}