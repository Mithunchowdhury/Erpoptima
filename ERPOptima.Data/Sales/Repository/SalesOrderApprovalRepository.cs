using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface ISalesOrderApprovalRepository : IRepository<SlsSalesOrderApproval>
    {
        IList<SlsSalesOrderApproval> GetAll();
        int AddEntity(SlsSalesOrderApproval obj);
        SlsSalesOrderApproval GetById(int id);
        int SaveChanges();
    }
    public class SalesOrderApprovalRepository : BaseRepository<SlsSalesOrderApproval>, ISalesOrderApprovalRepository
    {
        public SalesOrderApprovalRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<SlsSalesOrderApproval> GetAll()
        {
            return DataContext.SlsSalesOrderApprovals.ToList();
        }
        public int AddEntity(SlsSalesOrderApproval obj)
        {
            int Id = 1;
            SlsSalesOrderApproval last = DataContext.SlsSalesOrderApprovals.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }
        public SlsSalesOrderApproval GetById(int id)
        {
            return DataContext.SlsSalesOrderApprovals.Where(x => x.Id == id).FirstOrDefault();
        }
        public int SaveChanges()
        {
            return base.DataContext.SaveChanges();
        }
    }
}
