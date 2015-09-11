using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.Security
{
    public partial class SecFile
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TableName { get; set; }
        public int RefId { get; set; }
        public string URL { get; set; }
        public System.DateTime Date { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
