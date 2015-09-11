using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Accounts
{
    public partial class AnFDeliveryMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
