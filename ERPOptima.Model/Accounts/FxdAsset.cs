using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Accounts
{
    public partial class FxdAsset
    {
        public FxdAsset()
        {
            this.FxdAcquisitions = new List<FxdAcquisition>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<long> AnFChartOfAccountId { get; set; }
        public int SecCompanyId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual AnFChartOfAccount AnFChartOfAccount { get; set; }
        public virtual ICollection<FxdAcquisition> FxdAcquisitions { get; set; }
        public virtual SecCompany SecCompany { get; set; }
    }
}
