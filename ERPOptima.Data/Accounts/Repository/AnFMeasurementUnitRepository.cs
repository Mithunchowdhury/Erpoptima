using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPOptima.Data;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Accounts;


namespace ERPOptima.Data.Accounts.Repository
{
    #region Interface

    public interface IAnFMeasurementUnitRepository : IRepository<AnFMeasurementUnit>
    {
        IList<AnFMeasurementUnit> GetAnFMeasurementUnits();
        int AddEntity(AnFMeasurementUnit objAnFMeasurementUnit);
        AnFMeasurementUnit GetAnFMeasurementUnitById(int id);
    }

    #endregion

    public class AnFMeasurementUnitRepository : BaseRepository<AnFMeasurementUnit>, IAnFMeasurementUnitRepository
    {

        public AnFMeasurementUnitRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<AnFMeasurementUnit> GetAnFMeasurementUnits()
        {
            return DataContext.AnFMeasurementUnits.ToList();
        }

        public int AddEntity(AnFMeasurementUnit objAnFMeasurementUnit)
        {
            int Id = 1;
            AnFMeasurementUnit last = DataContext.AnFMeasurementUnits.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            objAnFMeasurementUnit.Id = Id;
            base.Add(objAnFMeasurementUnit);
            return Id;
        }

        public AnFMeasurementUnit GetAnFMeasurementUnitById(int id)
        {
            return DataContext.AnFMeasurementUnits.Where(x => x.Id == id).FirstOrDefault();
        }

    }    
}
