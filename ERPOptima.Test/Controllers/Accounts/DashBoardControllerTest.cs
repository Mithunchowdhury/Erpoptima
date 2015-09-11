using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optima.Areas.Accounts.Controllers;
using System.Web.Mvc;

namespace ERPOptima.Test.Controllers.Accounts
{
    [TestClass]
    public class DashBoardControllerTest
    {
        [TestMethod]
        public void GetDashBoardResult()
        {
            DashBoardController dbcontroller = new DashBoardController();
            JsonResult json = (JsonResult)dbcontroller.GetDashBoardResult();
            Assert.IsInstanceOfType(json,typeof(JsonResult));
        }
    }
}
