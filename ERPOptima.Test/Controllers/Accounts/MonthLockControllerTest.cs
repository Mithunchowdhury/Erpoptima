using System;
using System.Text;
using System.Collections.Generic;
using ERPOptima.Model.Common;
using Optima.Areas.Common.Controllers;
using ERPOptima.Service.Common;
using Moq;
using System.Web.Mvc;
using ERPOptima.Lib.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optima.Areas.Accounts.Controllers;
using Optima.Areas.Accounts.ViewModel;

namespace ERPOptima.Test.Controllers.Accounts
{
    [TestClass]
    public class MonthLockControllerTest
    {
        [TestMethod]
        public void GetByFinancialYearId()
        {
            MonthLockController mc = new MonthLockController();
            JsonResult json = (JsonResult)mc.GetByFinancialYearId(0);
            Assert.IsInstanceOfType(json, typeof(JsonResult));
        }

        public void UpdateMonthLock() {
            AnFMonthLockViewModel vm = new AnFMonthLockViewModel();
            
        
        
        }
            
    }
}
