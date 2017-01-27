using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            string kk = "валет, дамма";
            int mm = 30;
            int ss = 5;
            int oo = 15;
            string tttt = "";

            Console.WriteLine(new string('-', 50));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("NPC\t\t\t\t\tCумма:{0}\n", mm);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Очков:{0}\tКарты: {1}", oo, kk);
            Console.WriteLine(new string('-', 50));
            if (getNPC)
            {
                Console.WriteLine("NPC берёт карту");
            }
            

            Thread.Sleep(2000);

            Console.Write("\n\n\n");
            Console.Write("Ставка: {0}", ss);
            Console.Write("\n\n");

            Console.WriteLine(new string('-', 50));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Игрок\t\t\t\t\tCумма:{0}\n", mm);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Очков:{0}\tКарты: {1}", oo, kk);
            Console.WriteLine(new string('-', 50));            

            do
            {
                if (Console.ReadKey().Key == ConsoleKey.F1)
                {
                    Console.WriteLine("Нажата {0}", ConsoleKey.F1);
                    Console.WriteLine("НажатаТекст {0}", ConsoleKey.F1.ToString());
                }
                Console.WriteLine(Console.ReadKey().KeyChar.ToString());
            } while (true);

            Console.ReadKey();
        }
    }
}
