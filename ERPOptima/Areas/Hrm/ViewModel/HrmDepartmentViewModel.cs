using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Hrm.ViewModel
{
    public class HrmDepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public Nullable<int> InCharge { get; set; }
        public string Description { get; set; }
        public string Employee { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}