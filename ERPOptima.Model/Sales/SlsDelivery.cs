using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Sales
{
    public partial class SlsDelivery
    {
        public SlsDelivery()
        {
            this.SlsDeliverDetails = new List<SlsDeliverDetail>();
        }

        public int Id { get; set; }
        public Nullable<int> SlsSalesOrderId { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string ChallanNo { get; set; }
        public string InvoiceNo { get; set; }
        public string VehicleNo { get; set; }
        public string Remarks { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<int> ReceivedStatus { get; set; }
        public Nullable<DateTime> ReceivedDate { get; set; }
        public string ReceivedRemarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }

        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ICollection<SlsDeliverDetail> SlsDeliverDetails { get; set; }
        public virtual SlsSalesOrder SlsSalesOrder { get; set; }

        public virtual ICollection<SlsProductReceive> SlsProductReceives { get; set; }
    }

    public class ChallanList
    {

        public int Id { get; set; }
        public string ChallanNo { get; set; }

        public string InvoiceNo { get; set; }

    }





}
