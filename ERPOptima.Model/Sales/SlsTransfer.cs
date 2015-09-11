using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Sales
{
    public class SlsTransfer
    {
        public SlsTransfer()
        {
            this.SlsTransferDetails = new List<SlsTransferDetail>();
        }

        public int Id { get; set; }
        public string RefNo { get; set; }
        public int From { get; set; }
        public int FromInvStoreId { get; set; }
        public int To { get; set; }
        public int ToInvStoreId { get; set; }
        public System.DateTime Date { get; set; }
        public string VehicleNo { get; set; }
        public string ChallenNo { get; set; }
        public string GatepassNo { get; set; }
        public int SecCompanyId { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual ERPOptima.Model.Inventory.InvStore InvStore { get; set; }
        public virtual ERPOptima.Model.Inventory.InvStore InvStore1 { get; set; }
        public virtual ERPOptima.Model.Security.SecCompany SecCompany { get; set; }
        public virtual SlsOffice SlsOffice { get; set; }
        public virtual SlsOffice SlsOffice1 { get; set; }
        public virtual ICollection<SlsTransferDetail> SlsTransferDetails { get; set; }
    }











}
