using ERPOptima.Model.Inventory;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsProduct
    {
        public SlsProduct()
        {
            this.InvIssueDetails = new List<InvIssueDetail>();
            this.InvRequisitionDetails = new List<InvRequisitionDetail>();
            this.InvStockInOuts = new List<InvStockInOut>();
            this.InvStoreOpenings = new List<InvStoreOpening>();
            this.SlsCorporateSalesApplicationDetails = new List<SlsCorporateSalesApplicationDetail>();
            this.SlsDefectDetails = new List<SlsDefectDetail>();
            this.SlsFreeProducts = new List<SlsFreeProduct>();
            this.SlsProductDiscounts = new List<SlsProductDiscount>();
            this.SlsProductPrices = new List<SlsProductPrice>();
            this.SlsProducts1 = new List<SlsProduct>();
            this.SlsPromotionalOfferDetails = new List<SlsPromotionalOfferDetail>();
            this.SlsReplacementDetails = new List<SlsReplacementDetail>();
            this.SlsSalesOrderDetails = new List<SlsSalesOrderDetail>();
            this.SlsSalesReturnDetails = new List<SlsSalesReturnDetail>();
            this.SlsSalesTargetDetails = new List<SlsSalesTargetDetail>();
            this.SlsTransferDetails = new List<SlsTransferDetail>();
            this.SlsProductReceiveDetails = new List<SlsProductReceiveDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsProduct { get; set; }
        public bool NoCredit { get; set; }
        public Nullable<int> SlsProductId { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        public Nullable<int> SecCompanyId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<InvIssueDetail> InvIssueDetails { get; set; }
        public virtual ICollection<InvRequisitionDetail> InvRequisitionDetails { get; set; }
        public virtual ICollection<InvStockInOut> InvStockInOuts { get; set; }
        public virtual ICollection<InvStoreOpening> InvStoreOpenings { get; set; }
        public virtual SecCompany SecCompany { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual ICollection<SlsCorporateSalesApplicationDetail> SlsCorporateSalesApplicationDetails { get; set; }
        public virtual ICollection<SlsDefectDetail> SlsDefectDetails { get; set; }
        public virtual ICollection<SlsFreeProduct> SlsFreeProducts { get; set; }
        public virtual ICollection<SlsProductDiscount> SlsProductDiscounts { get; set; }
        public virtual ICollection<SlsProductPrice> SlsProductPrices { get; set; }
        public virtual ICollection<SlsProduct> SlsProducts1 { get; set; }
        public virtual SlsProduct SlsProduct1 { get; set; }
        public virtual ICollection<SlsPromotionalOfferDetail> SlsPromotionalOfferDetails { get; set; }
        public virtual ICollection<SlsReplacementDetail> SlsReplacementDetails { get; set; }
        public virtual ICollection<SlsSalesOrderDetail> SlsSalesOrderDetails { get; set; }
        public virtual ICollection<SlsSalesReturnDetail> SlsSalesReturnDetails { get; set; }
        public virtual ICollection<SlsSalesTargetDetail> SlsSalesTargetDetails { get; set; }
        public virtual ICollection<SlsTransferDetail> SlsTransferDetails { get; set; }
        public virtual ICollection<SlsProductReceiveDetail> SlsProductReceiveDetails { get; set; }
    }

    public class SlsProducts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsProduct { get; set; }
        public bool NoCredit { get; set; }
        public Nullable<int> SlsProductId { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        public Nullable<int> SecCompanyId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }





}

