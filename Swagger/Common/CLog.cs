using System;
using System.IO;
using System.Text;

namespace Swagger.Common
{
    public class CLog
    {
        public static int Log()
        {
            return 0;
        }

        public static void WriteLog(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string str = DateTime.Now.ToString("yyyy-M-d");
            string path = baseDirectory + "Log\\" + str + ".txt";
            if (!CLog.CreateDirectory(baseDirectory + "Log\\"))
                return;
            StreamWriter streamWriter = (StreamWriter)null;
            try
            {
                streamWriter = new StreamWriter(path, true, Encoding.Default);
                streamWriter.WriteLine(message + " [" + (object)DateTime.Now + "]");
                streamWriter.Flush();
            }
            catch
            {
            }
            finally
            {
                streamWriter?.Close();
            }
        }

        private static bool CreateDirectory(string path)
        {
            Directory.GetDirectoryRoot(path);
            if (!Directory.Exists(Directory.GetDirectoryRoot(path)))
                return false;
            try
            {
                Directory.CreateDirectory(path);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
