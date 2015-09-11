using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Model.ViewModel
{
    public class SlsNotificationViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string URL { get; set; }
        public string Type { get; set; }
        public System.DateTime Date { get; set; }


        //This will be set from each type - individually
        public string NotificationType { get; set; }
        //To display in Grid - that this notification is already read
        public bool IsRead { get; set; }
    }
}
