using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Blackjack.Infrastructure
{
    public class View
    {
        public void MainView(ConsoleColor color, string name, int sum, int points, string cards, bool npcPoints = false)
        {
            Console.WriteLine(new string('-', 50));
            Console.ForegroundColor = color;
            Console.WriteLine("{0}\t\t\t\t\tSum:{1}\n", name, sum);
            Console.ForegroundColor = ConsoleColor.White;

            if (!npcPoints)
            {
                Console.Write("Points:{0}", points);
            }           
            Console.Write("\tCards: {0}", cards);

            Console.WriteLine(new string('-', 50));
        }

        public void AdditionsView(int bet)
        {
            Console.Write("\n\nBet: {0}\n\n", bet);
        }

        //public void AdditionsMainView(string takeNPC)
        //{           
        //    Console.WriteLine("NPC takes the card.");

        //    Console.WriteLine("Вы будете брать карту?");

        //    Console.WriteLine("Ставьте ставку");

        //    Console.WriteLine("Вы выиграли {0}", ss);

        //    Console.WriteLine("Вы проиграли {0}", ss);
        //}
    }
}
