using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.ViewModel
{    
    public class TargetList
    {
        public int Id { get; set; }
        public string RefNo { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int HrmEmployeeId { get; set; }
        public string MonthName { get; set; }
        public string Employee { get; set; }
        public Nullable<decimal> TargetSalesAmount { get; set; }        
        public string Remarks { get; set; }
        public int SecCompanyId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public System.DateTime? ModifiedDate { get; set; }
    }
}
