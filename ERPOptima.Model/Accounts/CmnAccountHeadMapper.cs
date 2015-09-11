using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Accounts
{
    public partial class CmnAccountHeadMapper
    {
        public int Id { get; set; }
        public Nullable<int> SecCompanyId { get; set; }
        public Nullable<long> AnFChartOfAccountId { get; set; }
        public Nullable<int> CnmMappingTypeId { get; set; }
        public string Remarks { get; set; }
        public virtual AnFChartOfAccount AnFChartOfAccount { get; set; }
        public virtual CmnMappingType CmnMappingType { get; set; }
        public virtual SecCompany SecCompany { get; set; }
    }
}
