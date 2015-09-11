using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface ISalesReturnDetailRepository : IRepository<SlsSalesReturnDetail>
    {
        int AddEntity(SlsSalesReturnDetail obj);
        int AddEntityList(IList<SlsSalesReturnDetail> list);
        int SaveChanges();
    }
    public class SalesReturnDetailRepository : BaseRepository<SlsSalesReturnDetail>, ISalesReturnDetailRepository
    {
        public SalesReturnDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public int AddEntity(SlsSalesReturnDetail obj)
        {
            int Id = 1;
            SlsSalesReturnDetail last = DataContext.SlsSalesReturnDetails.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }

        public int AddEntityList(IList<SlsSalesReturnDetail> list)
        {
            int Id = 0;
            SlsSalesReturnDetail last = DataContext.SlsSalesReturnDetails.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id;
            }
            foreach (SlsSalesReturnDetail obj in list)
            {
                if (obj.Id <= 0)
                {
                    Id++;
                    obj.Id = Id;
                    base.Add(obj);
                }
            }
            return Id;
        }
        public int SaveChanges()
        {
            return base.DataContext.SaveChanges();
        }
    }
}
