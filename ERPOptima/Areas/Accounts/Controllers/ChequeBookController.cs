using ERPOptima.Service.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Lib.Model;
using ERPOptima.Model.Accounts;
using System.Data;
using Optima.Areas.Accounts.ViewModel;
using ERPOptima.Lib.Utilities;
using ERPOptima.Data.Accounts;
using ERPOptima.Web.Accounts.ViewModel;
using ERPOptima.Web.Filters;

namespace Optima.Areas.Accounts.Controllers
{
    public class ChequeBookController : Controller
    {
        //
        // GET: /Accounts/ChequeBook/
         private IAnFChequeBookService _chequeBookService;
         private IChartOfAccountService _AnFChartOfAccountService;
         public ChequeBookController()
        {
            var dbfactory = new DatabaseFactory();
            _chequeBookService = new AnFChequeBookService(new AnFChequeBookRepository(dbfactory), new UnitOfWork(dbfactory));
            _AnFChartOfAccountService = new ChartOfAccountService(new AnFChartOfAccountRepository(dbfactory), new UnitOfWork(dbfactory));
        }

         [AuthorizeUser]
         [ResourcePermissionAttribute]

        public ActionResult Index()
        {
            return View();
        }
        #region ChequeBook
        [HttpGet]
        public ActionResult GetChequeBooks(int anfCOAId)
        {           
            IList<AnFChequeBook> list = new List<AnFChequeBook>();
            list = _chequeBookService.GetChequeBooks().Where(x =>x.AnFChartOfAccountId==anfCOAId).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        

        public ActionResult GetTransactionalHeadForChequeBook()
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            var list = _AnFChartOfAccountService.GetParentWithChild(companyId).Select(ac => new
            {
                Id = ac.Id,
                AnFChartOfAccountId = ac.AnFChartOfAccountId,
                Code = ac.Code,
                Name = ac.Name,
                CmnCompanyId = ac.CmnCompanyId,
                ScheduleNo = ac.ScheduleNo,
                DepthLevel = ac.DepthLevel,
                IsTransactionalHead = ac.IsTransactionalHead,
                Status = ac.Status
            });
            var result = list.Where(x => x.Code.Contains("1020702") && x.IsTransactionalHead == true);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveChequeBook(AnFChequeBookViewModel anFChequeBookViewModel)
        {
            Operation objOperation = new Operation { Success = false };

            if (ModelState.IsValid)
            {
                AnFChequeBook anFChequeBook = new AnFChequeBook();
                anFChequeBook.Id = anFChequeBookViewModel.Id;
                anFChequeBook.ChequeBookNo = anFChequeBookViewModel.ChequeBookNo;
                anFChequeBook.IssueDate = anFChequeBookViewModel.IssueDate;
                anFChequeBook.AnFChartOfAccountId = anFChequeBookViewModel.AnFChartOfAccountId;
                anFChequeBook.NoofPage = anFChequeBookViewModel.NoofPage;
                anFChequeBook.StartingPageNo = anFChequeBookViewModel.StartingPageNo;
                anFChequeBook.EndingPageNo = anFChequeBookViewModel.EndingPageNo;


                if (anFChequeBook.Id == 0)
                {
                    if ((bool)Session["Add"])
                    {
                        objOperation = _chequeBookService.SaveAnFChequeBook(anFChequeBook);
                    }
                    else { objOperation.OperationId = -1; }
                }
                else
                {
                    if ((bool)Session["Edit"])
                    {
                        objOperation = _chequeBookService.UpdateAnFChequeBook(anFChequeBook);
                    }
                    else { objOperation.OperationId = -2; }
                }
            }

            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeleteChequeBook(int Id=0)
        {
            Operation objOperation = new Operation { Success=false};

            if (Id != 0)
            {
                AnFChequeBook obj = _chequeBookService.GetById(Id);

                if (obj == null)
                {
                    objOperation.Success = false;
                   
                }
                else {

                    objOperation = _chequeBookService.DeleteAnFChequeBook(obj);
                }
                return Json(objOperation, JsonRequestBehavior.DenyGet);
            }
            return Json(objOperation, JsonRequestBehavior.DenyGet);
        }

       

        #endregion
    }
}
