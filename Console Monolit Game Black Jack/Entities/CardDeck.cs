using System;
using System.Collections.Generic;

namespace Console_Monolit_Game_Black_Jack.Entities
{
    public class CardDeck
    {
        public List<Card> ListCards { get; set; }

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
            Ten,
            Ace,
            Jack,
            Queen,
            King
        }

        enum EnumSuitCards
        {
            Spade,
            Heart,
            Diamond,
            Club
        }

        public void CreateCardDeck()
        {
            List<Card> tempListCards = new List<Card>();
            string[] suit = Enum.GetNames(typeof(EnumSuitCards));
            string[] arrayNameCards = Enum.GetNames(typeof(EnumCards));            
            int[] arrayCardsValue = (int[])Enum.GetValues(typeof(EnumCards));
            
            for (int i = 0; i < arrayNameCards.Length; i++)
            {
                for (int j = 0; j < suit.Length; j++)
                {
                    if (arrayNameCards[i] == EnumCards.King.ToString() | arrayNameCards[i] == EnumCards.Queen.ToString() | arrayNameCards[i] == EnumCards.Jack.ToString())
                    {
                        tempListCards.Add(new Card { NameCard = arrayNameCards[i], SuitCard = suit[j], ValueCard = (int)EnumCards.Ten});
                    }
                    else
                    {
                        tempListCards.Add(new Card { NameCard = arrayNameCards[i], SuitCard = suit[j], ValueCard = arrayCardsValue[i]});
                    }
                }
            }
            ListCards = tempListCards;
        }

        public void ShuffleCardDeck()
        {
            Random rnd = new Random();

            for (int j = 0; j < (int)RandomEnum.CountShuffle; j++)
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
