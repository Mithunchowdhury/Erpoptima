using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Optima.Helper
{
    public static class MiscExtensions
    {
        // Ex: collection.TakeLast(5);
        public static IEnumerable<T> TakeLast<T>(IEnumerable<T> source, int N)
        {
            return source.Skip(Math.Max(0, source.Count() - N));
        }
    }

    public static class ReportUtil
    {
        public static string GetSalesReportPath(string fileName)
        {
            string basePath = HostingEnvironment.MapPath("~/Areas/Sales/Reports");

            return Path.Combine(basePath, fileName);
        }
    }
    public static class DateUtil
    {
        public static string DisplayMonthName(int month)
        {
            string name = "";
            switch (month)
            {
                case 1:
                    name = "January";
                    break;
                case 2:
                    name = "February";
                    break;
                case 3:
                    name = "March";
                    break;
                case 4:
                    name = "April";
                    break;
                case 5:
                    name = "May";
                    break;

                case 6:
                    name = "June";
                    break;

                case 7:
                    name = "July";
                    break;

                case 8:
                    name = "August";
                    break;

                case 9:
                    name = "September";
                    break;

                case 10:
                    name = "October";
                    break;

                case 11:
                    name = "November";
                    break;

                case 12:
                    name = "December";
                    break;
                default:
                    name = "";
                    break;

            }
            return name;

        }
    }

}