using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ERPOptima.Lib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPOptima.Helper
{
    public class CrystalReportResult:ActionResult
    {

        private readonly byte[] _contentBytes;

      
        public CrystalReportResult(string reportPath, object dataSet,List<ReportParameter> paramList,string type="pdf")
        {
            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load(reportPath);

            reportDocument.SetDataSource(dataSet);
            foreach (var item in paramList)
            {
                reportDocument.SetParameterValue(item.Name, item.Value);
            }

            #region extra
            //ParameterValues currentParameterValues = new ParameterValues();
            //ParameterDiscreteValue parameterDiscreteValue = new ParameterDiscreteValue();
            //ParameterFieldDefinitions parameterFieldDefinitions = reportDocument.DataDefinition.ParameterFields;
            //ParameterFieldDefinition parameterFieldDefinition;



            //foreach (var param in paramList)
            //{
            //    if (param.Value != null)
            //    {
            //        if (param.IsInteger)
            //            var value = Int64.Parse(param.Value);
            //        else if (param.IsDateTime)
            //            parameterDiscreteValue.Value = DateTime.Parse(param.Value);
            //        else if (param.IsBoolean)
            //            parameterDiscreteValue.Value = bool.Parse(param.Value);
            //        else
            //            parameterDiscreteValue.Value = param.Value;
            //    }
            //    else
            //    {
            //        parameterDiscreteValue.Value = DBNull.Value;
            //    }

            //    currentParameterValues.Add(parameterDiscreteValue);

            //    parameterFieldDefinition = parameterFieldDefinitions[param.Name];
            //    parameterFieldDefinition.ApplyCurrentValues(currentParameterValues);

            //}
            #endregion


            _contentBytes = StreamToBytes(reportDocument.ExportToStream(GetFormatType(type)));
        }


        public override void ExecuteResult(ControllerContext context)
        {

            var response = context.HttpContext.ApplicationInstance.Response;
            response.Clear();
            response.Buffer = false;
            response.ClearContent();
            response.ClearHeaders();
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.ContentType = "application/pdf";

            using (var stream = new MemoryStream(_contentBytes))
            {
                stream.WriteTo(response.OutputStream);
                stream.Flush();
            }
        }

        private static byte[] StreamToBytes(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private ExportFormatType GetFormatType(string type) 
        {
            if (type=="pdf")
            {
                return ExportFormatType.PortableDocFormat;
            }
            else
            {
                return ExportFormatType.PortableDocFormat;
            }

            
        }
    }
}