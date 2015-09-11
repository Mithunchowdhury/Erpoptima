using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Inventory
{
    public partial class InvDamageDetail
    {
        public int Id { get; set; }
        public int InvDamageId { get; set; }
        public int SlsProductId { get; set; }
        public decimal Quantity { get; set; }
        public int SlsUnitsId { get; set; }
        public string Reason { get; set; }
     
        public virtual InvDamage InvDamage { get; set; }
      
    }


    public class DamageDetail
    {

        public int Id { get; set; }
        public int InvDamageId { get; set; }
        public int SlsProductId { get; set; }
        public decimal Quantity { get; set; }
        public int SlsUnitsId { get; set; }
        public string Reason { get; set; }      
        public string ProductName { get; set; }
        public string UnitName { get; set; }




    }
}
