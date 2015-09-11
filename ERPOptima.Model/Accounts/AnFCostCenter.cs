using ERPOptima.Model.Common;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Accounts
{
    public partial class AnFCostCenter
    {
        public AnFCostCenter()
        {
            this.AnFAdvances = new List<AnFAdvance>();
            this.AnFExpenses = new List<AnFExpens>();
        }

        public int Id { get; set; }
        public int CmnCompanyId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ContactNo { get; set; }
        public string ContactPerson { get; set; }
        public string Remarks { get; set; }
        public bool Status { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<AnFAdvance> AnFAdvances { get; set; }
        public virtual ICollection<AnFExpens> AnFExpenses { get; set; }
    }
}
