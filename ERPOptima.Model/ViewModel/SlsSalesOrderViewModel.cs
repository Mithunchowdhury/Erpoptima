using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.ViewModel
{
    public partial class SlsSalesOrderViewModel
    {
        public int Id { get; set; }
        public string RefNo { get; set; }
        public Nullable<int> SlsOfficeId { get; set; }
        public int PartyType { get; set; }
        public Nullable<int> Party { get; set; }
        public System.DateTime PreferredDeliveryDate { get; set; }
        public Nullable<int> SlsCorporateSalesApplicationId { get; set; }
        public int TransactionType { get; set; }
        public string TransactionRefNo { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public Nullable<decimal> Advance { get; set; }
        public string BankName { get; set; }
        public string Remarks { get; set; }
        public int SecCompnayId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> ApprovalStatus { get; set; }
        public Nullable<int> SalesType { get; set; }
        public string OptionalPartyName { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<int> HrmEmployeeId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }


        public string PartyName { get; set; }
        public IList<SlsSalesOrderDetailViewModel> SalesOrderDetails { get; set; }

    }
    public partial class SlsSalesOrderDetailViewModel
    {
        public int Id { get; set; }
        public int SlsSalesOrderId { get; set; }
        public int SlsProductId { get; set; }
        public decimal SalesOrderQuantity { get; set; }
        public int SlsUnitId { get; set; }
        public decimal Rate { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }


        public string SlsProductName { get; set; }
        public string SlsUnitName { get; set; }
    }

    public partial class ApprovalStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    //public partial class RetailSalesViewModel
    //{
    //    public int Id { get; set; }
    //    public string RefNo { get; set; }
    //    public string PartyName { get; set; }
    //    public System.DateTime PreferredDeliveryDate { get; set; }
    //    public decimal Discount { get; set; }
    //    public decimal Total { get; set; }
    //    public string InvoiceNo { get; set; }
    //    public string ChallanNo { get; set; }
    //}
}
