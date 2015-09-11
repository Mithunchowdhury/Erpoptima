using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    
    public interface  IFieldVisitListRepository: IRepository<SlsFieldVisit>
    {
        IList<SlsFieldVisit> GetAll(int employeeId );
        SlsFieldVisit GetById(long nullable);
        int AddEntity(SlsFieldVisit objSlsFieldVisit);
    }



    public class FieldVisitListRepository : BaseRepository<SlsFieldVisit>, IFieldVisitListRepository
    {
        public FieldVisitListRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsFieldVisit> GetAll(int employeeId)
        {
            return DataContext.SlsFieldVisits.ToList();
        }


        public SlsFieldVisit GetById(long nullable)
        {
            var a = from v in DataContext.SlsFieldVisits
                     where v.Id == nullable
                   select v;
            return DataContext.SlsFieldVisits.Where(x => x.Id == nullable).FirstOrDefault();
        }
        public int AddEntity(SlsFieldVisit objSlsFieldVisit)
        {
            int Id = 1;
            SlsFieldVisit last = DataContext.SlsFieldVisits.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsFieldVisit.Id = Id;
            base.Add(objSlsFieldVisit);
            return Id;

        }

    }
}
