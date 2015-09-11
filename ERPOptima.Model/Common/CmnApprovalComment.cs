using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Common
{
    public partial class CmnApprovalComment
    {
        public long Id { get; set; }
        public long CmnApprovalId { get; set; }
        public string Comments { get; set; }
        public Nullable<int> Commentator { get; set; }
        public System.DateTime CommentDate { get; set; }
    }
}
