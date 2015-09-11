using System;
using NUnit.Framework;
using ERPOptima.Data.Accounts.Repository;
using Moq;
using Optima.Areas.Accounts.Controllers;
using Optima.Areas.Accounts.ViewModel;
using System.Collections.Generic;
using System.Web.Mvc;
using ERPOptima.Model.Accounts;
using ERPOptima.Web.Accounts.ViewModel;

namespace ERPOptima.Test.Controllers.Accounts
{
    [TestFixture]
    public class VoucherEntryControllerTest
    {       

        Mock<IAnFVoucherRepository> repository;
        VoucherController controller = new VoucherController();
        int companyId = 1;
        int financialYearId = 13222;

        int userId = 2162239;
        int moduleId = 2;
        [SetUp]
        public void SetUp()
        {
            repository = new Mock<IAnFVoucherRepository>();

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
        public void GetAnfTransactionalHeadsByCompanyId()
        {
            VoucherIdViewModel v = new VoucherIdViewModel();
            JsonResult json = controller.GetAnfTransactionalHeadsByCompanyId() as JsonResult;
            dynamic jsonCollection = json.Data;
            Assert.IsNotNull(json);
            Assert.IsInstanceOf(typeof(IEnumerable<PaymentVoucherAccountNameAndCode>), json.Data, "Wrong View Model");
            List<PaymentVoucherAccountNameAndCode> list = new List<PaymentVoucherAccountNameAndCode>();
            list = json.Data as List<PaymentVoucherAccountNameAndCode>;
            Assert.AreEqual(2883, list.Count);

        }


        [Test]
        public void Insert()
        {
            //Arrange
            //voucherRepository = new Mock<IAnFVoucherRepository>();
            VoucherDateAndType t = new VoucherDateAndType();
            t.Prefix = "JV";
            t.Type = 3;
            t.Date = DateTime.Now;

            VoucherViewModel voucher = new VoucherViewModel();

            voucher.CmnCompanyId = companyId;
            voucher.CmnFinancialYearId = financialYearId;
            voucher.Type = 3;
            //voucher.VoucherNumber = controller.GetVoucherNo(t);
            voucher.Date = DateTime.Now;
            voucher.TotalAmount = 1500;

            List<VoucherDetailsViewModel> list = new List<VoucherDetailsViewModel>{
           new VoucherDetailsViewModel { AnFChartOfAccountId = 13227,AnFCostCenterId = 1322111,CmnProjectId = 13221,VoucherSerial = 1,SubVoucherNumber = voucher.VoucherNumber + "-1",Credit = 500,Debit = 500},
           new VoucherDetailsViewModel { AnFChartOfAccountId = 13228,AnFCostCenterId = 1322111,CmnProjectId = 13221,VoucherSerial = 2,SubVoucherNumber = voucher.VoucherNumber + "-2",Credit = 1000,Debit = 1000}

        };
            voucher.VouchrerDetails = list;
            //Act
            //Int64 actual = controller.InsertVoucher(voucher);

            //Assert
            //Assert.AreEqual(3112231058, actual);
        }

        [Test]
        public void GetVoucherDetailsbyVoucherId()
        {
            VoucherIdViewModel v = new VoucherIdViewModel();
            v.id = 3112231053;
            JsonResult json = controller.GetVoucherDetailsbyVoucherId(v) as JsonResult;
            dynamic jsonCollection = json.Data;
            Assert.IsNotNull(json);
            Assert.IsInstanceOf(typeof(IEnumerable<AnFVoucherDetail>), json.Data, "Wrong View Model");
            List<AnFVoucherDetail> list = new List<AnFVoucherDetail>();
            list = json.Data as List<AnFVoucherDetail>;
            Assert.AreEqual(1, list.Count);

        }

        [Test]
        public void SearchByItems()
        {
            //GetByParameters(companyId, financialYearId, voucher.VoucherTypeId, voucher.ProjectId, voucher.DateFrom, voucher.ToDate, true);
            VoucherSearchForGridViewModel voucher = new VoucherSearchForGridViewModel();
            voucher.VoucherTypeId = 3;
            voucher.ProjectId = 13221;
            voucher.DateFrom = DateTime.Now;
            voucher.ToDate = DateTime.Now;

            JsonResult json = controller.SearchByItems(voucher) as JsonResult;
            dynamic jsonCollection = json.Data;
            Assert.IsNotNull(json);
            Assert.IsInstanceOf(typeof(List<VoucherSearchResultViewModel>), json.Data, "Wrong View Model");
            List<VoucherSearchResultViewModel> list = new List<VoucherSearchResultViewModel>();
            list = json.Data as List<VoucherSearchResultViewModel>;
            Assert.AreEqual(5, list.Count);

        }




    }
}
