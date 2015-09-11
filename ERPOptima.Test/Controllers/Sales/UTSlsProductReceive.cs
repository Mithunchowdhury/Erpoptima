using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Data.Infrastructure;

namespace ERPOptima.Test.Controllers.Sales
{
    [TestClass]
    public class UTSlsProductReceive
    {
        [TestMethod]
        public void GetAll()
        {
            SlsProductReceiveRepository repo = new SlsProductReceiveRepository(new DatabaseFactory());
            SlsProductReceiveDetailRepository repoDetail = new SlsProductReceiveDetailRepository(new DatabaseFactory());

            repo.Add(new Model.Sales.SlsProductReceive()
            {
                Id = 1,
                SlsDeliveryId = 1,
                ChallanNo = "1",
                InvoiceNo = "1",
                Remarks = "Remarks",
                VehicleNo = "1",
                CreatedBy = 2,
                CreatedDate = DateTime.Now,
                ModifiedBy = null,
                ModifiedDate = null
            });
            try
            {                
                repo.DataContext.Commit();
            }
            catch (System.Data.Entity.Core.UpdateException e)
            {

            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex) //DbContext
            {
                Console.WriteLine(ex.InnerException);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);                
            }
            

            var result = repo.GetAll();
            Assert.IsTrue(result != null, "We get product receive information...");

            var resultDetail = repoDetail.GetAll();
            Assert.IsTrue(resultDetail != null, "We get product receive detail information...");
        }
    }
}
