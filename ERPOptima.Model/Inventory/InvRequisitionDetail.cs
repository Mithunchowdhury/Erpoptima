using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Inventory
{
    public partial class InvRequisitionDetail
    {
        public int Id { get; set; }
        public int InvRequisitionId { get; set; }
        public int SlsProductId { get; set; }
        public decimal RequiredQuantity { get; set; }
        public int SlsUnitId { get; set; }       
        public virtual InvRequisition InvRequisition { get; set; }
        public virtual SlsProduct SlsProduct { get; set; }
        public virtual SlsUnit SlsUnit { get; set; }
    }

    public class ReqDetail
    {

        public int Id { get; set; }
        public int InvRequisitionId { get; set; }
        public int SlsProductId { get; set; }
        public decimal RequiredQuantity { get; set; }
        public int SlsUnitId { get; set; }
        public string ProductName { get; set; }
        public string UnitName { get; set; }



    }








}
