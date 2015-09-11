using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ERPOptima.Model.Common;
using ERPOptima.Service.Common;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Common.Repository;
using ERPOptima.Lib.Model;
using System.Web.Mvc;

namespace ERPOptima.Test.Service
{
    /// <summary>
    /// Summary description for ProcessLevelServiceTest
    /// </summary>
    [TestClass]
    public class ProcessLevelServiceTest
    {
        private ICmnProcessLevelService _CmnProcessLevelService;
        public ProcessLevelServiceTest()
        {
            var dbfactory = new DatabaseFactory();
            _CmnProcessLevelService = new CmnProcessLevelService(new CmnProcessLevelRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Test_Service_Save()
        {
            CmnProcessLevel pl = new CmnProcessLevel();
            pl.Name = "Test2";
            pl.Status = true;

          
            var result = _CmnProcessLevelService.Save(pl);


            Assert.IsInstanceOfType(result, typeof(Operation));
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual(true,result.Success,"A NullValueNotAllowedException was expected, but was not thrown.");
            
        }

        [TestMethod]
        public void Test_Service_GetById()
        {
            CmnProcessLevel result = _CmnProcessLevelService.GetById(1);
          
            Assert.AreEqual("Check", result.Name);
        }




    }
}
