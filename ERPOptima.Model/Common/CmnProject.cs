using ERPOptima.Model.Accounts;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Common
{
    public partial class CmnProject
    {
        public CmnProject()
        {
            //this.AnFOpeningBalances = new List<AnFOpeningBalance>();
            
             
           
        }
        
        public int Id { get; set; }
        
        public Nullable<int> CmnBusinessesId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Prefix { get; set; }
        public Nullable<int> HrmEmployeeId { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public Nullable<int> Status { get; set; }
        public string Description { get; set; }
        public Nullable<int> ClosingStatus { get; set; }
        public Nullable<int> ClosedBy { get; set; }
        public Nullable<System.DateTime> ClosingDate { get; set; }
        public string ClosingNote { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        //public virtual ICollection<AnFOpeningBalance> AnFOpeningBalances { get; set; }
        

        public virtual CmnBusiness CmnBusiness { get; set; }
       
    }
}
