using ERPOptima.Web.Filters;
using Optima.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Optima.Areas.Security.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Security/Index/

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Session["moduleId"] = 1; // do your stuff

            //Session["financialYear"] = new FinancialYearHelper().SetFinancialYearId(Convert.ToInt32(Session["companyId"].ToString()), Convert.ToInt32(Session["moduleId"].ToString()));
        }
        //[AuthorizeUser]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }



        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Images"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                //using (MemoryStream ms = new MemoryStream())
                //{
                //    file.InputStream.CopyTo(ms);
                //    byte[] array = ms.GetBuffer();
                //}

            }
            // after successfully uploading redirect the user
            return RedirectToAction("actionname", "controller name");
        }


    }
}
