using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{   
    public interface ISlsProductReceiveDetailRepository : IRepository<SlsProductReceiveDetail>
    {
        IList<SlsProductReceiveDetail> GetAll(int companyId);
        int AddEntity(SlsProductReceiveDetail obj);
        SlsProductReceiveDetail GetById(int id);

    }
    public class SlsProductReceiveDetailRepository : BaseRepository<SlsProductReceiveDetail>, ISlsProductReceiveDetailRepository
    {
        public SlsProductReceiveDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<SlsProductReceiveDetail> GetAll(int companyId)
        {

            return DataContext.SlsProductReceiveDetails.ToList();

        }

        public int AddEntity(SlsProductReceiveDetail obj)
        {
            int Id = 1;
            SlsProductReceiveDetail last = null;
            try
            {
                last = DataContext.SlsProductReceiveDetails.OrderByDescending(x => x.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //Possibly can occur when no data exists in table.
            }
            if (last != null)
            {
                Id = last.Id + 1;

            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }

        public SlsProductReceiveDetail GetById(int id)
        {
            return DataContext.SlsProductReceiveDetails.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
