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

    public interface IOfficeRepository : IRepository<SlsOffice>
    {
        IList<SlsOffice> GetAll();
        int AddEntity(SlsOffice obj);
        SlsOffice GetById(int id);

        SlsOffice GetUserOffice(int userId);
    }

    #endregion

    public class OfficeRepository : BaseRepository<SlsOffice>, IOfficeRepository
    {
        public OfficeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IList<SlsOffice> GetAll()
        {
            return DataContext.SlsOffices.ToList();
        }
        public int AddEntity(SlsOffice objSlsOffice)
        {
            int Id = 1;
            SlsOffice last = DataContext.SlsOffices.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;

            }
            objSlsOffice.Id = Id;
            base.Add(objSlsOffice);
            return Id;

        }

        public SlsOffice GetById(int id)
        {
            return DataContext.SlsOffices.Where(x => x.Id == id).FirstOrDefault();
        }
        public SlsOffice GetUserOffice(int userId)
        {
            SlsOffice ret = new SlsOffice();
            Model.Security.SecUser user= DataContext.SecUsers.Where(t=>t.Id==userId).FirstOrDefault();
            Model.HRM.HrmEmployee emp = DataContext.HrmEmployees.Where(t => t.Id == user.HrmEmployeeId).FirstOrDefault();
            if (emp != null)
            {
                ret = DataContext.SlsOffices.Where(t => t.Id == emp.SlsOfficeId).FirstOrDefault();
            }
            return ret;
        }
    }

    public class OfficeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SlsRegionId { get; set; }
        public Nullable<int> SlsOfficeTypeId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Nullable<int> Head { get; set; }
        public Nullable<int> InCharge { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }


        public string RegionName { get; set; }
        public string OfficeTypeName { get; set; }
        public string HeadOfficeName { get; set; }
        public string InChargeName { get; set; }

    }
}

