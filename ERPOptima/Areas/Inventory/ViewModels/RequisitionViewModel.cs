using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optima.Areas.Inventory.ViewModels
{
    public class RequisitionViewModel
    {

        public int Id { get; set; }
        public string RequisitionCode { get; set; }
        public System.DateTime PreferredDeliveryDate { get; set; }
        public Nullable<int> SecCompanyId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> ApprovalStatus { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }     
        public Nullable<int> ModifiedBy { get; set; }

        




    }
}