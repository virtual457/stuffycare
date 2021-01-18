using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace StuffyCare.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext filtercontext)
        {
            //string ControllerName = filtercontext.RouteData.Values["controller"].ToString();
            //string ActionName = filtercontext.RouteData.Values["action"].ToString();
            string ActionName = filtercontext.Exception.TargetSite.Name.ToString();
            string ControllerName = filtercontext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName.ToString();
            //log exception
            string message = string.Format("Date Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));

            message += Environment.NewLine;
            message += "-------------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Controller: {0}", ControllerName);
            message += Environment.NewLine;
            message += string.Format("Action: {0}", ActionName);
            message += Environment.NewLine;
            message += string.Format("Message: {0}", filtercontext.Exception.Message);
            message += Environment.NewLine;
            message += string.Format("Inner Exception: {0}", filtercontext.Exception.InnerException);
            message += Environment.NewLine;
            message += "-------------------------------------------------------------";
            message += Environment.NewLine;
            string file = "Error-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".txt";
            string filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/ExceptionLogs"), file);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(message + filtercontext);
                writer.Close();
            }
            if (filtercontext.Request.IsLocal())
            {
                //HttpWebResponse response=null;
                string exceptionmessage = filtercontext.Exception.Message.ToString();
                filtercontext.Response = filtercontext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exceptionmessage);
            }
            else
            {
                filtercontext.Response = filtercontext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some Error occured");
            }
            //var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            //{
            //    Content = new System.Net.Http.StringContent("An unhandled exception was thrown by service."),
            //    ReasonPhrase = "Internal Server Error.Please Contact your Administrator."
            //};
            //filtercontext.Response = response;
        }
    }
}
