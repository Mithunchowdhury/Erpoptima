using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface ISalesOrderDetailRepository : IRepository<SlsSalesOrderDetail>
    {
        IList<SlsSalesOrderDetail> GetAll();
        int AddEntity(SlsSalesOrderDetail obj);
        int AddEntityList(IList<SlsSalesOrderDetail> list);
        SlsSalesOrderDetail GetById(int id);
        int SaveChanges();
        IList<SalesOrderDetail> GetBySalesOrderId(int SalesOrderId);
    }
    public class SalesOrderDetailRepository : BaseRepository<SlsSalesOrderDetail>, ISalesOrderDetailRepository
    {
        public SalesOrderDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<SlsSalesOrderDetail> GetAll()
        {
            return DataContext.SlsSalesOrderDetails.ToList();
        }
        public int AddEntity(SlsSalesOrderDetail obj)
        {
            int Id = 1;
            SlsSalesOrderDetail last = DataContext.SlsSalesOrderDetails.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }

        public int AddEntityList(IList<SlsSalesOrderDetail> list)
        {
            int Id = 0;
            SlsSalesOrderDetail last = DataContext.SlsSalesOrderDetails.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id;                             
            }
            foreach (SlsSalesOrderDetail obj in list)
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

        public SlsSalesOrderDetail GetById(int id)
        {
            return DataContext.SlsSalesOrderDetails.Where(x => x.Id == id).FirstOrDefault();
        }
        public int SaveChanges()
        {
            try
            {
                return base.DataContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }



        


        public IList<SalesOrderDetail> GetBySalesOrderId(int SalesOrderId)
        {


            var list = DataContext.SlsSalesOrderDetails
                .Join(DataContext.SlsProducts
                    , r => r.SlsProductId
                    , p => p.Id
                    , (r, p) => new { r, p })
                .Join(DataContext.SlsUnits
                , rp => rp.r.SlsUnitId
                , u => u.Id
                , (rp, u) => new SalesOrderDetail()
                {
                    Id = rp.r.Id,
                    SlsSalesOrderId = rp.r.SlsSalesOrderId,
                    SlsProductId = rp.r.SlsProductId,
                    SalesOrderQuantity = rp.r.SalesOrderQuantity,
                    SlsUnitId = rp.r.SlsUnitId,
                    Rate=rp.r.Rate,
                    Price = rp.r.Price,
                    Discount = rp.r.Discount,
                    Total = rp.r.Total,
                    ProductName = rp.p.Name,
                    UnitName = u.ShortName
                   
                })
                .Where(req => req.SlsSalesOrderId == SalesOrderId).ToList();




            return list;

        }









    }

    
}
