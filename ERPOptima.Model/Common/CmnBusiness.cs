using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Common
{
    public partial class CmnBusiness
    {
        public CmnBusiness()
        {
            this.CmnProjects = new List<CmnProject>();
        }

        public int Id { get; set; }
        public int CmnCompanyId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> Status { get; set; }
        public string Prefix { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<CmnProject> CmnProjects { get; set; }
    }
}
