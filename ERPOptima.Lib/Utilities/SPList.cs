using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPOptima.Lib.Utilities
{
    public static partial class SPList
    {

        #region Security 
        
        public static class HrmDepartment
        {
                 public static string GetHrmDepartments = "GetHrmDepartments";
        }
        public static class HrmEmployee
        {
            public static string GetHrmEmployees = "GetHrmEmployees";
        }
        public static class SecDashboardPermission
        {
            public static string DeleteSecDashboardPermissionByRoleId = "DeleteSecDashboardPermissionByRoleId"; 
            public static string GetDashboardPermissionByRoleId = "GetDashboardPermissionByRoleId";
        }     
        public static class SecCompanyUser
        {
            public static string DeleteSecCompanyUsersBySecUserId = "DeleteSecCompanyUsersBySecUserId";
            public static string GetSecCompanyUsersBySecUserId = "GetSecCompanyUsersBySecUserId";
            public static string GetSecCompanyUserBySecUserId = "GetSecCompanyUserBySecUserId";
        }
        public static class SecGroup
        {
            //public static string DeleteSecGroupsById = "DeleteSecGroupsById";
            //public static string GetSecGroups = "GetSecGroups";
            //public static string GetSecGroupsById = "GetSecGroupsById";
            //public static string GetSecGroupsLastId = "GetSecGroupsLastId";
            //public static string InsertSecGroups = "InsertSecGroups";
            //public static string UpdateSecGroups = "UpdateSecGroups";
        }
        public static class SecModule
        {
            //public static string DeleteSecModulesById = "DeleteSecModulesById";
            public static string GetSecModules = "GetSecModules";
            public static string GetSecModulesByCompanyId = "GetSecModulesByCompanyId";
            public static string GetSecPermittedModuleByUserId = "GetSecPermittedModuleByUserId";
            
            //public static string GetSecModulesById = "GetSecModulesById";
            //public static string InsertSecModules = "InsertSecModules";
            //public static string UpdateSecModules = "UpdateSecModules";
            //public static string GetSecLastModuleId = "GetSecLastModuleId";
        }
        public static class SecResource
        {
            //public static string DeleteSecResourcesById = "DeleteSecResourcesById";
            //public static string GetSecResourceButtonPermission = "GetSecResourceButtonPermission";
            //public static string GetSecResources = "GetSecResources";
            //public static string GetSecResourcesById = "GetSecResourcesById";
            //public static string GetSecResourcesByModuleId = "GetSecResourcesByModuleId";
            //public static string GetSecResourcesByUserId = "GetSecResourcesByUserId";
            //public static string GetSecResourceVerificationByUserIdAndResourceName = "GetSecResourceVerificationByUserIdAndResourceName";
            public static string GetSecModuleResourcesByUserIdAndModuleId = "GetSecModuleResourcesByUserIdAndModuleId";
            
            public static string GetCmnProcessLevelByUserId="GetCmnProcessLevelByUserId";
            //public static string InsertOrUpdateSecResources = "InsertOrUpdateSecResources";
            //public static string GetSecResourcesPermission = "GetSecResourcesPermission";
        }
        public static class SecRolePermission
        {
            public static string DeleteSecRolePermissionByUserId = "DeleteSecRolePermissionByUserId";
            public static string DeleteSecRolePermissionByRoleId = "DeleteSecRolePermissionByRoleId";
            //public static string GetSecRolePermissionByResourceId = "GetSecRolePermissionByResourceId";
            //public static string GetSecRolePermissions = "GetSecRolePermissions";
            //public static string GetSecRolePermissionsById = "GetSecRolePermissionsById";
            //public static string GetSecRolePermissionsByResourceId = "GetSecRolePermissionsByResourceId";
            public static string GetSecRolePermissionsByRoleId = "GetSecRolePermissionsByRoleId";
            //public static string InsertSecRolePermissions = "InsertSecRolePermissions";
            //public static string UpdateSecRolePermissions = "UpdateSecRolePermissions";
        }
        public static class SecRole
        {
            //public static string DeleteSecRolesById = "DeleteSecRolesById";
            //public static string GetSecRoles = "GetSecRoles";
            //public static string GetSecRolesById = "GetSecRolesById";
            //public static string GetSecRolesByUserId = "GetSecRolesByUserId";
            //public static string InsertSecRoles = "InsertSecRoles";
            //public static string UpdateSecRoles = "UpdateSecRoles";
            //public static string GetSecUserRolesByUserId = "GetSecUserRolesByUserId";
        }
        public static class SecUserRole
        {
            //public static string DeleteSecUserRolesById = "DeleteSecUserRolesById";
            public static string DeleteSecUserRolesByUserId = "DeleteSecUserRolesByUserId";
            //public static string GetSecUserRoles = "GetSecUserRoles";
            //public static string GetSecUserRolesById = "GetSecUserRolesById";
            //public static string GetSecUserRolesByRoleId = "GetSecUserRolesByRoleId";
            //public static string GetSecUserRolesByUserId = "GetSecUserRolesByUserId";
            public static string GetSecUserRoleByUserId = "GetSecUserRoleByUserId";
            //public static string UpdateSecUserRoles = "UpdateSecUserRoles";
        }
        public static class SecUser
        {
            //public static string DeleteSecUsersById = "DeleteSecUsersById";
            //public static string GetSecUsers = "GetSecUsers";
            public static string GetSecUsersByCompanyId = "GetSecUsersByCompanyId";
            //public static string GetSecUsersByEmployeeId = "GetSecUsersByEmployeeId";
            //public static string GetSecUsersById = "GetSecUsersById";
            public static string GetSecUsersByLevel = "GetSecUsersByLevel";
            public static string GetSecUsersByLoginName = "GetSecUsersByLoginName";
            //public static string GetSecUsersByParentId = "GetSecUsersByParentId";
            //public static string GetSecUsersIdByLoginName = "GetSecUsersIdByLoginName";
            //public static string InsertSecUsers = "InsertSecUsers";
            //public static string UpdateSecUsersPassword = "UpdateSecUsersPassword";
            //public static string UpdateSecUsers = "UpdateSecUsers";
            //public static string GetSecLoginInfoByLoginName = "GetSecLoginInfoByLoginName";

        }
        #endregion

        #region Common

        public static class CmnAccountHeadMapper
        {
            //public static string GetCmnAccountHeadMappers = "GetCmnAccountHeadMappers";
            //public static string InsertCmnAccountHeadMappers = "InsertCmnAccountHeadMappers";
        }
      
        public static class CmnApprovalComment
        {
            public static string GetCmnApprovalCommentsByProcessAndRefId = "GetCmnApprovalCommentsByProcessAndRefId";
            public static string InsertCmnApprovalComment = "InsertCmnApprovalComment";

        }
        public static class CmnApprovalProcess
        {
            //public static string DeleteCmnApprovalProcessesById = "DeleteCmnApprovalProcessesById";
            //public static string GetCmnApprovalProcessByShortName = "GetCmnApprovalProcessByShortName";
            //public static string GetCmnApprovalProcesses = "GetCmnApprovalProcesses";
            //public static string GetCmnApprovalProcessesByCompanyId = "GetCmnApprovalProcessesByCompanyId";
            //public static string GetCmnApprovalProcessesById = "GetCmnApprovalProcessesById";
            public static string GetCmnApprovalProcessesByModuleId = "GetCmnApprovalProcessesByModuleId";
            //public static string GetCmnApprovalProcessesForApprove = "GetCmnApprovalProcessesForApprove";
            //public static string GetCmnApprovalProcessByCompDeptAndCompanyId = "GetCmnApprovalProcessByCompDeptAndCompanyId";
            //public static string InsertCmnApprovalProcesses = "InsertCmnApprovalProcesses";
            //public static string UpdateCmnApprovalProcesses = "UpdateCmnApprovalProcesses";
            //public static string UpdateCmnApprovalProcessesForMapping = "UpdateCmnApprovalProcessesForMapping";
        }
        public static class CmnApprovalUserPermissions
        {
            public static string GetApprovalUserPermission = "GetApprovalUserPermission";
            public static string GetCmnApprovalUserPermissionCountByUserAndProcessAndLevelId = "GetCmnApprovalUserPermissionCountByUserAndProcessAndLevelId";
            //public static string GetCmnApprovalRolePermissionss = "GetCmnApprovalRolePermissionss";
            //public static string GetCmnApprovalRolePermissionssByDepartmentId = "GetCmnApprovalRolePermissionssByDepartmentId";
            //public static string GetCountCmnApprovalRolePermissionsByUId = "GetCountCmnApprovalRolePermissionsByUId";
            //public static string InsertCmnApprovalRolePermissions = "InsertCmnApprovalRolePermissions";
            //public static string UpdateCmnApprovalRolePermissions = "UpdateCmnApprovalRolePermissions";
            //public static string GetCmnApprovalPermissionByRoleAndCompanyId = "GetCmnApprovalPermissionByRoleAndCompanyId";
        }
        public static class CmnApproval
        {
            public static string GetCmnApprovalsById = "GetCmnApprovalsById";
            public static string GetCmnApprovalsByRefAndProcessAndLevelId = "GetCmnApprovalsByRefAndProcessAndLevelId";
            public static string InsertCmnApprovals = "InsertCmnApprovals";
            public static string UpdateCmnApproval = "UpdateCmnApproval";
            public static string UpdateCmnApprovalByRefIdNProcessIdNLevelId = "UpdateCmnApprovalByRefIdNProcessIdNLevelId";
            public static string UpdateCmnApprovalWithComments = "UpdateCmnApprovalWithComments";
            public static string GetCmnApprovalHistoryByRefId = "GetCmnApprovalHistoryByRefId";
            public static string GetNoOfUnApproveByProcess = "GetNoOfUnApproveByProcess";
            public static string GetCmnApprovalsStatus = "GetCmnApprovalsStatus";
        }
      
        public static class CmnBank
        {
            //public static string DeleteCmnBanksById = "DeleteCmnBanksById";
            //public static string GetCmnBanks = "GetCmnBanks";
            //public static string GetCmnBanksByCountryId = "GetCmnBanksByCountryId";
            //public static string GetCmnBanksById = "GetCmnBanksById";
            //public static string InsertCmnBanks = "InsertCmnBanks";
            //public static string UpdateCmnBanks = "UpdateCmnBanks";
            //public static string GetCmnBanksByCmnCompanyId = "GetCmnBanksByCmnCompanyId";
        }
        public static class CmnBusinessSector
        {
            //public static string DeleteCmnBusinessSectorsById = "DeleteCmnBusinessSectorsById";
            //public static string GetCmnBusinessSectors = "GetCmnBusinessSectors";
            //public static string GetCmnBusinessesById = "GetCmnBusinessesById";
            //public static string GetCmnBusinessSectorsByCompanyId = "GetCmnBusinessSectorsByCompanyId";
            //public static string GetCmnBusinessSectorsLastCode = "GetCmnBusinessSectorsLastCode";
            //public static string InsertCmnBusinessSectors = "InsertCmnBusinessSectors";
            //public static string InsertCmnBusinessSectorsWithDefaultProject = "InsertCmnBusinessSectorsWithDefaultProject";
            //public static string UpdateCmnBusinessSectors = "UpdateCmnBusinessSectors";
        }
        public static class SecCompany
        {
            //public static string InsertCmnCompanies = "InsertCmnCompanies";
            //public static string DeleteCmnCompaniesById = "DeleteCmnCompaniesById";
            //public static string UpdateCmnCompanies = "UpdateCmnCompanies";
            //public static string GetCmnCompanies = "GetCmnCompanies";
            //public static string GetCmnCompaniesLastID = "GetCmnCompaniesLastID";
            //public static string GetCmnCompaniesByIdForProbationReport = "GetCmnCompaniesByIdForProbationReport";
            //public static string GetCmnCompaniesByGroupId = "GetCmnCompaniesByGroupId";
            //public static string GetCmnCompaniesById = "GetCmnCompaniesById";
        }
        public static class CmnApprovalProcessLevels
        {
            public static string GetCmnApprovalProcessLevelMappingByCompanyIdAndModuleId = "GetCmnApprovalProcessLevelMappingByCompanyIdAndModuleId";
            //public static string GetCmnCompanyDepartmentModuleByCompanyId = "GetCmnCompanyDepartmentModuleByCompanyId";
            //public static string GetCmnCompanyDepartmentModulesByCompanyAndModuleId = "GetCmnCompanyDepartmentModulesByCompanyAndModuleId";
            //public static string InsertCmnCompanyDepartmentModule = "InsertCmnCompanyDepartmentModule";
        }
        public static class CmnCountry
        {
            //public static string DeleteCmnCountriesById = "DeleteCmnCountriesById";
            //public static string GetCmnCountries = "GetCmnCountries";
            //public static string GetCmnCountriesById = "GetCmnCountriesById";
            //public static string InsertCmnCountries = "InsertCmnCountries";
            //public static string UpdateCmnCountries = "UpdateCmnCountries";
        }
        public static class CmnCurrency
        {
            //public static string DeleteCmnCurrenciesById = "DeleteCmnCurrenciesById";
            //public static string GetCmnCurrencies = "GetCmnCurrencies";
            //public static string GetCmnCurrenciesById = "GetCmnCurrenciesById";
            //public static string InsertCmnCurrencies = "InsertCmnCurrencies";
            //public static string UpdateCmnCurrencies = "UpdateCmnCurrencies";
        }
        public static class CmnDocument
        {
            //public static string DeleteCmnDocumentsByFileLocation = "DeleteCmnDocumentsByFileLocation";
            //public static string DeleteCmnDocumentsByRefIdAndCmnDocumentTypeId = "DeleteCmnDocumentsByRefIdAndCmnDocumentTypeId";
            //public static string GetCmnDocumentsByLocation = "GetCmnDocumentsByLocation";
            //public static string GetCmnDocumentsByRefId = "GetCmnDocumentsByRefId";
            //public static string GetCmnDocumentsBySubmittedTenderId = "GetCmnDocumentsBySubmittedTenderId";
            //public static string GetCmnDocumentsBySubmitTenderIdAndDocumentTypeId = "GetCmnDocumentsBySubmitTenderIdAndDocumentTypeId";
            //public static string GetCmnDocumentsByTenderId = "GetCmnDocumentsByTenderId";
            //public static string GetCmnDocumentsByTenderIdAndDocumentTypeId = "GetCmnDocumentsByTenderIdAndDocumentTypeId";
            //public static string DeleteTndDocumentsById = "DeleteTndDocumentsById";
            //public static string GetTndDocuments = "GetTndDocuments";
            //public static string InsertTndDocuments = "InsertTndDocuments";
            //public static string UpdateTndDocuments = "UpdateTndDocuments";
            //public static string GetTndParentsBOQByChildId = "GetTndParentsBOQByChildId";
            //public static string GetTndDocumentsById = "GetTndDocumentsById";
        }
       
       
        public static class CmnMappingType
        {
            //public static string DeleteCmnMappingTypesById = "DeleteCmnMappingTypesById";
            //public static string GetCmnMappingType = "GetCmnMappingType";
            //public static string GetCmnMappingTypesById = "GetCmnMappingTypesById";
            //public static string GetCmnMappingTypesByModuleId = "GetCmnMappingTypesByModuleId";
            //public static string InsertCmnMappingType = "InsertCmnMappingType";
            //public static string UpdateCmnMappingType = "UpdateCmnMappingType";
        }
        public static class CmnMenufacturer
        {
            //public static string DeleteCmnMenufacturersById = "DeleteCmnMenufacturersById";
            //public static string GetCmnMenufacturers = "GetCmnMenufacturers";
            //public static string GetCmnMenufacturersByCmnCompanyId = "GetCmnMenufacturersByCmnCompanyId";
            //public static string GetCmnMenufacturersById = "GetCmnMenufacturersById";
            //public static string InsertCmnMenufacturers = "InsertCmnMenufacturers";
            //public static string UpdateCmnMenufacturers = "UpdateCmnMenufacturers";
        }
        public static class CmnProcessLevel
        {
            public static string GetCmnProcessLevelsByApprovalProcessId = "GetCmnProcessLevelsByApprovalProcessId";
            //public static string GetCmnProcessLevelByUserId = "GetCmnProcessLevelByUserId";
            //public static string GetCmnProcessLevels = "GetCmnProcessLevels";
            //public static string GetCmnProcessLevelsByComapanyIdNDepartmentIdNApprovalProcessId = "GetCmnProcessLevelsByComapanyIdNDepartmentIdNApprovalProcessId";
            //public static string GetCmnProcessLevelsByDepartmentIdAndProcessId = "GetCmnProcessLevelsByDepartmentIdAndProcessId";
            //public static string GetCmnProcessLevelsById = "GetCmnProcessLevelsById";
            //public static string InsertCmnProcessLevels = "InsertCmnProcessLevels";
            //public static string UpdateCmnProcessLevels = "UpdateCmnProcessLevels";
        }
        public static class CmnProjects
        {
            //public static string GetCmnProjectSegments = "GetCmnProjectSegments";
            //public static string GetCmnProjectSegmentsByCmnBusinessSectorId = "GetCmnProjectSegmentsByCmnBusinessSectorId";
            //public static string GetCmnProjectSegmentsByCmnCompanyId = "GetCmnProjectSegmentsByCmnCompanyId";
            //public static string GetCmnProjectSegmentsByCompanyIdNCmnBusinessSectorId = "GetCmnProjectSegmentsByCompanyIdNCmnBusinessSectorId";
            //public static string GetCmnProjectSegmentsLastCode = "GetCmnProjectSegmentsLastCode";
            //public static string InsertCmnProjectSegments = "InsertCmnProjectSegments";
            //public static string UpdateCmnProjectSegmentsClosingStatus = "UpdateCmnProjectSegmentsClosingStatus";
            //public static string GetCmnProjectsById = "GetCmnProjectsById";
            public static string GetCmnProjectsByCompanyAndBusinessIdAndChartOfAccountId = "GetCmnProjectsByCompanyAndBusinessIdAndChartOfAccountId";
            //public static string GetAnFTransactinalHeadWiseProjectsDT = "GetAnFTransactinalHeadWiseProjectsDT";
            //public static string UpdateCmnProject = "UpdateCmnProject";
            //public static string GetAnFProjectClosingStatus = "GetAnFProjectClosingStatus";
        }
        public static class CmnFinancialYears
        {
            //public static string CloseCmnTransactionalYears = "CloseCmnTransactionalYears";
            //public static string DeleteCmnTransactionalYearsById = "DeleteCmnTransactionalYearsById";
            public static string GetCmnFinancialYears = "GetCmnFinancialYears";
            //public static string GetCmnTransactionalYearsByCompanyAndModuleId = "GetCmnTransactionalYearsByCompanyAndModuleId";
            //public static string GetCmnTransactionalYearsByFromDate = "GetCmnTransactionalYearsByFromDate";
            //public static string GetCmnTransactionalYearsById = "GetCmnTransactionalYearsById";
            //public static string GetCmnTransactionalYearsByModuleIdNCompanyId = "GetCmnTransactionalYearsByModuleIdNCompanyId";
            //public static string GetCmnTransactionalYearsIdByByTodaysDate = "GetCmnTransactionalYearsIdByByTodaysDate";
            //public static string GetCmnTransactionalYearsByCompanyIdLastOpened = "GetCmnTransactionalYearsByCompanyIdLastOpened";
            //public static string InsertCmnTransactionalYears = "InsertCmnTransactionalYears";
            //public static string UpdateCmnTransactionalYearClosingStatus = "UpdateCmnTransactionalYearClosingStatus";
            //public static string UpdateCmnTransactionalYears = "UpdateCmnTransactionalYears";
        }
       
        #endregion

        #region Accounts

        public static class AnFAccHeadFlipper
        {
            //public static string GetAnFAccHeadFlippersByCmnCompanyIdAndDrCOAId = "GetAnFAccHeadFlippersByCmnCompanyIdAndDrCOAId";
            //public static string InsertAnFAccHeadFlippers = "InsertAnFAccHeadFlippers";
        }
        public static class AnFChartOfAccounts
        {
            //public static string DeleteAnFCOAsById = "DeleteAnFCOAsById";
            //public static string GetAllTransectionHeadListByAnFCOAId = "GetAllTransectionHeadListByAnFCOAId";
            //public static string GetAnFCOAByCode = "GetAnFCOAByCode";
            //public static string GetAnFCOAByCompanyAndModuleId = "GetAnFCOAByCompanyAndModuleId";
            //public static string GetAnFCOAById = "GetAnFCOAById";
            //public static string GetAnFCOAIdForInvItems = "GetAnFCOAIdForInvItems";
            //public static string GetAnFCOAIdForInvSuppliers = "GetAnFCOAIdForInvSuppliers";
            //public static string GetAnFCOAs456LevelByCmnCompanyId = "GetAnFCOAs456LevelByCmnCompanyId";
            //public static string GetAnFCOAsAsAssets_LiabilitiesByCmnCompanyId = "GetAnFCOAsAsAssets_LiabilitiesByCmnCompanyId";
            //public static string GetAnFCOAsByAnFCOAId = "GetAnFCOAsByAnFCOAId";
            //public static string GetAnFCOAsChildByParentId = "GetAnFCOAsChildByParentId";
            //public static string GetAnFCOAsCodeByChild = "GetAnFCOAsCodeByChild";
            //public static string GetAnFCOAsParentsByChildId = "GetAnFCOAsParentsByChildId";
            //public static string GetAnFCOAsLastChildNode = "GetAnFCOAsLastChildNode";
            //public static string GetAnFParentParticularsById = "GetAnFParentParticularsById";
            //public static string GetAnFCOAsUpperLastNodeCodes = "GetAnFCOAsUpperLastNodeCodes";
            //public static string GetAnFCOAsWithLevel = "GetAnFCOAsWithLevel";
            //public static string InsertAnFCOAs = "InsertAnFCOAs";
            //public static string UpdateAnFCOAs = "UpdateAnFCOAs";
            //public static string UpdateAnFCOAsParticular = "UpdateAnFCOAsParticular";
            //public static string GetAnFCOAForFiexedAsset = "GetAnFCOAForFiexedAsset";
            //public static string GetAnFCOAsByCompanyIdAndModuleIdForFiexedAsset = "GetAnFCOAsByCompanyIdAndModuleIdForFiexedAsset";
            //public static string GetAnFCOAsByCompanyIdForFiexedAsset = "GetAnFCOAsByCompanyIdForFiexedAsset";
            //public static string GetAnFThiredLevelByCompanyIdAndDepthLevel = "GetAnFThiredLevelByCompanyIdAndDepthLevel";
            //public static string GetAnFChildHeadByCompanyId = "GetAnFChildHeadByCompanyId";
            //public static string GetFxdFixedAssetHeadByCompanyIdAndModuleId = "GetFxdFixedAssetHeadByCompanyIdAndModuleId";
            //public static string GetAnFCOAsByCompanyIdAndDepthLevel = "GetAnFCOAsByCompanyIdAndDepthLevel";
            public static string GetAnFTransactinalHeadByCmnCompanyId = "GetAnFTransactinalHeadByCmnCompanyId";
        }

        public static class AnFChequeBooks
        {
            public static string GetAnFCompanyCashAtBankByCompanyId = "GetAnFCompanyCashAtBankByCompanyId";
           
        }
        public static class AnFCOGSMapping
        {
            //public static string DeleteAnFCOGSMappingsByCOGSHeadIdAndAnFPLHeadId = "DeleteAnFCOGSMappingsByCOGSHeadIdAndAnFPLHeadId";
            //public static string InsertAnFCOGSMappings = "InsertAnFCOGSMappings";
            //public static string GetAnFCOGSHeadByCompanyId = "GetAnFCOGSHeadByCompanyId";
        }
        public static class AnFCOGSSubHead
        {
            //public static string GetAnFCOGSSubHeadMapping = "GetAnFCOGSSubHeadMapping";
            //public static string GetAnFCOGSSubHeads = "GetAnFCOGSSubHeads";
        }
       
        public static class AnFCompanyVoucherType
        {
            //public static string DeleteAnFCompanyVoucherTypesByCmnCompanyId = "DeleteAnFCompanyVoucherTypesByCmnCompanyId";
            //public static string InsertAnFCompanyVoucherTypes = "InsertAnFCompanyVoucherTypes";
        }
        public static class AnFCostCenter
        {
            //public static string DeleteAnFCostcentersById = "DeleteAnFCostcentersById";
            //public static string GetAnFCostCenterByCompanyId = "GetAnFCostCenterByCompanyId";
            //public static string GetAnFCostCenterById = "GetAnFCostCenterById";
            //public static string GetAnFCostCenterByCode = "GetAnFCostCenterByCode";
            //public static string GetAnFCostCentersByCmnCompanyId = "GetAnFCostCentersByCmnCompanyId";
            //public static string GetAnFCostCentersByCmnCompanyIdDT = "GetAnFCostCentersByCmnCompanyIdDT";
            //public static string GetAnFCostCentersById = "GetAnFCostCentersById";
            //public static string GetAnFCostCentersCodeByChild = "GetAnFCostCentersCodeByChild";
            //public static string GetAnFCostCentersLastChildNode = "GetAnFCostCentersLastChildNode";
            //public static string GetAnFCostCentersLastCodeByCompanyId = "GetAnFCostCentersLastCodeByCompanyId";
            //public static string InsertAnFCostcenters = "InsertAnFCostcenters";
            //public static string UpdateAnFCostcenters = "UpdateAnFCostcenters";
            //public static string GetProjectCostCenterStatus = "GetProjectCostCenterStatus";
            //public static string UpdateProjectCostCenter = "UpdateProjectCostCenter";
            //public static string SetProjectCostCenter = "SetProjectCostCenter";
            //public static string GetMappedProjectCostCenterStatus = "GetMappedProjectCostCenterStatus";
            //public static string GetCodeAnFCostcenters = "GetCodeAnFCostcenters";
           
        }
        public static class AnFInterProjectTransactionalHead
        {
            //public static string GetAnFInterProjectTransactionalHeadsByCmnProjectSegmentId = "GetAnFInterProjectTransactionalHeadsByCmnProjectSegmentId";
            //public static string InsertAnFInterProjectTransactionalHeads = "InsertAnFInterProjectTransactionalHeads";
        }
        public static class AnFMonthLock
        {
            //public static string GetAnFMonthLockByCmnFiscalYearId = "GetAnFMonthLockByCmnFiscalYearId";
            //public static string GetAnFMonthLockById = "GetAnFMonthLockById";
            //public static string GetAnFMonthLockStatusByCmnFiscalYearIdNMonthNo = "GetAnFMonthLockStatusByCmnFiscalYearIdNMonthNo";
            //public static string UpdateAnFMonthLockClosingStatus = "UpdateAnFMonthLockClosingStatus";
        }
        public static partial class AnFOpeningBalance
        {
            //public static string DeleteAnFOpeningBalancesById = "DeleteAnFOpeningBalancesById";
            //public static string GetAnFOpeningBalancesByANFCY = "GetAnFOpeningBalancesByANFCY";
            //public static string GetAnFOpeningBalancesByANFID = "GetAnFOpeningBalancesByANFID";
            //public static string GetAnFOpeningBalancesByFPC = "GetAnFOpeningBalancesByFPC";
            //public static string GetCountAnFOpeningBalancesByCmnProjectSegmentsIdAndAnFCOAsIdAndCmnTransactionalYearId = "GetCountAnFOpeningBalancesByCmnProjectSegmentsIdAndAnFCOAsIdAndCmnTransactionalYearId";
            //public static string InsertAnFOpeningBalances = "InsertAnFOpeningBalances";
            //public static string UpdateAnFOpeningBalances = "UpdateAnFOpeningBalances";
            //public static string GetAnFOpenningBalance = "GetAnFOpenningBalance";
            //public static string InsertAnFOpeningBalancesForNewFYOpenningHead = "InsertAnFOpeningBalancesForNewFYOpenningHead";

        }
        public static class AnFPLHead
        {
            //public static string DeleteAnFCOGSMappingsByCOGSHeadIdAndAnFPLHeadId = "DeleteAnFCOGSMappingsByCOGSHeadIdAndAnFPLHeadId";
            //public static string GetAnFPLHeadsByCmnCompanyId = "GetAnFPLHeadsByCmnCompanyId";
        }
        public static class AnFPLMapping
        {
            //public static string DeleteAnFPLMappingsByPLHeadIdAndCompanyId = "DeleteAnFPLMappingsByPLHeadIdAndCompanyId";
            //public static string GetAnFPLMappingsByPLHeadId = "GetAnFPLMappingsByPLHeadId";
            //public static string InsertAnFPLMappings = "InsertAnFPLMappings";
        }
      
        public static class AnFVoucherDetail
        {
            //public static string DeleteAnFVoucherDetailsByAnFVoucherId = "DeleteAnFVoucherDetailsByAnFVoucherId";
            public static string GetAnFVoucherDetailsByVoucherId = "GetAnFVoucherDetailsByVoucherId";
            //public static string InsertAnFVoucherDetails = "InsertAnFVoucherDetails";
            //public static string InsertAnFVoucherDetails1 = "InsertAnFVoucherDetails1";
            //public static string SearchAnFVoucherDetailsByVoucherId = "SearchAnFVoucherDetailsByVoucherId";
            //public static string UpdateAnFVoucherDetails = "UpdateAnFVoucherDetails";
            //public static string UpdateAnFVoucherDetailsForAccHeadTransfer = "UpdateAnFVoucherDetailsForAccHeadTransfer";
        }
        public static class AnFVoucher
        {
            //public static string GetAnFVouchersByCmnCompanyId = "GetAnFVouchersByCmnCompanyId";
            //public static string GetAnFVouchersByCmnCompanyIdNProcessIdNLevelId = "GetAnFVouchersByCmnCompanyIdNProcessIdNLevelId";
            //public static string InsertAnFVouchers = "InsertAnFVouchers";
            //public static string InsertAnFVouchers1 = "InsertAnFVouchers1";
            //public static string RollBackAnFVouchers = "RollBackAnFVouchers";
            public static string SearchAnFVouchers = "SearchAnFVouchers";
            //public static string SearchAnFVouchersByVoucherId = "SearchAnFVouchersByVoucherId";
            //public static string UpdateAnFVouchers = "UpdateAnFVouchers";
            //public static string UpdateAnFVouchersForCancelVoucher = "UpdateAnFVouchersForCancelVoucher";
            //public static string GetAnFVoucherById = "GetAnFVoucherById";
            public static string GetAnFVoucherNo = "GetAnFVoucherNo";
            //public static string GetAnFCheckedVouchers = "GetAnFCheckedVouchers";
            //public static string GetAnFCheckedVouchersByRefId = "GetAnFCheckedVouchersByRefId";
            //public static string GetAnFUnpostedVouchers = "GetAnFUnpostedVouchers";
            //public static string GetNoOfUnPostedAnFVoucherByType = "GetNoOfUnPostedAnFVoucherByType";
            public static string ExecuteSqlText = "ExecuteSqlText";
            //public static string GetNoOfAnFVoucherByType = "GetNoOfAnFVoucherByType";
            //public static string GetNoOfPostedAnFVoucherByType = "GetNoOfPostedAnFVoucherByType";
            //public static string GetNoOfCancelledAnFVoucherByType = "GetNoOfCancelledAnFVoucherByType";
            //public static string GetBnkVouchers = "GetBnkVouchers";
            public static string AnFVoucherSearch = "AnFVoucherSearch";
        }
        public static class AnFFixedAssets
        {
            //public static string GetAnFVoucherTypeById = "GetAnFVoucherTypeById";
            //public static string GetAnFVoucherTypeByPrefix = "GetAnFVoucherTypeByPrefix";
            //public static string GetAnFVoucherTypes = "GetAnFVoucherTypes";
            //public static string GetAnFVoucherTypesByComapanyIdNDepartmentId = "GetAnFVoucherTypesByComapanyIdNDepartmentId";
            //public static string GetAnFVoucherTypesByCompanyId = "GetAnFVoucherTypesByCompanyId";
        }

        public static class AnFClosingBlance
        {
            //public static string GetCmnAccountHeadMappers = "GetCmnAccountHeadMappers";
            //public static string InsertCmnAccountHeadMappers = "InsertCmnAccountHeadMappers";
            public static string GetAnfClosingBalanceByFinancialYearId = "GetAnfClosingBalanceByFinancialYearId";
        }

        public static class FxdAcquisition
        {
            //public static string GetCmnAccountHeadMappers = "GetCmnAccountHeadMappers";
            //public static string InsertCmnAccountHeadMappers = "InsertCmnAccountHeadMappers";
            public static string GetAllAcquisition = "GetAllAcquisition";
        }
      
        #endregion

        #region Inventory


        public static class DamageProduct
        {
            public static string DamageProductDetails = "DamageProductDetails";
          
        }

        public static class InvStoreOpening
        {
            public static string GetInvStoreOpeningByInvStoreId = "GetInvStoreOpeningByInvStoreId";
            //public static string GetInvCompanySuppliers = "GetInvCompanySuppliers";
            //public static string GetInvCompanySuppliersById = "GetInvCompanySuppliersById";
            //public static string InsertInvCompanySuppliers = "InsertInvCompanySuppliers";
            //public static string UpdateInvCompanySuppliers = "UpdateInvCompanySuppliers";
        }
        public static class InvCurrentStock
        {
            //public static string DeleteInvCurrentStocksById = "DeleteInvCurrentStocksById";
            //public static string GetInvCurrentStocks = "GetInvCurrentStocks";
            //public static string GetInvCurrentStocksById = "GetInvCurrentStocksById";
            //public static string InsertInvCurrentStocks = "InsertInvCurrentStocks";
            //public static string UpdateInvCurrentStocks = "UpdateInvCurrentStocks";
        }
        public static class InvDamage
        {
            //public static string DeleteInvDamagesById = "DeleteInvDamagesById";
            //public static string GetInvDamagesById = "GetInvDamagesById";
            //public static string GetInvDamagesByIsCancelled = "GetInvDamagesByIsCancelled";
            //public static string InsertInvDamages = "InsertInvDamages";
            //public static string UpdateInvDamages = "UpdateInvDamages";
            //public static string UpdateInvDamagesForCancel = "UpdateInvDamagesForCancel";
            //public static string GetInvActiveDamages = "GetInvActiveDamages";
        }
        public static class InvDamageType
        {
            //public static string DeleteInvDamageTypesById = "DeleteInvDamageTypesById";
            //public static string GetInvDamageTypes = "GetInvDamageTypes";
            //public static string GetInvDamageTypesById = "GetInvDamageTypesById";
            //public static string InsertInvDamageTypes = "InsertInvDamageTypes";
            //public static string UpdateInvDamageTypes = "UpdateInvDamageTypes";
        }
        public static class InvGateInformationDetail
        {
            //public static string InsertInvGateInformationDetails = "InsertInvGateInformationDetails";
            //public static string DeleteInvGateInfoDetailsByInvGateInfoId = "DeleteInvGateInfoDetailsByInvGateInfoId";
        }
        public static class InvGateInformation
        {
            //public static string GetInvGateInformationsById = "GetInvGateInformationsById";
            //public static string GetInvGateInfoByPOPRId = "GetInvGateInfoByPOPRId";
            //public static string InsertOrUpdateInvGateInfo = "InsertOrUpdateInvGateInfo";
            //public static string DeleteInvGateInfosById = "DeleteInvGateInfosById";
            //public static string GetInvGateInfoById = "GetInvGateInfoById";
            //public static string GetInvGateInfoByCmnCompanyId = "GetInvGateInfoByCmnCompanyId";
        }
        public static class InvInvoiceDetail
        {
            //public static string DeleteInvInvoiceDetailsByInvInvoiceId = "DeleteInvInvoiceDetailsByInvInvoiceId";
            //public static string GetInvInvoiceDetails = "GetInvInvoiceDetails";
            //public static string GetInvInvoiceDetailsById = "GetInvInvoiceDetailsById";
            //public static string GetInvInvoiceDetailsByInvInvoiceId = "GetInvInvoiceDetailsByInvInvoiceId";
            //public static string InsertInvInvoiceDetails = "InsertInvInvoiceDetails";
            //public static string UpdateInvInvoiceDetails = "UpdateInvInvoiceDetails";
        }
        public static class InvInvoice
        {
            //public static string DeleteInvInvoicesById = "DeleteInvInvoicesById";
            //public static string GetInvInvoices = "GetInvInvoices";
            //public static string GetInvInvoicesByCmnCompanyId = "GetInvInvoicesByCmnCompanyId";
            //public static string GetInvInvoicesById = "GetInvInvoicesById";
            //public static string GetInvInvoicesByInvPurchaseRequisitionIdORPrcPurchaseOrderId = "GetInvInvoicesByInvPurchaseRequisitionIdORPrcPurchaseOrderId";
            //public static string GetInvInvoicesDetails = "GetInvInvoicesDetails";
            //public static string InsertInvInvoices = "InsertInvInvoices";
            //public static string UpdateInvInvoices = "UpdateInvInvoices";
            //public static string UpdateInvInvoicesForCancel = "UpdateInvInvoicesForCancel";
            //public static string RptInvInvoices = "RptInvInvoices";
        }
        public static class InvIssueDetail
        {
            public static string GetIssueDetailByIssueId = "GetIssueDetailByIssueId";
            //public static string DeleteInvIssueDetailsByInvIssueId = "DeleteInvIssueDetailsByInvIssueId";
            //public static string GetInvIssueDetailByIssueId = "GetInvIssueDetailByIssueId";
            //public static string GetInvIssueDetailByItemId = "GetInvIssueDetailByItemId";
            //public static string GetInvIssueDetails = "GetInvIssueDetails";
            //public static string GetInvIssueDetailsById = "GetInvIssueDetailsById";
            //public static string GetInvIssueDetailsByIssueIdForFreeze = "GetInvIssueDetailsByIssueIdForFreeze";
            //public static string GetInvIssueDetailsByIssueIdForProductionStore = "GetInvIssueDetailsByIssueIdForProductionStore";
            //public static string InsertInvIssueDetails = "InsertInvIssueDetails";
            //public static string UpdateInvIssueDetails = "UpdateInvIssueDetails";
        }
        public static class InvIssue
        {
            //public static string DeleteInvIssuesById = "DeleteInvIssuesById";
            //public static string GetInvIssues = "GetInvIssues";
            //public static string GetInvIssuesById = "GetInvIssuesById";
            //public static string InsertInvIssues = "InsertInvIssues";
            //public static string UpdateInvIssues = "UpdateInvIssues";
            //public static string UpdateInvIssuesCancelStatus = "UpdateInvIssuesCancelStatus";
            //public static string UpdateInvIssueStatus = "UpdateInvIssueStatus";
            //public static string GetInvIssueInfo = "GetInvIssueInfo";
            //public static string GetInvIssueHistory = "GetInvIssueHistory";
        }
        public static class InvItemCountry
        {
            //public static string DeleteInvItemCountriesById = "DeleteInvItemCountriesById";
            //public static string GetInvItemCountries = "GetInvItemCountries";
            //public static string GetInvItemCountriesById = "GetInvItemCountriesById";
            //public static string InsertInvItemCountries = "InsertInvItemCountries";
            //public static string UpdateInvItemCountries = "UpdateInvItemCountries";
        }
        public static class InvItemManufacturer
        {
            //public static string DeleteInvItemManufacturersByInvItemId = "DeleteInvItemManufacturersByInvItemId";
            //public static string GetInvItemManufacturers = "GetInvItemManufacturers";
            //public static string GetInvItemManufacturersById = "GetInvItemManufacturersById";
            //public static string InsertInvItemManufacturers = "InsertInvItemManufacturers";
            //public static string UpdateInvItemManufacturers = "UpdateInvItemManufacturers";
        }
        public static class InvItemProperty
        {
            //public static string DeleteInvItemPropertiesById = "DeleteInvItemPropertiesById";
            //public static string DeleteInvItemPropertiesByInvItemId = "DeleteInvItemPropertiesByInvItemId";
            //public static string GetInvItemProperties = "GetInvItemProperties";
            //public static string GetInvItemPropertiesById = "GetInvItemPropertiesById";
            //public static string GetInvItemPropertiesByItemId = "GetInvItemPropertiesByItemId";
            //public static string InsertInvItemProperties = "InsertInvItemProperties";
            //public static string UpdateInvItemProperties = "UpdateInvItemProperties";
        }
        public static class InvItemRack
        {
            //public static string DeleteInvItemRacksById = "DeleteInvItemRacksById";
            //public static string GetInvItemRacks = "GetInvItemRacks";
            //public static string GetInvItemRacksById = "GetInvItemRacksById";
            //public static string InsertInvItemRacks = "InsertInvItemRacks";
            //public static string UpdateInvItemRacks = "UpdateInvItemRacks";
        }
        public static class InvItemReturn
        {
            //public static string DeleteInvItemReturnsById = "DeleteInvItemReturnsById";
            //public static string GetInvItemReturns = "GetInvItemReturns";
            //public static string GetInvItemReturnsById = "GetInvItemReturnsById";
            //public static string InsertInvItemReturns = "InsertInvItemReturns";
            //public static string UpdateInvItemReturns = "UpdateInvItemReturns";
        }
        public static class InvItem
        {
            //public static string DeleteInvItemsById = "DeleteInvItemsById";
            //public static string GetInvItemsBarcodeGen = "GetInvItemsBarcodeGen";
            //public static string GetInvItemBarcodeGenerateByCompanyId = "GetInvItemBarcodeGenerateByCompanyId";
            //public static string GetInvItemsByCmnCompanyId = "GetInvItemsByCmnCompanyId";
            //public static string GetInvItemsByCmnCompanyIdNStatus = "GetInvItemsByCmnCompanyIdNStatus";
            //public static string GetInvItemsByCmnCompanyIdNStatusForTender = "  ";
            //public static string GetInvItemsByCmnCompanyIdNStatusNLevelNo = "GetInvItemsByCmnCompanyIdNStatusNLevelNo";
            //public static string GetInvItemsByCode = "GetInvItemsByCode";
            //public static string GetInvItemsByCompanyId = "GetInvItemsByCompanyId";
            //public static string GetInvItemsById = "GetInvItemsById";
            //public static string GetInvItemsByName = "GetInvItemsByName";
            //public static string GetInvItemsbyRackId = "GetInvItemsbyRackId";
            //public static string GetInvItemsbyWarehouseId = "GetInvItemsbyWarehouseId";
            //public static string GetInvItemSearchByItemName = "GetInvItemSearchByItemName";
            //public static string GetInvItemsOnly = "GetInvItemsOnly";
            //public static string GetInvItemsSubSheetByTopSheet = "GetInvItemsSubSheetByTopSheet";
            //public static string GetInvItemsTopSheet = "GetInvItemsTopSheet";
            //public static string GetInvItemsByCompanyIdAndModuleId = "GetInvItemsByCompanyIdAndModuleId";
            //public static string InsertInvItems = "InsertInvItems";
            //public static string UpdateInvItems = "UpdateInvItems";
            //public static string GetInvParentItemsByChildId = "GetInvParentItemsByChildId";
            //public static string GetInvItemsByCmnCompanyIdAndStatusForTender = "GetInvItemsByCmnCompanyIdAndStatusForTender";
            //public static string GetInvItemsByCmnCompanyIdAndStatusAndLevelNo = "GetInvItemsByCmnCompanyIdAndStatusAndLevelNo";
            //public static string GetInvItemGroups = "GetInvItemGroups";
            //public static string GenerateInvItemCode = "GenerateInvItemCode";
            //public static string GetItemOpeningBalances = "GetItemOpeningBalances";
            //public static string GetInvSearchedItems = "GetInvSearchedItems";
            //public static string GetInvItemLastCode = "GetInvItemLastCode";
        }
        public static class InvItemSupplier
        {
            //public static string DeleteInvItemSuppliersByInvItemId = "DeleteInvItemSuppliersByInvItemId";
            //public static string GetInvItemSuppliers = "GetInvItemSuppliers";
            //public static string GetInvItemSuppliersById = "GetInvItemSuppliersById";
            //public static string InsertInvItemSuppliers = "InsertInvItemSuppliers";
            //public static string UpdateInvItemSuppliers = "UpdateInvItemSuppliers";
        }
        public static class InvMRRDetail
        {
            //public static string DeleteInvMRRDetailsById = "DeleteInvMRRDetailsById";
            //public static string DeleteInvMRRDetailsByInvMRRId = "DeleteInvMRRDetailsByInvMRRId";
            //public static string GetInvMRRDetails = "GetInvMRRDetails";
            //public static string GetInvMRRDetailsById = "GetInvMRRDetailsById";
            //public static string InsertInvMRRDetails = "InsertInvMRRDetails";
            //public static string UpdateInvMRRDetails = "UpdateInvMRRDetails";
            //public static string GetInvMRRDetailsByInvMRRId = "GetInvMRRDetailsByInvMRRId";
        }
        public static class InvMRR
        {
            //public static string DeleteInvMRRsById = "DeleteInvMRRsById";
            //public static string GetInvMRRsByCmnCompanyId = "GetInvMRRsByCmnCompanyId";
            //public static string GetInvMRRsById = "GetInvMRRsById";
            //public static string GetInvMRRsByPOId = "GetInvMRRsByPOId";
            //public static string InsertInvMRRs = "InsertInvMRRs";
            //public static string UpdateInvMRRs = "UpdateInvMRRs";
            //public static string UpdateInvMRRsCancelStatus = "UpdateInvMRRsCancelStatus";
            //public static string UpdateInvMRRsQCIdByMRRId = "UpdateInvMRRsQCIdByMRRId";
            //public static string UpdateInvPRAndPrcPOStatus = "UpdateInvPRAndPrcPOStatus";
            //public static string GetInvMRRByPOPRIdAndDateRange = "GetInvMRRByPOPRIdAndDateRange";
            //public static string GetInvMRRByPOPRId = "GetInvMRRByPOPRId";
            //public static string GetInvMRRById = "GetInvMRRById";
            //public static string GetInvMRRForApproval = "GetInvMRRForApproval";
        }
       
       
        public static class InvProperty
        {
            //public static string DeleteInvPropertiesById = "DeleteInvPropertiesById";
            //public static string GetInvProperties = "GetInvProperties";
            //public static string GetInvPropertiesByCmnCompanyId = "GetInvPropertiesByCmnCompanyId";
            //public static string GetInvPropertiesById = "GetInvPropertiesById";
            //public static string InsertInvProperties = "InsertInvProperties";
            //public static string UpdateInvProperties = "UpdateInvProperties";
        }
        public static class InvPropertyDetail
        {
            //public static string DeleteInvPropertyDetailsById = "DeleteInvPropertyDetailsById";
            //public static string GetInvPropertyDetails = "GetInvPropertyDetails";
            //public static string GetInvPropertyDetailsById = "GetInvPropertyDetailsById";
            //public static string GetInvPropertyDetailsByPropertyId = "GetInvPropertyDetailsByPropertyId";
            //public static string InsertInvPropertyDetails = "InsertInvPropertyDetails";
            //public static string UpdateInvPropertyDetails = "UpdateInvPropertyDetails";
        }
        public static class InvPurchaseRequisitionDetail
        {
            //public static string DeleteInvPurchaseRequisitionDetailsById = "DeleteInvPurchaseRequisitionDetailsById";
            //public static string DeleteInvPurchaseRequisitionDetailsByInvPurchaseRequisitionId = "DeleteInvPurchaseRequisitionDetailsByInvPurchaseRequisitionId";
            //public static string GetInvPurchaseRequisitionDetails = "GetInvPurchaseRequisitionDetails";
            //public static string GetInvPurchaseRequisitionDetailsByPRId = "GetInvPurchaseRequisitionDetailsByPRId";
            //public static string GetInvPurchaseRequisitionDetailsForMRRByPRId = "GetInvPurchaseRequisitionDetailsForMRRByPRId";
            //public static string GetInvPurchaseRequisitionDetailsForMRRByPRIdwithGate = "GetInvPurchaseRequisitionDetailsForMRRByPRIdwithGate";
            //public static string InsertInvPurchaseRequisitionDetails = "InsertInvPurchaseRequisitionDetails";
            //public static string UpdateInvPurchaseRequisitionDetails = "UpdateInvPurchaseRequisitionDetails";
            //public static string GetInvPOPRPendingQuantiyForMRR = "GetInvPOPRPendingQuantiyForMRR";
        }
        public static class InvPurchaseRequisition
        {
            //public static string UpdateInvPurchaseRequisitionsForAssign = "UpdateInvPurchaseRequisitionsForAssign";
            //public static string DeleteInvPurchaseRequisitionsById = "DeleteInvPurchaseRequisitionsById";
            //public static string GetInvPurchaseRequisitions = "GetInvPurchaseRequisitions";
            //public static string GetInvPurchaseRequisitionsByDateRange = "GetInvPurchaseRequisitionsByDateRange";
            //public static string GetInvPurchaseRequisitionsById = "GetInvPurchaseRequisitionsById";
            //public static string GetInvPurchaseRequisitionsforInvoice = "GetInvPurchaseRequisitionsforInvoice";
            //public static string GetInvPurchaseRequisitionswithGateById = "GetInvPurchaseRequisitionswithGateById";
            //public static string InsertInvPurchaseRequisitions = "InsertInvPurchaseRequisitions";
            //public static string UpdateInvPurchaseRequisitions = "UpdateInvPurchaseRequisitions";
            //public static string UpdateInvPurchaseRequisitionsApproval = "UpdateInvPurchaseRequisitionsApproval";
            //public static string UpdateInvPurchaseRequisitionsIsDirect = "UpdateInvPurchaseRequisitionsIsDirect";
            //public static string UpdateInvPurchaseRequisitionsIsLocal = "UpdateInvPurchaseRequisitionsIsLocal";
            //public static string GetIndirectPRs = "GetIndirectPRs";
            //public static string GetInvPurchaseRequsitionsByDate = "GetInvPurchaseRequsitionsByDate";
            //public static string DayCountFromTwoDates = "DayCountFromTwoDates";
            //public static string GetInvDirectPurchaseRequisitionsByCompanyId = "GetInvDirectPurchaseRequisitionsByCompanyId";
            //public static string GetInvDirectPRByCompanyForMRR = "GetInvDirectPRByCompanyForMRR";
            //public static string GetInvDirectPRForMRR = "GetInvDirectPRForMRR";
            //public static string GetInvIsPRIdExistInInvoiceOrRFQ = "GetInvIsPRIdExistInInvoiceOrRFQ";
            //public static string GetInvPRForInvoiceList = "GetInvPRForInvoiceList";
            //public static string GetInvPOPRStatusForInvoiceList = "GetInvPOPRStatusForInvoiceList";
            //public static string GetInvPRForApproval = "GetInvPRForApproval";
            //public static string GetInvPOPRByDateForReturn = "GetInvPOPRByDateForReturn";
            //public static string GetInvPurchaseRequisitionInfoForGateById = "GetInvPurchaseRequisitionInfoForGateById";

        }
        public static class InvPurchaseReturnDetail
        {
            //public static string DeleteInvPurchaseReturnDetailsByInvReturnToPartyId = "DeleteInvPurchaseReturnDetailsByInvReturnToPartyId";
            //public static string InsertInvPurchaseReturnDetails = "InsertInvPurchaseReturnDetails";
        }
        public static class InvPurchaseReturn
        {
            //public static string GetInvPurchaseReturnsByCmnCompanyId = "GetInvPurchaseReturnsByCmnCompanyId";
            //public static string InsertInvPurchaseReturns = "InsertInvPurchaseReturns";
            //public static string UpdateInvPurchaseReturns = "UpdateInvPurchaseReturns";
            //public static string UpdateInvPurchaseReturnsCancelStatus = "UpdateInvPurchaseReturnsCancelStatus";
        }
        public static class InvQualityControlDetail
        {
            //public static string DeleteInvQualityControlDetailsByInvQualityControlId = "DeleteInvQualityControlDetailsByInvQualityControlId";
            //public static string GetInvQualityControlDetails = "GetInvQualityControlDetails";
            //public static string GetInvQualityControlDetailsById = "GetInvQualityControlDetailsById";
            //public static string GetInvQualityControlDetailsByQCId = "GetInvQualityControlDetailsByQCId";
            //public static string InsertInvQualityControlDetails = "InsertInvQualityControlDetails";
            //public static string UpdateInvQualityControlDetails = "UpdateInvQualityControlDetails";
        }
        public static class InvQualityControl
        {
            //public static string DeleteInvQualityControlsById = "DeleteInvQualityControlsById";
            //public static string GetInvQualityControls = "GetInvQualityControls";
            //public static string GetInvQualityControlsById = "GetInvQualityControlsById";
            //public static string GetInvQualityControlsByPOId = "GetInvQualityControlsByPOId";
            //public static string GetInvQualityControlsByPOPRId = "GetInvQualityControlsByPOPRId";
            //public static string GetInvQualityControlsByQCId = "GetInvQualityControlsByQCId";
            //public static string GetInvQualityControlsForApproval = "GetInvQualityControlsForApproval";
            //public static string GetInvQualityControlsforMRR = "GetInvQualityControlsforMRR";
            //public static string GetInvQualityControlsMRRByPRId = "GetInvQualityControlsMRRByPRId";
            //public static string GetInvQualityControlsRejectReturnByPOPRId = "GetInvQualityControlsRejectReturnByPOPRId";
            //public static string GetInvQualityControlsReturnByPOPRId = "GetInvQualityControlsReturnByPOPRId";
            //public static string GetInvQualityControlsReturnStatus = "GetInvQualityControlsReturnStatus";
            //public static string InsertInvQualityControls = "InsertInvQualityControls";
            //public static string UpdateInvQualityControls = "UpdateInvQualityControls";
            //public static string UpdateInvQualityControlsApproval = "UpdateInvQualityControlsApproval";
            //public static string UpdateInvQualityControlsCancelStatus = "UpdateInvQualityControlsCancelStatus";
            //public static string GetInvPRforQC = "GetInvPRforQC";
            //public static string GetInvPRforQCwithGate = "GetInvPRforQCwithGate";
            //public static string GetPrcPOforQC = "GetPrcPOforQC";
            //public static string GetInvQCPendingMRR = "GetInvQCPendingMRR";
            //public static string GetInvChallanForQC = "GetInvChallanForQC";
        }
        public static class InvRack
        {
            //public static string DeleteInvRacksById = "DeleteInvRacksById";
            //public static string GetInvRacks = "GetInvRacks";
            //public static string GetInvRacksByCmnCompanyId = "GetInvRacksByCmnCompanyId";
            //public static string GetInvRacksByCmnCompanyIdNWareHouseIdNTypeId = "GetInvRacksByCmnCompanyIdNWareHouseIdNTypeId";
            //public static string GetInvRacksById = "GetInvRacksById";
            //public static string GetInvRacksByWareHouseId = "GetInvRacksByWareHouseId";
            //public static string InsertInvRacks = "InsertInvRacks";
            //public static string UpdateInvRacks = "UpdateInvRacks";
            //public static string GetInvRackForMRR = "GetInvRackForMRR";
            //public static string GetInvRacksAvailableByItemId = "GetInvRacksAvailableByItemId";
        }
        public static class InvRackType
        {
            //public static string DeleteInvRackTypesById = "DeleteInvRackTypesById";
            //public static string GetInvRackTypes = "GetInvRackTypes";
            //public static string GetInvRackTypesByCmnCompanyId = "GetInvRackTypesByCmnCompanyId";
            //public static string GetInvRackTypesById = "GetInvRackTypesById";
            //public static string InsertInvRackTypes = "InsertInvRackTypes";
            //public static string UpdateInvRackTypes = "UpdateInvRackTypes";
        }
        public static class InvReturn
        {
            //public static string DeleteInvReturnsById = "DeleteInvReturnsById";
            //public static string GetInvReturns = "GetInvReturns";
            //public static string GetInvPurchaseReturnsById = "GetInvPurchaseReturnsById";
            //public static string GetInvReturnsListByDateRange = "GetInvReturnsListByDateRange";
            //public static string InsertInvReturns = "InsertInvReturns";
            //public static string UpdateInvReturns = "UpdateInvReturns";
            //public static string UpdateInvReturnsApprovalStatus = "UpdateInvReturnsApprovalStatus";
            //public static string UpdateInvReturnsCancelStatus = "UpdateInvReturnsCancelStatus";
            //public static string GetInvReturnFromMRRByPOPRId = "GetInvReturnFromMRRByPOPRId";
            //public static string GetInvReturnDetailsByReturnId = "GetInvReturnDetailsByReturnId";
        }
        public static class InvReturnType
        {
            //public static string DeleteInvReturnTypesById = "DeleteInvReturnTypesById";
            //public static string GetInvReturnTypesById = "GetInvReturnTypesById";
            //public static string GetInvReturnTypesByStatus = "GetInvReturnTypesByStatus";
            //public static string InsertInvReturnTypes = "InsertInvReturnTypes";
            //public static string UpdateInvReturnTypes = "UpdateInvReturnTypes";
        }
        public static class InvStockInOut
        {
            //public static string GetInvStoreRequisitionDetailsByRequisitionId =
            //    "GetInvStoreRequisitionDetailsByRequisitionId";
            //public static string DeleteInvStockInOutsById = "DeleteInvStockInOutsById";
            //public static string DeleteInvStockInOutsByMRRId = "DeleteInvStockInOutsByMRRId";
            //public static string GetInvStockInOutById = "GetInvStockInOutById";
            //public static string GetInvStockInOuts = "GetInvStockInOuts";
            //public static string InsertInvStockInOut = "InsertInvStockInOut";
            //public static string UpdateInvStockInOut = "UpdateInvStockInOut";
        }
        public static class InvStockOpen
        {
            //public static string DeleteInvStockOpensById = "DeleteInvStockOpensById";
            //public static string GetInvStockOpens = "GetInvStockOpens";
            //public static string GetInvStockOpensByCmnCompanyId = "GetInvStockOpensByCmnCompanyId";
            //public static string GetInvStockOpensById = "GetInvStockOpensById";
            //public static string InsertInvStockOpens = "InsertInvStockOpens";
            //public static string UpdateInvStockOpeningBalance = "UpdateInvStockOpeningBalance";
            //public static string UpdateInvStockOpens = "UpdateInvStockOpens";
        }
        public static class InvStoreRequisitionDetail
        {
            //public static string GetInvStoreRequisitionDetailsByRequisitionId = "GetInvStoreRequisitionDetailsByRequisitionId";
            //public static string GetInvStoreRequisitionDetails = "GetInvStoreRequisitionDetails";
            //public static string InsertInvStoreRequisitionDetails = "InsertInvStoreRequisitionDetails";
            //public static string UpdateInvStoreRequisitionDetails = "UpdateInvStoreRequisitionDetails";
            //public static string GetInvSRDetailsBySRId = "GetInvSRDetailsBySRId";

            //public static string DeleteInvStoreRequisitionDetailsByInvStoreRequisitionId =
            //    "DeleteInvStoreRequisitionDetailsByInvStoreRequisitionId";
        }
        public static class InvStoreRequisition
        {
            //public static string DeleteInvStoreRequisitionsById = "DeleteInvStoreRequisitionsById";
            //public static string GetInvStoreRequisitionsByCmnCompanyIdNStatus = "GetInvStoreRequisitionsByCmnCompanyIdNStatus";
            //public static string GetInvStoreRequisitionsByDateRangeAndSRNo = "GetInvStoreRequisitionsByDateRangeAndSRNo";
            //public static string GetInvStoreRequisitionsById = "GetInvStoreRequisitionsById";
            //public static string GetInvStoreRequisitionsForApproval = "GetInvStoreRequisitionsForApproval";
            //public static string InsertInvStoreRequisitions = "InsertInvStoreRequisitions";
            //public static string UpdateInvStoreRequisitions = "UpdateInvStoreRequisitions";
            //public static string UpdateInvStoreRequisitionsApproval = "UpdateInvStoreRequisitionsApproval";
        }
        public static class InvSupplier
        {
            //public static string DeleteInvSuppliersById = "DeleteInvSuppliersById";
            //public static string GetInvSupplierByPOPRId = "GetInvSupplierByPOPRId";
            //public static string GetInvSuppliers = "GetInvSuppliers";
            //public static string GetInvSuppliersByCmnCompanyId = "GetInvSuppliersByCmnCompanyId";
            //public static string GetInvSuppliersById = "GetInvSuppliersById";
            //public static string GetInvSuppliersByRFQId = "GetInvSuppliersByRFQId";
            //public static string InsertInvSuppliers = "InsertInvSuppliers";
            //public static string UpdateInvSuppliers = "UpdateInvSuppliers";
        }
        public static class InvUnit
        {
            //public static string DeleteInvUnitsById = "DeleteInvUnitsById";
            //public static string GetInvUnits = "GetInvUnits";
            //public static string GetInvUnitsByCmnCompanyId = "GetInvUnitsByCmnCompanyId";
            //public static string GetInvUnitsById = "GetInvUnitsById";
            //public static string InsertInvUnits = "InsertInvUnits";
            //public static string UpdateInvUnits = "UpdateInvUnits";
        }
        public static class InvWareHouse
        {
            //public static string DeleteInvWareHousesById = "DeleteInvWareHousesById";
            //public static string GetInvWareHouses = "GetInvWareHouses";
            //public static string GetInvWareHousesByCompanyId = "GetInvWareHousesByCompanyId";
            //public static string GetInvWareHousesById = "GetInvWareHousesById";
            //public static string GetInvWareHousesCmnCompanyIdNProjectIdNTypeId = "GetInvWareHousesCmnCompanyIdNProjectIdNTypeId";
            //public static string GetInvWarehouseWhereItemIssued = "GetInvWarehouseWhereItemIssued";
            //public static string GetInvWarehouseLastCode = "GetInvWarehouseLastCode";
            //public static string GetInvRackForMRR = "GetInvRackForMRR";
            //public static string GetInvWarehouseForMRR = "GetInvWarehouseForMRR";
            //public static string GetInvWarehouseByRackId = "GetInvWarehouseByRackId";
            //public static string GetInvWarehouseAndRackByItem = "GetInvWarehouseAndRackByItem";
            //public static string InsertInvWareHouses = "InsertInvWareHouses";
            //public static string UpdateInvWareHouses = "UpdateInvWareHouses";
            //public static string GetInvWarehouseAvailableQtyByItemId = "GetInvWarehouseAvailableQtyByItemId";

        }
        public static class InvWareHouseType
        {
            //public static string DeleteInvWareHouseTypesById = "DeleteInvWareHouseTypesById";
            //public static string GetInvWareHouseTypes = "GetInvWareHouseTypes";
            //public static string GetInvWareHouseTypesByCmnCompanyId = "GetInvWareHouseTypesByCmnCompanyId";
            //public static string GetInvWareHouseTypesById = "GetInvWareHouseTypesById";
            //public static string InsertInvWareHouseTypes = "InsertInvWareHouseTypes";
            //public static string UpdateInvWareHouseTypes = "UpdateInvWareHouseTypes";
        }

        public static class InvRequisition
        {
            public static string GetAllInvRequisitions = "GetAllInvRequisitions";
        }

        #endregion

        #region Procurement

        public static class PrcPurchaseOrderDetail
        {
            //public static string DeletePrcPurchaseOrderDetailsById = "DeletePrcPurchaseOrderDetailsById";
            //public static string GetPrcPurchaseOrderDetails = "GetPrcPurchaseOrderDetails";
            //public static string GetPrcPurchaseOrderDetailsById = "GetPrcPurchaseOrderDetailsById";
            //public static string InsertPrcPurchaseOrderDetails = "InsertPrcPurchaseOrderDetails";
            //public static string UpdatePrcPurchaseOrderDetails = "UpdatePrcPurchaseOrderDetails";
        }
        public static class PrcPurchaseOrder
        {
            //public static string DeletePrcPurchaseOrdersById = "DeletePrcPurchaseOrdersById";
            //public static string GetPrcPurchaseOrders = "GetPrcPurchaseOrders";
            //public static string GetPrcPurchaseOrdersByCompanyId = "GetPrcPurchaseOrdersByCompanyId";
            //public static string GetPrcPurchaseOrdersByCompanyIdForMRR = "GetPrcPurchaseOrdersByCompanyIdForMRR";
            //public static string GetPrcPurchaseOrdersByCompanyIdForQC = "GetPrcPurchaseOrdersByCompanyIdForQC";
            //public static string GetPrcPurchaseOrdersById = "GetPrcPurchaseOrdersById";
            //public static string GetPrcPurchaseOrdersByRFQId = "GetPrcPurchaseOrdersByRFQId";
            //public static string GetPrcPurchaseOrdersForMRR = "GetPrcPurchaseOrdersForMRR";
            //public static string GetPrcPurchaseOrdersForMRRByPOId = "GetPrcPurchaseOrdersForMRRByPOId";
            //public static string GetPrcPurchaseOrdersForMRRwithGateByPOId = "GetPrcPurchaseOrdersForMRRwithGateByPOId";
            //public static string GetPrcPurchaseOrdersWithGateByPOId = "GetPrcPurchaseOrdersWithGateByPOId";
            //public static string InsertPrcPurchaseOrders = "InsertPrcPurchaseOrders";
            //public static string UpdatePrcPurchaseOrders = "UpdatePrcPurchaseOrders";
            //public static string UpdatePrcPurchaseOrdersStatus = "UpdatePrcPurchaseOrdersStatus";
            //public static string GetAllPrcPurchaseOrdersForMRR = "GetAllPrcPurchaseOrdersForMRR";
        }
        public static class PrcQuotationDetail
        {
            //public static string DeletePrcQuotationDetailsById = "DeletePrcQuotationDetailsById";
            //public static string GetPrcQuotationDetails = "GetPrcQuotationDetails";
            //public static string GetPrcQuotationDetailsById = "GetPrcQuotationDetailsById";
            //public static string GetPrcQuotationDetailsByRFQId = "GetPrcQuotationDetailsByRFQId";
            //public static string GetPrcQuotationDetailsItemsSR = "GetPrcQuotationDetailsItemsSR";
            //public static string InsertPrcQuotationDetails = "InsertPrcQuotationDetails";
            //public static string UpdatePrcQuotationDetails = "UpdatePrcQuotationDetails";
        }
        public static class PrcQuotation
        {
            //public static string DeletePrcQuotationsById = "DeletePrcQuotationsById";
            //public static string GetPrcQuotations = "GetPrcQuotations";
            //public static string GetPrcQuotationsById = "GetPrcQuotationsById";
            //public static string GetPrcQuotationsByRFQId = "GetPrcQuotationsByRFQId";
            //public static string InsertPrcQuotations = "InsertPrcQuotations";
            //public static string UpdatePrcQuotations = "UpdatePrcQuotations";
            //public static string UpdatePrcQuotationsByCmprtvNotes = "UpdatePrcQuotationsByCmprtvNotes";
            //public static string UpdatePrcQuotationsByFinalSelection = "UpdatePrcQuotationsByFinalSelection";
            //public static string GetPrcComprartiveNotesByRFQId = "GetPrcComprartiveNotesByRFQId";
            //public static string GetCmrQuotationIdByRFQId = "GetCmrQuotationIdByRFQId";
        }
        public static class PrcRFQDetail
        {
            //public static string DeletePrcRFQDetailsById = "DeletePrcRFQDetailsById";
            //public static string GetPrcRFQDetails = "GetPrcRFQDetails";
            //public static string GetPrcRFQDetailsById = "GetPrcRFQDetailsById";
            //public static string GetPrcRFQDetailsItemsSR = "GetPrcRFQDetailsItemsSR";
            //public static string GetPrcRFQDetailsSuppliersSR = "GetPrcRFQDetailsSuppliersSR";
            //public static string InsertPrcRFQDetails = "InsertPrcRFQDetails";
            //public static string UpdatePrcRFQDetails = "UpdatePrcRFQDetails";
        }
        public static class PrcRFQ
        {
            //public static string DeletePrcRFQsById = "DeletePrcRFQsById";
            //public static string DeletePrcRFQSuppliersById = "DeletePrcRFQSuppliersById";
            //public static string GetPrcRFQs = "GetPrcRFQs";
            //public static string GetPrcRFQsByPRId = "GetPrcRFQsByPRId";
            //public static string GetPrcRFQSuppliers = "GetPrcRFQSuppliers";
            //public static string GetPrcRFQSuppliersByRFQId = "GetPrcRFQSuppliersByRFQId";
            //public static string InsertPrcRFQs = "InsertPrcRFQs";
            //public static string InsertPrcRFQSuppliers = "InsertPrcRFQSuppliers";
            //public static string UpdatePrcRFQs = "UpdatePrcRFQs";
            //public static string UpdatePrcRFQsAtEmail = "UpdatePrcRFQsAtEmail";
            //public static string UpdatePrcRFQSuppliers = "UpdatePrcRFQSuppliers";
            //public static string GetCmrRFQList = "GetCmrRFQList";
            //public static string GetPrcPOforQCwithGate = "GetPrcPOforQCwithGate";
        }
        public static class PrcRFQSupplier
        {
            //public static string DeletePrcRFQSuppliersById = "DeletePrcRFQSuppliersById";
            //public static string GetPrcRFQSuppliers = "GetPrcRFQSuppliers";
            //public static string GetPrcRFQSuppliersByRFQId = "GetPrcRFQSuppliersByRFQId";
            //public static string InsertPrcRFQSuppliers = "InsertPrcRFQSuppliers";
            //public static string UpdatePrcRFQSuppliers = "UpdatePrcRFQSuppliers";
            //public static string GetEmailIdByRFQSuppliersId = "GetEmailIdByRFQSuppliersId";
        }

        #endregion

        #region Commercial

        public static class CmrAmendment
        {
            //public static string DeleteCmrAmendmentsById = "DeleteCmrAmendmentsById";
            //public static string GetCmrAmendments = "GetCmrAmendments";
            //public static string GetCmrAmendmentsByLCId = "GetCmrAmendmentsByLCId";
            //public static string InsertCmrAmendments = "InsertCmrAmendments";
            //public static string UpdateCmrAmendments = "UpdateCmrAmendments";
            //public static string UpdateCmrAmendmentsForRemoveDocsUrl = "UpdateCmrAmendmentsForRemoveDocsUrl";
        }
        public static class CmrCharge
        {
            //public static string DeleteCmrChargesByCmrLCIdAndChargeTypeId = "DeleteCmrChargesByCmrLCIdAndChargeTypeId";
            //public static string DeleteCmrChargesById = "DeleteCmrChargesById";
            //public static string GetCmrChargesByChargeTypeAndLCId = "GetCmrChargesByChargeTypeAndLCId";
            //public static string GetCmrChargesById = "GetCmrChargesById";
            //public static string GetCmrChargesByLCId = "GetCmrChargesByLCId";
            //public static string InsertCmrCharges = "InsertCmrCharges";
            //public static string UpdateCmrCharges = "UpdateCmrCharges";
            //public static string GetCmrLCCharges = "GetCmrLCCharges";
        }
      
        public static class CmrCNFAgent
        {
            //public static string DeleteCmrCNFAgentsById = "DeleteCmrCNFAgentsById";
            //public static string GetCmrCNFAgentById = "GetCmrCNFAgentById";
            //public static string GetCmrCNFAgents = "GetCmrCNFAgents";
            //public static string InsertCmrCNFAgent = "InsertCmrCNFAgent";
            //public static string UpdateCmrCNFAgent = "UpdateCmrCNFAgent";
            //public static string UpdateCmrCNFAgentsCancelStatus = "UpdateCmrCNFAgentsCancelStatus";
        }
        public static class CmrCNFBillDetail
        {
            //public static string DeleteCmrCNFBillDetailsByCmrCNFBillId = "DeleteCmrCNFBillDetailsByCmrCNFBillId";
            //public static string DeleteCmrCNFBillDetailsById = "DeleteCmrCNFBillDetailsById";
            //public static string GetCmrCNFBillDetails = "GetCmrCNFBillDetails";
            //public static string InsertCmrCNFBillDetails = "InsertCmrCNFBillDetails";
            //public static string UpdateCmrCNFBillDetails = "UpdateCmrCNFBillDetails";
        }
        public static class CmrCNFBill
        {
            //public static string DeleteCmrCNFBillsById = "DeleteCmrCNFBillsById";
            //public static string GetCmrCNFBills = "GetCmrCNFBills";
            //public static string GetCmrCNFBillsByRefNoOrId = "GetCmrCNFBillsByRefNoOrId";
            //public static string GetCmrCNFBillsByRequisitionId = "GetCmrCNFBillsByRequisitionId";
            //public static string InsertCmrCNFBills = "InsertCmrCNFBills";
            //public static string UpdateCmrCNFBills = "UpdateCmrCNFBills";
            //public static string GetCmrBillById = "GetCmrBillById";
            //public static string GetCmrBillSummaryByLCId = "GetCmrBillSummaryByLCId";
            //public static string DeleteCmrCNFBillDetailsByCNFBillId = "DeleteCmrCNFBillDetailsByCNFBillId";
            //public static string UpdateCmrCNFBillForRemoveDocsUrl = "UpdateCmrCNFBillForRemoveDocsUrl";
        }
        public static class CmrCNFRequisition
        {
            //public static string DeleteCmrCNFRequisitionById = "DeleteCmrCNFRequisitionById";
            //public static string GetCmrCNFRequisition = "GetCmrCNFRequisition";
            //public static string GetCmrCNFRequisitionById = "GetCmrCNFRequisitionById";
            //public static string GetCmrCNFRequisitionByLCId = "GetCmrCNFRequisitionByLCId";
            //public static string InsertCmrCNFRequisition = "InsertCmrCNFRequisition";
            //public static string UpdateCmrCNFRequisition = "UpdateCmrCNFRequisition";
            //public static string UpdateCmrCNFRequisitionForRemoveDocsUrl = "UpdateCmrCNFRequisitionForRemoveDocsUrl";
            //public static string GetCmrCNFRequisitionSummaryByLc = "GetCmrCNFRequisitionSummaryByLc";
        }
        public static class CmrCNFRequisitionDetail
        {
            //public static string DeleteCmrCNFRequisitionDetailsById = "DeleteCmrCNFRequisitionDetailsById";
            //public static string GetCmrCNFRequisitionDetailsByReuisitionId = "GetCmrCNFRequisitionDetailsByReuisitionId";
            //public static string InsertCmrCNFRequisitionDetails = "InsertCmrCNFRequisitionDetails";
            //public static string UpdateCmrCNFRequisitionDetails = "UpdateCmrCNFRequisitionDetails";
        }
       
        public static class CmrCoverNote
        {
            //public static string DeleteCmrCoverNotesById = "DeleteCmrCoverNotesById";
            //public static string GetCmrCoverNotes = "GetCmrCoverNotes";
            //public static string GetCmrCoverNotesById = "GetCmrCoverNotesById";
            //public static string GetCmrCoverNotesBySCId = "GetCmrCoverNotesBySCId";
            //public static string GetCmrCoverNotesList = "GetCmrCoverNotesList";
            //public static string GetCmrCoverNotesSalesContactList = "GetCmrCoverNotesSalesContactList";
            //public static string InsertCmrCoverNotes = "InsertCmrCoverNotes";
            //public static string UpdateCmrCoverNotes = "UpdateCmrCoverNotes";
            //public static string UpdateCmrCoverNotesForRemoveDocsUrl = "UpdateCmrCoverNotesForRemoveDocsUrl";
        }
        public static class CmrCoverNotesDocument
        {
            //public static string DeleteCmrCoverNotesDocumentsById = "DeleteCmrCoverNotesDocumentsById";
            //public static string InsertCmrCoverNotesDocuments = "InsertCmrCoverNotesDocuments";
            //public static string UpdateCmrCoverNotesDocuments = "UpdateCmrCoverNotesDocuments";
        }
        public static class CmrInspectionCharge
        {
            //public static string DeleteCmrInspectionChargesById = "DeleteCmrInspectionChargesById";
            //public static string GetCmrInspectionCharges = "GetCmrInspectionCharges";
            //public static string GetCmrInspectionChargesById = "GetCmrInspectionChargesById";
            //public static string InsertCmrInspectionCharges = "InsertCmrInspectionCharges";
            //public static string UpdateCmrInspectionCharges = "UpdateCmrInspectionCharges";
        }
        public static class CmrInspection
        {
            //public static string DeleteCmrInspectionsById = "DeleteCmrInspectionsById";
            //public static string GetCmrInspections = "GetCmrInspections";
            //public static string GetCmrInspectionsById = "GetCmrInspectionsById";
            //public static string GetCmrInspectionSummaryByLCId = "GetCmrInspectionSummaryByLCId";
            //public static string InsertCmrInspections = "InsertCmrInspections";
            //public static string UpdateCmrInspections = "UpdateCmrInspections";
            //public static string UpdateCmrInspectionForRemoveDocsUrl = "UpdateCmrInspectionForRemoveDocsUrl";
        }
        public static class CmrInsurenceCompany
        {
            //public static string DeleteCmrInsurenceCompaniesById = "DeleteCmrInsurenceCompaniesById";
            //public static string GetCmrInsurenceCompanies = "GetCmrInsurenceCompanies";
            //public static string GetCmrInsurenceCompaniesById = "GetCmrInsurenceCompaniesById";
            //public static string GetCmrInsurenceCompaniesList = "GetCmrInsurenceCompaniesList";
            //public static string InsertCmrInsurenceCompanies = "InsertCmrInsurenceCompanies";
            //public static string UpdateCmrInsurenceCompanies = "UpdateCmrInsurenceCompanies";
            //public static string UpdateCmrInsurenceCompaniesCancelStatus = "UpdateCmrInsurenceCompaniesCancelStatus";
            //public static string CancelCmrInsurenceCompanies = "CancelCmrInsurenceCompanies";
        }
        public static class CmrLCDetail
        {
            //public static string DeleteCmrLCDetailsById = "DeleteCmrLCDetailsById";
            //public static string GetCmrLCDetailsByCmrLCId = "GetCmrLCDetailsByCmrLCId";
            //public static string GetCmrLCDetailsByLCId = "GetCmrLCDetailsByLCId";
            //public static string InsertCmrLCDetails = "InsertCmrLCDetails";
            //public static string UpdateCmrLCDetails = "UpdateCmrLCDetails";
        }
        public static class CmrLCDocument
        {
            //public static string DeleteCmrLcDocumentsById = "DeleteCmrLcDocumentsById";
            //public static string GetCmrLCDocumentsById = "GetCmrLCDocumentsById";
            //public static string GetCmrLCDocumentsByLCId = "GetCmrLCDocumentsByLCId";
            //public static string InsertCmrLcDocuments = "InsertCmrLcDocuments";
            //public static string UpdateCmrLcDocuments = "UpdateCmrLcDocuments";
        }
        public static class CmrLC
        {
            //public static string DeleteCmrLCsById = "DeleteCmrLCsById";
            //public static string GetCmrLCs = "GetCmrLCs";
            //public static string GetCmrLCsByProformaInvoiceId = "GetCmrLCsByProformaInvoiceId";
            //public static string InsertCmrLCs = "InsertCmrLCs";
            //public static string UpdateCmrLCs = "UpdateCmrLCs";
            //public static string GetCmrLcByLCId = "GetCmrLcByLCId";
            //public static string GetCmrLCItemQuantityByItemId = "GetCmrLCItemQuantityByItemId";
            //public static string GetCmrLCItemQuantityByLCDetailsId = "GetCmrLCItemQuantityByLCDetailsId";
            //public static string GetCmrLCBySalesContactId = "GetCmrLCBySalesContactId";
        }
        public static class CmrLCType
        {
            //public static string DeleteCmrLCTypesById = "DeleteCmrLCTypesById";
            //public static string GetCmrLCTypes = "GetCmrLCTypes";
            //public static string GetCmrLCTypesById = "GetCmrLCTypesById";
            //public static string InsertCmrLCTypes = "InsertCmrLCTypes";
            //public static string UpdateCmrLCTypes = "UpdateCmrLCTypes";
        }
        public static class CmrProformaInvoice
        {
            //public static string DeleteCmrProformaInvoicesById = "DeleteCmrProformaInvoicesById";
            //public static string GetCmrProformaInvoiceLCBySCId = "GetCmrProformaInvoiceLCBySCId";
            //public static string GetCmrProformaInvoicesById = "GetCmrProformaInvoicesById";
            //public static string GetCmrProformaInvoicesBySCId = "GetCmrProformaInvoicesBySCId";
            //public static string GetCmrProformaInvoicesList = "GetCmrProformaInvoicesList";
            //public static string UpdateCmrProformaInvoiceForRemoveDocsUrl = "UpdateCmrProformaInvoiceForRemoveDocsUrl";
            //public static string UpdateCmrProformaInvoices = "UpdateCmrProformaInvoices";
            //public static string InsertCmrProformaInvoices = "InsertCmrProformaInvoices";
        }
        public static class CmrSalesContactDetail
        {
            //public static string DeleteCmrSalesContactDetailsById = "DeleteCmrSalesContactDetailsById";
            //public static string GetCmrSalesContactDetailsById = "GetCmrSalesContactDetailsById";
            //public static string GetCmrSalesContactDetailsBySCId = "GetCmrSalesContactDetailsBySCId";
            //public static string GetCmrSalesContactDetailsBySCIdForLC = "GetCmrSalesContactDetailsBySCIdForLC";
            //public static string GetCmrSalesContactDetailsList = "GetCmrSalesContactDetailsList";
            //public static string InsertCmrSalesContactDetails = "InsertCmrSalesContactDetails";
            //public static string UpdateCmrSalesContactDetails = "UpdateCmrSalesContactDetails";
        }
        public static class CmrSalesContactDocument
        {
            //public static string DeleteCmrSalesContactDocumentsById = "DeleteCmrSalesContactDocumentsById";
            //public static string GetCmrSalesContactDocuments = "GetCmrSalesContactDocuments";
            //public static string GetCmrSalesContactDocumentsById = "GetCmrSalesContactDocumentsById";
            //public static string GetCmrSalesContactDocumentsByProformaInvoiceId = "GetCmrSalesContactDocumentsByProformaInvoiceId";
            //public static string GetCmrSalesContactDocumentsByRFQNo = "GetCmrSalesContactDocumentsByRFQNo";
            //public static string InsertCmrSalesContactDocuments = "InsertCmrSalesContactDocuments";
            //public static string UpdateCmrSalesContactDocuments = "UpdateCmrSalesContactDocuments";
        }
        public static class CmrSalesContact
        {
            //public static string DeleteCmrSalesContactsById = "DeleteCmrSalesContactsById";
            //public static string GetCmrSalesContacts = "GetCmrSalesContacts";
            //public static string GetCmrSalesContactsById = "GetCmrSalesContactsById";
            //public static string GetCmrSalesContactsByRFQNo = "GetCmrSalesContactsByRFQNo";
            //public static string GetCmrSalesContactsList = "GetCmrSalesContactsList";
            //public static string InsertCmrSalesContacts = "InsertCmrSalesContacts";
            //public static string UpdateCmrSalesContacts = "UpdateCmrSalesContacts";
            //public static string UpdateCmrSalesContactsForRemoveDocsUrl = "UpdateCmrSalesContactsForRemoveDocsUrl";
            //public static string GetCmrSalesContactListForAmendment = "GetCmrSalesContactListForAmendment";
            //public static string GetCmrSalesContactList = "GetCmrSalesContactList";
        }

        #endregion

        //#region Tender

        //public static class TndAwardedTenderGuarantee
        //{
        //    public static string DeleteTndAwardedTenderGuaranteesByTndSubmittedTenderId = "DeleteTndAwardedTenderGuaranteesByTndSubmittedTenderId";
        //    public static string GetTndAwardedTenderGuaranteesById = "GetTndAwardedTenderGuaranteesById";
        //    public static string GetTndAwardedTenderGuaranteesBySubmitId = "GetTndAwardedTenderGuaranteesBySubmitId";
        //    public static string GetTndAwardedTenderGuaranteesByTenderId = "GetTndAwardedTenderGuaranteesByTenderId";
        //    public static string InsertTndAwardedTenderGuarantees = "InsertTndAwardedTenderGuarantees";
        //    public static string UpdateTndAwardedTenderGuarantees = "UpdateTndAwardedTenderGuarantees";

        //}
        //public static class TndBidOpening
        //{
        //    public static string DeleteTndBidOpeningsById = "DeleteTndBidOpeningsById";
        //    public static string GetTndBidOpenings = "GetTndBidOpenings";
        //    public static string GetTndBidOpeningsById = "GetTndBidOpeningsById";
        //    public static string GetTndBidOpeningsBySubmittedTenderId = "GetTndBidOpeningsBySubmittedTenderId";
        //    public static string GetTndBidOpeningsByTenderId = "GetTndBidOpeningsByTenderId";
        //    public static string GetTndBoBiddersByTndBidOpeningId = "GetTndBoBiddersByTndBidOpeningId";
        //    public static string InsertTndBidOpenings = "InsertTndBidOpenings";
        //    public static string UpdateTndBidOpenings = "UpdateTndBidOpenings";
        //}
        //public static class TndBoBidder
        //{
        //    public static string DeleteTndBoBiddersByBoId = "DeleteTndBoBiddersByBoId";
        //    public static string DeleteTndBoBiddersByTenderId = "DeleteTndBoBiddersByTenderId";
        //    public static string GetTndBoBidders = "GetTndBoBidders";
        //    public static string GetTndBoBiddersById = "GetTndBoBiddersById";
        //    public static string GetTndBoBiddersByTenderId = "GetTndBoBiddersByTenderId";
        //    public static string GetTndBoBiddersByTndBidOpeningId = "GetTndBoBiddersByTndBidOpeningId";
        //    public static string InsertTndBoBidders = "InsertTndBoBidders";
        //    public static string UpdateTndBoBidders = "UpdateTndBoBidders";
        //}
        //public static class TndBOQ
        //{
        //    public static string DeleteTndBOQsById = "DeleteTndBOQsById";
        //    public static string GetTndBOQs = "GetTndBOQs";
        //    public static string GetTndBOQsById = "GetTndBOQsById";
        //    public static string GetTndBOQsByName = "GetTndBOQsByName";
        //    public static string GetTndBOQsByPackageId = "GetTndBOQsByPackageId";
        //    public static string GetTndBOQsByPackageIdOnlyLastLevel = "GetTndBOQsByPackageIdOnlyLastLevel";
        //    public static string GetTndBOQsByTenderId = "GetTndBOQsByTenderId";
        //    public static string GetTndBOQsIsLastLevelByName = "GetTndBOQsIsLastLevelByName";
        //    public static string GetTndBOQsIsLastLevelByParentId = "GetTndBOQsIsLastLevelByParentId";
        //    public static string InsertTndBOQs = "InsertTndBOQs";
        //    public static string UpdateTndBOQs = "UpdateTndBOQs";
        //    public static string InsertTndBOQDetails = "InsertTndBOQDetails";
        //    public static string DeleteTndBOQDetails = "DeleteTndBOQDetails";
        //}
        //public static class TndDirectCost
        //{
        //    public static string DeleteTndDirectCostsById = "DeleteTndDirectCostsById";
        //    public static string GetTndDirectCosts = "GetTndDirectCosts";
        //    public static string GetTndDirectCostsByBOQId = "GetTndDirectCostsByBOQId";
        //    public static string InsertTndDirectCosts = "InsertTndDirectCosts";
        //    public static string UpdateTndDirectCosts = "UpdateTndDirectCosts";
        //}
        //public static class TndEarnestMoney
        //{
        //    public static string DeleteTndEarnestMoneyByTndTenderId = "DeleteTndEarnestMoneyByTndTenderId";
        //    public static string GetTndEarnestMoney = "GetTndEarnestMoney";
        //    public static string GetTndEarnestMoneyById = "GetTndEarnestMoneyById";
        //    public static string GetTndEarnestMoneyByTndTenderId = "GetTndEarnestMoneyByTndTenderId";
        //    public static string InsertTndEarnestMoney = "InsertTndEarnestMoney";
        //    public static string UpdateTndEarnestMoney = "UpdateTndEarnestMoney";
        //}
        //public static class TndGuaranteeTypeProperty
        //{
        //    public static string DeleteTndGuaranteeTypePropertiesById = "DeleteTndGuaranteeTypePropertiesById";
        //    public static string GetTndGuaranteeTypeProperties = "GetTndGuaranteeTypeProperties";
        //    public static string GetTndGuaranteeTypePropertiesById = "GetTndGuaranteeTypePropertiesById";
        //    public static string GetTndGuaranteeTypePropertiesByTndGuaranteeTypeId = "GetTndGuaranteeTypePropertiesByTndGuaranteeTypeId";
        //    public static string InsertTndGuaranteeTypeProperties = "InsertTndGuaranteeTypeProperties";
        //    public static string UpdateTndGuaranteeTypeProperties = "UpdateTndGuaranteeTypeProperties";
        //}
        //public static class TndGuaranteeType
        //{
        //    public static string DeleteTndGuaranteeTypesById = "DeleteTndGuaranteeTypesById";
        //    public static string GetTndGuaranteeTypes = "GetTndGuaranteeTypes";
        //    public static string GetTndGuaranteeTypesById = "GetTndGuaranteeTypesById";
        //    public static string InsertTndGuaranteeTypes = "InsertTndGuaranteeTypes";
        //    public static string UpdateTndGuaranteeTypes = "UpdateTndGuaranteeTypes";
        //}
        //public static class TndInDirectCostHead
        //{
        //    public static string DeleteTndInDirectCostHeadsById = "DeleteTndInDirectCostHeadsById";
        //    public static string GetTndInDirectCostHeads = "GetTndInDirectCostHeads";
        //    public static string GetTndInDirectCostHeadsById = "GetTndInDirectCostHeadsById";
        //    public static string UpdateTndInDirectCostHeads = "UpdateTndInDirectCostHeads";
        //}
        //public static class TndInDirectCost
        //{
        //    public static string DeleteTndInDirectCostHeadsById = "DeleteTndInDirectCostHeadsById";
        //    public static string DeleteTndInDirectCostsById = "DeleteTndInDirectCostsById";
        //    public static string GetTndInDirectCostHeads = "GetTndInDirectCostHeads";
        //    public static string GetTndInDirectCostHeadsById = "GetTndInDirectCostHeadsById";
        //    public static string GetTndInDirectCosts = "GetTndInDirectCosts";
        //    public static string GetTndInDirectCostsByBOQId = "GetTndInDirectCostsByBOQId";
        //    public static string GetTndInDirectCostsById = "GetTndInDirectCostsById";
        //    public static string InsertTndInDirectCosts = "InsertTndInDirectCosts";
        //    public static string UpdateTndInDirectCostHeads = "UpdateTndInDirectCostHeads";
        //    public static string UpdateTndInDirectCosts = "UpdateTndInDirectCosts";
        //}
        //public static class TndItemMapping
        //{
        //    public static string InsertTndItemMapping = "InsertTndItemMapping";
        //}
        //public static class TndItem
        //{
        //    public static string DeleteTndItemsByCode = "DeleteTndItemsByCode";
        //    public static string GetTndItemsAnFCOAIdByItemId = "GetTndItemsAnFCOAIdByItemId";
        //    public static string GetTndItemsByCmnCompanyId = "GetTndItemsByCmnCompanyId";
        //    public static string GetTndItemsByCode = "GetTndItemsByCode";
        //    public static string GetTndItemsById = "GetTndItemsById";
        //    public static string GetTndItemsForCostOnly = "GetTndItemsForCostOnly";
        //    public static string GetTndItemsOnly = "GetTndItemsOnly";
        //    public static string GetTndItemsParents = "GetTndItemsParents";
        //    public static string GetTndItemsRootOnly = "GetTndItemsRootOnly";
        //    public static string InsertTndItems = "InsertTndItems";
        //    public static string UpdateTndItemsNormal = "UpdateTndItemsNormal";
        //    public static string UpdateTndItemspCode = "UpdateTndItemspCode";
        //    public static string GetTndItemGroups = "GetTndItemGroups";
        //    public static string GenerateTndItemCode = "GenerateTndItemCode";
        //    public static string GetAllInvItemsTender = "GetAllInvItemsTender";
        //    public static string InsertTndItemProperties = "InsertTndItemProperties";
        //}
        //public static class TndPackage
        //{
        //    public static string DeleteTndPackagesById = "DeleteTndPackagesById";
        //    public static string GetTndPackageIdByName = "GetTndPackageIdByName";
        //    public static string GetTndPackageParentIdByName = "GetTndPackageParentIdByName";
        //    public static string GetTndPackages = "GetTndPackages";
        //    public static string GetTndPackagesById = "GetTndPackagesById";
        //    public static string GetTndPackagesByTenderId = "GetTndPackagesByTenderId";
        //    public static string InsertTndPackages = "InsertTndPackages";
        //    public static string UpdateTndPackages = "UpdateTndPackages";
        //}
        //public static class TndParticipant
        //{
        //    public static string DeleteTndParticipantsById = "DeleteTndParticipantsById";
        //    public static string GetAllTndParticipants = "GetAllTndParticipants";
        //    public static string GetTndParticipantsByParticipantTypeId = "GetTndParticipantsByParticipantTypeId";
        //    public static string GetAllTndParticipantsByParticipantTypeGrd = "GetAllTndParticipantsByParticipantTypeGrd";
        //    public static string GetTndParticipantsById = "GetTndParticipantsById";
        //    public static string InsertTndParticipants = "InsertTndParticipants";
        //    public static string UpdateTndParticipants = "UpdateTndParticipants";
        //}
        //public static class TndParticipantType
        //{
        //    public static string GetAllTndPartcipantType = "GetAllTndPartcipantType";
        //}
        //public static class TndProcuredTender
        //{
        //    public static string DeleteTndProcuredTendersById = "DeleteTndProcuredTendersById";
        //    public static string GetAllTndProcuredTenders = "GetAllTndProcuredTenders";
        //    public static string GetAllTndProcuredTendersByTenderID = "GetAllTndProcuredTendersByTenderID";
        //    public static string GetTndProcuredTendersById = "GetTndProcuredTendersById";
        //    public static string InsertTndProcuredTenders = "InsertTndProcuredTenders";
        //    public static string UpdateTndProcuredTenders = "UpdateTndProcuredTenders";
        //}
        //public static class TndSource
        //{
        //    public static string DeleteTndSourcesById = "DeleteTndSourcesById";
        //    public static string GetAllTndSources = "GetAllTndSources";
        //    public static string GetTndSourcesById = "GetTndSourcesById";
        //    public static string InsertTndSources = "InsertTndSources";
        //    public static string UpdateTndSources = "UpdateTndSources";
        //}
        //public static class TndSubmittedTender
        //{
        //    public static string DeleteTndAwardedTenderGuaranteesByTndSubmittedTenderId = "DeleteTndAwardedTenderGuaranteesByTndSubmittedTenderId";
        //    public static string DeleteTndSubmittedTendersById = "DeleteTndSubmittedTendersById";
        //    public static string GetAllTndSubmittedTenders = "GetAllTndSubmittedTenders";
        //    public static string GetAllTndSubmittedTendersByTenderID = "GetAllTndSubmittedTendersByTenderID";
        //    public static string GetAllTndSubmittedTendersSubmitList = "GetAllTndSubmittedTendersSubmitList";
        //    public static string GetTndSubmittedTendersById = "GetTndSubmittedTendersById";
        //    public static string InsertTndSubmittedTenders = "InsertTndSubmittedTenders";
        //    public static string UpdateTndSubmittedTenders = "UpdateTndSubmittedTenders";
        //}
        //public static class TndTenderMethodType
        //{
        //    public static string DeleteTndTenderMethodTypesById = "DeleteTndTenderMethodTypesById";
        //    public static string GetAllTndTenderMethodTypes = "GetAllTndTenderMethodTypes";
        //    public static string GetTndTenderMethodTypesById = "GetTndTenderMethodTypesById";
        //    public static string InsertTndTenderMethodTypes = "InsertTndTenderMethodTypes";
        //    public static string UpdateTndTenderMethodTypes = "UpdateTndTenderMethodTypes";
        //}
        //public static class TndTenderParticipant
        //{
        //    public static string DeleteTndTenderParticipantsByTndTenderIdAndCmnDocumentTypeId = "DeleteTndTenderParticipantsByTndTenderIdAndCmnDocumentTypeId";
        //    public static string GetAllTndTenderParticipantDateOFAggreByTenderID = "GetAllTndTenderParticipantDateOFAggreByTenderID";
        //    public static string GetAllTndTenderParticipants = "GetAllTndTenderParticipants";
        //    public static string GetAllTndTenderParticipantsByTenderID = "GetAllTndTenderParticipantsByTenderID";
        //    public static string GetTndTenderParticipantsById = "GetTndTenderParticipantsById";
        //    public static string InsertTndTenderParticipants = "InsertTndTenderParticipants";
        //    public static string UpdateTndTenderParticipants = "UpdateTndTenderParticipants";
        //}
        //public static class TndTender
        //{
        //    public static string DeleteTndTendersById = "DeleteTndTendersById";
        //    public static string GetAllTndTenders = "GetAllTndTenders";
        //    public static string GetAllTndTendersByTenderList = "GetAllTndTendersByTenderList";
        //    public static string GetTndTenderPropertyByCode = "GetTndTenderPropertyByCode";
        //    public static string GetTndTendersById = "GetTndTendersById";
        //    public static string InsertTndTenders = "InsertTndTenders";
        //    public static string UpdateTndTenders = "UpdateTndTenders";
        //}
        //public static class TndTenderSource
        //{
        //    public static string DeleteTndTenderSourcesById = "DeleteTndTenderSourcesById";
        //    public static string DeleteTndTenderSourcesbyTndTenderId = "DeleteTndTenderSourcesbyTndTenderId";
        //    public static string GetAllTndTenderSources = "GetAllTndTenderSources";
        //    public static string GetTndTenderSourcesById = "GetTndTenderSourcesById";
        //    public static string GetTndTenderSourcesByTenderId = "GetTndTenderSourcesByTenderId";
        //    public static string InsertTndTenderSources = "InsertTndTenderSources";
        //    public static string UpdateTndTenderSources = "UpdateTndTenderSources";
        //    public static string UpdateTndTenderSourcesByTenderId = "UpdateTndTenderSourcesByTenderId";
        //}
        //public static class TndTenderSourceType
        //{
        //    public static string DeleteTndTenderSourceTypesById = "DeleteTndTenderSourceTypesById";
        //    public static string GetAllTndTenderSourceTypes = "GetAllTndTenderSourceTypes";
        //    public static string GetTndTenderSourceTypesById = "GetTndTenderSourceTypesById";
        //    public static string InsertTndTenderSourceTypes = "InsertTndTenderSourceTypes";
        //    public static string UpdateTndTenderSourceTypes = "UpdateTndTenderSourceTypes";
        //}
        //public static class TndTenderType
        //{
        //    public static string DeleteTndTenderTypesById = "DeleteTndTenderTypesById";
        //    public static string GetAllTndTenderTypes = "GetAllTndTenderTypes";
        //    public static string GetTndTenderTypesById = "GetTndTenderTypesById";
        //    public static string InsertTndTenderTypes = "InsertTndTenderTypes";
        //    public static string UpdateTndTenderTypes = "UpdateTndTenderTypes";
        //}
        //#endregion

        //#region HRM
        //public static class HrmAdvertisement
        //{

        //}
        //public static class HrmApplicantInformation
        //{

        //}
        //public static class HrmAttendance
        //{
        //    public static string DeleteHrmAttendancesByCardNoAndInOutTime = "DeleteHrmAttendancesByCardNoAndInOutTime";
        //    public static string GetCountHrmAttendancesByCardNoAndInOutTime = "GetCountHrmAttendancesByCardNoAndInOutTime";
        //    public static string InsertHrmAttendances = "InsertHrmAttendances";
        //    public static string UpdateHrmAttendancesStatus = "UpdateHrmAttendancesStatus";
        //    public static string InsertManualAttendances = "InsertManualAttendances";
        //    public static string DeleteManualAttendances = "DeleteManualAttendances";
        //}
        //public static class HrmAttendanceTimeSetup
        //{
        //    public static string DeleteHrmAttendanceTimeSetupsById = "DeleteHrmAttendanceTimeSetupsById";
        //    public static string GetHrmAttendanceTimeSetups = "GetHrmAttendanceTimeSetups";
        //    public static string GetHrmAttendanceTimeSetupsById = "GetHrmAttendanceTimeSetupsById";
        //    public static string InsertHrmAttendanceTimeSetups = "InsertHrmAttendanceTimeSetups";
        //    public static string UpdateHrmAttendanceTimeSetups = "UpdateHrmAttendanceTimeSetups";
        //}
        //public static class HrmAttendenceType
        //{
        //    public static string DeleteHrmAttendenceTypesById = "DeleteHrmAttendenceTypesById";
        //    public static string GetHrmAttendenceTypes = "GetHrmAttendenceTypes";
        //    public static string GetHrmAttendenceTypesById = "GetHrmAttendenceTypesById";
        //    public static string InsertHrmAttendenceTypes = "InsertHrmAttendenceTypes";
        //    public static string UpdateHrmAttendenceTypes = "UpdateHrmAttendenceTypes";
        //}
        //public static class HrmCompanyDepartment
        //{
        //    public static string DeleteHrmCompanyDepartmentsByCompanyId = "DeleteHrmCompanyDepartmentsByCompanyId";
        //    public static string DeleteHrmCompanyDepartmentsById = "DeleteHrmCompanyDepartmentsById";
        //    public static string GetHrmCompanyDepartments = "GetHrmCompanyDepartments";
        //    public static string GetHrmCompanyDepartmentsById = "GetHrmCompanyDepartmentsById";
        //    public static string GetHrmCompanyDepartmentsByComIdDeptId = "GetHrmCompanyDepartmentsByComIdDeptId";
        //    public static string InsertHrmCompanyDepartments = "InsertHrmCompanyDepartments";
        //    public static string UpdateHrmCompanyDepartments = "UpdateHrmCompanyDepartments";
        //    public static string GetHrmCompanyDepartmentByCompanyId = "GetHrmCompanyDepartmentByCompanyId";
        //}
        //public static class HrmConfirmationOfService
        //{
        //    public static string DeleteHrmConfirmationOfServicesById = "DeleteHrmConfirmationOfServicesById";
        //    public static string GetHrmConfirmationOfService = "GetHrmConfirmationOfService";
        //    public static string GetHrmConfirmationOfServiceById = "GetHrmConfirmationOfServiceById";
        //    public static string InsertHrmConfirmationOfService = "InsertHrmConfirmationOfService";
        //    public static string UpdateHrmConfirmationOfService = "UpdateHrmConfirmationOfService";
        //    public static string GetHrmConfirmationServiceLastCodeByCompanyId = "GetHrmConfirmationServiceLastCodeByCompanyId";
        //}
        //public static class HrmDepartmentApprovalProcess
        //{
        //    public static string DeleteHrmDepartmentApprovalProcessesById = "DeleteHrmDepartmentApprovalProcessesById";
        //    public static string InsertHrmDepartmentApprovalProcesses = "InsertHrmDepartmentApprovalProcesses";
        //    public static string GetHrmDepartmentApprovalProcessesByComDeptId = "GetHrmDepartmentApprovalProcessesByComDeptId";
        //    public static string GetHrmApprovalProcessByComDeptAndProcessId = "GetHrmApprovalProcessByComDeptAndProcessId";
        //}
        //public static class HrmDepartmentApprovalProcessLevel
        //{
        //    public static string DeleteHrmDepartmentApprovalProcessLevelsById = "DeleteHrmDepartmentApprovalProcessLevelsById";
        //    public static string InsertHrmDepartmentApprovalProcessLevels = "InsertHrmDepartmentApprovalProcessLevels";
        //    public static string UpdateHrmDepartmentApprovalProcessLevel = "UpdateHrmDepartmentApprovalProcessLevel";
        //    public static string GetHrmDepartmentApprovalProcessLevelsByDeptApprovalProcessId = "GetHrmDepartmentApprovalProcessLevelsByDeptApprovalProcessId";
        //    public static string GetHrmDepartmentApprovalProcessLevelsById = "GetHrmDepartmentApprovalProcessLevelsById";
        //    public static string GetCmnApprovalProcessLevelByProcessId = "GetCmnApprovalProcessLevelByProcessId";
        //}
        //public static class HrmDepartment
        //{
        //    public static string DeleteHrmDepartmentsById = "DeleteHrmDepartmentsById";
        //    public static string GetHrmDepartments = "GetHrmDepartments";
        //    public static string GetHrmDepartmentsByCmnCompanyId = "GetHrmDepartmentsByCmnCompanyId";
        //    public static string GetHrmDepartmentsById = "GetHrmDepartmentsById";
        //    public static string InsertHrmDepartments = "InsertHrmDepartments";
        //    public static string UpdateHrmDepartments = "UpdateHrmDepartments";
        //    public static string GetHrmDepartmentsApprovalByCompanyId = "GetHrmDepartmentsApprovalByCompanyId";
        //    public static string GetHrmDepartmentByCategory = "GetHrmDepartmentByCategory";
        //    public static string GetHrmDepartmentMappingByCompanyId = "GetHrmDepartmentMappingByCompanyId";
        //}
        //public static class HrmDepot
        //{
        //    public static string DeleteHrmDepotsById = "DeleteHrmDepotsById";
        //    public static string GetHrmDepots = "GetHrmDepots";
        //    public static string GetHrmDepotsByCompanyId = "GetHrmDepotsByCompanyId";
        //    public static string GetHrmDepotsById = "GetHrmDepotsById";
        //    public static string InsertHrmDepot = "InsertHrmDepot";
        //    public static string UpdateHrmDepot = "UpdateHrmDepot";
        //}
        //public static class HrmDesignationFunctionalStatus
        //{

        //}
        //public static class HrmDesignation
        //{
        //    public static string DeleteHrmDesignationsById = "DeleteHrmDesignationsById";
        //    public static string GetHrmDesignations = "GetHrmDesignations";
        //    public static string GetHrmDesignationsByCmnCompanyId = "GetHrmDesignationsByCmnCompanyId";
        //    public static string GetHrmDesignationsById = "GetHrmDesignationsById";
        //    public static string InsertHrmDesignations = "InsertHrmDesignations";
        //    public static string UpdateHrmDesignations = "UpdateHrmDesignations";
        //    public static string UpdateHrmDesignationsPriority = "UpdateHrmDesignationsPriority";
        //}
        //public static class HrmEducation
        //{
        //    public static string DeleteHrmEducationsById = "DeleteHrmEducationsById";
        //    public static string GetHrmEducations = "GetHrmEducations";
        //    public static string GetHrmEducationsByEmployeeId = "GetHrmEducationsByEmployeeId";
        //    public static string InsertHrmEducations = "InsertHrmEducations";
        //    public static string UpdateHrmEducations = "UpdateHrmEducations";
        //}
        //public static class HrmEmployeeFunctionalStatus
        //{

        //}
        //public static class HrmEmployee
        //{
        //    public static string DeleteHrmEmployeesById = "DeleteHrmEmployeesById";
        //    public static string DeleteHrmEmployeesOnly = "DeleteHrmEmployeesOnly";
        //    public static string GetHrmEmployeesByCompanyAndDepartmentId = "GetHrmEmployeesByCompanyAndDepartmentId";
        //    public static string GetHrmEmployeesByCompanyIdAndDepartmentCategory = "GetHrmEmployeesByCompanyIdAndDepartmentCategory";
        //    public static string GetHrmEmployeesByCompanyId = "GetHrmEmployeesByCompanyId";
        //    public static string GetHrmEmployeesByDepartmentId = "GetHrmEmployeesByDepartmentId";
        //    public static string GetHrmEmployeesByDesignation = "GetHrmEmployeesByDesignation";
        //    public static string GetHrmEmployeesByEmployeeIdNCmnCompanyId = "GetHrmEmployeesByEmployeeIdNCmnCompanyId";
        //    public static string GetHrmEmployeesById = "GetHrmEmployeesById";
        //    public static string GetHrmEmployeesByEmployeeId = "GetHrmEmployeesByEmployeeId";
        //    public static string GetHrmEmployeesByIdforListView = "GetHrmEmployeesByIdforListView";
        //    public static string GetHrmEmployeeSearch = "GetHrmEmployeeSearch";
        //    public static string GetHrmEmployeeSearchforPayroll = "GetHrmEmployeeSearchforPayroll";
        //    public static string GetHrmEmployeesIsExists = "GetHrmEmployeesIsExists";
        //    public static string GetHrmEmployeesList = "GetHrmEmployeesList";
        //    public static string GetHrmEmployeesNameDesignation = "GetHrmEmployeesNameDesignation";
        //    public static string GetHrmEmployeesWithBankByCompanyId = "GetHrmEmployeesWithBankByCompanyId";
        //    public static string InsertHrmEmployees = "InsertHrmEmployees";
        //    public static string UpdateHrmEmployees = "UpdateHrmEmployees";
        //    public static string GetHrmEmployeeForAttendance = "GetHrmEmployeeForAttendance";
        //    public static string GetHrmCCEmployeeId = "GetHrmCCEmployeeId";
        //    public static string GetHrmEmployeeInformation = "GetHrmEmployeeInformation";
        //    public static string GetHrmScheduableEmployess = "GetHrmScheduableEmployess";
        //    public static string GetHrmEmployeeIdByEmployeeId = "GetHrmEmployeeIdByEmployeeId";
        //    public static string GetHrmParentEmployeeByDesignationId = "GetHrmParentEmployeeByDesignationId";
        //    public static string GetHrmChildEmployeeById = "GetHrmChildEmployeeById";
        //    public static string GetHrmPayrollEmployees = "GetHrmPayrollEmployees";
        //    public static string GetHrmEmployeesInfoById = "GetHrmEmployeesInfoById";
        //    public static string GetHrmEmployeesExistsId = "GetHrmEmployeesExistsId";
        //    public static string GetHrmProbationaryEmployees = "GetHrmProbationaryEmployees";
        //    public static string RptHrmEmployeeCV = "RptHrmEmployeeCV";
        //}
        //public static class HrmEmploymentType
        //{
        //    public static string DeleteHrmEmploymentTypeById = "DeleteHrmEmploymentTypeById";
        //    public static string GetHrmEmploymentType = "GetHrmEmploymentType";
        //    public static string GetHrmEmploymentTypeByEmployeeTypeId = "GetHrmEmploymentTypeByEmployeeTypeId";
        //    public static string GetHrmEmploymentTypeById = "GetHrmEmploymentTypeById";
        //    public static string InsertHrmEmploymentType = "InsertHrmEmploymentType";
        //    public static string UpdateHrmEmploymentType = "UpdateHrmEmploymentType";
        //}
        //public static class HrmExamination
        //{
        //    public static string DeleteHrmExaminationsById = "DeleteHrmExaminationsById";
        //    public static string GetHrmExaminations = "GetHrmExaminations";
        //    public static string GetHrmExaminationsById = "GetHrmExaminationsById";
        //    public static string InsertHrmExaminations = "InsertHrmExaminations";
        //    public static string UpdateHrmExaminations = "UpdateHrmExaminations";
        //}
        //public static class HrmExperience
        //{
        //    public static string DeleteHrmExperiencesById = "DeleteHrmExperiencesById";
        //    public static string GetHrmExperiences = "GetHrmExperiences";
        //    public static string GetHrmExperiencesByEmployeeId = "GetHrmExperiencesByEmployeeId";
        //    public static string InsertHrmExperiences = "InsertHrmExperiences";
        //    public static string UpdateHrmExperiences = "UpdateHrmExperiences";
        //}
        //public static class HrmFunctionalStatusRecord
        //{

        //}
        //public static class HrmHoliday
        //{
        //    public static string DeleteHrmHolidaysById = "DeleteHrmHolidaysById";
        //    public static string GetHrmHolidays = "GetHrmHolidays";
        //    public static string GetHrmHolidaysById = "GetHrmHolidaysById";
        //    public static string GetHrmHolidaysOfYearByYearId = "GetHrmHolidaysOfYearByYearId";
        //    public static string InsertHrmHolidays = "InsertHrmHolidays";
        //    public static string UpdateHrmHolidays = "UpdateHrmHolidays";
        //}
        //public static class HrmLanguage
        //{
        //    public static string GetHrmLanguage = "GetHrmLanguage";
        //}
        //public static class HrmLanguageProficiency
        //{
        //    public static string DeleteHrmLanguageProfienciesById = "DeleteHrmLanguageProfienciesById";
        //    public static string GetHrmLanguageProficiencies = "GetHrmLanguageProficiencies";
        //    public static string GetHrmLanguageProficienciesByEmployeeId = "GetHrmLanguageProficienciesByEmployeeId";
        //    public static string InsertHrmLanguageProficiency = "InsertHrmLanguageProficiency";
        //    public static string UpdateHrmLanguageProficiency = "UpdateHrmLanguageProficiency";
        //}
        //public static class HrmLeaveBalance
        //{
        //    public static string InsertOrUpdateHrmLeaveBalances = "InsertOrUpdateHrmLeaveBalances";
        //    public static string GetHrmEmploeeLeaveBalance = "GetHrmEmploeeLeaveBalance";
        //}
        //public static class HrmLeaveDetail
        //{
        //    public static string DeleteHrmLeaveDetailsById = "DeleteHrmLeaveDetailsById";
        //    public static string GetHrmLeaveDetails = "GetHrmLeaveDetails";
        //    public static string GetHrmLeaveDetailsById = "GetHrmLeaveDetailsById";
        //    public static string InsertHrmLeaveDetails = "InsertHrmLeaveDetails";
        //    public static string UpdateHrmLeaveDetails = "UpdateHrmLeaveDetails";
        //    public static string GetHrmEmployeesLeaveInformation = "GetHrmEmployeesLeaveInformation";
        //    public static string GetHrmLeaveForSchedule = "GetHrmLeaveForSchedule";

        //}
        //public static class HrmLeaveType
        //{
        //    public static string DeleteHrmLeaveTypesById = "DeleteHrmLeaveTypesById";
        //    public static string GetHrmLeaveTypeByEmploymentTypeId = "GetHrmLeaveTypeByEmploymentTypeId";
        //    public static string GetHrmLeaveTypeById = "GetHrmLeaveTypeById";
        //    public static string GetHrmLeaveTypesByCmnCompanyId = "GetHrmLeaveTypesByCmnCompanyId";
        //    public static string InsertHrmLeaveType = "InsertHrmLeaveType";
        //    public static string UpdateHrmLeaveType = "UpdateHrmLeaveType";
        //}
        //public static class HrmPicture
        //{
        //    public static string GetHrmPicturesByEmpId = "GetHrmPicturesByEmpId";
        //    public static string GetHrmPicturesByEmployeeId = "GetHrmPicturesByEmployeeId";
        //    public static string InsertHrmPictures = "InsertHrmPictures";
        //    public static string UpdateHrmPictures = "UpdateHrmPictures";
        //}
        //public static class HrmProfessionalMembership
        //{
        //    public static string GetGetHrmProfessionalMembershipsByEmployeeId = "GetGetHrmProfessionalMembershipsByEmployeeId";
        //    public static string GetHrmProfessionalMemberships = "GetHrmProfessionalMemberships";
        //    public static string InsertHrmProfessionalMemberships = "InsertHrmProfessionalMemberships";
        //    public static string UpdateHrmProfMembershipInfo = "UpdateHrmProfMembershipInfo";
        //    public static string DeleteHrmProfMembershipInfosById = "DeleteHrmProfMembershipInfosById";
        //}
        //public static class HrmProfessionalTraning
        //{
        //    public static string DeleteHrmProfessionalTraningBeforeThisCompanyById = "DeleteHrmProfessionalTraningBeforeThisCompanyById";
        //    public static string GetHrmProfessionalTranings = "GetHrmProfessionalTranings";
        //    public static string GetHrmProfessionalTraningsByEmployeeId = "GetHrmProfessionalTraningsByEmployeeId";
        //    public static string InsertHrmProfessionalTranings = "InsertHrmProfessionalTranings";
        //    public static string UpdateHrmProfessionalTranings = "UpdateHrmProfessionalTranings";
        //}
        //public static class HrmPublication
        //{
        //    public static string DeleteHrmPublicationsById = "DeleteHrmPublicationsById";
        //    public static string GetHrmPublications = "GetHrmPublications";
        //    public static string GetHrmPublicationsByEmployeeId = "GetHrmPublicationsByEmployeeId";
        //    public static string InsertHrmPublications = "InsertHrmPublications";
        //    public static string UpdateHrmPublications = "UpdateHrmPublications";
        //}
        //public static class HrmRelationType
        //{
        //    public static string GetHrmRelationType = "GetHrmRelationType";
        //}
        //public static class HrmSalaryBreakdown
        //{
        //    public static string DeleteHrmSalaryBreakdownById = "DeleteHrmSalaryBreakdownById";
        //    public static string GetHrmSalaryBreakdowns = "GetHrmSalaryBreakdowns";
        //    public static string GetHrmSalaryBreakdownsById = "GetHrmSalaryBreakdownsById";
        //    public static string InsertHrmSalaryBreakdown = "InsertHrmSalaryBreakdown";
        //    public static string UpdateHrmSalaryBreakdown = "UpdateHrmSalaryBreakdown";
        //    public static string GetHrmGrossSalry = "GetHrmGrossSalry";
        //}
        //public static class HrmScheduleDetail
        //{
        //    public static string DeleteHrmScheduleDetailsByHrmScheduleId = "DeleteHrmScheduleDetailsByHrmScheduleId";
        //    public static string GetHrmScheduleDetailsByHrmScheduleId = "GetHrmScheduleDetailsByHrmScheduleId";
        //    public static string InsertHrmScheduleDetails = "InsertHrmScheduleDetails";
        //}
        //public static class HrmSchedule
        //{
        //    public static string DeleteHrmSchedulesById = "DeleteHrmSchedulesById";
        //    public static string GetHrmSchedules = "GetHrmSchedules";
        //    public static string GetHrmSchedulesById = "GetHrmSchedulesById";
        //    public static string GetHrmSchedulesByRefNo = "GetHrmSchedulesByRefNo";
        //    public static string InsertHrmSchedules = "InsertHrmSchedules";
        //    public static string UpdateHrmSchedules = "UpdateHrmSchedules";
        //}
        //public static class HrmSection
        //{
        //    public static string DeleteHrmSectionsById = "DeleteHrmSectionsById";
        //    public static string GetHrmSections = "GetHrmSections";
        //    public static string GetHrmSectionsByDepartmentId = "GetHrmSectionsByDepartmentId";
        //    public static string GetHrmSectionsByHrmDepartmentId = "GetHrmSectionsByHrmDepartmentId";
        //    public static string GetHrmSectionsById = "GetHrmSectionsById";
        //    public static string InsertHrmSections = "InsertHrmSections";
        //    public static string UpdateHrmSections = "UpdateHrmSections";

        //}
        //public static class HrmSeminarWorkshop
        //{
        //    public static string DeleteHrmSeminarWorkshopsById = "DeleteHrmSeminarWorkshopsById";
        //    public static string GetHrmSeminarWorkshops = "GetHrmSeminarWorkshops";
        //    public static string GetHrmSeminarWorkshopsByEmployeeId = "GetHrmSeminarWorkshopsByEmployeeId";
        //    public static string InsertHrmSeminarWorkshops = "InsertHrmSeminarWorkshops";
        //    public static string UpdateHrmSeminarWorkshops = "UpdateHrmSeminarWorkshops";
        //}
        //public static class HrmShift
        //{
        //    public static string DeleteHrmShiftsById = "DeleteHrmShiftsById";
        //    public static string GetHrmShiftsByCmnCompanyId = "GetHrmShiftsByCmnCompanyId";
        //    public static string GetHrmShiftsByCmnCompanyIdAndIsSchedulable = "GetHrmShiftsByCmnCompanyIdAndIsSchedulable";
        //    public static string GetHrmShiftsById = "GetHrmShiftsById";
        //    public static string GetHrmShiftsByName = "GetHrmShiftsByName";
        //    public static string InsertHrmShifts = "InsertHrmShifts";
        //    public static string UpdateHrmShifts = "UpdateHrmShifts";
        //    public static string GetHrmNonRestoreShifts = "GetHrmNonRestoreShifts";
        //}
        //public static class HrmSignatory
        //{
        //    public static string GetHrmSignatories = "GetHrmSignatories";
        //}
        //public static class HrmSkillStatus
        //{

        //}
        //public static class HrmSubSection
        //{
        //    public static string DeleteHrmSubSectionsById = "DeleteHrmSubSectionsById";
        //    public static string GetHrmSubSections = "GetHrmSubSections";
        //    public static string GetHrmSubSectionsById = "GetHrmSubSectionsById";
        //    public static string GetHrmSubSectionsBySectionId = "GetHrmSubSectionsBySectionId";
        //    public static string InsertHrmSubSections = "InsertHrmSubSections";
        //    public static string UpdateHrmSubSections = "UpdateHrmSubSections";
        //}
        //public static class HrmTraining
        //{

        //}
        //public static class HrmTransactionHistory
        //{

        //}
        //#endregion

        //#region Payroll

        //public static class PrlPayroll
        //{
        //    public static string InsertPrlPayrolls = "InsertPrlPayrolls";
        //}
        //public static class PrlSalaryEntry
        //{
        //    public static string DeletePrlSalaryEntriesById = "DeletePrlSalaryEntriesById";
        //    public static string GetPrlSalaryEntries = "GetPrlSalaryEntries";
        //    public static string InsertPrlSalaryEntries = "InsertPrlSalaryEntries";
        //}
        //public static class PrlSalaryHead
        //{
        //    public static string DeletePrlSalaryHeadsById = "DeletePrlSalaryHeadsById";
        //    public static string GetPrlSalaryHeadById = "GetPrlSalaryHeadById";
        //    public static string GetPrlSalaryHeads = "GetPrlSalaryHeads";
        //    public static string InsertPrlSalaryHead = "InsertPrlSalaryHead";
        //    public static string UpdatePrlSalaryHeads = "UpdatePrlSalaryHeads";
        //}
        //#endregion

        //#region Sales

        //public static class SlsChallanDetail
        //{
        //    public static string DeleteSlsChallanDetailsById = "DeleteSlsChallanDetailsById";
        //    public static string GetSlsChallanDetailByIdForReturn = "GetSlsChallanDetailByIdForReturn";
        //    public static string InsertSlsChallanDetails = "InsertSlsChallanDetails";
        //    public static string UpdateSlsChallanDetails = "UpdateSlsChallanDetails";
        //    public static string GetSlsDeliveryChallanDetailInfoBySoNo = "GetSlsDeliveryChallanDetailInfoBySoNo";
        //    public static string GetSlsDeliveryChallanDetailByChallanId = "GetSlsDeliveryChallanDetailByChallanId";
        //}
        //public static class SlsChallan
        //{
        //    public static string DeleteSlsChallansById = "DeleteSlsChallansById";
        //    public static string GetSlsChallanInfoByChallanId = "GetSlsChallanInfoByChallanId";
        //    public static string GetSlsChallanInfoBySOId = "GetSlsChallanInfoBySOId";
        //    public static string GetSlsChallanIsExist = "GetSlsChallanIsExist";
        //    public static string GetSlsChallanLastCode = "GetSlsChallanLastCode";
        //    public static string GetSlsChallanNoForReturn = "GetSlsChallanNoForReturn";
        //    public static string InsertSlsChallan = "InsertSlsChallan";
        //    public static string UpdateSlsChallans = "UpdateSlsChallans";
        //    public static string UpdateSlsChallansPrintStatus = "UpdateSlsChallansPrintStatus";
        //    public static string GetSlsDeliveryChallanSearchByChallanNo = "GetSlsDeliveryChallanSearchByChallanNo";
        //    public static string GetSlsDeliveryChallanSearch = "GetSlsDeliveryChallanSearch";
        //    public static string GetSlsGatePassLastCodeByCompanyId = "GetSlsGatePassLastCodeByCompanyId";
        //}
        //public static class SlsCurrentStock
        //{

        //}
        //public static class SlsDamage
        //{
        //    public static string DeleteSlsDamagesById = "DeleteSlsDamagesById";
        //    public static string GetSlsDamageLastCode = "GetSlsDamageLastCode";
        //    public static string GetSlsDamageById = "GetSlsDamageById";
        //    public static string GetSlsDamageSearch = "GetSlsDamageSearch";
        //    public static string InsertSlsDamages = "InsertSlsDamages";
        //    public static string UpdateSlsDamages = "UpdateSlsDamages";
        //}
        //public static class SlsDamageType
        //{
        //    public static string DeleteSlsDamageTypesById = "DeleteSlsDamageTypesById";
        //    public static string GetSlsDamageTypeById = "GetSlsDamageTypeById";
        //    public static string GetSlsDamageTypes = "GetSlsDamageTypes";
        //    public static string InsertSlsDamageType = "InsertSlsDamageType";
        //    public static string UpdateSlsDamageType = "UpdateSlsDamageType";
        //}
        //public static class SlsEmployeesMapping
        //{
        //    public static string InsertSlsEmployeesMapping = "InsertSlsEmployeesMapping";
        //    public static string UpdateSlsEmployeesMapping = "UpdateSlsEmployeesMapping";
        //    public static string GetSlsMappedEmployeed = "GetSlsMappedEmployeed";
        //    public static string DeleteSlsEmployeeMappingsBySrHrmEmployeeIdNHrmDesignationId = "DeleteSlsEmployeeMappingsBySrHrmEmployeeIdNHrmDesignationId";
        //    public static string GetSlsRepresentativeName = "GetSlsRepresentativeName";
        //    public static string GetSlsReportingManagerByEmployee = "GetSlsReportingManagerByEmployee";
        //}
        //public static class SlsInvoiceDetail
        //{
        //    public static string InsertSlsInvoiceDetails = "InsertSlsInvoiceDetails";
        //    public static string UpdateSlsInvoiceDetails = "UpdateSlsInvoiceDetails";
        //}
        //public static class SlsInvoice
        //{
        //    public static string GetSlsInvoiceById = "GetSlsInvoiceById";
        //    public static string GetSlsInvoiceLastCode = "GetSlsInvoiceLastCode";
        //    public static string GetSlsInvoiceSearch = "GetSlsInvoiceSearch";
        //    public static string InsertSlsInvoices = "InsertSlsInvoices";
        //    public static string UpdateSlsInvoices = "UpdateSlsInvoices";
        //    public static string UpdateSlsInvoicesPrintStatus = "UpdateSlsInvoicesPrintStatus";
        //}
        //public static class SlsProductPrice
        //{
        //    public static string DeleteSlsProductPricesById = "DeleteSlsProductPricesById";
        //    public static string GetSlsProductPriceParentId = "GetSlsProductPriceParentId";
        //    public static string InsertSlsProductPrices = "InsertSlsProductPrices";
        //    public static string UpdateSlsProductPrices = "UpdateSlsProductPrices";
        //    public static string GetSlsProductPreviousDate = "GetSlsProductPreviousDate";
        //}
        //public static class SlsProductReceive
        //{
        //    public static string GetSlsProductReceiveById = "GetSlsProductReceiveById";
        //    public static string GetSlsProductReceiveLastCode = "GetSlsProductReceiveLastCode";
        //    public static string GetSlsProductReceiveSearch = "GetSlsProductReceiveSearch";
        //    public static string InsertSlsProductReceives = "InsertSlsProductReceives";
        //    public static string UpdateSlsProductReceives = "UpdateSlsProductReceives";
        //    public static string GetSlsProductRecvInfoByRecvNo = "GetSlsProductRecvInfoByRecvNo";
        //}
        //public static class SlsProductRequisitionDetail
        //{
        //    public static string DeleteSlsProductRequisitionDetailsById = "DeleteSlsProductRequisitionDetailsById";
        //    public static string InsertSlsProductRequisitionDetails = "InsertSlsProductRequisitionDetails";
        //    public static string UpdateSlsProductRequisitionDetails = "UpdateSlsProductRequisitionDetails";
        //    public static string GetSlsProductReqDetailsByReqId = "GetSlsProductReqDetailsByReqId";
        //}
        //public static class SlsProductRequisition
        //{
        //    public static string GetSlsProductRequisitionSearch = "GetSlsProductRequisitionSearch";
        //    public static string InsertSlsProductRequisitions = "InsertSlsProductRequisitions";
        //    public static string UpdateSlsProductRequisitions = "UpdateSlsProductRequisitions";
        //    public static string UpdateSlsProductRequisitionsStatus = "UpdateSlsProductRequisitionsStatus";
        //    public static string GetSlsProductionReqLastCode = "GetSlsProductionReqLastCode";
        //    public static string GetSlsProductReqById = "GetSlsProductReqById";
        //}
        //public static class SlsProductReturn
        //{
        //    public static string InsertSlsProductReturns = "InsertSlsProductReturns";
        //}
        //public static class SlsReturn
        //{
        //    public static string DeleteSlsReturnTypesById = "DeleteSlsReturnTypesById";
        //    public static string GetSlsReturnDetailById = "GetSlsReturnDetailById";
        //    public static string GetSlsReturnLastCode = "GetSlsReturnLastCode";
        //    public static string GetSlsReturnSearch = "GetSlsReturnSearch";
        //    public static string GetSlsReturnTypeById = "GetSlsReturnTypeById";
        //    public static string GetSlsReturnTypes = "GetSlsReturnTypes";
        //    public static string InsertSlsReturns = "InsertSlsReturns";
        //    public static string InsertSlsReturnType = "InsertSlsReturnType";
        //    public static string UpdateSlsReturnType = "UpdateSlsReturnType";
        //}
        //public static class SlsReturnType
        //{
        //    public static string DeleteSlsReturnTypesById = "DeleteSlsReturnTypesById";
        //    public static string GetSlsReturnTypeById = "GetSlsReturnTypeById";
        //    public static string GetSlsReturnTypes = "GetSlsReturnTypes";
        //    public static string InsertSlsReturnType = "InsertSlsReturnType";
        //    public static string UpdateSlsReturnType = "UpdateSlsReturnType";
        //}
        //public static class SlsSDP
        //{
        //    public static string DeleteSlsSDPsById = "DeleteSlsSDPsById";
        //    public static string GetSlsSDPsByCompanyId = "GetSlsSDPsByCompanyId";
        //    public static string GetSlsSDPsParentIdByChildId = "GetSlsSDPsParentIdByChildId";
        //    public static string GetSlsSDPsParentName = "GetSlsSDPsParentName";
        //    public static string InsertSlsSDPs = "InsertSlsSDPs";
        //    public static string UpdateSlsSDPs = "UpdateSlsSDPs";
        //    public static string GetSlsSDPTypeParentBySDPType = "GetSlsSDPTypeParentBySDPType";
        //    public static string GetSlsSDPBySDPType = "GetSlsSDPBySDPType";
        //    public static string GetSlsDepotByCompanyId = "GetSlsDepotByCompanyId";
        //    public static string GetSlsPermittedSDPsByCompanyId = "GetSlsPermittedSDPsByCompanyId";
        //    public static string GetSlsPermittedSDPsBySDPType = "GetSlsPermittedSDPsBySDPType";
        //    public static string GetSlsOutletBySDPId = "GetSlsOutletBySDPId";
        //    public static string GetSlsSDPById = "GetSlsSDPById";
        //}
        //public static class SlsSDPType
        //{
        //    public static string DeleteSlsSDPTypesById = "DeleteSlsSDPTypesById";
        //    public static string GetSlsSDPTypeParentBySDPTypeId = "GetSlsSDPTypeParentBySDPTypeId";
        //    public static string GetSlsSDPTypesByCmnCompanyId = "GetSlsSDPTypesByCmnCompanyId";
        //    public static string GetSlsSDPTypesById = "GetSlsSDPTypesById";
        //    public static string InsertSlsSDPTypes = "InsertSlsSDPTypes";
        //    public static string UpdateSlsSDPTypes = "UpdateSlsSDPTypes";
        //    public static string GetSlsSDPointNameByName = "GetSlsSDPointNameByName";
        //}
        //public static class SlsSO
        //{
        //    public static string DeleteSlsSOsById = "DeleteSlsSOsById";
        //    public static string GetSlsSOApprovedQtyBySRNo = "GetSlsSOApprovedQtyBySRNo";
        //    public static string GetSlsSOById = "GetSlsSOById";
        //    public static string GetSlsSODateAndSDPointNameBySOId = "GetSlsSODateAndSDPointNameBySOId";
        //    public static string GetSlsSOForInvoice = "GetSlsSOForInvoice";
        //    public static string GetSlsSOListByDate = "GetSlsSOListByDate";
        //    public static string GetSlsSONoByCompanyId = "GetSlsSONoByCompanyId";
        //    public static string GetSlsSOSearch = "GetSlsSOSearch";
        //    public static string GetSlsSOSearchBySONo = "GetSlsSOSearchBySONo";
        //    public static string GetSlsSOsLastCodeByCompanyId = "GetSlsSOsLastCodeByCompanyId";
        //    public static string InsertSlsSOs = "InsertSlsSOs";
        //    public static string UpdateSlsSOCancelStatus = "UpdateSlsSOCancelStatus";
        //    public static string UpdateSlsSOPrintStatus = "UpdateSlsSOPrintStatus";
        //    public static string UpdateSlsSOs = "UpdateSlsSOs";
        //    public static string UpdateSlsSOStatus = "UpdateSlsSOStatus";
        //    public static string GetSlsDeliveryChallanNoBySoNo = "GetSlsDeliveryChallanNoBySoNo";
        //    public static string GetSlsDeliveryChallanInfoBySoNo = "GetSlsDeliveryChallanInfoBySoNo";
        //}
        //public static class SlsSOsDetail
        //{
        //    public static string DeleteSlsSOsDetailsById = "DeleteSlsSOsDetailsById";
        //    public static string InsertSlsSOsDetails = "InsertSlsSOsDetails";
        //    public static string UpdateSlsSOsDetails = "UpdateSlsSOsDetails";
        //}

        //public static class SlsSR
        //{
        //    public static string DeleteSlsSRsById = "DeleteSlsSRsById";
        //    public static string GetSlsSRsByCompanyId = "GetSlsSRsByCompanyId";
        //    public static string GetSlsSRsByIdForSO = "GetSlsSRsByIdForSO";
        //    public static string GetSlsSRsLastCodeByCompanyId = "GetSlsSRsLastCodeByCompanyId";
        //    public static string InsertSlsSRs = "InsertSlsSRs";
        //    public static string UpdateSlsSRs = "UpdateSlsSRs";
        //    public static string UpdateSlsSRStatus = "UpdateSlsSRStatus";
        //    public static string GetSlsSRListByCompanyId = "GetSlsSRListByCompanyId";
        //    public static string GetSlsRequisitionInfoByRequisitionId = "GetSlsRequisitionInfoByRequisitionId";
        //    public static string GetSlsSRInfoForSOBySRId = "GetSlsSRInfoForSOBySRId";
        //    public static string GetSlsIsStoreToStoreTransfer = "GetSlsIsStoreToStoreTransfer";
        //    public static string GetSlsSRByCompanyId = "GetSlsSRByCompanyId";
        //    public static string UpdateSlsSRCancelStatus = "UpdateSlsSRCancelStatus";
        //    public static string GetSlsSRPartyDetailsBySRId = "GetSlsSRPartyDetailsBySRId";
        //}
        //public static class SlsSRsDetail
        //{
        //    public static string DeleteSlsSRsDetailsById = "DeleteSlsSRsDetailsById";
        //    public static string InsertSlsSRsDetails = "InsertSlsSRsDetails";
        //    public static string UpdateSlsSRsDetails = "UpdateSlsSRsDetails";
        //}
        //public static class SlsSRsProductDetail
        //{
        //    public static string DeleteSlsSRsProductDetailsById = "DeleteSlsSRsProductDetailsById";
        //    public static string InsertSlsSRsProductDetails = "InsertSlsSRsProductDetails";
        //    public static string UpdateSlsSRsProductDetails = "UpdateSlsSRsProductDetails";
        //}
        //public static class SlsStockInOut
        //{
        //    public static string GetSlsStockInOutByStoreId = "GetSlsStockInOutByStoreId";
        //    public static string InsertSlsStockInOut = "InsertSlsStockInOut";
        //    public static string UpdateSlsStockInOut = "UpdateSlsStockInOut";
        //    public static string UpdateSlsStockInOutForReceive = "UpdateSlsStockInOutForReceive";
        //}
        //public static class SlsStoreOpen
        //{
        //    public static string InsertSlsStoreOpens = "InsertSlsStoreOpens";
        //    public static string UpdateSlsStoreOpens = "UpdateSlsStoreOpens";
        //}
        //public static class SlsStorePermission
        //{
        //    public static string InsertSlsStorePermissions = "InsertSlsStorePermissions";
        //    public static string GetSlsPermittedStoreByEmployeeId = "GetSlsPermittedStoreByEmployeeId";
        //}
        //public static class SlsStore
        //{
        //    public static string DeleteSlsStoresById = "DeleteSlsStoresById";
        //    public static string GetSlsStoresByCompanyId = "GetSlsStoresByCompanyId";
        //    public static string GetSlsStoresByCompanyIdForProductRequisition = "GetSlsStoresByCompanyIdForProductRequisition";
        //    public static string GetSlsStoresBySDPId = "GetSlsStoresBySDPId";
        //    public static string InsertSlsStores = "InsertSlsStores";
        //    public static string UpdateSlsStores = "UpdateSlsStores";
        //    public static string GetSlsParentStoreIdBySOId = "GetSlsParentStoreIdBySOId";
        //    public static string GetSlsChildStoreIdBySOId = "GetSlsChildStoreIdBySOId";
        //    public static string GetSlsStoreMaxPrefixByCompanyId = "GetSlsStoreMaxPrefixByCompanyId";
        //    public static string GetSlsStorePrefixByStoresSDPId = "GetSlsStorePrefixByStoresSDPId";
        //}
        //public static class SlsTruckInfo
        //{
        //    public static string DeleteSlsTruckInfosById = "DeleteSlsTruckInfosById";
        //    public static string GetSlsTruckInfoBySOId = "GetSlsTruckInfoBySOId";
        //    public static string InsertSlsTruckInfos = "InsertSlsTruckInfos";
        //    public static string UpdateSlsTruckInfo = "UpdateSlsTruckInfo";
        //    public static string UpdateSlsTruckInfosCancelStatus = "UpdateSlsTruckInfosCancelStatus";
        //    public static string GetSlsMaxDailySerialNo = "GetSlsMaxDailySerialNo";
        //    public static string GetSlsTruckAndSOInfoByDate = "GetSlsTruckAndSOInfoByDate";
        //    public static string UpdateSlsTruckOutTime = "UpdateSlsTruckOutTime";
        //    public static string UpdateSlsTruckCancelStatus = "UpdateSlsTruckCancelStatus";
        //}
        //#endregion

        //#region Production

        //public static class PrdAdditionalParticular
        //{
        //    public static string DeletePrdAdditionalParticularsById = "DeletePrdAdditionalParticularsById";
        //    public static string GetPrdAdditionalParticularsByCmnCompanyId = "GetPrdAdditionalParticularsByCmnCompanyId";
        //    public static string GetPrdAdditionalParticularsById = "GetPrdAdditionalParticularsById";
        //    public static string InsertPrdAdditionalParticulars = "InsertPrdAdditionalParticulars";
        //    public static string UpdatePrdAdditionalParticulars = "UpdatePrdAdditionalParticulars";
        //}

        //public static class PrdBOM
        //{
        //    public static string GetPrdBOMLastCode = "GetPrdBOMLastCode";
        //    public static string InsertPrdBOMs = "InsertPrdBOMs";
        //    public static string GetPrdBOMProductDetailsByBomId = "GetPrdBOMProductDetailsByBomId";
        //    public static string GetPrdBOMSearch = "GetPrdBOMSearch";
        //    public static string UpdatePrdProductBOMs = "UpdatePrdProductBOMs";
        //}

        //public static class PrdCurrentStock
        //{

        //}
        //public static class PrdDailyConsumption
        //{
        //    //public static string GetPrdDailyConsumptionSearch = "GetPrdDailyConsumptionSearch";
        //    //public static string GetPrdDailyConsumptionStandardById = "GetPrdDailyConsumptionStandardById";
        //    //public static string GetPrdDailyConsumptionStandardLastCode = "GetPrdDailyConsumptionStandardLastCode";
        //    //public static string GetPrdDailyConsumptionStandardSearch = "GetPrdDailyConsumptionStandardSearch";
        //    //public static string UpdatePrdDailyConsumptionActualsStatus = "UpdatePrdDailyConsumptionActualsStatus";
        //    //public static string UpdatePrdDailyConsumptionStandards = "UpdatePrdDailyConsumptionStandards";
        //    //public static string UpdatePrdDailyConsumptionStandardsStatus = "UpdatePrdDailyConsumptionStandardsStatus";
        //    public static string InsertPrdDailyConsumptions = "InsertPrdDailyConsumptions";
        //}
        //public static class PrdDailyProduction
        //{
        //    public static string InsertPrdDailyProductions = "InsertPrdDailyProductions";
        //    public static string GetPrdDailyProductionSearch = "GetPrdDailyProductionsSearch";
        //    public static string UpdatePrdDailyProductionsStatus = "UpdatePrdDailyProductionsStatus";
        //    public static string GetPrdProductionInfoByDateRange = "GetPrdProductionInfoByDateRange";
        //    public static string GetPrdProductionInfoForEditByDateRange = "GetPrdProductionInfoForEditByDateRange";
        //    //Added By Oli
        //    public static string GetPrdItemCurrentStockByItemId = "GetPrdItemCurrentStockByItemId";
        //}
        //public static class PrdDailyProductionTarget
        //{
        //    public static string GetPrdDailyProductionTargetsSearch = "GetPrdDailyProductionTargetsSearch";
        //    public static string InsertPrdDailyProductionTargets = "InsertPrdDailyProductionTargets";
        //    public static string GetPrdDailyProductionTargetsById = "GetPrdDailyProductionTargetsById";
        //    public static string UpdatePrdDailyProductionTargets = "UpdatePrdDailyProductionTargets";
        //    public static string UpdatePrdDailyProductionTargetsStatus = "UpdatePrdDailyProductionTargetsStatus";
        //    public static string GetPrdDailyProductionTargetsLastCode = "GetPrdDailyProductionTargetsLastCode";
        //}
        //public static class PrdDamage
        //{

        //}
        //public static class PrdDamageType
        //{

        //}
        //public static class PrdFinishedGoodsReceivedDetail
        //{
        //    public static string InsertPrdFinishedGoodsReceivedDetails = "InsertPrdFinishedGoodsReceivedDetails";
        //}
        //public static class PrdFinishedGoodsReceive
        //{
        //    public static string GetPrdFinishedGoodsReceiveLastCode = "GetPrdFinishedGoodsReceiveLastCode";
        //    public static string InsertPrdFinishedGoodsReceives = "InsertPrdFinishedGoodsReceives";
        //}
        //public static class PrdIssueDetail
        //{
        //    public static string InsertPrdIssueDetails = "InsertPrdIssueDetails";
        //    public static string UpdatePrdIssueDetails = "UpdatePrdIssueDetails";
        //}
        //public static class PrdIssue
        //{
        //    public static string GetPrdIssueById = "GetPrdIssueById";
        //    public static string GetPrdIssueLastCode = "GetPrdIssueLastCode";
        //    public static string GetPrdIssuesSearch = "GetPrdIssuesSearch";
        //    public static string InsertPrdIssues = "InsertPrdIssues";
        //    public static string UpdatePrdIssues = "UpdatePrdIssues";
        //    public static string UpdatePrdIssueStatus = "UpdatePrdIssueStatus";
        //    public static string GetPrdProductReqisitionDetailsByIdForIssue = "GetPrdProductReqisitionDetailsByIdForIssue";
        //}
        //public static class PrdItemCurrentStock
        //{

        //}
        //public static class PrdItemReceive
        //{
        //    public static string GetPrdInventoryIssueInfoByDate = "GetPrdInventoryIssueInfoByDate";
        //    public static string GetPrdItemReceivesById = "GetPrdItemReceivesById";
        //    public static string GetPrdItemReceivesLastCode = "GetPrdItemReceivesLastCode";
        //    public static string GetPrdItemReceivesSearch = "GetPrdItemReceivesSearch";
        //    public static string InsertPrdItemReceives = "InsertPrdItemReceives";
        //    public static string UpdatePrdItemReceives = "UpdatePrdItemReceives";
        //}
        //public static class PrdItemStockInOut
        //{
        //    public static string InsertPrdItemStockInOut = "InsertPrdItemStockInOut";
        //    public static string UpdatePrdItemStockInOut = "UpdatePrdItemStockInOut";
        //}
        //public static class PrdItemStoreOpen
        //{

        //}
        //public static class PrdItemStore
        //{
        //    public static string GetPrdItemStoreByItemId = "GetPrdItemStoreByItemId";
        //    public static string GetPrdItemStoresByCompanyId = "GetPrdItemStoresByCompanyId";
        //}
        //public static class PrdProductDetail
        //{
        //    public static string InsertPrdProductDetails = "InsertPrdProductDetails";
        //    public static string DeletePrdProductDetailsById = "DeletePrdProductDetailsById";
        //    public static string GetPrdProductDetailsByProductId = "GetPrdProductDetailsByProductId";
        //    public static string UpdatePrdProductBOMDetails = "UpdatePrdProductBOMDetails";
        //}
        //public static class PrdProductionCostDetail
        //{
        //    public static string DeletePrdProductionCostDetailsById = "DeletePrdProductionCostDetailsById";
        //    public static string GetPrdProductionCostDetailsByProductionCostId = "GetPrdProductionCostDetailsByProductionCostId";
        //    public static string InsertPrdProductionCostDetails = "InsertPrdProductionCostDetails";
        //    public static string UpdatePrdProductionCostDetails = "UpdatePrdProductionCostDetails";
        //}
        //public static class PrdProductionCost
        //{

        //    public static string InsertPrdProductionCosts = "InsertPrdProductionCosts";
        //    public static string GetPrdProductionCostsById = "GetPrdProductionCostsById";
        //    public static string GetPrdProductionCostsLastCode = "GetPrdProductionCostsLastCode";
        //    public static string UpdatePrdProductionCosts = "UpdatePrdProductionCosts";
        //}
        //public static class PrdProductProperty
        //{

        //    public static string InsertPrdProductProperties = "InsertPrdProductProperties";
        //    public static string DeletePrdProductPropertiesByPrdProductId = "DeletePrdProductPropertiesByPrdProductId";
        //    public static string GetPrdProductPropertyByProductId = "GetPrdProductPropertyByProductId";
        //}
        //public static class PrdProduct
        //{
        //    public static string GetPrdProductsByCmnCompanyId = "GetPrdProductsByCmnCompanyId";
        //    public static string GetPrdProductsByCompanyId = "GetPrdProductsByCompanyId";
        //    public static string GetPrdProductsByProductType = "GetPrdProductsByProductType";
        //    public static string GetPrdProductsRegardingBOMsByCompanyId = "GetPrdProductsRegardingBOMsByCompanyId";
        //    public static string GetPrdProductStoreOpen = "GetPrdProductStoreOpen";
        //    public static string GetPrdProductsWithPriceByCmnCompanyId = "GetPrdProductsWithPriceByCmnCompanyId";
        //    public static string UpdatePrdProducts = "UpdatePrdProducts";
        //    public static string GetPrdProductTypesByCompanyId = "GetPrdProductTypesByCompanyId";
        //    public static string GetSlsLatestProductPriceInfoBySDPType = "GetSlsLatestProductPriceInfoBySDPType";
        //    public static string GetSlsProductInfoByEffectiveDate = "GetSlsProductInfoByEffectiveDate";
        //    public static string GetSlsProductStoreOpen = "GetSlsProductStoreOpen";
        //    public static string GetPrdVirtualProductsByCompanyId = "GetPrdVirtualProductsByCompanyId";
        //    public static string GetPrdProductParents = "GetPrdProductParents";
        //    public static string GetPrdProductLastCode = "GetPrdProductLastCode";
        //    public static string GetPrdProductById = "GetPrdProductById";
        //    public static string InsertPrdProducts = "InsertPrdProducts";
        //    public static string GetPrdDuplicateProduct = "GetPrdDuplicateProduct";
        //    public static string GetPrdProductsWithCostByCompanyId = "GetPrdProductsWithCostByCompanyId";
        //}
        //public static class PrdProductUnit
        //{
        //    public static string DeletePrdProductUnitsByPrdProductId = "DeletePrdProductUnitsByPrdProductId";
        //    public static string GetPrdProductUnitById = "GetPrdProductUnitById";
        //    public static string InsertPrdProductUnits = "InsertPrdProductUnits";
        //    public static string UpdatePrdProductUnits = "UpdatePrdProductUnits";
        //}
        //public static class PrdStockInOut
        //{
        //    public static string InsertPrdStockInOuts = "InsertPrdStockInOuts";
        //    public static string UpdatePrdStockInOut = "UpdatePrdStockInOut";
        //}
        //public static class PrdStoreOpen
        //{
        //    public static string InsertPrdStoreOpens = "InsertPrdStoreOpens";
        //    public static string UpdatePrdStoreOpens = "UpdatePrdStoreOpens";
        //    public static string GetPrdProductStoreOpen = "GetPrdProductStoreOpen";
        //}
        //public static class PrdStore
        //{
        //    public static string DeletePrdStoresById = "DeletePrdStoresById";
        //    public static string GetPrdStoreIdByPrdProductId = "GetPrdStoreIdByPrdProductId";
        //    public static string GetPrdStoresByCmnCompanyId = "GetPrdStoresByCmnCompanyId";
        //    public static string GetPrdStoresById = "GetPrdStoresById";
        //    public static string GetPrdStoresMaxPrefixByCompanyId = "GetPrdStoresMaxPrefixByCompanyId";
        //    public static string InsertPrdStores = "InsertPrdStores";
        //    public static string UpdatePrdStores = "UpdatePrdStores";
        //}
        //#endregion

        //#region Fixed Asset

        //public static class FxdAcquisitionDetail
        //{
        //    public static string GetFxdAcquisitionDetailsByAcquisitionId = "GetFxdAcquisitionDetailsByAcquisitionId";
        //    public static string GetFxdAcquisitionDetailsCurrentActiveRecordByFxdAcquisitionId = "GetFxdAcquisitionDetailsCurrentActiveRecordByFxdAcquisitionId";
        //    public static string InsertFxdAcquisitionDetails = "InsertFxdAcquisitionDetails";
        //    public static string InsertFxdAcquisitionDetailsForAdjustmentOrTransaction = "InsertFxdAcquisitionDetailsForAdjustmentOrTransaction";
        //    public static string UpdateFxdAcquisitionDetails = "UpdateFxdAcquisitionDetails";
        //    public static string UpdateFxdAcquisitionDetailsStatus = "UpdateFxdAcquisitionDetailsStatus";
        //    public static string UpdateFxdAcquisitionDetailsStatusByFxdAcquisitionId = "UpdateFxdAcquisitionDetailsStatusByFxdAcquisitionId";
        //}
        //public static class FxdAcquisitionLocation
        //{
        //    public static string InsertFxdAcquisitionLocations = "InsertFxdAcquisitionLocations";
        //    public static string InsertFxdAcquisitionLocations1 = "InsertFxdAcquisitionLocations1";
        //    public static string UpdateFxdAcquisitionLocations = "UpdateFxdAcquisitionLocations";
        //}
        //public static class FxdAcquisition
        //{
        //    public static string GetFxdAcquisitionsByCompanyIdNId = "GetFxdAcquisitionsByCompanyIdNId";
        //    public static string GetFxdAcquisitionsByCompanyIdNStatus = "GetFxdAcquisitionsByCompanyIdNStatus";
        //    public static string GetFxdAcquisitionsById = "GetFxdAcquisitionsById";
        //    public static string GetFxdIsItemExist = "GetFxdIsItemExist";
        //    public static string GetFxdAcquisitionsForVoucher = "GetFxdAcquisitionsForVoucher";
        //    public static string GetFxdAcquisitionsLastChildNode = "GetFxdAcquisitionsLastChildNode";
        //    public static string InsertFxdAcquisitions = "InsertFxdAcquisitions";
        //    public static string UpdateFxdAcquisitionVoucherId = "UpdateFxdAcquisitionVoucherId";
        //    public static string UpdateFxdAcquisitions = "UpdateFxdAcquisitions";
        //    public static string UpdateFxdAcquisitionsItemStatus = "UpdateFxdAcquisitionsItemStatus";
        //}
        //public static class FxdDepriciationMethod
        //{
        //    public static string DeleteFxdDepriciationMethods = "DeleteFxdDepriciationMethods";
        //    public static string GetFxdDepriciationMethodsByCmnCompanyId = "GetFxdDepriciationMethodsByCmnCompanyId";
        //    public static string GetFxdDepriciationMethodsById = "GetFxdDepriciationMethodsById";
        //    public static string InsertFxdDepriciationMethods = "InsertFxdDepriciationMethods";
        //    public static string UpdateFxdDepriciationMethods = "UpdateFxdDepriciationMethods";
        //}
        //public static class FxdDepriciationProcess
        //{
        //    public static string GetFxdDepriciationProcessLastCode = "GetFxdDepriciationProcessLastCode";
        //    public static string InsertFxdDepriciationProcesses = "InsertFxdDepriciationProcesses";
        //    public static string FxdDepriciationProcesses = "FxdDepriciationProcess";
        //}
        //public static class FxdDepriciationRate
        //{
        //    public static string DeleteFxdDepriciationRatesByAnFCOAParentId = "DeleteFxdDepriciationRatesByAnFCOAParentId";
        //    public static string GetFxdDepriciationRatesByCompanyId = "GetFxdDepriciationRatesByCompanyId";
        //    public static string GetFxdDepriciationRatesById = "GetFxdDepriciationRatesById";
        //    public static string InsertFxdDepriciationRates = "InsertFxdDepriciationRates";
        //    public static string UpdateFxdDepriciationRates = "UpdateFxdDepriciationRates";
        //}
        //public static class FxdSupplier
        //{
        //    public static string DeleteFxdSuppliersById = "DeleteFxdSuppliersById";
        //    public static string GetFxdSupplier = "GetFxdSupplier";
        //    public static string GetFxdSuppliers = "GetFxdSuppliers";
        //    public static string GetFxdManufacturer = "GetFxdManufacturer";
        //    public static string GetFxdSuppliersByCmnCompanyId = "GetFxdSuppliersByCmnCompanyId";
        //    public static string GetFxdSuppliersById = "GetFxdSuppliersById";
        //    public static string InsertFxdSuppliers = "InsertFxdSuppliers";
        //    public static string UpdateFxdSuppliers = "UpdateFxdSuppliers";
        //}
        //public static class FxdTransactionDetail
        //{
        //    public static string GetLatestRevaluedFxdTransactionDetailsByFxdAcquisitionId = "GetLatestRevaluedFxdTransactionDetailsByFxdAcquisitionId";
        //    public static string InsertFxdTransactionDetails = "InsertFxdTransactionDetails";
        //    public static string UpdateFxdTransactionDetails = "UpdateFxdTransactionDetails";
        //}
        //public static class FxdTransaction
        //{
        //    public static string GetFxdTransactionsLastCode = "GetFxdTransactionsLastCode";
        //    public static string InsertFxdTransactions = "InsertFxdTransactions";
        //    public static string UpdateFxdTransactions = "UpdateFxdTransactions";
        //}
        //public static class FxdTransactionType
        //{
        //    public static string DeleteFxdTransactionTypesById = "DeleteFxdTransactionTypesById";
        //    public static string GetFxdTransactionTypesByCmnCompanyId = "GetFxdTransactionTypesByCmnCompanyId";
        //    public static string GetFxdTransactionTypesByCmnCompanyIdNIdNStatus = "GetFxdTransactionTypesByCmnCompanyIdNIdNStatus";
        //    public static string GetFxdTransactionTypesById = "GetFxdTransactionTypesById";
        //    public static string InsertFxdTransactionTypes = "InsertFxdTransactionTypes";
        //    public static string UpdateFxdTransactionTypes = "UpdateFxdTransactionTypes";
        //}
        //#endregion

        //#region Bank

        //public static class BnkAccountCharge
        //{
        //    public static string DeleteBnkAccountChargesByBnkAccountId = "DeleteBnkAccountChargesByBnkAccountId";
        //    public static string GetBnkAccountChargesById = "GetBnkAccountChargesById";
        //    public static string InsertBnkAccountCharges = "InsertBnkAccountCharges";
        //    public static string UpdateBnkAccountCharges = "UpdateBnkAccountCharges";
        //}
        //public static class BnkAccountInterestHistory
        //{
        //    public static string DeleteBnkAccountInterestHistoriesByBnkAccountId = "DeleteBnkAccountInterestHistoriesByBnkAccountId";
        //    public static string GetBnkAccountInterestHistoriesById = "GetBnkAccountInterestHistoriesById";
        //    public static string InsertBnkAccountInterestHistories = "InsertBnkAccountInterestHistories";
        //    public static string UpdateBnkAccountInterestHistories = "UpdateBnkAccountInterestHistories";
        //}
        //public static class BnkAccount
        //{
        //    public static string DeleteBnkAccountsById = "DeleteBnkAccountsById";
        //    public static string GetBnkAccounts = "GetBnkAccounts";
        //    public static string GetBnkAccountsById = "GetBnkAccountsById";
        //    public static string GetBnkAccountsCheck = "GetBnkAccountsCheck";
        //    public static string GetBnkAccountsListByDate = "GetBnkAccountsListByDate";
        //    public static string GetBnkAccountNoById = "GetBnkAccountNoById";
        //    public static string InsertBnkAccounts = "InsertBnkAccounts";
        //    public static string UpdateBnkAccounts = "UpdateBnkAccounts";
        //}
        //public static class BnkAccountTypeInterest
        //{
        //    public static string DeleteBnkAccountTypeInterestsById = "DeleteBnkAccountTypeInterestsById";
        //    public static string GetBnkAccountTypeInterestsById = "GetBnkAccountTypeInterestsById";
        //    public static string GetBnkInterestsByAccountTypeId = "GetBnkInterestsByAccountTypeId";
        //    public static string InsertBnkAccountTypeInterests = "InsertBnkAccountTypeInterests";
        //    public static string UpdateBnkAccountTypeInterests = "UpdateBnkAccountTypeInterests";
        //}
        //public static class BnkAccountType
        //{
        //    public static string DeleteBnkAccountTypesById = "DeleteBnkAccountTypesById";
        //    public static string GetBnkAccountTypesByBankId = "GetBnkAccountTypesByBankId";
        //    public static string GetBnkAccountTypesById = "GetBnkAccountTypesById";
        //    public static string GetBnkAccountTypesByLoanSanctionId = "GetBnkAccountTypesByLoanSanctionId";
        //    public static string InsertBnkAccountTypes = "InsertBnkAccountTypes";
        //    public static string UpdateBnkAccountTypes = "UpdateBnkAccountTypes";
        //}
        //public static class BnkBranch
        //{
        //    public static string DeleteBnkBranchesById = "DeleteBnkBranchesById";
        //    public static string GetBnkBranchesByBankId = "GetBnkBranchesByBankId";
        //    public static string GetBnkBranchesById = "GetBnkBranchesById";
        //    public static string InsertBnkBranches = "InsertBnkBranches";
        //    public static string UpdateBnkBranches = "UpdateBnkBranches";
        //}
        //public static class BnkCharge
        //{
        //    public static string DeleteBnkChargesById = "DeleteBnkChargesById";
        //    public static string GetBnkCharges = "GetBnkCharges";
        //    public static string GetBnkChargesById = "GetBnkChargesById";
        //    public static string InsertBnkCharges = "InsertBnkCharges";
        //    public static string UpdateBnkCharges = "UpdateBnkCharges";
        //}
        //public static class BnkCheckBook
        //{
        //    public static string DeleteBnkCheckBooksById = "DeleteBnkCheckBooksById";
        //    public static string GetBnkCheckBooksByBnkAccountId = "GetBnkCheckBooksByBnkAccountId";
        //    public static string GetBnkCheckBooksById = "GetBnkCheckBooksById";
        //    public static string GetBnkCheckBookSummaryByAccountNo = "GetBnkCheckBookSummaryByAccountNo";
        //    public static string InsertBnkCheckBooks = "InsertBnkCheckBooks";
        //    public static string UpdateBnkCheckBooks = "UpdateBnkCheckBooks";
        //}
        //public static class BnkCompanyTransaction
        //{
        //    public static string GetBnkCompanyTransactionsByBnkTransactionTypeId = "GetBnkCompanyTransactionsByBnkTransactionTypeId";
        //}
        //public static class BnkFormula
        //{
        //    public static string DeleteBnkFormulasById = "DeleteBnkFormulasById";
        //    public static string GetBnkFormulas = "GetBnkFormulas";
        //    public static string GetBnkFormulasById = "GetBnkFormulasById";
        //    public static string GetBnkFormulasByMethodId = "GetBnkFormulasByMethodId";
        //    public static string InsertBnkFormulas = "InsertBnkFormulas";
        //    public static string UpdateBnkFormulas = "UpdateBnkFormulas";
        //}
        //public static class BnkKeyWord
        //{
        //    public static string DeleteBnkKeyWordsById = "DeleteBnkKeyWordsById";
        //    public static string GetBnkKeyWords = "GetBnkKeyWords";
        //    public static string GetBnkKeyWordsById = "GetBnkKeyWordsById";
        //    public static string InsertBnkKeyWords = "InsertBnkKeyWords";
        //    public static string UpdateBnkKeyWords = "UpdateBnkKeyWords";
        //}
        //public static class BnkLoanProduct
        //{
        //    public static string DeleteBnkLoanProductsById = "DeleteBnkLoanProductsById";
        //    public static string GetBnkLoanProducts = "GetBnkLoanProducts";
        //    public static string GetBnkLoanProductsById = "GetBnkLoanProductsById";
        //    public static string InsertBnkLoanProducts = "InsertBnkLoanProducts";
        //    public static string UpdateBnkLoanProducts = "UpdateBnkLoanProducts";
        //}
        //public static class BnkLoanSanctionDetail
        //{
        //    public static string DeleteBnkLoanSanctionDetailsById = "DeleteBnkLoanSanctionDetailsById";
        //    public static string GetBnkLoanSanctionDetailsById = "GetBnkLoanSanctionDetailsById";
        //    public static string InsertBnkLoanSanctionDetails = "InsertBnkLoanSanctionDetails";
        //    public static string UpdateBnkLoanSanctionDetails = "UpdateBnkLoanSanctionDetails";
        //}
        //public static class BnkLoanSanction
        //{
        //    public static string DeleteBnkLoanSanctionsById = "DeleteBnkLoanSanctionsById";
        //    public static string GetBnkLoanSanctionById = "GetBnkLoanSanctionById";
        //    public static string GetBnkLoanSanctionByDate = "GetBnkLoanSanctionByDate";
        //    public static string InsertBnkLoanSanction = "InsertBnkLoanSanction";
        //    public static string UpdateBnkLoanSanctions = "UpdateBnkLoanSanctions";
        //}
        //public static class BnkMethodOfLoan
        //{
        //    public static string DeleteBnkMethodOfLoansById = "DeleteBnkMethodOfLoansById";
        //    public static string GetBnkMethodOfLoans = "GetBnkMethodOfLoans";
        //    public static string GetBnkMethodOfLoansById = "GetBnkMethodOfLoansById";
        //    public static string InsertBnkMethodOfLoans = "InsertBnkMethodOfLoans";
        //    public static string UpdateBnkMethodOfLoans = "UpdateBnkMethodOfLoans";
        //}
        //public static class BnkOverdueStatus
        //{
        //    public static string DeleteBnkOverdueStatusById = "DeleteBnkOverdueStatusById";
        //    public static string GetBnkOverdueStatus = "GetBnkOverdueStatus";
        //    public static string GetBnkOverdueStatusById = "GetBnkOverdueStatusById";
        //    public static string InsertBnkOverdueStatus = "InsertBnkOverdueStatus";
        //    public static string UpdateBnkOverdueStatus = "UpdateBnkOverdueStatus";
        //}
        //public static class BnkReconciliationHead
        //{
        //    public static string DeleteBnkReconciliationHeadsById = "DeleteBnkReconciliationHeadsById";
        //    public static string GetBnkReconciliationHeads = "GetBnkReconciliationHeads";
        //    public static string GetBnkReconciliationHeadsById = "GetBnkReconciliationHeadsById";
        //    public static string GetBnkReconciliationHeadsByName = "GetBnkReconciliationHeadsByName";
        //    public static string InsertBnkReconciliationHeads = "InsertBnkReconciliationHeads";
        //    public static string UpdateBnkReconciliationHeads = "UpdateBnkReconciliationHeads";
        //}
        //public static class BnkReconciliation
        //{
        //    public static string DeleteBnkReconciliationsById = "DeleteBnkReconciliationsById";
        //    public static string GetBnkReconciliationsByAccountId = "GetBnkReconciliationsByAccountId";
        //    public static string GetBnkReconciliationsById = "GetBnkReconciliationsById";
        //    public static string InsertBnkReconciliations = "InsertBnkReconciliations";
        //    public static string UpdateBnkReconciliations = "UpdateBnkReconciliations";
        //}
        //public static class BnkReconciliationsDetail
        //{

        //    public static string DeleteBnkReconciliationsDetailsByBnkReconciliationId = "DeleteBnkReconciliationsDetailsByBnkReconciliationId";
        //    public static string GetBnkReconciliationsDetailsById = "GetBnkReconciliationsDetailsById";
        //    public static string InsertBnkReconciliationsDetails = "InsertBnkReconciliationsDetails";
        //    public static string UpdateBnkReconciliationsDetails = "UpdateBnkReconciliationsDetails";
        //}
        //public static class BnkTransaction
        //{
        //    public static string DeleteBnkTransactionsByAnFVoucherId = "DeleteBnkTransactionsByAnFVoucherId";
        //    public static string GetBnkTransactionsById = "GetBnkTransactionsById";
        //    public static string InsertBnkTransactions = "InsertBnkTransactions";
        //    public static string UpdateBnkTransactions = "UpdateBnkTransactions";
        //}
        //public static class BnkTransactionType
        //{
        //    public static string DeleteBnkTransactionTypesById = "DeleteBnkTransactionTypesById";
        //    public static string GetBnkTransactionTypes = "GetBnkTransactionTypes";
        //    public static string GetBnkTransactionTypesById = "GetBnkTransactionTypesById";
        //    public static string InsertBnkTransactionTypes = "InsertBnkTransactionTypes";
        //    public static string UpdateBnkTransactionTypes = "UpdateBnkTransactionTypes";
        //}
        //#endregion

        //#region Freeze

        //public static class FrzAdjustment
        //{

        //}
        //public static class FrzAutoRentAdjustment
        //{

        //}
        //public static class FrzDamarage
        //{

        //}
        //public static class FrzDemandDetail
        //{
        //    public static string DeleteFrzDemandDetailsByFrzDemandId = "DeleteFrzDemandDetailsByFrzDemandId";
        //    public static string GetFrzDemandDetailsByFrzDemandId = "GetFrzDemandDetailsByFrzDemandId";
        //    public static string InsertFrzDemandDetails = "InsertFrzDemandDetails";
        //    public static string UpdateFrzDemandDetails = "UpdateFrzDemandDetails";
        //    public static string UpdateFrzDemandDetailsFromInjection = "UpdateFrzDemandDetailsFromInjection";
        //}
        //public static class FrzDemand
        //{
        //    public static string GetFrzDemandsByCompanyIdNDateRange = "GetFrzDemandsByCompanyIdNDateRange";
        //    public static string GetFrzDemandsByDateRange = "GetFrzDemandsByDateRange";
        //    public static string GetFrzDemandsById = "GetFrzDemandsById";
        //    public static string GetFrzDemandsLastCode = "GetFrzDemandsLastCode";
        //    public static string InsertFrzDemands = "InsertFrzDemands";
        //    public static string UpdateFrzDemands = "UpdateFrzDemands";
        //    public static string UpdateFrzDemandsBySRId = "UpdateFrzDemandsBySRId";
        //    public static string GetFrzRequisitonByDate = "GetFrzRequisitonByDate";
        //    public static string GetFrzIssueByDemandId = "GetFrzIssueByDemandId";
        //}
        //public static class FrzFreeze
        //{
        //    public static string GetFrzFreezeItemByInvItemId = "GetFrzFreezeItemByInvItemId";
        //    public static string GetFrzFreezesByCompanyAndSDPId = "GetFrzFreezesByCompanyAndSDPId";
        //    public static string GetFrzFreezesById = "GetFrzFreezesById";
        //    public static string GetFrzFreezesDuplicateTrackingNo = "GetFrzFreezesDuplicateTrackingNo";
        //    public static string GetFrzFreezesTrackingNoByFrzDemandId = "GetFrzFreezesTrackingNoByFrzDemandId";
        //    public static string GetFrzInjectionsByFrzFreezeId = "GetFrzInjectionsByFrzFreezeId";
        //    public static string InsertFrzFreezes = "InsertFrzFreezes";
        //    public static string UpdateFrzFreezes = "UpdateFrzFreezes";
        //    public static string UpdateFrzFreezesStatus = "UpdateFrzFreezesStatus";
        //}
        //public static class FrzInjection
        //{
        //    public static string DeleteFrzInjectionsById = "DeleteFrzInjectionsById";
        //    public static string GetFrzInjectionsByFrzFreezeId = "GetFrzInjectionsByFrzFreezeId";
        //    public static string GetFrzInjectionsLastCode = "GetFrzInjectionsLastCode";
        //    public static string InsertFrzInjections = "InsertFrzInjections";
        //    public static string UpdateFrzInjections = "UpdateFrzInjections";
        //}
        //public static class FrzMovement
        //{

        //}
        //public static class FrzReceiveDetail
        //{
        //    public static string InsertFrzReceiveDetails = "InsertFrzReceiveDetails";
        //}
        //public static class FrzReceive
        //{
        //    public static string GetFrzReceivesLastCode = "GetFrzReceivesLastCode";
        //    public static string InsertFrzReceiveDetails = "InsertFrzReceiveDetails";
        //    public static string InsertFrzReceives = "InsertFrzReceives";
        //}
        //public static class FrzSDPPermission
        //{
        //    public static string GetFrzPermittedDepotByEmployeeId = "GetFrzPermittedDepotByEmployeeId";
        //    public static string InsertFrzSDPPermissions = "InsertFrzSDPPermissions";
        //}
        //public static class FrzService
        //{
        //    public static string GetFrzServiceByDate = "GetFrzServiceByDate";
        //    public static string GetFrzServiceById = "GetFrzServiceById";
        //    public static string GetFrzServiceLastCode = "GetFrzServiceLastCode";
        //    public static string InsertFrzServices = "InsertFrzServices";
        //    public static string UpdateFrzServices = "UpdateFrzServices";
        //}
        //public static class FrzTransaction
        //{
        //    public static string DeleteFrzTransactionsById = "DeleteFrzTransactionsById";
        //    public static string GetFrzTransactionsByCompanyId = "GetFrzTransactionsByCompanyId";
        //    public static string GetFrzTransactionsById = "GetFrzTransactionsById";
        //    public static string GetFrzTransactionsLastCode = "GetFrzTransactionsLastCode";
        //    public static string InsertFrzTransactions = "InsertFrzTransactions";
        //    public static string UpdateFrzTransactions = "UpdateFrzTransactions";
        //}

        //#endregion

        //#region CRM

        //public static class CrmAccountInformation
        //{
        //    public static string DeleteCrmAccountInformationsById = "DeleteCrmAccountInformationsById";
        //    public static string GetCrmAccountInformationLastCode = "GetCrmAccountInformationLastCode";
        //    public static string GetCrmAccountInformations = "GetCrmAccountInformations";
        //    public static string GetCrmAccountInformationsById = "GetCrmAccountInformationsById";
        //    public static string InsertCrmAccountInformations = "InsertCrmAccountInformations";
        //    public static string UpdateCrmAccountInformations = "UpdateCrmAccountInformations";
        //    public static string GetCrmAccountInfoSearch = "GetCrmAccountInfoSearch";
        //}
        //public static class CrmAccountType
        //{
        //    public static string DeleteCrmAccountTypesById = "DeleteCrmAccountTypesById";
        //    public static string GetCrmAccountTypes = "GetCrmAccountTypes";
        //    public static string InsertCrmAccountTypes = "InsertCrmAccountTypes";
        //    public static string UpdateCrmAccountTypes = "UpdateCrmAccountTypes";
        //}
        //public static class CrmCampaign
        //{
        //    public static string DeleteCrmCampaigns = "DeleteCrmCampaigns";
        //    public static string GetCrmCampaignInfoSearch = "GetCrmCampaignInfoSearch";
        //    public static string GetCrmCampaigns = "GetCrmCampaigns";
        //    public static string GetCrmCampaignsById = "GetCrmCampaignsById";
        //    public static string GetCrmCampaignsLastCode = "GetCrmCampaignsLastCode";
        //    public static string InsertCrmCampaigns = "InsertCrmCampaigns";
        //    public static string UpdateCrmCampaigns = "UpdateCrmCampaigns";
        //}
        //public static class CrmContactInformation
        //{
        //    public static string DeleteCrmContactInformationsById = "DeleteCrmContactInformationsById";
        //    public static string GetCrmContactInformations = "GetCrmContactInformations";
        //    public static string GetCrmContactInformationsById = "GetCrmContactInformationsById";
        //    public static string GetCrmContactInfoSearch = "GetCrmContactInfoSearch";
        //    public static string InsertCrmContactInformations = "InsertCrmContactInformations";
        //    public static string UpdateCrmContactInformations = "UpdateCrmContactInformations";
        //}
        //public static class CrmEvent
        //{
        //    public static string DeleteCrmEventsById = "DeleteCrmEventsById";
        //    public static string GetCrmEvents = "GetCrmEvents";
        //    public static string GetCrmEventsById = "GetCrmEventsById";
        //    public static string InsertCrmEvents = "InsertCrmEvents";
        //    public static string UpdateCrmEvents = "UpdateCrmEvents";
        //}
        //public static class CrmIndustryType
        //{
        //    public static string DeleteCrmIndustryTypesById = "DeleteCrmIndustryTypesById";
        //    public static string GetCrmIndustryTypes = "GetCrmIndustryTypes";
        //    public static string InsertCrmIndustryTypes = "InsertCrmIndustryTypes";
        //    public static string UpdateCrmIndustryTypes = "UpdateCrmIndustryTypes";
        //}
        //public static class CrmLeadInformation
        //{
        //    public static string DeleteCrmLeadInformations = "DeleteCrmLeadInformations";
        //    public static string GetCrmLeadInformationLastCode = "GetCrmLeadInformationLastCode";
        //    public static string GetCrmLeadInformations = "GetCrmLeadInformations";
        //    public static string GetCrmLeadInformationsById = "GetCrmLeadInformationsById";
        //    public static string InsertCrmLeadInformations = "InsertCrmLeadInformations";
        //    public static string UpdateCrmLeadInformations = "UpdateCrmLeadInformations";
        //    public static string UpdateCrmLeadInformationsIsAccount = "UpdateCrmLeadInformationsIsAccount";
        //}
        //public static class CrmLeadStatus
        //{
        //    public static string DeleteCrmLeadStatusById = "DeleteCrmLeadStatusById";
        //    public static string GetCrmLeadStatus = "GetCrmLeadStatus";
        //    public static string GetCrmLeadStatusById = "GetCrmLeadStatusById";
        //    public static string InsertCrmLeadStatus = "InsertCrmLeadStatus";
        //    public static string UpdateCrmLeadStatus = "UpdateCrmLeadStatus";
        //}
        //public static class CrmOwnership
        //{
        //    public static string DeleteCrmOwnershipsById = "DeleteCrmOwnershipsById";
        //    public static string GetCrmOwnerships = "GetCrmOwnerships";
        //    public static string InsertCrmOwnerships = "InsertCrmOwnerships";
        //    public static string UpdateCrmOwnerships = "UpdateCrmOwnerships";
        //}
        //public static class CrmPotential
        //{
        //    public static string DeleteCrmPotentialsById = "DeleteCrmPotentialsById";
        //    public static string GetCrmPotentials = "GetCrmPotentials";
        //    public static string GetCrmPotentialsById = "GetCrmPotentialsById";
        //    public static string GetCrmPotentialsLastCode = "GetCrmPotentialsLastCode";
        //    public static string InsertCrmPotentials = "InsertCrmPotentials";
        //    public static string UpdateCrmPotentials = "UpdateCrmPotentials";
        //}
        //public static class CrmRating
        //{
        //    public static string DeleteCrmRatingsById = "DeleteCrmRatingsById";
        //    public static string GetCrmRatings = "GetCrmRatings";
        //    public static string InsertCrmRatings = "InsertCrmRatings";
        //    public static string UpdateCrmRatings = "UpdateCrmRatings";
        //}
        //public static class CrmSource
        //{
        //    public static string DeleteCrmSourcesById = "DeleteCrmSourcesById";
        //    public static string GetCrmSources = "GetCrmSources";
        //    public static string GetCrmSourcesByCompanyId = "GetCrmSourcesByCompanyId";
        //    public static string GetCrmSourcesById = "GetCrmSourcesById";
        //    public static string InsertCrmSources = "InsertCrmSources";
        //    public static string UpdateCrmSources = "UpdateCrmSources";
        //}
        //public static class CrmStage
        //{
        //    public static string DeleteCrmStagesById = "DeleteCrmStagesById";
        //    public static string GetCrmStages = "GetCrmStages";
        //    public static string GetCrmStagesById = "GetCrmStagesById";
        //    public static string InsertCrmStages = "InsertCrmStages";
        //    public static string UpdateCrmStages = "UpdateCrmStages";
        //}
        //public static class CrmTaskEventSubject
        //{
        //    public static string DeleteCrmTaskEventSubjectsById = "DeleteCrmTaskEventSubjectsById";
        //    public static string GetCrmTaskEventSubjects = "GetCrmTaskEventSubjects";
        //    public static string GetCrmTaskEventSubjectsById = "GetCrmTaskEventSubjectsById";
        //    public static string InsertCrmTaskEventSubjects = "InsertCrmTaskEventSubjects";
        //    public static string UpdateCrmTaskEventSubjects = "UpdateCrmTaskEventSubjects";
        //}
        //public static class CrmTask
        //{
        //    public static string DeleteCrmTasksById = "DeleteCrmTasksById";
        //    public static string GetCrmTasks = "GetCrmTasks";
        //    public static string GetCrmTasksById = "GetCrmTasksById";
        //    public static string GetCrmTasksLastCode = "GetCrmTasksLastCode";
        //    public static string InsertCrmTasks = "InsertCrmTasks";
        //    public static string UpdateCrmTasks = "UpdateCrmTasks";
        //}
        //public static class CrmTaskStatus
        //{

        //    public static string DeleteCrmTaskStatusById = "DeleteCrmTaskStatusById";
        //    public static string GetCrmTaskStatus = "GetCrmTaskStatus";
        //    public static string InsertCrmTaskStatus = "InsertCrmTaskStatus";
        //    public static string UpdateCrmTaskStatus = "UpdateCrmTaskStatus";
        //}
        //#endregion

        //#region View

        //public static class View
        //{
        //    public static string GetAnFvwTransactinalHeadByCmnCompanyId = "GetAnFvwTransactinalHeadByCmnCompanyId";
        //    public static string GetAnFvwCOAsByCmnCompanyId = "GetAnFvwCOAsByCmnCompanyId";
        //}
        //#endregion

        #region Common

        public static class Common
        {
            //public static string config_ClientAuthorization = "config_ClientAuthorization";
            //public static string config_getClientDatabase = "config_getClientDatabase";
            //public static string config_updateClientConnString = "config_updateClientConnString";
            //public static string GetAnFAccCtrlNotesDetails = "GetAnFAccCtrlNotesDetails";
            //public static string GetAnFAccCtrlNotes = "GetAnFAccCtrlNotes";
            //public static string GetAnFAccCtrlGL = "GetAnFAccCtrlGL";
            //public static string GetAnFAccCtrlTrialBalance = "GetAnFAccCtrlTrialBalance";
            //public static string GetAnFTransactinalHeadForMappingByCompanyId = "GetAnFTransactinalHeadForMappingByCompanyId";
            public static string GetAnFTransactinalHeadForMappingByProjectId = "GetAnFTransactinalHeadForMappingByProjectId";            
            //public static string CheckAnFTransactionByProject = "CheckAnFTransactionByProject";
            //public static string GetFxdAcquisitionCostNCurrentWdvByFxdAcquisitionId = "GetFxdAcquisitionCostNCurrentWdvByFxdAcquisitionId";
            //public static string MapAllProject = "MapAllProject";
            //public static string GetTableName = "GetTableName";
            //public static string GetSecPermittedCompanyByUserId = "GetSecPermittedCompanyByUserId";
            public static string GetSecPermittedModuleByUserId = "GetSecPermittedModuleByUserId";
            //public static string GetSecMenuByUserId = "GetSecMenuByUserId";
            //public static string GetSecButtonPermissionsByUserIdAndResourceName = "GetSecButtonPermissionsByUserIdAndResourceName";
            public static string GetSecResourceButtonPermission = "GetSecResourceButtonPermission";
            //public static string InvGenRefNo = "InvGenRefNo";
        }
        #endregion

        #region Report

        public static partial class Report
        {
            public static string RptMoneyReceipt = "RptGetAllSpSlsCollections";
            public static string RptSalesReturnById = "RptSalesReturnById";

            public static string RptAnFCostCentre = "RptAnFCostCentre";
            public static string RptSlsPartyList = "RptSlsPartyList";
            public static string RptSlsProductPrice = "RptSlsProductPrice";
            public static string GetAllProductList = "GetAllProductList";
            public static string RptAnFBalanceSheet = "RptAnFBalanceSheet";
            public static string RptGetAllFreeProducts = "RptGetAllFreeProducts";
            public static string RptSlsDailySales = "RptSlsDailySales";
            public static string RptGetChallenStatement = "RptGetChallenStatement";
            public static string RptSlsTargetNAchievement = "RptSlsTargetNAchievement";
            public static string GetSlsFieldVisits = "GetSlsFieldVisits";
            public static string GetAnfAdvanceList = "GetAnfAdvanceList";
            public static string RptAnFChartsofAccount = "RptAnFChartsofAccount";
            public static string RptGetInvoiceNo = "RptGetInvoiceNo";
            public static string RptSalesOrderList = "RptSalesOrderList";
            public static string RptStoreStockBalance = "RptStoreStockBalance";
            public static string RptAnFProfitLoss = "RptAnFProfitLoss";
            public static string RptAnFGeneralLedgerProject = "RptAnFGeneralLedgerProject";
            public static string RptAnFOpenningBalance = "RptAnFOpenningBalance";
            public static string RptAnFCostOfGoodsSold = "RptAnFCostOfGoodsSold";
            public static string RptSalesCommission = "RptSalesCommission";
            public static string RptPartyCredit = "RptPartyCredit";
            public static string RptSalesReturnList = "RptSalesReturnList";
            public static string RptCollectionReport = "RptCollectionReport";
            public static string RptPartyLedger = "RptPartyLedger";
            public static string RptSalesTargetCollection = "RptSalesTargetCollection";
            //public static string RptFinishGoodsDemandReceive = "RptFinishGoodsDemandReceive";
            //public static string RptFixedAssetSchedule = "RptFixedAssetSchedule";
            //public static string RptFrzFreezeInformation = "RptFrzFreezeInformation";
            //public static string RptFxdAssetRegister = "RptFxdAssetRegister";
            //public static string RptAnFGeneralLedger = "RptAnFGeneralLedger";
            //public static string RptHrmConfirmation = "RptHrmConfirmation";
            //public static string RptHrmEmployeeCV = "RptHrmEmployeeCV";
            //public static string RptHrmEmployeesLeaveBalance = "RptHrmEmployeesLeaveBalance";
            //public static string RptHrmScheduleAllShifts = "RptHrmScheduleAllShifts";
            //public static string RptHrmScheduleShiftStatement = "RptHrmScheduleShiftStatement";
            //public static string RptHrmScheduleStatement = "RptHrmScheduleStatement";
            //public static string RptHrmSummaryofSalaryIncreaseDecrease = "RptHrmSummaryofSalaryIncreaseDecrease";
            //public static string RptHrmSummaryofSalarySheet = "RptHrmSummaryofSalarySheet";
            //public static string RptInvInvoices = "RptInvInvoices";
            //public static string RptInvIssue = "RptInvIssue";
            //public static string RptInvItemList = "RptInvItemList";
            //public static string RptInvMRR = "RptInvMRR";
            //public static string RptInvMRRDetails = "RptInvMRRDetails";
            //public static string RptInvMRRList = "RptInvMRRList";
            //public static string RptInvMRList = "RptInvMRList";
            //public static string RptInvPRDetails = "RptInvPRDetails";
            //public static string RptInvPurchaseRequisition = "RptInvPurchaseRequisition";
            //public static string RptInvPurchaseRequisitionDetails = "RptInvPurchaseRequisitionDetails";
            //public static string RptInvPurchaseRequisitionsList = "RptInvPurchaseRequisitionsList";
            //public static string RptInvQualityControlDetails = "RptInvQualityControlDetails";
            //public static string RptInvQualityControls = "RptInvQualityControls";
            //public static string RptInvQualityControlsList = "RptInvQualityControlsList";
            //public static string RptInvStockRegisterByItemWise = "RptInvStockRegisterByItemWise";
            //public static string RptInvStoreRequisition = "RptInvStoreRequisition";
            //public static string RptJointParticipants = "RptJointParticipants";
            //public static string RptCrmLeads = "RptCrmLeads";
            //public static string RptMultipleDeliveryChallanPrint = "RptMultipleDeliveryChallanPrint";
            //public static string RptMultipleInvoicePrint = "RptMultipleInvoicePrint";
            //public static string RptMultipleSOPrint = "RptMultipleSOPrint";
            public static string RptAnFNoteSchedule = "RptAnFNoteSchedule";
            //public static string RptPrcPurchaseOrderDetails = "RptPrcPurchaseOrderDetails";
            //public static string RptPrcPurchaseOrders = "RptPrcPurchaseOrders";
            //public static string RptPrcPurchaseOrdersList = "RptPrcPurchaseOrdersList";
            //public static string RptPrcQuotationDetails = "RptPrcQuotationDetails";
            //public static string RptPrcQuotationList = "RptPrcQuotationList";
            //public static string RptPrcRFQ = "RptPrcRFQ";
            //public static string RptPrcRFQDetails = "RptPrcRFQDetails";
            //public static string RptPrdFinishedGoodsIssue = "RptPrdFinishedGoodsIssue";
            //public static string RptPrdFinishGoodsProduction = "RptPrdFinishGoodsProduction";
            //public static string RptPrdFinishGoodsStoreStockPosition = "RptPrdFinishGoodsStoreStockPosition";
            //public static string RptPrdItemReceive = "RptPrdItemReceive";
            //public static string RptPrequalifiedTenders = "RptPrequalifiedTenders";
            //public static string RptMonthlySalaryStatement = "RptMonthlySalaryStatement";
            //public static string RptPrlPaySlip = "RptPrlPaySlip";
            //public static string RptProductInfoByEffectiveDate = "RptProductInfoByEffectiveDate";
            //public static string RptSlsProductPriceList = "RptSlsProductPriceList";
            //public static string RptProductWiseDaySale = "RptProductWiseDaySale";
            public static string RptAnFNoteScheduleProject = "RptAnFNoteScheduleProject";
            //public static string RptProjectWiseOpeningBalances = "RptProjectWiseOpeningBalances";
            //public static string RptProjectWiseOpeningBalancesNew = "RptProjectWiseOpeningBalancesNew";
            public static string RptAnFTrialBalanceProjectWise = "RptAnFTrialBalanceProjectWise";
            //public static string RptRequisitionInfoById = "RptRequisitionInfoById";
            //public static string RptRFQList = "RptRFQList";
            //public static string RptSalesOrderStatement = "RptSalesOrderStatement";
            //public static string RptSDPBySDPType = "RptSDPBySDPType";
            //public static string RptSecUsersInformation = "RptSecUsersInformation";
            //public static string RptSO = "RptSO";
            //public static string RptStockReceiveSummary = "RptStockReceiveSummary";
            //public static string RptStockRegister = "RptStockRegister";
            //public static string RptStockStatement = "RptStockStatement";
            //public static string RptStockStatementSummary = "RptStockStatementSummary";
            //public static string RptStoreRequisitionDetails = "RptStoreRequisitionDetails";
            //public static string RptStoreRequisitionsIssueDetails = "RptStoreRequisitionsIssueDetails";
            //public static string RptStoreRequisitionsList = "RptStoreRequisitionsList";
            //public static string RptSubmittedTenders = "RptSubmittedTenders";
            //public static string RptSuppliers = "RptSuppliers";
            public static string RptAnFTrialBalanceDetails = "RptAnFTrialBalanceDetails";
            //public static string RptAnFTrialBalanceSummary = "RptAnFTrialBalanceSummary";
            //public static string RptUndeliveredSOList = "RptUndeliveredSOList";
            public static string RptAnFVoucherDetailsByVoucherId = "RptAnFVoucherDetailsByVoucherId";
            public static string RptAnFVoucherDetailsList = "RptAnFVoucherDetailsList";
            public static string RptAnFVoucherList = "RptAnFVoucherList";
            //public static string RptStoreRequisition = "RptStoreRequisition";
            //public static string RptPurchaseRequisition = "RptPurchaseRequisition";
            //public static string RptQualityControls = "RptQualityControls";
            //public static string RptMRR = "RptMRR";
            //public static string GetBnkLedgerByAccountNo = "GetBnkLedgerByAccountNo";
            //public static string GetBnkCheckBookSummaryByAccountNo = "GetBnkCheckBookSummaryByAccountNo";
            //public static string GetBnkTransactionByDate = "GetBnkTransactionByDate";
            //public static string GetBnkAccountBalance = "GetBnkAccountBalance";
            //public static string RptFxdLifetimeDepreciation = "RptFxdLifetimeDepreciation";
            //public static string RptFxdDepriciation = "RptFxdDepriciation";
            //public static string RptFxdYearlyDepriciation = "RptFxdYearlyDepriciation";
            //public static string GetHrmEmployeesList = "GetHrmEmployeesList";
            //public static string RptHrmEmployeesList = "RptHrmEmployeesList";
            //public static string GetHrmEmployeesLeaveDetailsR = "GetHrmEmployeesLeaveDetailsR";
            //public static string RptHrmMonthlyAttendanceStatement = "RptHrmMonthlyAttendanceStatement";
            //public static string RptSalarySummarySheet = "RptSalarySummarySheet";
            //public static string RptStoreStockPosition = "RptStoreStockPosition";
            //public static string RptProductionRawMeterialStockPosition = "RptProductionRawMeterialStockPosition";

            public static string RptPaymentVouchers = "RptPaymentVouchers";

            public static string RptReceiptVouchers = "RptReceiptVouchers";

            public static string RptAnFGeneralLedger = "RptAnFGeneralLedger";


            public static string RptReceivableReport = "RptReceivableReport";

            public static string RptPayableReport = "RptPayableReport";

            public static string RptAnFAdvanceAdjustmentReport = "RptAnFAdvanceAdjustmentReport";
        }

        #endregion
         
        #region Sales
        public static class Area
        {
            public static string GetSlsAreas = "GetSlsAreas";
            public static string DeleteConfiguration = "DeleteConfiguration";
            public static string GetAreaByEmployee = "GetAreaByEmployee";
            public static string GetAreaConfigurationByEmployee = "GetAreaConfigurationByEmployee";
            public static string GetAreaConfigurationByEmployeeId = "GetAreaConfigurationByEmployeeId";

        }
        public static class Region
        {
            public static string GetRegionByEmployee = "GetRegionByEmployee";
        }
        public static class SlsSalesReturn
        {
            public static string RptSalesReturnList = "RptSalesReturnList";
        }
        //public static class SlsFreeProduct
        //{
        //    public static string GetAllFreeProducts = "GetAllFreeProducts";
        //}
        public static class Thana
        {
            public static string GetSlsThanas = "GetSlsThanas";
            public static string GetThanaByEmployee = "GetThanaByEmployee";
            public static string GetThanasForDistrict = "GetThanasForDistrict";
            public static string GetThanasForOffice = "GetThanasForOffice";
            public static string GetThanasForRegion = "GetThanasForRegion";            
        }
        
        public static class District
        {
            public static string GetSlsDistricts = "GetSlsDistricts";
            public static string GetDistrictByEmployee = "GetDistrictByEmployee";
        }

        public static class Office
        {
            public static string GetAllSlsOffices = "GetAllSlsOffices";
            public static string GetOfficeByEmployee = "GetOfficeByEmployee";
        }
        
        public static class PromotionalOffer
        {
            public static string GetAllSlsPromotionalOffers = "GetAllSlsPromotionalOffers";
            public static string GetAllSlsPromotionalOfferDetails = "GetAllSlsPromotionalOfferDetails";
            public static int CategoryLevel = 1;
        }

        public static class CollectionData
        {
            public static string GetAllSpSlsCollections = "GetAllSpSlsCollections";
           // public static string GetAllSlsPromotionalOfferDetails = "GetAllSlsPromotionalOfferDetails";
            public static int CategoryLevel = 1;
        }
        public static class ProductPrice
        {
            public static string GetProductPriceByProductId = "GetProductPriceByProductId";
            // public static string GetProductPriceByEmployee = "GetProductPriceByEmployee";
        }

        public static class SalesOrder
        {
            public static string SOProductDetails = "SOProductDetails";
            public static string SOProductDetailsOffer = "SOProductDetailsOffer";
            public static string SOProductDetailsWithoutDiscount = "SOProductDetailsWithoutDiscount";
            public static string GetSOProductDetails = "GetSOProductDetails";
            public static string GetAllSlsSalesOrders = "GetAllSlsSalesOrders";
            public static string GetPartyCredit = "GetPartyCredit";
        }


        public static class CorporateClient
        {
            public static string GetSlsCorporateClients = "GetCorporateClients";
            public static string GetCorporateClientById = "GetCorporateClientId";
        }
        public static class GeneralDiscount
        {
            public static string GetSlsGeneralDiscounts = "GetSlsGeneralDiscounts";
        }
        public static class ProductDiscount
        {
            public static string GetSlsProductDiscountBySlsRegionIdNCategoryId = "GetSlsProductDiscountBySlsRegionIdNCategoryId";
        }
        public static class CollectionTarget
        {
            public static string GetCollectionTarget = "GetSlsCollectionTarget";
        }
        public static class SalesDelivery
        {
            public static string GetAllSlsDeliveryDetails = "GetAllSlsDeliveryDetails";
            public static string GetDetailsByOrder = "GetDetailsByOrder";
            public static string GetOrders = "GetOrders";

        }

        public static class GetProductDetails
        {
            public static string GetProductByIssue = "GetProductByIssue";
        }
        public static class GetProductRcvDelivery
        {
            public static string GetProductByDelivery = "GetProductByDelivery";
            public static string GetChallanList = "GetChallanList";
        }
        
        public static class GetProductReceiveDetails
        {
            public static string GetAllProductReceiveDetails = "GetAllProductReceiveDetails";
            public static string GetAllProductReceiveDistDetails = "GetAllProductReceiveDistDetails";
        }
                
        #endregion

        public static class DashboardChart
        {
            public static string RegionWiseSales = "RegionWiseSales";
        }
    }
 }

