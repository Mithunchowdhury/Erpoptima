using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsNotification
    {
        public SlsNotification()
        {
            this.SlsNotificationDetails = new List<SlsNotificationDetail>();
        }

        public int Id { get; set; }
        public string Message { get; set; }
        public string URL { get; set; }
        public string Type { get; set; }
        public System.DateTime Date { get; set; }
        public virtual ICollection<SlsNotificationDetail> SlsNotificationDetails { get; set; }
    }
}
