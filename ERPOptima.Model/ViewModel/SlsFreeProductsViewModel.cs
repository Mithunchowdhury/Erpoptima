using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.ViewModel
{
    public partial class SlsFreeProductsViewModel
    {
        public int Id { get; set; }
        public int SlsProductId { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public int MeasurementQuantity { get; set; }
        public int SlsUnitId { get; set; }
        public int FreeQuantity { get; set; }
        public int FreeUnitId { get; set; }
        public string Remarks { get; set; }
        public int SecCompnayId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        //view model
        public string SlsProductName { get; set; }
        public string SlsUnitName { get; set; }
        public string FreeUnitName { get; set; }

    }
}
