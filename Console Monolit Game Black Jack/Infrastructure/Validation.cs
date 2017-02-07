using System;
using System.Text.RegularExpressions;
using Console_Monolit_Game_Black_Jack.Entities;

namespace Console_Monolit_Game_Black_Jack.Infrastructure
{
    public class Validation
    {
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
                    tempText = OutputDataRepresentation.GetInputUser();
                    success = regexName.IsMatch(tempText);
                    if (success)
                    {
                        user.Name = tempText;
                        break;
                    }
                    OutputDataRepresentation.ErrorName();
                    OutputDataRepresentation.EnterName();

                } while (!success);
            }
            catch (Exception ex)
            {
                OutputDataRepresentation.Error(ex.Message);
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
                    tempText = OutputDataRepresentation.GetInputUser();
                    success = regexMoney.IsMatch(tempText);
                    if (success)
                    {
                        if (Int32.Parse(tempText) >= (int)RestrictionsEnum.MinimumLimitOfMoney & Int32.Parse(tempText) <= (int)RestrictionsEnum.MaximumLimitOfMoney)
                        {
                            user.Money = Int32.Parse(tempText);
                            break;
                        }
                        success = false;
                        OutputDataRepresentation.ErrorMoney();
                        OutputDataRepresentation.EnterAmountOfMoney();
                        continue;
                    }
                    OutputDataRepresentation.ErrorMoney();
                    OutputDataRepresentation.EnterAmountOfMoney();
                } while (!success);
            }
            catch (Exception ex)
            {
                OutputDataRepresentation.Error(ex.Message);
            }
        }
    }
}