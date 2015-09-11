using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Accounts
{
    public partial class CmnMappingType
    {
        public CmnMappingType()
        {
            this.CmnAccountHeadMappers = new List<CmnAccountHeadMapper>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ModuleId { get; set; }
        public string Remarks { get; set; }
        public virtual ICollection<CmnAccountHeadMapper> CmnAccountHeadMappers { get; set; }
    }
}
