using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales
{
    

    public interface IFieldVisitRepository : IRepository<SlsFieldVisit>
    {
        int AddEntity(SlsFieldVisit obj);
        int getAutoNumber();
    }
    public class FieldVisitRepository : BaseRepository<SlsFieldVisit>, IFieldVisitRepository
    {

        public FieldVisitRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public int AddEntity(SlsFieldVisit objFieldVisit)
        {
            int Id = 1;
            SlsFieldVisit last = DataContext.SlsFieldVisits.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            objFieldVisit.Id = Id;
            base.Add(objFieldVisit);
            return Id;
        }

        public int getAutoNumber()
        {
            int SL = 1;
            SlsFieldVisit last = null;
            try
            {
                last = DataContext.SlsFieldVisits.OrderByDescending(x => x.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            if (last != null)
            {
                SL = int.Parse(last.RefNo.Split('/')[1]) + 1;

            }
            return SL;
           
        }


        public SlsFieldVisit GetById(int id)
        {
            return DataContext.SlsFieldVisits.Where(x => x.Id == id).FirstOrDefault();
        }
    }

}
