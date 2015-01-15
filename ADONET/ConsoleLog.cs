using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONET
{
    static public class ConsoleLog
    {
        public static ConsoleColor DefautColor = ConsoleColor.White;
        static public void WriteOnConsole(bool isFail, string message)
        {
            Console.Write("[");
            Console.ForegroundColor = isFail ? ConsoleColor.Red : ConsoleColor.Green;
            Console.Write("*");
            Console.ForegroundColor = DefautColor;
            Console.WriteLine("] " + message);
        }
    }
}
