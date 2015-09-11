using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Accounts.ViewModel
{
    public class VoucherDashBoardViewModel
    {

        public string Type { get; set; }
        public int Total { get; set; }

        public int Cancelled { get; set; }
        public int Posted { get; set; }
        public int UnPosted { get; set; }

        public int Check { get; set; }

        public int Approve { get; set; }
    }
}