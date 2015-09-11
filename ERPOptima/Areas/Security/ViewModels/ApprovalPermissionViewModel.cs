using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Security.ViewModel
{
    public class ApprovalPermissionViewModel
    {
        public int Id { get; set; }
        public int ApprovalProcessLevelId { get; set; }
        public string ApprovalProcessName { get; set; }

        public string ProcessLevelName { get; set; }

        public bool Mapped { get; set; }
    }
}