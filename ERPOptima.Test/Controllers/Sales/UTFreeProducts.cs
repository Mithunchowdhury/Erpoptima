using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using ERPOptima.Service.Sales;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Model.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace ERPOptima.Test.Controllers.Sales
{
    [TestClass]
    public class UTFreeProducts : Controller
    {
        [TestMethod]
        public void GetAll()
        {
            DatabaseFactory dbFactory = new DatabaseFactory();
            IFreeProductService service = new FreeProductService(new FreeProductRepository(dbFactory),
                new ChartOfProductRepository(dbFactory), new UnitOfMeasurementRepository(dbFactory), new UnitOfWork(dbFactory));
            var list = service.GetAll(1);
            var result = list.Select(i => new SlsFreeProductsViewModel()
            {
                Id = i.Id,
                SlsProductId = i.SlsProductId,
                StartDate = i.StartDate,
                EndDate = i.EndDate,
                MeasurementQuantity = i.MeasurementQuantity,
                SlsUnitId = i.SlsUnitId,
                FreeQuantity = i.FreeQuantity,
                FreeUnitId = i.FreeUnitId,
                Remarks = i.Remarks,
                SecCompnayId = i.SecCompnayId,
                CreatedBy = i.CreatedBy,
                CreatedDate = i.CreatedDate,
                ModifiedBy = i.ModifiedBy,
                ModifiedDate = i.ModifiedDate,

                SlsProductName = i.SlsProductName,
                SlsUnitName = i.SlsUnitName,
                FreeUnitName = i.FreeUnitName
            }).Distinct().ToList();
                      
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
    }
    
}
