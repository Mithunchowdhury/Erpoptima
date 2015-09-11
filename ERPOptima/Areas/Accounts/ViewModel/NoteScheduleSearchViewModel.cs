using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Accounts.ViewModel
{
    public class NoteScheduleSearchViewModel
    {
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
        public int? BusinessId { get; set; }
        public int? ProjectId { get; set; }

        public int? CostcenterId { get; set; }
        public int AnFChartOfAccountId { get; set; }
        [Required]
        public int Type { get; set; }
       
    }
}