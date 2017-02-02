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

        private const string patternMoney = @"^[0-9]+$";
        private const string patternName = @"^[a-zA-Zа-яА-Я]+$";
        bool success;

        private Regex regexName = new Regex(patternName);
        private Regex regexMoney = new Regex(patternMoney);

        public string ExamineEnterName()
        {
            Console.WriteLine(dictTextByGame["enterName"]);
            try
            {
                string temp = string.Empty;
                success = false;
                do
                {
                    temp = Console.ReadLine();
                    success = regexName.IsMatch(temp);
                    if (success)
                    {
                        return temp;
                    }
                    else
                    {
                        Console.WriteLine("\n" + dictTextByGame["erroName"] + "\n");
                        Console.WriteLine(dictTextByGame["enterName"]);
                    }
                } while (!success);
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
                string temp = string.Empty;
                success = false;
                do
                {
                    temp = Console.ReadLine();
                    success = regexMoney.IsMatch(temp);
                    if (success)
                    {
                        if (Int32.Parse(temp) > 5)
                        {
                            Console.Clear();
                            return Int32.Parse(temp);
                        }
                        else
                        {
                            success = false;
                            Console.WriteLine("\n" + dictTextByGame["erroNumber"] + "\n");
                            Console.WriteLine(dictTextByGame["enterMoney"]);
                        }                                           
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
