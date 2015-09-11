using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Inventory.Repository
{
    public interface IRequisitionApprovalRepository : IRepository<InvRequisitionApproval>
    {
        IList<InvRequisitionApproval> GetAll();
        int AddEntity(InvRequisitionApproval obj);
        InvRequisitionApproval GetById(int id);
        int SaveChanges();
    }
    public class RequisitionApprovalRepository : BaseRepository<InvRequisitionApproval>, IRequisitionApprovalRepository
    {
        public RequisitionApprovalRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<InvRequisitionApproval> GetAll()
        {
            return DataContext.InvRequisitionApprovals.ToList();
        }
        public int AddEntity(InvRequisitionApproval obj)
        {
            int Id = 1;
            InvRequisitionApproval last = DataContext.InvRequisitionApprovals.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }
        public InvRequisitionApproval GetById(int id)
        {
            return DataContext.InvRequisitionApprovals.Where(x => x.Id == id).FirstOrDefault();
        }
        public int SaveChanges()
        {
            return base.DataContext.SaveChanges();
        }
    }

    public class InvRequisitionViewModel
    {
        public int Id { get; set; }
        public string RequisitionCode { get; set; }
        public System.DateTime PreferredDeliveryDate { get; set; }
        public Nullable<int> SecCompanyId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> ApprovalStatus { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }


        public string CompanyName { get; set; }
        public string StatusName { get; set; }
    }
}
