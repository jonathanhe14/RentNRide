using DataAccess.Crud;
using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class ExceptionManager
    {

        public string PATH = "";

        private static ExceptionManager instance;

        private static Dictionary<int, ApplicationMessage> messages = new Dictionary<int, ApplicationMessage>();

        private ExceptionManager()
        {
            LoadMessages();
            PATH = ConfigurationManager.AppSettings.Get("LOG_PATH");
        }

        public static ExceptionManager GetInstance()
        {
            if (instance == null)
                instance = new ExceptionManager();

            return instance;
        }

        public void Process(Exception ex)
        {

            var bex = new BussinessException();


            if (ex.GetType() == typeof(BussinessException))
            {
                bex = (BussinessException)ex;
                bex.ExceptionDetails = GetMessage(bex).Mensaje;
            }
            else
            {
                bex = new BussinessException(0, ex);
            }

            ProcessBussinesException(bex);

        }

        private void ProcessBussinesException(BussinessException bex)
        {
            //2021022009            
            var today = DateTime.Now.ToString("yyyyMMdd_HH");
            var logName = PATH + today + "_" + "log.txt";

            var message = bex.ExceptionDetails + "\n" + bex.StackTrace + "\n";

            if (bex.InnerException!=null)
                message += bex.InnerException.Message + "\n" + bex.InnerException.StackTrace;

            using (StreamWriter w = File.AppendText(logName))
            {
                Log(message, w);
            }

            bex.AppMessage = GetMessage(bex);

            throw bex;

        }

        public ApplicationMessage GetMessage(BussinessException bex)
        {

            var appMessage = new ApplicationMessage
            {
                Mensaje = "Message not found!"
            };

            if (messages.ContainsKey(bex.ExceptionId))
                appMessage = messages[bex.ExceptionId];

            return appMessage;

        }

        private void LoadMessages()
        {
            var crudMessages = new ExceptionsCrudFactory();

            var lstMessages = crudMessages.RetrieveAll<ApplicationMessage>();

            foreach (var appMessage in lstMessages)
            {
                messages.Add(appMessage.Id, appMessage);
            }

        }

        private void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }

    }
}
