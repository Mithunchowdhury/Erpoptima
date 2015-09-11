using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Common.ViewModel
{
    public class CmnApprovalProcessLevelMappingViewModel
    {
        public int Id { get; set; }
        public int ProcessLevelId { get; set; }

        public string Name { get; set; }

        public bool Mapped { get; set; }

    }
}