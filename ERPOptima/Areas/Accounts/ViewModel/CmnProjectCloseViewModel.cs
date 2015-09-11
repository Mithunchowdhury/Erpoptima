using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Accounts.ViewModel
{
    public class CmnProjectCloseViewModel
    {
       
        public int Id { get; set; }
        public int CmnCompanyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int ClosingStatus { get; set; }
        public string ClosingNote { get; set; }
    }
}