using System;
using System.Text.RegularExpressions;
using Console_Monolit_Game_Black_Jack.Entities;

namespace Console_Monolit_Game_Black_Jack.Infrastructure
{
    public class Inspection
    {
        enum RestrictionsEnum
        {
            MinimumLimitOfMoney = 100,
            MaximumLimitOfMoney = 400
        }

        public void ExamineEnterName(User user)
        {
            string patternName = @"^[a-zA-Zа-яА-Я]+$";
            Regex regexName = new Regex(patternName);

            try
            {
                string tempText = string.Empty;
                bool success = false;
                do
                {
                    tempText = Console.ReadLine();
                    success = regexName.IsMatch(tempText);
                    if (success)
                    {
                        user.Name = tempText;
                    }
                    else
                    {
                        ViewConsole.ErrorName();
                        ViewConsole.EnterName();
                    }
                } while (!success);
            }
            catch (Exception ex)
            {
                ViewConsole.Error(ex.Message);
            }
        }

        public void ExamineEnterMoney(User user)
        {
            string patternMoney = @"^[0-9]+$";
            Regex regexMoney = new Regex(patternMoney);

            try
            {
                string tempText = string.Empty;
                bool success = false;
                do
                {
                    tempText = Console.ReadLine();
                    success = regexMoney.IsMatch(tempText);
                    if (success)
                    {
                        if (Int32.Parse(tempText) >= (int)RestrictionsEnum.MinimumLimitOfMoney & Int32.Parse(tempText) <= (int)RestrictionsEnum.MaximumLimitOfMoney)
                        {
                            user.Money = Int32.Parse(tempText);
                        }
                        else
                        {
                            success = false;
                            ViewConsole.ErrorMoney();
                            ViewConsole.EnterAmountOfMoney();
                        }
                    }
                    else
                    {
                        ViewConsole.ErrorMoney();
                        ViewConsole.EnterAmountOfMoney();
                    }
                } while (!success);
            }
            catch (Exception ex)
            {
                ViewConsole.Error(ex.Message);
            }
        }
    }
}