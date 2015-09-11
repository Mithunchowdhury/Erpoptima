using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface ICorporateSalesDetailRepository : IRepository<SlsCorporateSalesApplicationDetail>
    {
        IList<CorporateSalesDetail> GetByCorporateSalesId(int CorporateSalesId);
        void AddEntityList(List<SlsCorporateSalesApplicationDetail> list);
        int SaveChanges();

    }
    public class CorporateSalesDetailRepository : BaseRepository<SlsCorporateSalesApplicationDetail>, ICorporateSalesDetailRepository
    {
        public CorporateSalesDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public void AddEntityList(List<SlsCorporateSalesApplicationDetail> list)
        {
            int Id = 0;
            SlsCorporateSalesApplicationDetail last = DataContext.SlsCorporateSalesApplicationDetails.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id;
            }
            foreach (SlsCorporateSalesApplicationDetail obj in list)
            {
                if (obj.Id <= 0)
                {
                    Id++;
                    obj.Id = Id;
                    base.Add(obj);
                }
            }
        }

        public int SaveChanges()
        {
            return base.DataContext.SaveChanges();
        }


        public IList<CorporateSalesDetail> GetByCorporateSalesId(int CorporateSalesId)
        {
            var list = DataContext.SlsCorporateSalesApplicationDetails
                .Join(DataContext.SlsProducts
                    , r => r.SlsProductId
                    , p => p.Id
                    , (r, p) =>  new CorporateSalesDetail()
                {
                    Id = r.Id,
                    SlsCorporateSalesApplicationId = r.SlsCorporateSalesApplicationId,
                    SlsProductId = r.SlsProductId,
                    AppliedPercentage = r.AppliedPercentage,
                    ApprovedPercentage = r.ApprovedPercentage,
                    ProductName = p.Name
                    
                })
                .Where(req => req.SlsCorporateSalesApplicationId == CorporateSalesId).ToList();
            
            return list;

        }






    }
}
