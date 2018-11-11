using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace God
{
    class Program
    {
       public static StringBuilder files_found=new StringBuilder("");
        static void Main(string[] args)
        {
            AbstractBar bar;
            bar = new AnimatedBar();
            int wait = 100;
            int end = 50;

         
           
            


            string[] s = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            string[] s1 = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

           
            Console.WriteLine("");

            for (int i = 0; i <= 25; i++)
            {
                string domain_name = "";
                domain_name = s[i];
                //Animated bar
               // Test(bar, wait, end);

                Console.WriteLine("Searching. Please wait");
                foreach (var secondword in s1)
                {
                    frameDomainName(domain_name + secondword);
                    Console.Write(".");
                }
                Console.Clear();
            }
            Console.WriteLine("Files found \n" + files_found);
            Console.WriteLine("Scan Completed");
            Console.ReadKey();
        }

        public static void Test(AbstractBar bar, int wait, int end)
        {
            bar.PrintMessage("Loading...");
            for (int cont = 0; cont < end; cont++)
            {
                bar.Step();
                //Thread.Sleep(wait);
            }
            bar.PrintMessage("Finished");
        }


        public static void WriteLog(string strLog)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string logFilePath = "D:\\Logs\\";
            logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            log.WriteLine(strLog);
            log.Close();
        }

        public static void frameDomainName(string name)
        {
            try {
                string uriName = "http://tamilrockers." + name;

                Uri uriResult;
                bool result = Uri.TryCreate(uriName, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
                if (result)
                {
                    //Console.WriteLine(uriName);
                    //Console.WriteLine(result);
                }
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(uriName);
                myRequest.Method = "GET";
                WebResponse myResponse = myRequest.GetResponse();
                Stream data = myResponse.GetResponseStream();

                string html = String.Empty;
                using (StreamReader sr = new StreamReader(data))
                {
                    html = sr.ReadToEnd();
                }
                bool isvalid = !html.Contains("YOU ARE NOT AUTHORIZED TO ACCESS THIS WEB PAGE");

                bool isvalidtamilrockersite = html.Contains("1_clclcl");
                if (isvalid && isvalidtamilrockersite)
                {
                    //Console.WriteLine(uriName);
                    files_found.Append(uriName+"\n");
                    WriteLog(uriName);
                }
               // Console.WriteLine(isvalid);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
