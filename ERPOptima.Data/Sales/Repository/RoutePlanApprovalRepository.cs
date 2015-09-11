using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface IRoutePlanApprovalRepository : IRepository<SlsRoutePlanApproval>
    {
        IList<SlsRoutePlanApproval> GetAll();
        int AddEntity(SlsRoutePlanApproval obj);
        SlsRoutePlanApproval GetById(int id);
        int SaveChanges();
    }
    public class RoutePlanApprovalRepository : BaseRepository<SlsRoutePlanApproval>, IRoutePlanApprovalRepository
    {
        public RoutePlanApprovalRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<SlsRoutePlanApproval> GetAll()
        {
            return DataContext.SlsRoutePlanApprovals.ToList();
        }
        public int AddEntity(SlsRoutePlanApproval obj)
        {
            int Id = 1;
            SlsRoutePlanApproval last = DataContext.SlsRoutePlanApprovals.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }
        public SlsRoutePlanApproval GetById(int id)
        {
            return DataContext.SlsRoutePlanApprovals.Where(x => x.Id == id).FirstOrDefault();
        }
        public int SaveChanges()
        {
            return base.DataContext.SaveChanges();
        }
    }
}
