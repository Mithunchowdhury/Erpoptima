using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Accounts.ViewModel
{
    public class GeneralLedgerSearchViewModel
    {

        public DateTime DateFrom { get; set; }

        public DateTime ToDate { get; set; }

        public int? BusinessId { get; set; }

        public int? ProjectId { get; set; }


        public int? CostcenterId { get; set; }

        [Required]
        public int AnFChartOfAccountId { get; set; }

        [Required]
        public bool Status { get; set; }

        public int Type { get; set; }
    }
}