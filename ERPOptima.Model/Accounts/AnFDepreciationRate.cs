using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Accounts
{
    public partial class AnFDepreciationRate
    {
        public int Id { get; set; }
        public long AnFChartOfAccountId { get; set; }
        public decimal Rate { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Remarks { get; set; }
        public Nullable<long> AnFCOAParentId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual AnFChartOfAccount AnFChartOfAccount { get; set; }
    }
}
