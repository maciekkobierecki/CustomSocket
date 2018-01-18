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
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("{0} | " + s, GetTimeStamp());
        }

        public static void WhiteLog(string s)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("{0} | " + s, GetTimeStamp());
        }

        public static void GreenLog(string s)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0} | " + s, GetTimeStamp());
        }

        public static void MagentaLog(string s)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("{0} | " + s, GetTimeStamp());
        }

        public static void CyanLog(string s)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("{0} | " + s, GetTimeStamp());
        }

        public static string GetTimeStamp()
        {
            DateTime dateTime = DateTime.Now;
            return dateTime.ToString("HH:mm:ss.ff");
        }
    }
}
