using System;
using System.Collections.Generic;

namespace ERPOptima.Model.Sales
{
    public partial class SlsRoutePlanDetail
    {
        public int Id { get; set; }
        public int SlsRoutePlanId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> SlsRouteId { get; set; }
        public virtual SlsRoutePlan SlsRoutePlan { get; set; }
        public virtual SlsRoute SlsRoute { get; set; }
    }


    public class RoutePlanDetail
    {
        public int Id { get; set; }
        public int SlsRoutePlanId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> SlsRouteId { get; set; }
        public string RouteName { get; set; }
    }






}
