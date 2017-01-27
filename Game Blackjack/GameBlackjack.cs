using System;
using Game_Blackjack.Interface;
using Game_Blackjack.Infrastructure;
using System.Collections.Generic;
using System.Threading;

namespace Game_Blackjack
{
    class GameBlackjack
    {
        private static void PreView(string screen)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t" + screen);
            Console.ForegroundColor = ConsoleColor.White;

            Thread.Sleep(5000);
            Console.Clear();
        }

        static void Main(string[] args)
        {
            string name = null;
            int money = 0;

            SpecificationGame spec = new SpecificationGame();
            IInspection insp = new Inspection(spec.dictTextByGame);

            Console.Title = spec.dictTextByGame["nameGame"];
            PreView(spec.dictTextByGame["nameGame"]);
            
            Console.WriteLine(spec.dictTextByGame["info"]+"\n");            

            name = insp.ExamineEnterName();
            Console.WriteLine("\n\n");
            money = insp.ExamineEnterMoney();

            if (!(name == null && money == -1))
            {
                IDataGame data = new DataGame(name, money);
                IBusinessLogicGame busin = new BusinessLogicGame(data, spec.dictTextByGame);
                busin.Start();
            }
            else
            {
                Console.WriteLine(spec.dictTextByGame["erroFields"]);
                Console.ReadKey();
            }
        }     
    }
}