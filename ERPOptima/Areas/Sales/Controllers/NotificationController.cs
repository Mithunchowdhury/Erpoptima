using ERPOptima.Data.Infrastructure;
using ERPOptima.Data.Sales.Repository;
using ERPOptima.Model.Sales;
using ERPOptima.Service.Sales;
using ERPOptima.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Optima.Areas.Sales.Controllers
{
    public class NotificationController : Controller
    {

        private INotificationService _NotificationService;
        public NotificationController()
        {
            var dbfactory = new DatabaseFactory();
            _NotificationService = new NotificationService(new NotificationRepository(dbfactory),new NotificationDetailRepository(dbfactory),
                new UnitOfWork(dbfactory));
        }

        //
        // GET: /Sales/Notification/
        [AuthorizeUser]
        [ResourcePermissionAttribute]

        public ActionResult Index()     
        {
            return View();
        }        


        [HttpGet]
        public ActionResult GetAll()
        {
            int employeeId = Convert.ToInt32(Session["employeeId"]);
            var list = _NotificationService.GetAll(employeeId);

            var result = list.Select(i => new { Id = i.Id, Message = i.Message, URL = i.URL, Date = i.Date, IsRead = i.IsRead, Type = i.NotificationType }).Distinct().ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetNewNotifications()
        {
            int employeeId = Convert.ToInt32(Session["employeeId"]);
            var list = _NotificationService.GetNewNotifications(employeeId);
            list = list.OrderByDescending(i => i.Date).ToList();  // order by date

            var result = list.Select(i => new { Id = i.Id, Message = i.Message, URL = i.URL, Date = i.Date, IsRead = i.IsRead, Type = i.NotificationType }).Distinct().ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult IgnoreNotification(int nId)
        {
            var result = _NotificationService.IgnoreNotification(nId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }



    }
}
