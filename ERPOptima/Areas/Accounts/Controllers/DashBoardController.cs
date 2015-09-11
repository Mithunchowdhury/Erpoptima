using ERPOptima.Data.Accounts.Repository;
using ERPOptima.Data.Common.Repository;
using ERPOptima.Data.Infrastructure;
using ERPOptima.Service.Accounts;
using ERPOptima.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPOptima.Model.Accounts;
using ERPOptima.Model.Common;
using ERPOptima.Web.Filters;

namespace Optima.Areas.Accounts.Controllers
{
    public class DashBoardController : Controller
    {

        private int ModuleId = 2;

         private IAnFVoucherService _anfVoucherService;
         private ICmnApprovalProcessService _cmnApprovalProcessService;
         private ICmnApprovalProcessLevelService _cmnApprovalProcessLevelService;
         private ICmnProcessLevelService _cmnProcessLevelService;
         private ICmnApprovalService _cmnApprovalService;
        
         public DashBoardController()
         {
              var dbFactory = new DatabaseFactory();
             _anfVoucherService = new AnFVoucherService(new AnFVoucherRepository(dbFactory), new UnitOfWork(dbFactory));
             _cmnApprovalProcessService=new CmnApprovalProcessService(new CmnApprovalProcessRepository(dbFactory),new UnitOfWork(dbFactory));
             _cmnApprovalProcessLevelService = new CmnApprovalProcessLevelService(new CmnApprovalProcessLevelRepository(dbFactory), new UnitOfWork(dbFactory));
             _cmnProcessLevelService = new CmnProcessLevelService(new CmnProcessLevelRepository(dbFactory), new UnitOfWork(dbFactory));
             _cmnApprovalService = new CmnApprovalService(new CmnApprovalRepository(dbFactory), new UnitOfWork(dbFactory));
        }


        //
        // GET: /Accounts/DashBoard/
                //[AuthorizeUser]

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDashBoardResult(int companyId = 1, int financialYear = 1)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            CmnApprovalProcess objCmnApprovalProcess = null;

            Dictionary<string, string> templist = null;
            foreach (AnFVoucherTypes voucherType in AnFVoucherTypes.GetValues(typeof(AnFVoucherTypes)))
            {

                templist = new Dictionary<string, string>();
                objCmnApprovalProcess = _cmnApprovalProcessService.GetByShortName(voucherType.ToString(), ModuleId);
                
                if (objCmnApprovalProcess !=null)
                {
                    List<CmnApprovalProcessLevel> aplList=_cmnApprovalProcessLevelService.GetByAPId(objCmnApprovalProcess.Id).ToList();
                    
                    
                    int value=(int)Enum.Parse(typeof(AnFVoucherTypes),voucherType.ToString());//get value from enum

                    templist.Add("Name", objCmnApprovalProcess.Name);

                    int total = _anfVoucherService.GetTotalByCompanyId(companyId, financialYear, value);
                    templist.Add("Total", total.ToString());

                    int cancelled = _anfVoucherService.GetTotalCancelByCompanyIdAndType(companyId, financialYear, value);
                    templist.Add("Cancelled", cancelled.ToString());

                    int posted = _anfVoucherService.GetTotalPostedByCompanyIdAndType(companyId, financialYear, value);

                    templist.Add("Posted", posted.ToString());
                    int unPosted = _anfVoucherService.GetTotalUnPostedByCompanyIdAndType(companyId, financialYear, value);

                    templist.Add("UnPosted", unPosted.ToString());

                    foreach (CmnApprovalProcessLevel apl in aplList)
                    {
                        int _tmpUnposted = this.CalculateTotalApproved(companyId, financialYear, objCmnApprovalProcess.Id, apl.Id, aplList, objCmnApprovalProcess);
                       templist.Add(apl.CmnProcessLevel.Name, _tmpUnposted.ToString());

                    }
                    list.Add(templist);
                }
                
            }

            return Json(list,JsonRequestBehavior.AllowGet);

        }


        #region Private For VoucherDashboard


        private int CalculateTotalApproved(int companyId, int financialYear, int processId, int levelId, List<CmnApprovalProcessLevel> records, CmnApprovalProcess prcs)
        {


            int result = 0;
            //CmnProcessLevelBLL bll = new CmnProcessLevelBLL();
            //Collection<CmnProcessLevel> records = bll.GetAll(companyId, EnumClass.DepartmentId, processId);
            //IList<int> pre = new List<int>();
            //IList<int> post = new List<int>();
            //int index = 0;
            //for (int i = 0; i < records.Count; i++)
            //{
            //    if (records[i].CmnProcessLevel.Id == levelId)
            //    {
            //        index = i;
            //    }
            //}
            //for (int i = 0; i < records.Count; i++)
            //{
            //    if (index > i)
            //    {
            //        pre.Add((int)records[i].CmnProcessLevel.Id);
            //    }
            //    else if (index < i)
            //    {
            //        post.Add((int)records[i].CmnProcessLevel.Id);
            //    }
            //}
            string sqlText = string.Empty;
            sqlText += "SELECT  COUNT(*) FROM CmnApprovals ";
            sqlText += "WHERE ((Value IS NULL) OR  (Value = 0)) AND (CmnCompanyId = " + companyId.ToString() + ") AND (CmnProcessLevelId = " + levelId.ToString() + ") AND (CmnApprovalProcessId = " + processId.ToString() + ") ";
            //foreach (int i in pre)
            //{
            //    string lvlId = "pre" + i.ToString();
            //    sqlText += "AND 1=";
            //    sqlText += "(";
            //    sqlText += "SELECT Value FROM CmnApprovals as " + lvlId + " ";
            //    sqlText += "WHERE (" + lvlId + ".CmnApprovalProcessId = " + processId.ToString() + ") AND (" + lvlId + ".CmnProcessLevelId = " + i.ToString() + ") AND (" + lvlId + ".CmnCompanyId = " + companyId.ToString() + ") AND (Value = 1) AND " + lvlId + ".RefId=CmnApprovals.RefId";
            //    sqlText += ") ";
            //}
            //foreach (int i in post)
            //{
            //    string lvlId = "post" + i.ToString();
            //    sqlText += "AND 0=";
            //    sqlText += "(";
            //    sqlText += "SELECT Value FROM CmnApprovals as " + lvlId + " ";
            //    sqlText += "WHERE (" + lvlId + ".CmnApprovalProcessId = " + processId.ToString() + ") AND (" + lvlId + ".CmnProcessLevelId = " + i.ToString() + ") AND (" + lvlId + ".CmnCompanyId = " + companyId.ToString() + ") AND (Value = 0) AND " + lvlId + ".RefId=CmnApprovals.RefId";
            //    sqlText += ") ";
            //}


            //sqlText += "AND RefId IN (SELECT Id FROM " + prcs.TableName + " WHERE CmnFinancialYearId=" + financialYear + " and IsCancel=0)";
            //result = new CmnApprovalBLL().GetNoOfUnposted(sqlText);
            result=_cmnApprovalService.GetNoOfUnposted(sqlText);
            return result;

            
        
        }

        #endregion

    }
}
