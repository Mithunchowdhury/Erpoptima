using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Security.ViewModel
{
    public class CmnApprovalUserPermissionViewModel
    {

        public int Id { get; set; }
        public int CmnApprovalProcessLevelId { get; set; }
        public int SecUserId { get; set; }

        public bool Mapped { get; set; }

    }
}