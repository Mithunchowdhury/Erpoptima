using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Accounts.ViewModel
{
    public class CodeGenerationViewModel
    {
        public string ParentCode { get; set; }

        public string ChildCode { get; set; }
        public int Level { get; set; }
        public bool IsLastNode { get; set; }
    }
}