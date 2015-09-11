using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Sales.Repository
{
    public interface IDeliveryRepository : IRepository<SlsDelivery>
    {
        int GetLastChallanNo();
        int GetLastInvoiceNo();
        IList<SlsDelivery> GetAll();
        int AddEntity(SlsDelivery obj);
        SlsDelivery GetById(int id);
        int SaveChanges();
        System.Data.Entity.DbContextTransaction BeginTransaction();
        void Rollback(System.Data.Entity.DbContextTransaction contextTxn);
        void Commit(System.Data.Entity.DbContextTransaction contextTxn);

        SlsDelivery GetAllByOrderID(int orderId);

        IList<SlsDelivery> GetDetailsByOrderIDVM(int orderId);
    }
    public class DeliveryRepository : BaseRepository<SlsDelivery>, IDeliveryRepository
    {

        public DeliveryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }


        public int GetLastChallanNo()
        {

            int SL = 1;
            SlsDelivery last = DataContext.SlsDeliveries.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                SL = int.Parse(last.ChallanNo.Split('/')[1]) + 1;

            }
            return SL;

        }//end of GetLastChallanNo
        public int GetLastInvoiceNo()
        {

            int SL = 1;
            SlsDelivery last = DataContext.SlsDeliveries.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                SL = int.Parse(last.InvoiceNo.Split('/')[1]) + 1;

            }
            return SL;

        }//end of GetLastInvoiceNo

        public IList<SlsDelivery> GetAll()
        {            
            return DataContext.SlsDeliveries.ToList();

        }

        public IList<SlsDelivery> GetDetailsByOrderIDVM(int orderId)
        {
            return DataContext.SlsDeliveries.ToList();
        }
        public SlsDelivery GetAllByOrderID(int orderId)
        {
            SlsDelivery SalesDelivery = new SlsDelivery();
            SalesDelivery = DataContext.SlsDeliveries.Where(x => x.SlsSalesOrderId == orderId).FirstOrDefault();
            return SalesDelivery;
        }
        public int AddEntity(SlsDelivery obj)
        {
            int Id = 1;
            SlsDelivery last = DataContext.SlsDeliveries.OrderByDescending(x => x.Id).FirstOrDefault();

            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;

        }
        public SlsDelivery GetById(int id)
        {
            return DataContext.SlsDeliveries.Where(x => x.Id == id).FirstOrDefault();
        }
        public int SaveChanges()
        {
            return base.DataContext.SaveChanges();
        }

        public System.Data.Entity.DbContextTransaction BeginTransaction()
        {
            return base.DataContext.Database.BeginTransaction();
        }

        public void Rollback(System.Data.Entity.DbContextTransaction contextTxn)
        {
            contextTxn.Rollback();
        }

        public void Commit(System.Data.Entity.DbContextTransaction contextTxn)
        {
            contextTxn.Commit();
        }



    }

    public partial class SlsDeliverDetailViewModel
    {
        public int Id { get; set; }
        public int SlsDeliveryId { get; set; }
        public int SlsProductId { get; set; }
        public decimal Quantity { get; set; }
        public int SlsUnitId { get; set; }
        public decimal Rate { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public decimal SalesOrderQuantity { get; set; }
        public string SlsProductName { get; set; }
        public string SlsUnitName { get; set; }
    }
    public partial class SlsDeliveryViewModel
    {
        public int Id { get; set; }
        public Nullable<int> SlsSalesOrderId { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string ChallanNo { get; set; }
        public string InvoiceNo { get; set; }
        public string VehicleNo { get; set; }
        public string Remarks { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<int> ReceivedStatus { get; set; }
        public Nullable<DateTime> ReceivedDate { get; set; }
        public string ReceivedRemarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }


        public IList<SlsDeliverDetailViewModel> SlsDeliverDetails { get; set; }
        
    }
}
