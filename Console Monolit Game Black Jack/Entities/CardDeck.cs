using System;
using System.Collections.Generic;

namespace Console_Monolit_Game_Black_Jack.Entities
{
    public class CardDeck
    {
        public List<Card> listCards { get; set; }

        enum RandomEnum
        {
            CountShuffle = 3
        }

        enum EnumCards
        {
            Two = 2,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten = 10,
            Jack = 10,
            Queen = 10,
            King = 10,
            Ace = 11
        }

        public void CreateCardDeck()
        {
            List<Card> tempListCards = new List<Card>();
            string[] suit = { "Spade", "Heart", "Diamond", "Club" };
            string[] arryNameCards = Enum.GetNames(typeof(EnumCards));            
            int[] arryCardsValue = (int[])Enum.GetValues(typeof(EnumCards));            

            for (int i = 0; i < arryNameCards.Length; i++)
            {
                for (int j = 0; j < suit.Length; j++)
                {
                    tempListCards.Add(new Card { NameCard = arryNameCards[i], SuitCard = suit[j], ValueCard = arryCardsValue[i] });
                }
            }
            listCards = tempListCards;
        }

        public void ShuffleCardDeck()
        {
            Random rnd = new Random();

            for (int j = 0; j < (int)RandomEnum.CountShuffle; j++)
            {
                for (int i = 0; i < listCards.Count; i++)
                {
                    Card temp = listCards[i];
                    listCards.RemoveAt(i);
                    listCards.Insert(rnd.Next(listCards.Count), temp);
                }
            }           
        }
    }
}
