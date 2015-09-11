using System;
using System.Web.Mvc;
using NUnit.Framework;
using Optima.Areas.Accounts.Controllers;
using Moq;
using ERPOptima.Data.Accounts.Repository;
using Optima.Areas.Accounts.ViewModel;
using ERPOptima.Model.Accounts;
using System.Collections.Generic;
using ERPOptima.Web.Accounts.ViewModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data;


namespace ERPOptima.Test.Controllers.Accounts
{
    [TestFixture]
    public class ChartofAccountControllerTest
    {
        //Mock<IAnFCh> repository;
        ChartofAccountController controller = new ChartofAccountController();
        int companyId = 1;
        int financialYearId = 13222;
        int userId = 2162239;
        int moduleId = 2;
        [SetUp]
        public void SetUp()
        {
            //repository = new Mock<IAnFVoucherRepository>();

        }

        //[TestFixtureTearDown]
        //public void TearDown()
        //{

        //}
        //[Test]
        //public void Index()
        //{
        //    //Arrange
            

        //    //Act
        //    var actual = controller.Index();

        //    //Assert
        //    Assert.IsInstanceOf<ActionResult>(actual);
        //}

        [Test]
        public void GenerateChartOfAccountChildCode()
        {
            CodeGenerationViewModel obj = new CodeGenerationViewModel();
            obj.ParentCode = "101010101";
            obj.ChildCode = "";
            obj.Level = 5;

            string code = controller.GenerateChartOfAccountChildCode(obj);
           
            //Assert.IsNotNull(json);
            //Assert.IsInstanceOf(typeof(IEnumerable<PaymentVoucherAccountNameAndCode>), json.Data, "Wrong View Model");
            //List<PaymentVoucherAccountNameAndCode> list = new List<PaymentVoucherAccountNameAndCode>();
            //list = json.Data as List<PaymentVoucherAccountNameAndCode>;
            Assert.IsNotEmpty(code);
            Assert.AreEqual("101010101001", code);

        }


        [Test]
        public void Insert()
        {
            //Arrange
            //voucherRepository = new Mock<IAnFVoucherRepository>();
            //VoucherDateAndType t = new VoucherDateAndType();
            //t.Prefix = "JV";
            //t.Type = 3;
            //t.Date = DateTime.Now;

            //VoucherViewModel voucher = new VoucherViewModel();

            //voucher.CmnCompanyId = companyId;
            //voucher.CmnFinancialYearId = financialYearId;
            //voucher.Type = 3;
            //voucher.VoucherNumber = controller.GetVoucherNo(t);
            //voucher.Date = DateTime.Now;
            //voucher.TotalAmount = 1500;

           // List<VoucherDetailsViewModel> list = new List<VoucherDetailsViewModel>{
           //new VoucherDetailsViewModel { AnFChartOfAccountId = 13227,AnFCostCenterId = 1322111,CmnProjectId = 13221,VoucherSerial = 1,SubVoucherNumber = voucher.VoucherNumber + "-1",Credit = 500,Debit = 500},
           //new VoucherDetailsViewModel { AnFChartOfAccountId = 13228,AnFCostCenterId = 1322111,CmnProjectId = 13221,VoucherSerial = 2,SubVoucherNumber = voucher.VoucherNumber + "-2",Credit = 1000,Debit = 1000}

        //};
        //    voucher.VouchrerDetails = list;
        //    //Act
        //    Int64 actual = controller.InsertVoucher(voucher);

        //    //Assert
        //    Assert.AreEqual(3112231058, actual);
        }

        [Test]
        public void GetChartOfAccount()
        {
            JsonResult json = controller.GetChartOfAccount(1) as JsonResult;
            dynamic jsonCollection = json.Data;
            AnFChartOfAccount [] array= {};
            array = (AnFChartOfAccount[])json.Data;
            var anonList = json.Data;

            IQueryable<AnFChartOfAccount> list2 = (IQueryable<AnFChartOfAccount>)anonList;
            //List<AnFChartOfAccount> objectList = anonList.Cast<AnFChartOfAccount>().ToList();
            Assert.IsNotNull(json);
            //Assert.IsInstanceOf(typeof(IEnumerable<AnFChartOfAccount>), json.Data, "Wrong View Model");
            List<AnFChartOfAccount> list = new List<AnFChartOfAccount>();
            //list = json.Data as List<AnFChartOfAccount>;
            Assert.AreEqual(1, anonList);

        }

        [Test]
        public void SearchByItems()
        {
            //GetByParameters(companyId, financialYearId, voucher.VoucherTypeId, voucher.ProjectId, voucher.DateFrom, voucher.ToDate, true);
            //VoucherSearchForGridViewModel voucher = new VoucherSearchForGridViewModel();
            //voucher.VoucherTypeId = 3;
            //voucher.ProjectId = 13221;
            //voucher.DateFrom = DateTime.Now;
            //voucher.ToDate = DateTime.Now;

            //JsonResult json = controller.SearchByItems(voucher) as JsonResult;
            //dynamic jsonCollection = json.Data;
            //Assert.IsNotNull(json);
            //Assert.IsInstanceOf(typeof(List<VoucherSearchResultViewModel>), json.Data, "Wrong View Model");
            //List<VoucherSearchResultViewModel> list = new List<VoucherSearchResultViewModel>();
            //list = json.Data as List<VoucherSearchResultViewModel>;
            //Assert.AreEqual(5, list.Count);

        }




    }
}
