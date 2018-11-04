
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    class GotoAndStringLiteralVerbatim
    {
        static void Main(string[] args)
        {
            //mainMethod(args);
            //Console.WriteLine(GotoMethod() + " GotoMethod");
            Switch(3);
        }

        #region TestingStaticMainCommand-line parameters
        static void mainMethod(string[] args)
        {
            if (args == null)
            {
                Console.WriteLine("args is null");
            }
            else
            {
                Console.WriteLine("args length is " + args.Length);
                for (int i = 0; i < args.Length; i++)
                {
                    Console.WriteLine("args index {0} is [{1}]", i, args[i]);
                }
            }
            Console.ReadLine();
        }
        #endregion
        
        delegate int delg(int x, int y); //anonymous method.

        #region GotoMethodTesting
        static int GotoMethod()
        {
            int dummy = 0, x = 0, y = 0;
            
            for (int a = 0; a < 3; a++)
            {
                for (; y < 3;  ++y) // first run doesn't go to ++y so bascially same as y++.
                {
                    var ok = true;
                    for (; x < 3;) 
                    {
                        ++x;  //don't not restart at 0.
                        if (x == 3 && y == 2)
                        {
                            ok = false; break;
                        }
                    }
                    if (!ok) goto outer;
                    dummy++;
                }
                outer: break;
            }
            return dummy;
        }
        #endregion
        
        #region SwitchMethodTesting
        static void Switch(int Eprsn)
        {
            var anonymousClass = new { Name = "Anonymous", Salary = 1000 };

            switch (Eprsn)
            {
                case 1:
                    Console.WriteLine("Types char, string, bool, integer values such as long or double, enum");
                    break;
                case 2:
                    Console.WriteLine("Two types of string literals: Regular and Verbatim."); // Latter; "\\\u0066\n" with escape sequence \\ for backslash, \u0066 for the letter f, and \n for newline. or use 
                    break;
                case 3:
                    Console.WriteLine("string Verbatim literal examples");
                    Console.WriteLine("\\\u0066\n");
                    Console.WriteLine(@"\n""\/a");
                    Console.WriteLine("\\n\"\\/a");
                    break;
                default:
                    Console.WriteLine($"An unexpected value ({Eprsn})");
                    //return DefWindowProc(hWnd, Message, wParam, lParam);
                    break;
            }
        }
        #endregion
    }
}
