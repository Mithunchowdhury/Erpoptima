using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Accounts.ViewModel
{
    public class AnFTransactionalHeadViewModel
    {

        public long Id { get; set; }
          
        public string Code { get; set; }
        public string Name { get; set; }

        public long OpeningBalanceId { get; set; }
        public bool Mapped { get; set; }

        
    }
}