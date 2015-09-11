using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Accounts
{
   public class AnFVoucherType
    {
        public int Id { get; set; }
        public string Prefix { get; set; }

        public string Name { get; set; }

        public bool? Status { get; set; }       
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
