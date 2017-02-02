using System;
using System.Collections.Generic;
using System.Threading;

namespace Game_Blackjack.Infrastructure
{
    public class View
    {
        public void PreView(string screen)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t" + screen);
            Console.ForegroundColor = ConsoleColor.White;

            Thread.Sleep(1000);
            Console.Clear();
        }

        public void MainView(ConsoleColor color, string name, int points, string cards, int sum = 0)
        {
            Console.WriteLine(new string('-', 70));

            Console.ForegroundColor = color;
            if (sum==0)
            {
                Console.WriteLine("{0}\n", name);
            }
            else
            {
                Console.WriteLine("{0}\t\t\t\t\tSum:{1}\n", name, sum);
            }            
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Points:{0}", points);
            Console.Write("\tCards: {0}\n", cards);

            Console.WriteLine(new string('-', 70));
        }

        public void AdditionsView(int bet)
        {
            Console.Write("\nBet: {0}\n\n", bet);            
        }

        public void ShowRules(Dictionary<string, string> dictTextByGame)
        {
            Console.Clear();           
            Console.WriteLine(dictTextByGame["helpCardVal"]);
            Console.WriteLine(dictTextByGame["helpYouWin"]);
            Console.WriteLine(dictTextByGame["helpDraw"]);
            Console.WriteLine(dictTextByGame["helpBust"]);
            Console.WriteLine(dictTextByGame["helpSplit"]);
            Console.WriteLine(dictTextByGame["helpDoubling"]);
            Console.WriteLine(dictTextByGame["helpControl"]);
            Console.WriteLine(dictTextByGame["presKey"]);
            Console.ReadKey();
        }

        public void End(int end, Dictionary<string, string> dictTextByGame)
        {
            if (end == 1)
            {
                Console.Clear();
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t" + dictTextByGame["win"]);
                Thread.Sleep(1000);
            }
            else if (end == -1)            
            {                
                Console.Clear();
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t" + dictTextByGame["lose"]);
                Thread.Sleep(1000);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t" + dictTextByGame["draw"]);
                Thread.Sleep(1000);
            }
        }

        public void OneResultOutput(int numberGame, string[,] textOutput, string dictTextByGame)
        {
            Console.Clear();
            Console.WriteLine(dictTextByGame);
            Console.WriteLine("Number game:{0}\n", numberGame+1);
            Console.WriteLine("You have money: {0}\tBet: {1}", textOutput[numberGame, 0], textOutput[numberGame, 1]);
            Console.WriteLine("Your point: {0}\nYour ards: {1}\n", textOutput[numberGame, 3], textOutput[numberGame, 5]);
            Console.WriteLine("NPC point: {0}\nNPC ards: {1}\n", textOutput[numberGame, 2], textOutput[numberGame, 4]);
        }

        public void AllResultOutput(int numberGame, string[,] textOutput, string dictTextByGame)
        {
            bool flag = true;
            Console.Clear();
            for (int i = 0; i < 5; i++)
            {
                if (textOutput[i,0]!=null)
                {
                    if (flag)
                    {
                        Console.WriteLine(dictTextByGame);
                        flag = false;
                    }                    
                    Console.WriteLine("\n\nNumber game:{0}\tYou have money: {1}\tBet: {2}", i + 1, textOutput[i, 0], textOutput[i, 1]);
                    Console.WriteLine("Your point: {0}\t NPC point: {1}", textOutput[i, 3], textOutput[i, 2]);
                    Console.WriteLine("Your cards: {0}\t NPC ards: {1}", textOutput[i, 5], textOutput[i, 4]);
                    Console.WriteLine();
                }
            }
            Console.ReadKey();
        }
    }
}
