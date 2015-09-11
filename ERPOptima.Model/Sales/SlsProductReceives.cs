using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Sales
{
    public partial class SlsProductReceive
    {
        public SlsProductReceive()
        {
            this.SlsProductReceiveDetails = new List<SlsProductReceiveDetail>();
        }

        public int Id { get; set; }
        public int SlsDeliveryId { get; set; }        
        public string ChallanNo { get; set; }
        public string InvoiceNo { get; set; }
        public string VehicleNo { get; set; }
        public System.DateTime RcvDate { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public virtual ICollection<SlsProductReceiveDetail> SlsProductReceiveDetails { get; set; }
        public virtual SlsDelivery SlsDelivery { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
    }
}
