using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.HRM
{
    public partial class HrmDesignation
    {
        public HrmDesignation()
        {
            this.HrmDesignations1 = new List<HrmDesignation>();
            this.HrmEmployees = new List<HrmEmployee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public Nullable<int> HrmDesignationId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<HrmDesignation> HrmDesignations1 { get; set; }
        public virtual HrmDesignation HrmDesignation1 { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual ICollection<HrmEmployee> HrmEmployees { get; set; }
    }

}
