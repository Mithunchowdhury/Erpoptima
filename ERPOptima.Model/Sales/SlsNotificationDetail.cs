using ERPOptima.Model.HRM;
using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsNotificationDetail
    {
        public int Id { get; set; }
        public int SlsNotificationId { get; set; }
        public int HrmEmployeeId { get; set; }
        public System.DateTime Date { get; set; }
        public bool IsRead { get; set; }
        public virtual HrmEmployee HrmEmployee { get; set; }
        public virtual SlsNotification SlsNotification { get; set; }
    }
}
