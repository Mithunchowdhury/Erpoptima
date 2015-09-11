using ERPOptima.Model.Sales;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Inventory
{
    public partial class InvIssueDetail
    {
        public int Id { get; set; }
        public int InvIssueId { get; set; }
        public int SlsProductId { get; set; }
        public decimal RequiredQuantity { get; set; }
        public decimal IssuedQuantity { get; set; }
        public int SlsUnitId { get; set; }      
        public virtual InvIssue InvIssue { get; set; }
        public virtual SlsProduct SlsProduct { get; set; }
        public virtual SlsUnit SlsUnit { get; set; }
      
    }

    public class InvIssueDetails
    {   
        public int Id { get; set; }
        public int InvIssueId { get; set; }
        public int SlsProductId { get; set; }
        public decimal RequiredQuantity { get; set; }
        public decimal PrevIssedQuantity { get; set; }
        public decimal PendingQuantity { get; set; }
        public decimal IssuedQuantity { get; set; }
        public int SlsUnitId { get; set; }
        public string ProductName { get; set; }
        public string UnitName { get; set; }

    }





}
