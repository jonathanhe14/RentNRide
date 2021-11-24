using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPI.Filters
{
    public class UserLogFilter : ActionFilterAttribute
    {
        public string PATH = @"A:\Mensajes";

        public UserLogFilter()
        {
            PATH = ConfigurationManager.AppSettings.Get("ULOG_PATH");
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            UserLog(string.Format("Controller: {0} EXECUTING method: {1}", actionContext.ActionDescriptor.ControllerDescriptor.ControllerName, actionContext.ActionDescriptor.ActionName));
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            UserLog(string.Format("Controller: {0} method EXECUTED: {1}", actionExecutedContext.ActionContext.ControllerContext.Controller, actionExecutedContext.ActionContext.ActionDescriptor.ActionName));
        }


        protected readonly object lockObj = new object();

        private void UserLog(String message)
        {
            var today = DateTime.Now.ToString("yyyyMMdd_hh");
            var logName = PATH + today + "_" + "log.txt";


            lock (lockObj)
            {
                using (StreamWriter w = File.AppendText(logName))
                {
                    w.Write("\r\nLog Entry : ");
                    w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                        DateTime.Now.ToLongDateString());
                    w.WriteLine("  :");
                    w.WriteLine("  :{0}", message);
                    w.WriteLine("-------------------------------");
                }
            }



        }



    }
}