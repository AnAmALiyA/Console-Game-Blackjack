using System;
using Game_Blackjack.Interface;
using Game_Blackjack.Infrastructure;
using System.Collections.Generic;
using System.Threading;

namespace Game_Blackjack
{
    class GameBlackjack
    {
        static void Main(string[] args)
        {
            string name = null;
            int money = 0;

            SpecificationGame spec = new SpecificationGame();
            IInspection insp = new Inspection(spec.dictTextByGame);
            View view = new View();

            Console.Title = spec.dictTextByGame["nameGame"];
            view.PreView(spec.dictTextByGame["nameGame"]);

            Console.WriteLine(spec.dictTextByGame["seeRules"]);
            if (Console.ReadKey().Key == ConsoleKey.F1)
            {
                view.ShowRules(spec.dictTextByGame);
            }
            Console.Clear();
            Console.WriteLine(spec.dictTextByGame["info"]+"\n");            

            name = insp.ExamineEnterName();
            Console.WriteLine("\n\n");
            money = insp.ExamineEnterMoney();

            if (!(name == null && money == -1))
            {
                IDataGame data = new DataGame(name, money);
                IBusinessLogicGame busin = new BusinessLogicGame(data, spec.dictTextByGame, view);
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