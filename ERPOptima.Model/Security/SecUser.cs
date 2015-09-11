using ERPOptima.Model.Common;
using ERPOptima.Model.HRM;
using ERPOptima.Model.Inventory;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Security
{
    public partial class SecUser
    {
        public SecUser()
        {
            this.HrmDepartments = new List<HrmDepartment>();
            this.HrmDepartments1 = new List<HrmDepartment>();
            this.HrmDesignations = new List<HrmDesignation>();
            this.HrmDesignations1 = new List<HrmDesignation>();
            this.HrmEmployees = new List<HrmEmployee>();
            this.HrmEmployees1 = new List<HrmEmployee>();
            this.InvDamageApprovalProducts = new List<InvDamageApprovalProduct>();
            this.InvDamageApprovalProducts1 = new List<InvDamageApprovalProduct>();
            this.InvDamageApprovals = new List<InvDamageApproval>();
            this.InvDamageApprovals1 = new List<InvDamageApproval>();
            this.InvDamages = new List<InvDamage>();
            this.InvDamages1 = new List<InvDamage>();
            this.InvIssues = new List<InvIssue>();
            this.InvIssues1 = new List<InvIssue>();
            this.InvProductReceives = new List<InvProductReceive>();
            this.InvProductReceives1 = new List<InvProductReceive>();
            this.InvProductSendDetailItems = new List<InvProductSendDetailItem>();
            this.InvProductSendDetailItems1 = new List<InvProductSendDetailItem>();
            this.InvProductSends = new List<InvProductSend>();
            this.InvProductSends1 = new List<InvProductSend>();
            this.InvRequisitionApprovals = new List<InvRequisitionApproval>();
            this.InvRequisitionApprovals1 = new List<InvRequisitionApproval>();
            this.InvRequisitions = new List<InvRequisition>();
            this.InvRequisitions1 = new List<InvRequisition>();
            this.InvStoreOpenings = new List<InvStoreOpening>();
            this.InvStoreOpenings1 = new List<InvStoreOpening>();
            this.SecCompanyUsers = new List<SecCompanyUser>();
            this.SecCompanyUsers1 = new List<SecCompanyUser>();
            this.SecCompanyUsers2 = new List<SecCompanyUser>();
            this.SecDashboardPermissions = new List<SecDashboardPermission>();
            this.SecDashboardPermissions1 = new List<SecDashboardPermission>();
            this.SecResources = new List<SecResource>();
            this.SecRolePermissions = new List<SecRolePermission>();
            this.SecRolePermissions1 = new List<SecRolePermission>();
            this.SecUsers1 = new List<SecUser>();
            this.SecUsers11 = new List<SecUser>();
            this.SlsAreaConfigurations = new List<SlsAreaConfiguration>();
            this.SlsAreaConfigurations1 = new List<SlsAreaConfiguration>();
            this.SlsAreas = new List<SlsArea>();
            this.SlsAreas1 = new List<SlsArea>();
            this.SlsCollections = new List<SlsCollection>();
            this.SlsCollections1 = new List<SlsCollection>();
            this.SlsCollectionTargets = new List<SlsCollectionTarget>();
            this.SlsCollectionTargets1 = new List<SlsCollectionTarget>();
            this.SlsCommissions = new List<SlsCommission>();
            this.SlsCommissions1 = new List<SlsCommission>();
            this.SlsCorporateClients = new List<SlsCorporateClient>();
            this.SlsCorporateClients1 = new List<SlsCorporateClient>();
            this.SlsCorporateSalesApplications = new List<SlsCorporateSalesApplication>();
            this.SlsCorporateSalesApplications1 = new List<SlsCorporateSalesApplication>();
            this.SlsCorporateSalesApprovals = new List<SlsCorporateSalesApproval>();
            this.SlsCorporateSalesApprovals1 = new List<SlsCorporateSalesApproval>();
            this.SlsCorporateTypes = new List<SlsCorporateType>();
            this.SlsCorporateTypes1 = new List<SlsCorporateType>();
            this.SlsDealers = new List<SlsDealer>();
            this.SlsDealers1 = new List<SlsDealer>();
            this.SlsDefects = new List<SlsDefect>();
            this.SlsDefects1 = new List<SlsDefect>();
            this.SlsDiscountSettings = new List<SlsDiscountSetting>();
            this.SlsDiscountSettings1 = new List<SlsDiscountSetting>();
            this.SlsDistributors = new List<SlsDistributor>();
            this.SlsDistributors1 = new List<SlsDistributor>();
            this.SlsDistricts = new List<SlsDistrict>();
            this.SlsDistricts1 = new List<SlsDistrict>();
            this.SlsFieldVisits = new List<SlsFieldVisit>();
            this.SlsFieldVisits1 = new List<SlsFieldVisit>();
            this.SlsFreeProducts = new List<SlsFreeProduct>();
            this.SlsFreeProducts1 = new List<SlsFreeProduct>();
            this.SlsGeneralDiscounts = new List<SlsGeneralDiscount>();
            this.SlsGeneralDiscounts1 = new List<SlsGeneralDiscount>();           
            this.SlsIncentives = new List<SlsIncentive>();
            this.SlsIncentives1 = new List<SlsIncentive>();
            this.SlsIncentiveSettings = new List<SlsIncentiveSetting>();
            this.SlsIncentiveSettings1 = new List<SlsIncentiveSetting>();
            this.SlsOffices = new List<SlsOffice>();
            this.SlsOffices1 = new List<SlsOffice>();
            this.SlsOfficeTypes = new List<SlsOfficeType>();
            this.SlsOfficeTypes1 = new List<SlsOfficeType>();
            this.SlsProductPrices = new List<SlsProductPrice>();
            this.SlsProductPrices1 = new List<SlsProductPrice>();
            this.SlsProducts = new List<SlsProduct>();
            this.SlsProducts1 = new List<SlsProduct>();
            this.SlsProductUnits = new List<SlsProductUnit>();
            this.SlsProductUnits1 = new List<SlsProductUnit>();
            this.SlsPromotionalOffers = new List<SlsPromotionalOffer>();
            this.SlsPromotionalOffers1 = new List<SlsPromotionalOffer>();
            this.SlsRegions = new List<SlsRegion>();
            this.SlsRegions1 = new List<SlsRegion>();
            this.SlsRetailers = new List<SlsRetailer>();
            this.SlsRetailers1 = new List<SlsRetailer>();
            this.SlsRoutePlans = new List<SlsRoutePlan>();
            this.SlsRoutePlans1 = new List<SlsRoutePlan>();
            this.SlsRoutes = new List<SlsRoute>();
            this.SlsRoutes1 = new List<SlsRoute>();
            this.SlsSalesOrderApprovals = new List<SlsSalesOrderApproval>();
            this.SlsSalesOrderApprovals1 = new List<SlsSalesOrderApproval>();
            this.SlsSalesOrders = new List<SlsSalesOrder>();
            this.SlsSalesOrders1 = new List<SlsSalesOrder>();
            this.SlsSalesReturns = new List<SlsSalesReturn>();
            this.SlsSalesReturns1 = new List<SlsSalesReturn>();
            this.SlsSalesTargets = new List<SlsSalesTarget>();
            this.SlsSalesTargets1 = new List<SlsSalesTarget>();
            this.SlsThanas = new List<SlsThana>();
            this.SlsThanas1 = new List<SlsThana>();
            this.SlsUnits = new List<SlsUnit>();
            this.SlsUnits1 = new List<SlsUnit>();
            this.SlsProductReceives = new List<SlsProductReceive>();
            this.SlsProductReceives1 = new List<SlsProductReceive>();
        }

        public int Id { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public Nullable<int> HrmEmployeeId { get; set; }
        public int SecRoleId { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<HrmDepartment> HrmDepartments { get; set; }
        public virtual ICollection<HrmDepartment> HrmDepartments1 { get; set; }
        public virtual ICollection<HrmDesignation> HrmDesignations { get; set; }
        public virtual ICollection<HrmDesignation> HrmDesignations1 { get; set; }
        public virtual ICollection<HrmEmployee> HrmEmployees { get; set; }
        public virtual ICollection<HrmEmployee> HrmEmployees1 { get; set; }
        public virtual ICollection<InvDamageApprovalProduct> InvDamageApprovalProducts { get; set; }
        public virtual ICollection<InvDamageApprovalProduct> InvDamageApprovalProducts1 { get; set; }
        public virtual ICollection<InvDamageApproval> InvDamageApprovals { get; set; }
        public virtual ICollection<InvDamageApproval> InvDamageApprovals1 { get; set; }
        public virtual ICollection<InvDamage> InvDamages { get; set; }
        public virtual ICollection<InvDamage> InvDamages1 { get; set; }
        public virtual ICollection<InvIssue> InvIssues { get; set; }
        public virtual ICollection<InvIssue> InvIssues1 { get; set; }
        public virtual ICollection<InvProductReceive> InvProductReceives { get; set; }
        public virtual ICollection<InvProductReceive> InvProductReceives1 { get; set; }
        public virtual ICollection<InvProductSendDetailItem> InvProductSendDetailItems { get; set; }
        public virtual ICollection<InvProductSendDetailItem> InvProductSendDetailItems1 { get; set; }
        public virtual ICollection<InvProductSend> InvProductSends { get; set; }
        public virtual ICollection<InvProductSend> InvProductSends1 { get; set; }
        public virtual ICollection<InvRequisitionApproval> InvRequisitionApprovals { get; set; }
        public virtual ICollection<InvRequisitionApproval> InvRequisitionApprovals1 { get; set; }
        public virtual ICollection<InvRequisition> InvRequisitions { get; set; }
        public virtual ICollection<InvRequisition> InvRequisitions1 { get; set; }
        public virtual ICollection<InvStoreOpening> InvStoreOpenings { get; set; }
        public virtual ICollection<InvStoreOpening> InvStoreOpenings1 { get; set; }
        public virtual ICollection<SecCompanyUser> SecCompanyUsers { get; set; }
        public virtual ICollection<SecCompanyUser> SecCompanyUsers1 { get; set; }
        public virtual ICollection<SecCompanyUser> SecCompanyUsers2 { get; set; }
        public virtual ICollection<SecDashboardPermission> SecDashboardPermissions { get; set; }
        public virtual ICollection<SecDashboardPermission> SecDashboardPermissions1 { get; set; }
        public virtual ICollection<SecResource> SecResources { get; set; }
        public virtual ICollection<SecRolePermission> SecRolePermissions { get; set; }
        public virtual ICollection<SecRolePermission> SecRolePermissions1 { get; set; }
        public virtual SecRole SecRole { get; set; }
        public virtual ICollection<SecUser> SecUsers1 { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual ICollection<SecUser> SecUsers11 { get; set; }
        public virtual SecUser SecUser2 { get; set; }
        public virtual ICollection<SlsAreaConfiguration> SlsAreaConfigurations { get; set; }
        public virtual ICollection<SlsAreaConfiguration> SlsAreaConfigurations1 { get; set; }
        public virtual ICollection<SlsArea> SlsAreas { get; set; }
        public virtual ICollection<SlsArea> SlsAreas1 { get; set; }
        public virtual ICollection<SlsCollection> SlsCollections { get; set; }
        public virtual ICollection<SlsCollection> SlsCollections1 { get; set; }
        public virtual ICollection<SlsCollectionTarget> SlsCollectionTargets { get; set; }
        public virtual ICollection<SlsCollectionTarget> SlsCollectionTargets1 { get; set; }
        public virtual ICollection<SlsCommission> SlsCommissions { get; set; }
        public virtual ICollection<SlsCommission> SlsCommissions1 { get; set; }
        public virtual ICollection<SlsCorporateClient> SlsCorporateClients { get; set; }
        public virtual ICollection<SlsCorporateClient> SlsCorporateClients1 { get; set; }
        public virtual ICollection<SlsCorporateSalesApplication> SlsCorporateSalesApplications { get; set; }
        public virtual ICollection<SlsCorporateSalesApplication> SlsCorporateSalesApplications1 { get; set; }
        public virtual ICollection<SlsCorporateSalesApproval> SlsCorporateSalesApprovals { get; set; }
        public virtual ICollection<SlsCorporateSalesApproval> SlsCorporateSalesApprovals1 { get; set; }
        public virtual ICollection<SlsCorporateType> SlsCorporateTypes { get; set; }
        public virtual ICollection<SlsCorporateType> SlsCorporateTypes1 { get; set; }
        public virtual ICollection<SlsDealer> SlsDealers { get; set; }
        public virtual ICollection<SlsDealer> SlsDealers1 { get; set; }
        public virtual ICollection<SlsDefect> SlsDefects { get; set; }
        public virtual ICollection<SlsDefect> SlsDefects1 { get; set; }
        public virtual ICollection<SlsDiscountSetting> SlsDiscountSettings { get; set; }
        public virtual ICollection<SlsDiscountSetting> SlsDiscountSettings1 { get; set; }
        public virtual ICollection<SlsDistributor> SlsDistributors { get; set; }
        public virtual ICollection<SlsDistributor> SlsDistributors1 { get; set; }
        public virtual ICollection<SlsDistrict> SlsDistricts { get; set; }
        public virtual ICollection<SlsDistrict> SlsDistricts1 { get; set; }
        public virtual ICollection<SlsFieldVisit> SlsFieldVisits { get; set; }
        public virtual ICollection<SlsFieldVisit> SlsFieldVisits1 { get; set; }
        public virtual ICollection<SlsFreeProduct> SlsFreeProducts { get; set; }
        public virtual ICollection<SlsFreeProduct> SlsFreeProducts1 { get; set; }
        public virtual ICollection<SlsGeneralDiscount> SlsGeneralDiscounts { get; set; }
        public virtual ICollection<SlsGeneralDiscount> SlsGeneralDiscounts1 { get; set; }       
        public virtual ICollection<SlsIncentive> SlsIncentives { get; set; }
        public virtual ICollection<SlsIncentive> SlsIncentives1 { get; set; }
        public virtual ICollection<SlsIncentiveSetting> SlsIncentiveSettings { get; set; }
        public virtual ICollection<SlsIncentiveSetting> SlsIncentiveSettings1 { get; set; }
        public virtual ICollection<SlsOffice> SlsOffices { get; set; }
        public virtual ICollection<SlsOffice> SlsOffices1 { get; set; }
        public virtual ICollection<SlsOfficeType> SlsOfficeTypes { get; set; }
        public virtual ICollection<SlsOfficeType> SlsOfficeTypes1 { get; set; }
        public virtual ICollection<SlsProductPrice> SlsProductPrices { get; set; }
        public virtual ICollection<SlsProductPrice> SlsProductPrices1 { get; set; }
        public virtual ICollection<SlsProduct> SlsProducts { get; set; }
        public virtual ICollection<SlsProduct> SlsProducts1 { get; set; }
        public virtual ICollection<SlsProductUnit> SlsProductUnits { get; set; }
        public virtual ICollection<SlsProductUnit> SlsProductUnits1 { get; set; }
        public virtual ICollection<SlsPromotionalOffer> SlsPromotionalOffers { get; set; }
        public virtual ICollection<SlsPromotionalOffer> SlsPromotionalOffers1 { get; set; }
        public virtual ICollection<SlsRegion> SlsRegions { get; set; }
        public virtual ICollection<SlsRegion> SlsRegions1 { get; set; }
        public virtual ICollection<SlsRetailer> SlsRetailers { get; set; }
        public virtual ICollection<SlsRetailer> SlsRetailers1 { get; set; }
        public virtual ICollection<SlsRoutePlan> SlsRoutePlans { get; set; }
        public virtual ICollection<SlsRoutePlan> SlsRoutePlans1 { get; set; }
        public virtual ICollection<SlsRoute> SlsRoutes { get; set; }
        public virtual ICollection<SlsRoute> SlsRoutes1 { get; set; }
        public virtual ICollection<SlsSalesOrderApproval> SlsSalesOrderApprovals { get; set; }
        public virtual ICollection<SlsSalesOrderApproval> SlsSalesOrderApprovals1 { get; set; }
        public virtual ICollection<SlsSalesOrder> SlsSalesOrders { get; set; }
        public virtual ICollection<SlsSalesOrder> SlsSalesOrders1 { get; set; }
        public virtual ICollection<SlsSalesReturn> SlsSalesReturns { get; set; }
        public virtual ICollection<SlsSalesReturn> SlsSalesReturns1 { get; set; }
        public virtual ICollection<SlsSalesTarget> SlsSalesTargets { get; set; }
        public virtual ICollection<SlsSalesTarget> SlsSalesTargets1 { get; set; }
        public virtual ICollection<SlsThana> SlsThanas { get; set; }
        public virtual ICollection<SlsThana> SlsThanas1 { get; set; }
        public virtual ICollection<SlsUnit> SlsUnits { get; set; }
        public virtual ICollection<SlsUnit> SlsUnits1 { get; set; }
        public virtual ICollection<SlsProductReceive> SlsProductReceives { get; set; }
        public virtual ICollection<SlsProductReceive> SlsProductReceives1 { get; set; }
    }
}
