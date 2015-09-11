using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface ICorporateSalesApprovalRepository : IRepository<SlsCorporateSalesApproval>
    {
        IList<SlsCorporateSalesApproval> GetAll();
        int AddEntity(SlsCorporateSalesApproval obj);
        SlsCorporateSalesApproval GetById(int id);
        int SaveChanges();
    }
    public class CorporateSalesApprovalRepository : BaseRepository<SlsCorporateSalesApproval>, ICorporateSalesApprovalRepository
    {
        public CorporateSalesApprovalRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<SlsCorporateSalesApproval> GetAll()
        {
            return DataContext.SlsCorporateSalesApprovals.ToList();
        }
        public int AddEntity(SlsCorporateSalesApproval obj)
        {
            int Id = 1;
            SlsCorporateSalesApproval last = DataContext.SlsCorporateSalesApprovals.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }
        public SlsCorporateSalesApproval GetById(int id)
        {
            return DataContext.SlsCorporateSalesApprovals.Where(x => x.Id == id).FirstOrDefault();
        }
        public int SaveChanges()
        {
            return base.DataContext.SaveChanges();
        }


    }
}
