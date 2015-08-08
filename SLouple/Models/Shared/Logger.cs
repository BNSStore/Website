using SLouple.App_Start;
using SLouple.MVC.Account;
using SLouple.MVC.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace SLouple.MVC.Shared
{
    public class Logger
    {
        private static volatile List<string> logs = new List<string>();

        private HttpContext httpContext;
        private string format = "ncsa";
        private SqlStoredProcedures sqlSP;
        private bool useDB = false;
        private static Writer writer = new Writer();
        private static Thread writingThread;

        public Logger(HttpContext httpContext)
        {
            this.httpContext = httpContext;
        }

        public Logger(HttpContext httpContext, string format)
        {
            this.httpContext = httpContext;
            this.format = format.ToLower();
        }

        public Logger(HttpContext httpContext, string format, SqlStoredProcedures sqlSP)
        {
            this.httpContext = httpContext;
            this.format = format.ToLower();
            this.sqlSP = sqlSP;
            this.useDB = true;
        }

        public void SetFormat(string format)
        {
            this.format = format.ToLower();
        }

        public void SetUSeDB(bool useDB)
        {
            this.useDB = useDB;
        }

        public void Log()
        {
            string log;

            if (format == "ncsa")
            {
                log = GetNCSALog();
            }
            else
            {
                log = GetIISLog();
            }
            if (log != null)
            {
                if (useDB)
                {
                    WriteToDB();
                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine(log);
                    logs.Add(log);
                }
            }
        }

        private void WriteToDB()
        {
        }

        public static void StartWriting()
        {
            if (writingThread == null || !writingThread.IsAlive)
            {
                writingThread = new Thread(writer.Write);
                writingThread.Start();
            }
        }

        public static void StopWriting()
        {
            writer.RequestStop();
            writingThread.Join();
        }

        private string GetIISLog()
        {
            //Check if is 301 Redirect
            if (httpContext.Response.Filter.GetType() != typeof(ResponseLengthCalculatingStream))
            {
                return null;
            }

            StringBuilder log = new StringBuilder();
            log.Append(httpContext.Request.ServerVariables["REMOTE_ADDR"] + ", "); //IP
            log.Append(", "); //Identity
            DateTime now = DateTime.UtcNow;
            log.Append(now.ToString("MM/dd/yy") + ", "); //Date
            log.Append(now.ToString("HH/mm/ss") + ", "); //Time
            return log.ToString();
        }

        private string GetNCSALog()
        {
            //Check if is 301 Redirect
            if (httpContext.Response.Filter.GetType() != typeof(ResponseLengthCalculatingStream))
            {
                return null;
            }

            StringBuilder log = new StringBuilder();

            log.Append(httpContext.Request.ServerVariables["REMOTE_ADDR"] + " "); //IP
            log.Append("- "); //Identity
            log.Append("- "); //Username
            log.Append("[" + DateTime.UtcNow.ToString("dd/MMM/yyyy:HH:mm:ss").Replace("-", "/") + " +0000" + "] "); //Time

            string protocal = httpContext.Request.IsSecureConnection ? "HTTPS/1.1" : "HTTP/1.1";
            log.Append("\"" + httpContext.Request.HttpMethod + " " + httpContext.Request.Path + " " + protocal + "\" "); //Request Line
            log.Append(httpContext.Response.StatusCode + " "); //Status Code
            
            ResponseLengthCalculatingStream stream = (ResponseLengthCalculatingStream)httpContext.Response.Filter;
            log.Append(stream.responseSize + " "); //Response Size
            if (httpContext.Request.Headers.AllKeys.Contains("Referer")) //Referer
            {
                log.Append("\"" + httpContext.Request.Headers["Referer"] + "\" ");
            }
            else
            {
                log.Append("\"" + "-" + "\"");
            }

            log.Append("\"" + httpContext.Request.UserAgent + "\""); //User-agent


            return log.ToString();
        }

        public class Writer
        {
            public void Write()
            {
                DateTime date = DateTime.Now;
                string file = System.AppDomain.CurrentDomain.BaseDirectory + "/Log/" + date.Year + "/" + date.Month + ".log";
                Directory.CreateDirectory(Path.GetDirectoryName(file));


                using (StreamWriter writer = File.AppendText(file))
                {
                    int counter = 0;
                    while (!stop)
                    {
                        counter++;
                        if (counter > 100)
                        {
                            writer.Flush();
                            counter = 0;
                        }
                        if (logs.Count == 0)
                        {
                            Thread.Sleep(500);
                        }
                        else
                        {
                            writer.WriteLine(logs.First());
                            logs.RemoveAt(0);
                        }
                    }

                }
            }
            public void RequestStop()
            {
                stop = true;
            }
            private volatile bool stop;
        }
    }
}