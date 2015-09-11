using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface IRoutePlanDetailRepository
    {
        void AddEntities(List<SlsRoutePlanDetail> records);
        IList<RoutePlanDetail> GetByRoutePlanId(int RoutePlanId);
    }
    public class RoutePlanDetailRepository : BaseRepository<SlsRoutePlanDetail>, IRoutePlanDetailRepository
    {
        public RoutePlanDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public void AddEntities(List<SlsRoutePlanDetail> records)
        {
            int Id = 0;
            SlsRoutePlanDetail last = DataContext.SlsRoutePlanDetails.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            foreach (SlsRoutePlanDetail obj in records)
            {
                if (obj.SlsRouteId != null)
                {
                    if (obj.Id <= 0)
                    {
                        Id++;
                        obj.Id = Id;
                        base.Add(obj);
                    }
                }
            }
        }

        public IList<RoutePlanDetail> GetByRoutePlanId(int RoutePlanId)
        {

            var list = DataContext.SlsRoutePlanDetails
                .Join(DataContext.SlsRoutes
                    , r => r.SlsRouteId
                    , p => p.Id
                    , (r, p) => new RoutePlanDetail()
                {
                    Id = r.Id,
                    SlsRoutePlanId = r.SlsRoutePlanId,
                    Date = r.Date,
                    SlsRouteId = r.SlsRouteId,
                    RouteName = p.Name
                })
                .Where(req => req.SlsRoutePlanId == RoutePlanId).ToList();

            
            return list;

        }













    }
}
