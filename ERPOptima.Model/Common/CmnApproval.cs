using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Common
{
    public partial class CmnApproval
    {
        public long Id { get; set; }
        public int CmnApprovalProcessId { get; set; }
        public long RefId { get; set; }
        public int CmnProcessLevelId { get; set; }
        public bool Value { get; set; }
        public Nullable<int> DoneBy { get; set; }
        public Nullable<System.DateTime> DoneDateTime { get; set; }
        public int CmnCompanyId { get; set; }
        public virtual CmnApprovalProcess CmnApprovalProcess { get; set; }
        public virtual CmnProcessLevel CmnProcessLevel { get; set; }
    }
}
