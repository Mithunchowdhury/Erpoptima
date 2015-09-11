using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.ViewModel.Sales
{
    public partial class SlsIncentiveViewModel
    {
        private DateTime DefaultNullDatetime = DateTime.MinValue;

        public string MonthName { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeDesignation { get; set; }

        public int Id { get; set; }
        public int HrmEmployeeId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Commission { get; set; }
        public decimal AmountPaid { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy
        {
            get;
            set;
        }
        public DateTime ModifiedDate
        {
            get;
            set;
        }

        
    }
}
