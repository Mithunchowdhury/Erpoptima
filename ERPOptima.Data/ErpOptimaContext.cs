using ERPOptima.Data.Mapping;
using ERPOptima.Model.Accounts;
using ERPOptima.Model.Common;
using ERPOptima.Model.HRM;
using ERPOptima.Model.Inventory;
using ERPOptima.Model.Sales;
using ERPOptima.Model.Security;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ERPOptima.Data
{
    public partial class ErpOptimaContext : DbContext 
    {
        static ErpOptimaContext()
        {
            Database.SetInitializer<ErpOptimaContext>(null);
        }

        public ErpOptimaContext()
            : base("Name=ErpOptimaContext")
        {
            //Database.SetInitializer<ErpOptimaContext>(new CreateDatabaseIfNotExists<ErpOptimaContext>());
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;            
            var objectContext = (this as IObjectContextAdapter).ObjectContext;

            // Sets the command timeout for all the commands
            objectContext.CommandTimeout = 300;
            
        }

        public DbSet<AnFChartOfAccount> AnFChartOfAccounts { get; set; }
        public DbSet<AnFCostCenter> AnFCostCenters { get; set; }
        public DbSet<AnFDeliveryMethod> AnFDeliveryMethods { get; set; }
        public DbSet<AnFDepreciationRate> AnFDepreciationRates { get; set; }
        public DbSet<AnFBankReconciliationItem> AnFBankReconciliationItems { get; set; }
        public DbSet<AnFMeasurementUnit> AnFMeasurementUnits { get; set; }
        public DbSet<AnFChequeBook> AnFChequeBooks { get; set; }
        public DbSet<AnFExpens> AnFExpenses { get; set; }
        public DbSet<AnFAdvance> AnFAdvances { get; set; }
        public DbSet<AnFAdjustment> AnFAdjustments { get; set; }
        public DbSet<AnFMonthLock> AnFMonthLocks { get; set; }
        public DbSet<AnFOpeningBalance> AnFOpeningBalances { get; set; }
        public DbSet<AnFPaymentMethod> AnFPaymentMethods { get; set; }
        public DbSet<AnFPaymentTerm> AnFPaymentTerms { get; set; }
        public DbSet<AnFProductOrServiceTax> AnFProductOrServiceTaxes { get; set; }
        public DbSet<AnFProductOrServiceType> AnFProductOrServiceTypes { get; set; }
        public DbSet<AnFVoucherDetail> AnFVoucherDetails { get; set; }
        public DbSet<AnFVoucher> AnFVouchers { get; set; }


        public DbSet<CmnApprovalProcessLevel> CmnApprovalProcessLevels { get; set; }
        public DbSet<CmnApprovalComment> CmnApprovalComments { get; set; }
        public DbSet<CmnApprovalProcess> CmnApprovalProcesses { get; set; }
        public DbSet<CmnApprovalUserPermission> CmnApprovalUserPermissions { get; set; }
        public DbSet<CmnApproval> CmnApprovals { get; set; }
        //public DbSet<CmnBusiness> CmnBusinesses { get; set; }
        public DbSet<CmnFinancialYear> CmnFinancialYears { get; set; }
        public DbSet<CmnProcessLevel> CmnProcessLevels { get; set; }
        public DbSet<CmnCountry> CmnCountries { get; set; }
        public DbSet<CmnCurrency> CmnCurrencies { get; set; }
        //public DbSet<CmnProject> CmnProjects { get; set; }
        public DbSet<FxdAcquisition> FxdAcquisitions { get; set; }
        public DbSet<FxdAsset> FxdAssets { get; set; }
        public DbSet<FxdDepreciation> FxdDepreciations { get; set; }
        public DbSet<FxdDisposal> FxdDisposals { get; set; }
        public DbSet<FxdRevaluation> FxdRevaluations { get; set; }

        public DbSet<CmnAccountHeadMapper> CmnAccountHeadMappers { get; set; }
        public DbSet<CmnMappingType> CmnMappingTypes { get; set; }
        public DbSet<AnFClosingBalance> AnFClosingBalances { get; set; }

        public DbSet<HrmDepartment> HrmDepartments { get; set; }
        public DbSet<HrmEmployee> HrmEmployees { get; set; }
        public DbSet<HrmDesignation> HrmDesignations { get; set; }


        public DbSet<SecGroup> SecGroups { get; set; }
        public DbSet<SecCompanyUser> SecCompanyUsers { get; set; }
        public DbSet<SecCompany> SecCompanies { get; set; }
        public DbSet<SecModule> SecModules { get; set; }
        public DbSet<SecResource> SecResources { get; set; }
        public DbSet<SecRolePermission> SecRolePermissions { get; set; }
        public DbSet<SecRole> SecRoles { get; set; }
        public DbSet<SecDashboardPermission> SecDashboardPermissions { get; set; }
        public DbSet<SecDashboard> SecDashboards { get; set; }
        public DbSet<SecUser> SecUsers { get; set; }


        public DbSet<InvDamageApprovalProduct> InvDamageApprovalProducts { get; set; }
        public DbSet<InvDamageApproval> InvDamageApprovals { get; set; }
        public DbSet<InvDamageDetail> InvDamageDetails { get; set; }
        public DbSet<InvDamage> InvDamages { get; set; }
        public DbSet<InvIssueDetail> InvIssueDetails { get; set; }
        public DbSet<InvIssue> InvIssues { get; set; }
        public DbSet<InvProductReceiveDetail> InvProductReceiveDetails { get; set; }
        public DbSet<InvProductReceive> InvProductReceives { get; set; }
        public DbSet<InvProductSendDetailItem> InvProductSendDetailItems { get; set; }
        public DbSet<InvProductSendDetail> InvProductSendDetails { get; set; }
        public DbSet<InvProductSend> InvProductSends { get; set; }
        public DbSet<InvRequisitionApproval> InvRequisitionApprovals { get; set; }
        public DbSet<InvRequisitionDetail> InvRequisitionDetails { get; set; }
        public DbSet<InvRequisition> InvRequisitions { get; set; }
        public DbSet<InvStockInOut> InvStockInOuts { get; set; }
        public DbSet<InvStore> InvStores { get; set; }
        public DbSet<SecFile> SecFiles { get; set; }

        public DbSet<SlsOffice> SlsOffices { get; set; }
        public DbSet<SlsOfficeType> SlsOfficeTypes { get; set; }

        public DbSet<SlsAreaConfigurationDetail> SlsAreaConfigurationDetails { get; set; }
        public DbSet<SlsAreaConfiguration> SlsAreaConfigurations { get; set; }
        public DbSet<SlsArea> SlsAreas { get; set; }
        public DbSet<SlsCollection> SlsCollections { get; set; }
        public DbSet<SlsCollectionTarget> SlsCollectionTargets { get; set; }
        public DbSet<SlsCommissionPackage> SlsCommissionPackages { get; set; }
        public DbSet<SlsCommission> SlsCommissions { get; set; }
        public DbSet<SlsCorporateClient> SlsCorporateClients { get; set; }
        public DbSet<SlsCorporateSalesApplicationDetail> SlsCorporateSalesApplicationDetails { get; set; }
        public DbSet<SlsCorporateSalesApplication> SlsCorporateSalesApplications { get; set; }
        public DbSet<SlsCorporateSalesApproval> SlsCorporateSalesApprovals { get; set; }
        public DbSet<SlsCorporateType> SlsCorporateTypes { get; set; }
        public DbSet<SlsDeliverDetail> SlsDeliverDetails { get; set; }
        public DbSet<SlsDelivery> SlsDeliveries { get; set; }
        public DbSet<SlsDealer> SlsDealers { get; set; }
        public DbSet<SlsDefectDetail> SlsDefectDetails { get; set; }
        public DbSet<SlsDefect> SlsDefects { get; set; }
        public DbSet<SlsDiscountSetting> SlsDiscountSettings { get; set; }
        public DbSet<SlsDistributor> SlsDistributors { get; set; }
        public DbSet<SlsDistrict> SlsDistricts { get; set; }
        public DbSet<SlsFieldVisit> SlsFieldVisits { get; set; }
        public DbSet<SlsFreeProduct> SlsFreeProducts { get; set; }
        public DbSet<SlsGeneralDiscount> SlsGeneralDiscounts { get; set; }
        public DbSet<SlsIncentive> SlsIncentives { get; set; }
        public DbSet<SlsIncentiveSetting> SlsIncentiveSettings { get; set; }
        public DbSet<SlsNotificationDetail> SlsNotificationDetails { get; set; }
        public DbSet<SlsNotification> SlsNotifications { get; set; }
        public DbSet<SlsProductDiscount> SlsProductDiscounts { get; set; }
        public DbSet<SlsProductPrice> SlsProductPrices { get; set; }
        public DbSet<SlsProduct> SlsProducts { get; set; }
        public DbSet<SlsProductUnit> SlsProductUnits { get; set; }
        public DbSet<SlsPromotionalOfferDetail> SlsPromotionalOfferDetails { get; set; }
        public DbSet<SlsPromotionalOffer> SlsPromotionalOffers { get; set; }
        public DbSet<SlsRegion> SlsRegions { get; set; }
        public DbSet<SlsReplacement> SlsReplacements { get; set; }
        public DbSet<SlsReplacementDetail> SlsReplacementDetails { get; set; }
        public DbSet<SlsRetailer> SlsRetailers { get; set; }
        public DbSet<SlsRouteDetail> SlsRouteDetails { get; set; }
        public DbSet<SlsRoutePlanApproval> SlsRoutePlanApprovals { get; set; }
        public DbSet<SlsRoutePlanDetail> SlsRoutePlanDetails { get; set; }
        public DbSet<SlsRoutePlan> SlsRoutePlans { get; set; }
        public DbSet<SlsRoute> SlsRoutes { get; set; }
        public DbSet<SlsSalesOrderApproval> SlsSalesOrderApprovals { get; set; }
        public DbSet<SlsSalesOrderDetail> SlsSalesOrderDetails { get; set; }
        public DbSet<SlsSalesOrder> SlsSalesOrders { get; set; }
        public DbSet<SlsSalesReturnDetail> SlsSalesReturnDetails { get; set; }
        public DbSet<SlsSalesReturn> SlsSalesReturns { get; set; }
        public DbSet<SlsSalesTargetDetail> SlsSalesTargetDetails { get; set; }
        public DbSet<SlsSalesTarget> SlsSalesTargets { get; set; }
        public DbSet<InvStoreOpening> InvStoreOpening { get; set; }
        public DbSet<SlsThana> SlsThanas { get; set; }
        public DbSet<SlsUnit> SlsUnits { get; set; }
        public DbSet<SlsTransfer> SlsTransfers { get; set; } 
        public DbSet<SlsTransferDetail> SlsTransferDetails { get; set; }        
        public DbSet<SlsProductReceiveDetail> SlsProductReceiveDetails { get; set; }
        public DbSet<SlsProductReceive> SlsProductReceives { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AnFChartOfAccountMap());
            modelBuilder.Configurations.Add(new AnFCostCenterMap());
            modelBuilder.Configurations.Add(new AnFDeliveryMethodMap());
            modelBuilder.Configurations.Add(new AnFDepreciationRateMap());
            modelBuilder.Configurations.Add(new AnFExpensMap());
            modelBuilder.Configurations.Add(new AnFMeasurementUnitMap());
            modelBuilder.Configurations.Add(new AnFAdvanceMap());
            modelBuilder.Configurations.Add(new AnFAdjustmentMap());
            modelBuilder.Configurations.Add(new AnFMonthLockMap());
            modelBuilder.Configurations.Add(new AnFOpeningBalanceMap());
            modelBuilder.Configurations.Add(new AnFPaymentMethodMap());
            modelBuilder.Configurations.Add(new AnFPaymentTermMap());
            modelBuilder.Configurations.Add(new AnFProductOrServiceTaxMap());
            modelBuilder.Configurations.Add(new AnFProductOrServiceTypeMap());
            modelBuilder.Configurations.Add(new AnFVoucherDetailMap());
            modelBuilder.Configurations.Add(new AnFVoucherMap());

            modelBuilder.Configurations.Add(new CmnApprovalProcessLevelMap());
            modelBuilder.Configurations.Add(new CmnApprovalCommentMap());
            modelBuilder.Configurations.Add(new CmnApprovalProcessMap());
            modelBuilder.Configurations.Add(new CmnApprovalUserPermissionMap());
            modelBuilder.Configurations.Add(new CmnApprovalMap());
            //modelBuilder.Configurations.Add(new CmnBusinessMap());
            modelBuilder.Configurations.Add(new CmnFinancialYearMap());
            modelBuilder.Configurations.Add(new CmnProcessLevelMap());
            modelBuilder.Configurations.Add(new CmnCountryMap());
            modelBuilder.Configurations.Add(new CmnCurrencyMap());
            //modelBuilder.Configurations.Add(new CmnProjectMap());
            modelBuilder.Configurations.Add(new FxdAcquisitionMap());
            modelBuilder.Configurations.Add(new FxdAssetMap());
            modelBuilder.Configurations.Add(new FxdDepreciationMap());
            modelBuilder.Configurations.Add(new FxdDisposalMap());
            modelBuilder.Configurations.Add(new FxdRevaluationMap());

            modelBuilder.Configurations.Add(new CmnAccountHeadMapperMap()); 
            modelBuilder.Configurations.Add(new CmnMappingTypeMap());
            modelBuilder.Configurations.Add(new AnFClosingBalanceMap());

            modelBuilder.Configurations.Add(new HrmDepartmentMap());
            modelBuilder.Configurations.Add(new HrmDesignationMap());
            modelBuilder.Configurations.Add(new HrmEmployeeMap());

            modelBuilder.Configurations.Add(new InvDamageApprovalProductMap());
            modelBuilder.Configurations.Add(new InvDamageApprovalMap());
            modelBuilder.Configurations.Add(new InvDamageDetailMap());
            modelBuilder.Configurations.Add(new InvDamageMap());
            modelBuilder.Configurations.Add(new InvIssueDetailMap());
            modelBuilder.Configurations.Add(new InvIssueMap());
            modelBuilder.Configurations.Add(new InvProductReceiveDetailMap());
            modelBuilder.Configurations.Add(new InvProductReceiveMap());
            modelBuilder.Configurations.Add(new InvProductSendDetailItemMap());
            modelBuilder.Configurations.Add(new InvProductSendDetailMap());
            modelBuilder.Configurations.Add(new InvProductSendMap());
            modelBuilder.Configurations.Add(new InvRequisitionApprovalMap());
            modelBuilder.Configurations.Add(new InvRequisitionDetailMap());
            modelBuilder.Configurations.Add(new InvRequisitionMap());
            modelBuilder.Configurations.Add(new InvStockInOutMap());
            modelBuilder.Configurations.Add(new InvStoreMap());

            modelBuilder.Configurations.Add(new SecCompanyMap());
            modelBuilder.Configurations.Add(new SecGroupMap());
            modelBuilder.Configurations.Add(new SecCompanyUserMap());
            modelBuilder.Configurations.Add(new SecModuleMap());
            modelBuilder.Configurations.Add(new SecDashboardPermissionMap());
            modelBuilder.Configurations.Add(new SecDashboardMap());
            modelBuilder.Configurations.Add(new SecResourceMap());
            modelBuilder.Configurations.Add(new SecRolePermissionMap());
            modelBuilder.Configurations.Add(new SecRoleMap());
            modelBuilder.Configurations.Add(new SecUserMap());
            modelBuilder.Configurations.Add(new SecFileMap());

            modelBuilder.Configurations.Add(new SlsOfficeMap());
            modelBuilder.Configurations.Add(new SlsOfficeTypeMap());         
            modelBuilder.Configurations.Add(new SlsAreaConfigurationDetailMap());
            modelBuilder.Configurations.Add(new SlsAreaConfigurationMap());
            modelBuilder.Configurations.Add(new SlsAreaMap());
            modelBuilder.Configurations.Add(new SlsCollectionMap());
            modelBuilder.Configurations.Add(new SlsCollectionTargetMap());
            modelBuilder.Configurations.Add(new SlsCommissionPackageMap());
            modelBuilder.Configurations.Add(new SlsCommissionMap());
            modelBuilder.Configurations.Add(new SlsCorporateClientMap());
            modelBuilder.Configurations.Add(new SlsCorporateSalesApplicationDetailMap());
            modelBuilder.Configurations.Add(new SlsCorporateSalesApplicationMap());
            modelBuilder.Configurations.Add(new SlsCorporateSalesApprovalMap());
            modelBuilder.Configurations.Add(new SlsCorporateTypeMap());
            modelBuilder.Configurations.Add(new SlsDeliverDetailMap());
            modelBuilder.Configurations.Add(new SlsDeliveryMap());
            modelBuilder.Configurations.Add(new SlsDealerMap());
            modelBuilder.Configurations.Add(new SlsDefectDetailMap());
            modelBuilder.Configurations.Add(new SlsDefectMap());
            modelBuilder.Configurations.Add(new SlsDiscountSettingMap());
            modelBuilder.Configurations.Add(new SlsDistributorMap());
            modelBuilder.Configurations.Add(new SlsDistrictMap());
            modelBuilder.Configurations.Add(new SlsFieldVisitMap());
            modelBuilder.Configurations.Add(new SlsFreeProductMap());
            modelBuilder.Configurations.Add(new SlsGeneralDiscountMap());
            modelBuilder.Configurations.Add(new SlsIncentiveMap());
            modelBuilder.Configurations.Add(new SlsIncentiveSettingMap());
            modelBuilder.Configurations.Add(new SlsNotificationDetailMap());
            modelBuilder.Configurations.Add(new SlsNotificationMap());            
            modelBuilder.Configurations.Add(new SlsProductDiscountMap());
            modelBuilder.Configurations.Add(new SlsProductPriceMap());
            modelBuilder.Configurations.Add(new SlsProductMap());
            modelBuilder.Configurations.Add(new SlsProductUnitMap());
            modelBuilder.Configurations.Add(new SlsPromotionalOfferDetailMap());
            modelBuilder.Configurations.Add(new SlsPromotionalOfferMap());
            modelBuilder.Configurations.Add(new SlsRegionMap());
            modelBuilder.Configurations.Add(new SlsReplacementMap());
            modelBuilder.Configurations.Add(new SlsReplacementDetailMap());
            modelBuilder.Configurations.Add(new SlsRetailerMap());
            modelBuilder.Configurations.Add(new SlsRouteDetailMap());
            modelBuilder.Configurations.Add(new SlsRoutePlanApprovalMap());
            modelBuilder.Configurations.Add(new SlsRoutePlanDetailMap());
            modelBuilder.Configurations.Add(new SlsRoutePlanMap());
            modelBuilder.Configurations.Add(new SlsRouteMap());
            modelBuilder.Configurations.Add(new SlsSalesOrderApprovalMap());
            modelBuilder.Configurations.Add(new SlsSalesOrderDetailMap());
            modelBuilder.Configurations.Add(new SlsSalesOrderMap());
            modelBuilder.Configurations.Add(new SlsSalesReturnDetailMap());
            modelBuilder.Configurations.Add(new SlsSalesReturnMap());
            modelBuilder.Configurations.Add(new SlsSalesTargetDetailMap());
            modelBuilder.Configurations.Add(new SlsSalesTargetMap());
            modelBuilder.Configurations.Add(new InvStoreOpeningMap());
            modelBuilder.Configurations.Add(new SlsThanaMap());
            modelBuilder.Configurations.Add(new SlsUnitMap());
            modelBuilder.Configurations.Add(new SlsTransferMap());
            modelBuilder.Configurations.Add(new SlsTransferDetailMap());
            modelBuilder.Configurations.Add(new SlsProductReceiveMap());
            modelBuilder.Configurations.Add(new SlsProductReceiveDetailMap());
                                    
        }
    }
}