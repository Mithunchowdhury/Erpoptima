using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.ViewModel
{
    public class HrmEmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> HrmDesignationId { get; set; }
        public Nullable<int> HrmDepartmentId { get; set; }
        public Nullable<int> SecCompanyId { get; set; }
        public Nullable<int> LineManager { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Nullable<int> SlsOfficeId { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> SlsDistributorId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
