using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Security.ViewModel
{
    public class CmnApprovalProcessLevelViewModel
    {
        public int Id { get; set; }
        public int CmnApprovalProcessId { get; set; }
        public int CmnProcessLevelId { get; set; }

        public bool Mapped { get; set; }
        public int CmnCompanyId { get; set; }
        public int Priority { get; set; }

    }
}