using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Optima.Areas.Accounts.ViewModel
{
    public class ReportReceivableAndPayableViewModel
    {
        public long Id { get; set; }  
        public string Name { get; set; }
        public float Debit { get; set; }
        public float Credit { get; set; }
        public long Balance { get; set; }
        public int? CostcenterId { get; set; }

    }
}