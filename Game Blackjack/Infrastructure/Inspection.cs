using System;
using System.Text.RegularExpressions;
using Game_Blackjack.Interface;
using System.Collections.Generic;

namespace Game_Blackjack.Infrastructure
{
    public class Inspection : IInspection
    {
        private Dictionary<string, string> dictTextByGame;
        public Inspection(Dictionary<string, string> dictTextByGame)
        {
            this.dictTextByGame = dictTextByGame;
        }

        private const string patternMoney = @"[0-9]";
        private const string patternName = @"[a-zA-Zа-яА-Я]";
        bool success;

        private Regex regexName = new Regex(patternName);
        private Regex regexMoney = new Regex(patternMoney);

        public string ExamineEnterName()
        {
            Console.WriteLine(dictTextByGame["enterName"]);
            try
            {
                success = false;
                do
                {
                    success = regexName.IsMatch(Console.ReadLine());
                    if (success)
                    {
                        return Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("\n" + dictTextByGame["erroName"] + "\n");
                        Console.WriteLine(dictTextByGame["enterName"]);
                    }
                } while (!success);   //while (!success && Console.ReadKey().Key != ConsoleKey.Escape);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public int ExamineEnterMoney()
        {
            Console.Write(dictTextByGame["enterMoney"]);
            try
            {
                success = false;
                do
                {                    
                    success = regexMoney.IsMatch(Console.ReadLine());
                    if (success & Int32.Parse(Console.ReadLine()) > 5)
                    {
                        return Int32.Parse(Console.ReadLine());                       
                    }
                    else
                    {
                        Console.WriteLine("\n" + dictTextByGame["erroNumber"] + "\n");
                        Console.WriteLine(dictTextByGame["enterMoney"]);
                    }
                } while (!success);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }
    }
}
