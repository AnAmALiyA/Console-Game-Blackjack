using System.Collections.Generic;

namespace Console_Monolit_Game_Black_Jack.Entities
{
    public class User
    {
        public string Name { get; set; }
        public int Money { get; set; }
        public int Bet { get; set; }
        public int WinLost { get; set; }
        public List<Card> Cards { get; set; }

        public User()
        {
            Cards = new List<Card>();
        }
        
        public int SumPointsCards()
        {
            int tempSum = 0;
            for (int i = 0; i < Cards.Count; i++)
            {
                tempSum += Cards[i].ValueCard;
            }
            return tempSum;
        }

        public string RowNamesCards()
        {
            string tempStr = string.Empty;
            for (int i = 0; i < Cards.Count; i++)
            {
                if (i == 0)
                {
                    tempStr += Cards[0].NameCard + "-" + Cards[0].SuitCard;
                }
                else
                {
                    tempStr +=", " + Cards[i].NameCard + "-" + Cards[0].SuitCard;
                }
            }
            return tempStr;
        }
    }
}
