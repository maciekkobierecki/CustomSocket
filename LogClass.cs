using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSocket
{
    public class LogClass
    {
        public static void Log(string s)
        {
            Console.WriteLine("{0} | " + s, GetTimeStamp());
        }

        public static string GetTimeStamp()
        {
            DateTime dateTime = DateTime.Now;
            return dateTime.ToString("HH:mm:ss.ff");
        }
    }
}
