using ERPOptima.Model.HRM;
using ERPOptima.Model.Security;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsFieldVisit
    {
        public int Id { get; set; }
        public string RefNo { get; set; }
        public int HrmEmployeeId { get; set; }
        public System.DateTime VisitDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDetails { get; set; }
        public string CustomerMobileNo { get; set; }
        public string VisitDetails { get; set; }
        public Nullable<System.DateTime> FollowupDate { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual HrmEmployee HrmEmployee { get; set; }
        public virtual SecUser SecUser { get; set; }
        public virtual SecUser SecUser1 { get; set; }
    }


    public class FieldVisitList
    {       
        public string RefNo { get; set; }       
        public System.DateTime VisitDate { get; set; }
        public string CustomerName { get; set; }
        public string Details { get; set; }
        public string CustomerMobileNo { get; set; }
        public string VisitDetails { get; set; }
        
        public string Visitedby { get; set; }
       
    }




}
