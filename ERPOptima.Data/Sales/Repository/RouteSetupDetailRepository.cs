using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    #region Interface

    public interface IRouteSetupDetailRepository : IRepository<SlsRouteDetail>
    {
        IList<SlsRouteDetail> GetAll();
        int AddEntity(SlsRouteDetail obj);
        SlsRouteDetail GetById(int id);
        int AddEntityList(IList<SlsRouteDetail> list);
        int SaveChanges();
    }

    #endregion
    public class RouteSetupDetailRepository : BaseRepository<SlsRouteDetail>, IRouteSetupDetailRepository
    {
        public RouteSetupDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsRouteDetail> GetAll()
        {
            return DataContext.SlsRouteDetails.ToList();

        }
        //Save
        public int AddEntity(SlsRouteDetail record)
        {
            int Id = 1;
            SlsRoute last = null;
            try
            {
                last = DataContext.SlsRoutes.OrderByDescending(x => x.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //Possibly can occur when no data exists in table.
            }
            if (last != null)
            {
                Id = last.Id + 1;

            }
            record.Id = Id;
            base.Add(record);
            return Id;
        }
        public int AddEntityList(IList<SlsRouteDetail> list)
        {
            int Id = 0;
            SlsRouteDetail last = DataContext.SlsRouteDetails.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id;
            }
            foreach (SlsRouteDetail obj in list)
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
        public SlsRouteDetail GetById(int id)
        {
            return DataContext.SlsRouteDetails.Where(x => x.Id == id).FirstOrDefault();
        }

        public int SaveChanges()
        {
            return base.DataContext.SaveChanges();
        }
    }
}
