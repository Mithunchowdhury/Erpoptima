using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Accounts.Repository
{
    public interface IAnfAdvanceAdjustmentRepository : IRepository<AnFAdjustment>
    {
        IList<AnFAdjustment> GetAll(int companyId, int financialYearId);
        IList<AnFAdjustment> GetAllByAdvanceId(int AdvanceId);
        IList<AnFAdjustment> GetAllWithoutId();
        int AddEntity(AnFAdjustment objadvanceEntry);
    }
    public class AnfAdvanceAdjustmentRepository : BaseRepository<AnFAdjustment>, IAnfAdvanceAdjustmentRepository
    {
        public AnfAdvanceAdjustmentRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<AnFAdjustment> GetAll(int companyId, int financialYearId)
        {
            return DataContext.AnFAdjustments.Include("AnFAdvance").Include("AnFAdvance.HrmEmployee").ToList();
            //IList<AnFAdjustment> list = new List<AnFAdjustment>();
            //return list;
        }
        public IList<AnFAdjustment> GetAllByAdvanceId(int AdvanceId)
        {
            return DataContext.AnFAdjustments.Include("AnFAdvance").Include("AnFAdvance.HrmEmployee").Where(t => t.AnFAdvanceId == AdvanceId).ToList();
        }
        public IList<AnFAdjustment> GetAllWithoutId()
        {
            return DataContext.AnFAdjustments.Include("AnFAdvance").Include("AnFAdvance.HrmEmployee").ToList();
        }

        public int AddEntity(AnFAdjustment objadvanceEntry)
        {
            int Id = 1;
            AnFAdjustment last = DataContext.AnFAdjustments.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objadvanceEntry.Id = Id;
            base.Add(objadvanceEntry);
            return Id;
        }


    }
}
