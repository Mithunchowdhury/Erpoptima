using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsDefect
    {
        public SlsDefect()
        {
            this.SlsDefectDetails = new List<SlsDefectDetail>();
        }

        public int Id { get; set; }
        public Nullable<int> SlsDistributorId { get; set; }
        public Nullable<int> SlsDealerId { get; set; }
        public Nullable<int> SlsRetailerId { get; set; }
        public Nullable<int> SlsCorporateClientId { get; set; }
        public int SecCompanyId { get; set; }
        public string RefNo { get; set; }
        public System.DateTime Date { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual SlsCorporateClient SlsCorporateClient { get; set; }
        public virtual SlsDealer SlsDealer { get; set; }
        public virtual ICollection<SlsDefectDetail> SlsDefectDetails { get; set; }
        public virtual SlsDistributor SlsDistributor { get; set; }
        public virtual SlsRetailer SlsRetailer { get; set; }
    }
    public partial class SlsDefectDetailViewModel
    {
        public int Id { get; set; }
        public int SlsDefectId { get; set; }
        public int SlsProductId { get; set; }
        public decimal Quantity { get; set; }
        public int SlsUnitId { get; set; }
        public string Reason { get; set; }
        public decimal ReplacedQuantity { get; set; }
        public decimal AdjustedAmount { get; set; }


        public string SlsProductName { get; set; }
        public string SlsUnitName { get; set; }
        
    }
}
