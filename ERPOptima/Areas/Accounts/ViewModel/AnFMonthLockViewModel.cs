using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Optima.Areas.Accounts.ViewModel
{
    public class AnFMonthLockViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public bool LockStatus { get; set; }
    }
}