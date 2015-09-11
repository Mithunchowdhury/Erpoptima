using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Security
{
    public partial class SecGroup 
    {
        public SecGroup()
        {
            this.SecCompanies = new List<SecCompany>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Remarks { get; set; }
        public string Web { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<SecCompany> SecCompanies { get; set; }
    }
}
