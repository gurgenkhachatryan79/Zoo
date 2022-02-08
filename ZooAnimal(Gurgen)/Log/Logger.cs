using System;
using System.IO;

namespace ZooAnimal_Gurgen_.Log
{
    enum Log
    {
        Error,
        Information,
        Warning
    }
    class Logger : ILogger
    {

        private static Logger instance;
        private static object syncRoot = new Object();
        private DateTime InitialDateTime = new DateTime(2022, 02, 01, 22, 00, 10);
       // private Logger() { }
        public static Logger CreateLogObject()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new Logger();
                }
            }
            return instance;
        }

        public void LogError(string text)
        {
            string lmessage = Log.Error + "     " + DateTime.Now + "      " + text;
            WriteMessage(lmessage);
        }

        public void LogInformation(string text)
        {
            string lmessage = Log.Information + "       " + DateTime.Now + "      " + text;
            WriteMessage(lmessage);
        }

        public void LogWarning(string text)
        {
            string lmessage = Log.Warning + "      " + DateTime.Now + "      " + text;
            WriteMessage(lmessage);
        }

        public void WriteMessage(string text)
        {
            if (DateTime.Now.Minute - InitialDateTime.Minute > 1)
            {
                InitialDateTime = DateTime.Now;
            }
            string filename = InitialDateTime.ToString("yyyy.MM.dd.hh.mm.ss");
            var pathDirectory = "..//..//..//Log//Messages//";
            var pathFile = Path.Combine(Environment.CurrentDirectory, pathDirectory, filename + ".txt");

            DirectoryInfo directoryInfo = new DirectoryInfo(pathDirectory);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(pathFile, true))
                {
                    streamWriter.WriteLine(text);
                };
            }
            catch (DirectoryNotFoundException exd) { Console.WriteLine(exd.Message); }
            catch (FileNotFoundException ex) { Console.WriteLine(ex.Message); }
            catch
            {
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
