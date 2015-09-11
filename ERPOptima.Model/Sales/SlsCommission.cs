using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsCommission
    {
        public int Id { get; set; }
        public Nullable<int> SlsDitributorId { get; set; }
        public Nullable<int> SlsDealerId { get; set; }
        public int MonthFrom { get; set; }
        public int MonthTo { get; set; }
        public int YearFrom { get; set; }
        public int YearTo { get; set; }
        public decimal NetSaleAmount { get; set; }
        public decimal CommissionPercentage { get; set; }
        public decimal Commission { get; set; }
        public System.DateTime Date { get; set; }
        public string ChequeNo { get; set; }
        public string Bank { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual SlsDealer SlsDealer { get; set; }
    }
}
