using ERPOptima.Model.Accounts;
using ERPOptima.Model.Common;
using ERPOptima.Model.Sales;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.HRM
{
    public partial class HrmEmployee
    {
        public HrmEmployee()
        {

            this.HrmEmployees1 = new List<HrmEmployee>();
            this.SlsAreaConfigurations = new List<SlsAreaConfiguration>();
            this.SlsCollectionTargets = new List<SlsCollectionTarget>();
            this.SlsCorporateSalesApplications = new List<SlsCorporateSalesApplication>();
            this.SlsFieldVisits = new List<SlsFieldVisit>();
            this.SlsIncentives = new List<SlsIncentive>();
            this.SlsNotificationDetails = new List<SlsNotificationDetail>();
            this.SlsOffices = new List<SlsOffice>();
            this.SlsRegions = new List<SlsRegion>();
            this.SlsRoutePlans = new List<SlsRoutePlan>();
            this.SlsSalesTargets = new List<SlsSalesTarget>();
            this.AnFAdvances = new List<AnFAdvance>(); 
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> HrmDesignationId { get; set; }
        public Nullable<int> HrmDepartmentId { get; set; }
        public Nullable<int> SecCompanyId { get; set; }
        public Nullable<int> LineManager { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Nullable<int> SlsOfficeId { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> SlsDistributorId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual HrmDepartment HrmDepartment { get; set; }
        public virtual HrmDesignation HrmDesignation { get; set; }
        public virtual ICollection<HrmEmployee> HrmEmployees1 { get; set; }
        public virtual HrmEmployee HrmEmployee1 { get; set; }
        public virtual SecCompany SecCompany { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
        public virtual ICollection<SlsAreaConfiguration> SlsAreaConfigurations { get; set; }
        public virtual ICollection<SlsCollectionTarget> SlsCollectionTargets { get; set; }
        public virtual ICollection<SlsCorporateSalesApplication> SlsCorporateSalesApplications { get; set; }
        public virtual ICollection<SlsFieldVisit> SlsFieldVisits { get; set; }
        public virtual ICollection<SlsIncentive> SlsIncentives { get; set; }
        public virtual ICollection<SlsNotificationDetail> SlsNotificationDetails { get; set; }
        public virtual ICollection<SlsOffice> SlsOffices { get; set; }
        public virtual ICollection<SlsRegion> SlsRegions { get; set; }
        public virtual ICollection<SlsRoutePlan> SlsRoutePlans { get; set; }
        public virtual ICollection<SlsSalesTarget> SlsSalesTargets { get; set; }
        public virtual ICollection<AnFAdvance> AnFAdvances { get; set; }
    }
}
