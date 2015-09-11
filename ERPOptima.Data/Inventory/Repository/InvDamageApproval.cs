using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Inventory.Repository
{    
    public interface IInvDamageApprovalRepository : IRepository<InvDamageApproval>
    {
        IList<InvDamageApproval> GetAll();
        int AddEntity(InvDamageApproval obj);
        InvDamageApproval GetById(int id);
        int SaveChanges();
    }
    public class InvDamageApprovalRepository : BaseRepository<InvDamageApproval>, IInvDamageApprovalRepository
    {
        public InvDamageApprovalRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<InvDamageApproval> GetAll()
        {
            return DataContext.InvDamageApprovals.ToList();
        }
        public int AddEntity(InvDamageApproval obj)
        {
            int Id = 1;
            InvDamageApproval last = DataContext.InvDamageApprovals.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }
        public InvDamageApproval GetById(int id)
        {
            return DataContext.InvDamageApprovals.Where(x => x.Id == id).FirstOrDefault();
        }
        public int SaveChanges()
        {
            return base.DataContext.SaveChanges();
        }
    }
}
