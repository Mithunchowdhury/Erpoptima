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
using ERPOptima.Web.Accounts.ViewModel;
namespace ERPOptima.Test.Controllers.Accounts
{
    [TestClass]
    public class OpeningBalanceControllerTest
    {
        [TestMethod]
        public void Update()
        {
            List<OpeningBalanceViewModel> list = new List<OpeningBalanceViewModel>();
            list.Add(new OpeningBalanceViewModel { Id=0,Debit=0,Credit=0});
            OpeningBalanceController oc = new OpeningBalanceController();
            JsonResult json = (JsonResult)oc.Update(list);
            Assert.IsInstanceOfType(json, typeof(JsonResult));
        }
    }
}
