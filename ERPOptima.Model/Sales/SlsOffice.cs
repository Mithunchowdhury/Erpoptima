using ERPOptima.Model.HRM;
using ERPOptima.Model.Inventory;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsOffice
    {
        public SlsOffice()
        {
            this.InvProductSendDetails = new List<InvProductSendDetail>();
            this.InvStores = new List<InvStore>();
            this.SlsDealers = new List<SlsDealer>();
            this.SlsDealers1 = new List<SlsDealer>();
            this.SlsDistributors = new List<SlsDistributor>();
            this.SlsDistricts = new List<SlsDistrict>();
            this.SlsOffices1 = new List<SlsOffice>();
            this.SlsRetailers = new List<SlsRetailer>();
            this.SlsRoutes = new List<SlsRoute>();
            this.SlsSalesOrders = new List<SlsSalesOrder>();
            this.SlsTransfers = new List<SlsTransfer>();
            this.SlsTransfers1 = new List<SlsTransfer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int SlsRegionId { get; set; }
        public Nullable<int> SlsOfficeTypeId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Nullable<int> Head { get; set; }
        public Nullable<int> InCharge { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual HrmEmployee HrmEmployee { get; set; }
        public virtual ICollection<InvProductSendDetail> InvProductSendDetails { get; set; }
        public virtual ICollection<InvStore> InvStores { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual ICollection<SlsDealer> SlsDealers { get; set; }
        public virtual ICollection<SlsDealer> SlsDealers1 { get; set; }
        public virtual ICollection<SlsDistributor> SlsDistributors { get; set; }
        public virtual ICollection<SlsDistrict> SlsDistricts { get; set; }
        public virtual ICollection<SlsOffice> SlsOffices1 { get; set; }
        public virtual SlsOffice SlsOffice1 { get; set; }
        public virtual SlsOfficeType SlsOfficeType { get; set; }
        public virtual SlsRegion SlsRegion { get; set; }
        public virtual ICollection<SlsRetailer> SlsRetailers { get; set; }
        public virtual ICollection<SlsRoute> SlsRoutes { get; set; }
        public virtual ICollection<SlsSalesOrder> SlsSalesOrders { get; set; }
        public virtual ICollection<SlsTransfer> SlsTransfers { get; set; }
        public virtual ICollection<SlsTransfer> SlsTransfers1 { get; set; }
    }
}
