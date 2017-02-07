using System;
using System.Collections.Generic;
using Console_Monolit_Game_Black_Jack.Infrastructure;

namespace Console_Monolit_Game_Black_Jack.Entities
{
    public class CardDeck
    {
        public List<Card> ListCards { get; set; }
        private int CountShuffle = 3;
        
        public void CreateCardDeck()
        {
            List<Card> tempListCards = new List<Card>();

            foreach (EnumCards nameCards in Enum.GetValues(typeof(EnumCards)))
            {
                foreach (EnumSuitCards nameSuitCards in Enum.GetValues(typeof(EnumSuitCards)))
                {
                    if (nameCards == EnumCards.King | nameCards == EnumCards.Queen | nameCards == EnumCards.Jack)
                    {
                        tempListCards.Add( new Card { NameCard = nameCards, SuitCard = nameSuitCards, ValueCard = (int)EnumCards.Ten });
                        continue;
                    }
                    tempListCards.Add(new Card { NameCard = nameCards, SuitCard = nameSuitCards, ValueCard = (int)nameCards });
                }
            }           
            ListCards = tempListCards;
        }

        public void ShuffleCardDeck()
        {
            Random rnd = new Random();

            for (int j = 0; j < CountShuffle; j++)
            {
                for (int i = 0; i < ListCards.Count; i++)
                {
                    Card temp = ListCards[i];
                    ListCards.RemoveAt(i);
                    ListCards.Insert(rnd.Next(ListCards.Count), temp);
                }
            }           
        }
    }
}
