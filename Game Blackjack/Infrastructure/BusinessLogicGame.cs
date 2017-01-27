using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_Blackjack.Interface;
using System.Text.RegularExpressions;
using System.Threading;

namespace Game_Blackjack.Infrastructure
{
    public class BusinessLogicGame : IBusinessLogicGame
    {
        private IDataGame data;
        private Dictionary<string, string> dictTextByGame;
        private View view;
        private string[] cards;

        public BusinessLogicGame(IDataGame data, Dictionary<string, string> dictTextByGame)
        {
            this.data = data;
            this.dictTextByGame = dictTextByGame;
            View view = new View();
        }

        private const string pattern = @"[0-9]";
        private Regex regex = new Regex(pattern);

        Random ran = new Random();

        public void Start()
        {
            string[] playerCards = new string[8];
            string[] npcCards = new string[8];

            GetCardDeck(cards);
            ViewCosole(data.MoneyNPC, data.MoneyPlayer);

            int bet = 0;
            do
            {
                bet = GetBet();
                if ((data.MoneyPlayer - bet) < 0)
                {
                    Console.WriteLine(dictTextByGame["erroBetMoney"]);
                }
            } while ((data.MoneyPlayer - bet) < 0);
            
            ViewCosole(data.MoneyNPC - bet, data.MoneyPlayer - bet, bet * 2);

            Console.WriteLine(dictTextByGame["distribution"]);
            Thread.Sleep(3000);

            

            npcCards[0] = GetCard(cards);
            npcCards[1] = GetCard(cards);

            playerCards[0] = GetCard(cards);
            playerCards[1] = GetCard(cards);

            // TODO передать в метод карты и он посчитает их и выводит результат
            ViewCosole(data.MoneyNPC - bet, data.MoneyPlayer - bet, bet * 2);

            ViewCosole(data.MoneyNPC - bet, data.MoneyPlayer - bet, bet * 2); //вывод результата

            // TODO после выиграша изменить значения в data.MoneyNPC, data.MoneyPlayer для другой игры
            Console.WriteLine(dictTextByGame["continue"]);
            if (Console.ReadKey().KeyChar.ToString().ToLower() == "y")
            {
                Start();
            }            
        }

        private void GetCardDeck(string[] cards)
        {
            List<string> tempCards = new List<string>();

            string[] suit = { "Spade", "Heart", "Diamond", "Club" };
            string[] cardsArray = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

            for (int i = 0; i < cardsArray.Length; i++)
            {
                for (int j = 0; j < suit.Length; j++)
                {
                    tempCards.Add(cardsArray[i] + "-" + suit[j]);
                }
            }
            cards = tempCards.ToArray();
        }

        private string GetCard(string[] cards)
        {
            int item = 0;
            string tempCard = null;
            do
            {
                item = ran.Next(cards.Length);
                if (cards[item] != String.Empty)
                {
                    tempCard = cards[item];                    
                }                
            } while (cards[item] == String.Empty);
            cards[item] = String.Empty;
            return tempCard;
        }

        private void ViewCosole(int moneyNPC, int moneyPlayer, int pointsNPC = 0, int pointsPlayer = 0, string cardsNPC = "none", string cardsPlayer = "none", int bet = 0)
        {
            bool npcPoints = true;
            view.MainView(ConsoleColor.Green, data.NameNPC, moneyNPC, pointsNPC, cardsNPC, npcPoints);
            Console.WriteLine();
            view.AdditionsView(bet);
            view.MainView(ConsoleColor.Yellow, data.NamePlayer, moneyPlayer, pointsPlayer, cardsPlayer);
        }       

        private int GetBet()
        {
            int bet = 0;

            Console.WriteLine();
            Console.WriteLine(dictTextByGame["rates"]);
            Console.WriteLine(dictTextByGame["putAbet"]);
            do
            {
                int temp = 0;
                if (regex.IsMatch(Console.ReadLine()))
                {
                    temp = Int32.Parse(Console.ReadLine());
                }

                if (temp == 5 || temp == 10 || temp == 25 || temp == 50 || temp == 100)
                {
                    bet = temp;
                }
                else
                {
                    Console.WriteLine(dictTextByGame["erroBet"]);
                }
            } while (bet != 0);

            return bet;
        }
    }
}
