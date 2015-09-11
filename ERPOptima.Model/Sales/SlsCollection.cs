using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsCollection
    {
        public int Id { get; set; }
        public string RefNo { get; set; }
        public int PaymentMode { get; set; }
        public int PartyType { get; set; }
        public int CollectedFrom { get; set; }
        public decimal Amount { get; set; }
        public int TransactionType { get; set; }
        public string TransactionRefNo { get; set; }
        public string BankName { get; set; }
        public Nullable<System.DateTime> CollectionDate { get; set; }
        public Nullable<int> HrmEmployeeId { get; set; }
        public Nullable<int> SecCompanyId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }

        public virtual SecCompany SecCompanies { get; set; }
    }
}
