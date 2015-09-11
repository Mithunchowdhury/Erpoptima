using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Optima.Areas.Sales.ViewModel
{
    public class AreaConfigurationViewModel  
    {
        public Nullable<int> Id { get; set; }
        public Nullable<int> HrmEmployeeId { get; set; }
        public Nullable<bool> IsRegionBased { get; set; }
        public Nullable<bool> IsOfficeBased { get; set; }
        public Nullable<bool> IsDistrictBased { get; set; }
        public Nullable<bool> IsThanaBased { get; set; }
        public Nullable<bool> IsAreaBased { get; set; }
        public Nullable<Int32> SlsRegionId { get; set; }
        public Nullable<Int32> SlsOfficeId { get; set; }
        public Nullable<Int32> SlsDistrictId { get; set; }
        public Nullable<Int32> SlsThanaId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int RefId { get; set; }
        public Nullable<bool> Status { get; set; }

        Collection<SlsAreaConfigurationDetail> details = null;
        public Collection<SlsAreaConfigurationDetail> AreaConfigurationDetails
        {
            get
            {
                if (details == null)
                {
                    details = new Collection<SlsAreaConfigurationDetail>();
                }
                return details;
            }
            set
            {
                details = value;
            }
        }

    }
}