using ERPOptima.Model.Inventory;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsUnit
    {
        public SlsUnit()
        {
            this.InvIssueDetails = new List<InvIssueDetail>();
            this.InvProductReceiveDetails = new List<InvProductReceiveDetail>();
            this.InvRequisitionDetails = new List<InvRequisitionDetail>();
            this.InvStockInOuts = new List<InvStockInOut>();
            this.InvStoreOpenings = new List<InvStoreOpening>();
            this.SlsDefectDetails = new List<SlsDefectDetail>();
            this.SlsFreeProducts = new List<SlsFreeProduct>();
            this.SlsFreeProducts1 = new List<SlsFreeProduct>();
            this.SlsProductPrices = new List<SlsProductPrice>();
            this.SlsProductUnits = new List<SlsProductUnit>();
            this.SlsProductUnits1 = new List<SlsProductUnit>();
            this.SlsReplacementDetails = new List<SlsReplacementDetail>();
            this.SlsSalesOrderDetails = new List<SlsSalesOrderDetail>();
            this.SlsSalesReturnDetails = new List<SlsSalesReturnDetail>();
            this.SlsSalesTargetDetails = new List<SlsSalesTargetDetail>();
            this.SlsTransferDetails = new List<SlsTransferDetail>();
            this.SlsProductReceiveDetails = new List<SlsProductReceiveDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<InvIssueDetail> InvIssueDetails { get; set; }
        public virtual ICollection<InvProductReceiveDetail> InvProductReceiveDetails { get; set; }
        public virtual ICollection<InvRequisitionDetail> InvRequisitionDetails { get; set; }
        public virtual ICollection<InvStockInOut> InvStockInOuts { get; set; }
        public virtual ICollection<InvStoreOpening> InvStoreOpenings { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual ICollection<SlsDefectDetail> SlsDefectDetails { get; set; }
        public virtual ICollection<SlsFreeProduct> SlsFreeProducts { get; set; }
        public virtual ICollection<SlsFreeProduct> SlsFreeProducts1 { get; set; }
        public virtual ICollection<SlsProductPrice> SlsProductPrices { get; set; }
        public virtual ICollection<SlsProductUnit> SlsProductUnits { get; set; }
        public virtual ICollection<SlsProductUnit> SlsProductUnits1 { get; set; }
        public virtual ICollection<SlsReplacementDetail> SlsReplacementDetails { get; set; }
        public virtual ICollection<SlsSalesOrderDetail> SlsSalesOrderDetails { get; set; }
        public virtual ICollection<SlsSalesReturnDetail> SlsSalesReturnDetails { get; set; }
        public virtual ICollection<SlsSalesTargetDetail> SlsSalesTargetDetails { get; set; }
        public virtual ICollection<SlsTransferDetail> SlsTransferDetails { get; set; }
        public virtual ICollection<SlsProductReceiveDetail> SlsProductReceiveDetails { get; set; }
    }

    public class SlsUnits
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }



}
