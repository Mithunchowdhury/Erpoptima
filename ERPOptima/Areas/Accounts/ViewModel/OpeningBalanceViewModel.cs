using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Accounts.ViewModel
{
    public class OpeningBalanceViewModel
    {

        [Range(0, int.MaxValue)]
        public long Id { get; set; }

        public long AnFChartOfAccountId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
        public Nullable<decimal> Debit { get; set; }
        public Nullable<decimal> Credit { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> IsEditable { get; set; }

    }
}