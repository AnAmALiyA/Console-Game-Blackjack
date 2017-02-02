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
        private int[] pointsArryNPC, pointsArryPlayer;

        private int pointNPC, pointNPCview, pointPlayerView, bet = 0;
        private string cardsNPCview, cardsPlayerView;
        private string[,] textOutput;

        private int count = 0;

        public BusinessLogicGame(IDataGame data, Dictionary<string, string> dictTextByGame, View view)
        {
            this.data = data;
            this.dictTextByGame = dictTextByGame;
            this.view = view;
            textOutput = new string[5, 6];
        }

        private const string pattern = @"^[0-9]+$";
        private Regex regex = new Regex(pattern);

        Random ran = new Random();

        public void Start()
        {
            string[] playerArryCards = new string[8];
            string[] npcArryCards = new string[8];

            cards = GetCardDeck();
            ViewCosole(data.MoneyPlayer, bet);

            do
            {
                bet = GetBet();
                if ((data.MoneyPlayer - bet) < 0)
                {
                    Console.WriteLine(dictTextByGame["erroBetMoney"]);
                }
            } while ((data.MoneyPlayer - bet) < 0);

            ViewCosole(data.MoneyPlayer - bet, bet);

            Console.WriteLine(dictTextByGame["distribution"]);
            Thread.Sleep(2000);

            GetInitialCard(playerArryCards, npcArryCards, bet);
            GetGame(playerArryCards, npcArryCards, pointsArryNPC, pointsArryPlayer, ref bet, cards);

            if (count < 5)
            {
                cardsPlayerView = GetCardsView(playerArryCards, data.NamePlayer);
                cardsNPCview = GetCardsView(npcArryCards, data.NamePlayer);

                textOutput[count, 0] = data.MoneyPlayer.ToString();
                textOutput[count, 1] = bet.ToString();
                bet = 0;
                textOutput[count, 2] = pointNPC.ToString();
                textOutput[count, 3] = pointPlayerView.ToString();
                textOutput[count, 4] = cardsNPCview;
                textOutput[count, 5] = cardsPlayerView;

                view.OneResultOutput(count, textOutput, dictTextByGame["outOnce"]);

                Console.WriteLine(dictTextByGame["continueRound"]);
                if (Console.ReadKey().KeyChar.ToString().ToLower() == "y")
                {
                    count++;
                    Start();
                }
            }

            view.AllResultOutput(count, textOutput, dictTextByGame["outPut"]);

            Console.WriteLine(dictTextByGame["continue"]);
            if (Console.ReadKey().KeyChar.ToString().ToLower() == "y")
            {
                count = 0;
                Start();
            }
        }

        private void GetGame(string[] playerArryCards, string[] npcArryCards, int[] pointsArryNPC, int[] pointsArryPlayer, ref int bet, string[] cards)
        {
            bool keyEnough = true, turn = false, keyBet = true, exit = false; ;
            int logicPointNPC = 17;

            do
            {
                DataAnalysis(playerArryCards, npcArryCards, pointsArryNPC, pointsArryPlayer, cards, ref turn, keyEnough, logicPointNPC, ref exit);
                if (exit)
                {
                    break;
                }
                ViewCosole(data.MoneyPlayer, bet, pointNPCview, pointPlayerView, cardsNPCview, cardsPlayerView);

                if (keyEnough)
                {
                    Console.WriteLine(dictTextByGame["askGiveCard"]);

                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.F1:
                            view.ShowRules(dictTextByGame);
                            turn = false;
                            break;

                        case ConsoleKey.F2:

                            if (keyEnough)
                            {
                                GetViewOneMove(playerArryCards, pointsArryPlayer, bet, cards);

                                Console.WriteLine(dictTextByGame["npc"]);
                                Thread.Sleep(1000);

                                keyBet = true;
                            }
                            break;

                        case ConsoleKey.F3:

                            if (keyEnough)
                            {
                                keyEnough = false;
                            }
                            break;

                        case ConsoleKey.F4:

                            if (playerArryCards[2] == null & keyBet)
                            {
                                string[] tempOne = npcArryCards[0].Split(new char[] { '-' });
                                string[] tempTwo = npcArryCards[1].Split(new char[] { '-' });

                                if (tempOne[0] == tempOne[1])
                                {
                                    SplitCards(playerArryCards, cards, data.NamePlayer);
                                    doubleMoney();
                                }
                                else
                                {
                                    Console.WriteLine(dictTextByGame["erroSplit"]);
                                    Thread.Sleep(2000);
                                    turn = false;
                                }
                            }
                            break;

                        case ConsoleKey.F5:

                            if (keyBet)
                            {
                                turn = false;
                                doubleMoney();
                                keyBet = false;
                            }
                            else
                            {
                                Console.WriteLine(dictTextByGame["erroRepeatBet"]);
                                Thread.Sleep(2000);
                                turn = false;
                            }
                            break;

                        case ConsoleKey.F6:

                            if (playerArryCards[2] == null & keyBet)
                            {
                                doubleMoney();
                                GetViewOneMove(playerArryCards, pointsArryPlayer, bet, cards);
                                keyEnough = false;
                            }
                            else
                            {
                                Console.WriteLine(dictTextByGame["erroDouble"]);
                                Thread.Sleep(2000);
                                turn = false;
                            }
                            break;

                        case ConsoleKey.F7:
                            exit = true;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(dictTextByGame["npc"]);
                    Thread.Sleep(1000);
                }
            } while (!exit);
        }

        private void GetViewOneMove(string[] playerArryCards, int[] pointsArryPlayer, int bet, string[] cards)
        {
            GiveCard(playerArryCards, cards);
            pointsArryPlayer = GetPointsByCards(playerArryCards);
            pointPlayerView = GetCount(pointsArryPlayer);
            cardsPlayerView = GetCardsView(playerArryCards, data.NamePlayer);
            ViewCosole(data.MoneyPlayer, bet, pointNPCview, pointPlayerView, cardsNPCview, cardsPlayerView);
        }

        private void DataAnalysis(string[] playerArryCards, string[] npcArryCards, int[] pointsArryNPC, int[] pointsArryPlayer, string[] cards, ref bool turn, bool keyEnough, int logicPointNPC, ref bool exit)
        {
            int limitPoint = 21;
            if (turn)
            {
                if (pointsArryNPC[2] == 0)
                {
                    string[] tempOne = npcArryCards[0].Split(new char[] { '-' });
                    string[] tempTwo = npcArryCards[1].Split(new char[] { '-' });

                    if (tempOne[0] == tempOne[1])
                    {
                        SplitCards(npcArryCards, cards, data.NameNPC);
                    }
                }

                if (pointNPC > limitPoint)
                {
                    for (int i = 0; i < pointsArryNPC.Length; i++)
                    {
                        if (pointsArryNPC[i] == 11)
                        {
                            pointsArryNPC[i] = 1;
                            break;
                        }
                    }
                }

                if (pointNPC < logicPointNPC)
                {
                    GiveCard(npcArryCards, cards);
                    pointsArryNPC = GetPointsByCards(npcArryCards);
                    pointNPC = GetCount(pointsArryNPC);
                }
                turn = false;
            }
            else
            {
                turn = true;
            }

            if (pointPlayerView > limitPoint)
            {
                view.End(-1, dictTextByGame);
                exit = true;
            }
            else if (pointNPC > limitPoint)
            {
                data.MoneyPlayer += bet * 2;
                view.End(1, dictTextByGame);
                exit = true;
            }
            else if (pointNPC >= logicPointNPC & !keyEnough)
            {
                int tempPointNPC = pointsArryNPC[0] + pointsArryNPC[1],
                tempPointPlayer = pointsArryPlayer[0] + pointsArryPlayer[1];

                if (tempPointNPC == 21 & tempPointPlayer == 21)
                {
                    data.MoneyPlayer += bet;
                    view.End(0, dictTextByGame);
                }
                else if (tempPointPlayer == 21)
                {
                    double tempMoney = (double)bet * 1.5;
                    data.MoneyPlayer += (int)tempMoney;
                    view.End(1, dictTextByGame);
                }
                else if (tempPointNPC == 21)
                {
                    view.End(-1, dictTextByGame);
                }
                else
                {
                    if (pointPlayerView > pointNPC)
                    {
                        data.MoneyPlayer += bet * 2;
                        view.End(1, dictTextByGame);
                    }
                    else if (pointPlayerView < pointNPC)
                    {
                        view.End(-1, dictTextByGame);
                    }
                    else
                    {
                        data.MoneyPlayer += bet;
                        view.End(0, dictTextByGame);
                    }
                }
                exit = true;
            }
        }

        private void SplitCards(string[] arryCardsPlayer, string[] cards, string name)
        {
            arryCardsPlayer[1] = GetRendomCard(cards);
            if (name == data.NamePlayer)
            {
                pointsArryPlayer = GetPointsByCards(arryCardsPlayer);
                pointPlayerView = GetCount(pointsArryPlayer);
                cardsPlayerView = GetCardsView(arryCardsPlayer, name);
            }
            else
            {
                pointsArryNPC = GetPointsByCards(arryCardsPlayer);
                pointNPC = GetCount(pointsArryPlayer);
                cardsNPCview = GetCardsView(arryCardsPlayer, name);
            }
        }

        private void doubleMoney()
        {
            if ((data.MoneyPlayer - bet) > 0)
            {
                data.MoneyPlayer = data.MoneyPlayer - bet;
                bet = bet * 2;
            }
            else
            {
                Console.WriteLine(dictTextByGame["notMoney"]);
            }
        }

        private int GetCount(int[] pointsArry)
        {
            int temp = 0;
            for (int i = 0; i < pointsArry.Length; i++)
            {
                temp += pointsArry[i];
            }
            return temp;
        }

        private string GetCardsView(string[] arryCards, string name)
        {
            string tempCardsView = string.Empty;
            if (name == data.NamePlayer)
            {
                bool flag = true;
                for (int i = 0; i < arryCards.Length; i++)
                {
                    if (arryCards[i] != null)
                    {
                        if (flag)
                        {
                            tempCardsView += arryCards[i];
                            flag = false;
                        }
                        else
                        {
                            tempCardsView += ", " + arryCards[i];
                            flag = true;
                        }
                    }
                }
                tempCardsView += ";";
            }
            else
            {
                tempCardsView = arryCards[0] + ";";
            }
            return tempCardsView;
        }

        private void GiveCard(string[] arryCardsPlayer, string[] allArryCards)
        {
            int index = Array.FindIndex(arryCardsPlayer, 0, SearchMatches);
            arryCardsPlayer[index] = GetRendomCard(allArryCards);
        }

        private bool SearchMatches(string card)
        {
            if (card == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GetInitialCard(string[] playerArryCards, string[] npcArryCards, int bet)
        {
            data.MoneyPlayer = data.MoneyPlayer - bet;

            npcArryCards[0] = GetRendomCard(cards);
            npcArryCards[1] = GetRendomCard(cards);

            playerArryCards[0] = GetRendomCard(cards);
            playerArryCards[1] = GetRendomCard(cards);

            pointsArryNPC = GetPointsByCards(npcArryCards);
            pointsArryPlayer = GetPointsByCards(playerArryCards);

            pointNPCview = pointsArryNPC[0];
            pointNPC = pointsArryNPC[0] + pointsArryNPC[1];
            pointPlayerView = pointsArryPlayer[0] + pointsArryPlayer[1];

            cardsNPCview = npcArryCards[0] + @", [Hidden cards]";
            cardsPlayerView = playerArryCards[0] + ", " + playerArryCards[1];
        }

        private int[] GetPointsByCards(string[] arryCards)
        {
            int[] tempArry = new int[arryCards.Length];

            for (int i = 0; i < arryCards.Length; i++)
            {
                if (arryCards[i] != null)
                {
                    string[] arry = arryCards[i].Split(new char[] { '-' });
                    switch (arry[0])
                    {
                        case "Jack":
                        case "Queen":
                        case "King":
                            tempArry[i] = 10;
                            break;
                        case "Ace":
                            tempArry[i] = 11;
                            break;
                        default:
                            tempArry[i] = Int32.Parse(arry[0]);
                            break;
                    }
                }
            }
            return tempArry;
        }

        private string[] GetCardDeck()
        {
            List<string> tempCards = new List<string>();

            string[] suit = { "Spade", "Heart", "Diamond", "Club" };
            string[] cardsArray = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

            for (int i = 0; i < cardsArray.Length; i++)
            {
                for (int j = 0; j < suit.Length; j++)
                {
                    tempCards.Add(cardsArray[i] + "-" + suit[j]);
                }
            }
            return tempCards.ToArray();
        }

        private string GetRendomCard(string[] cards)
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

        private void ViewCosole(int moneyPlayer, int bet, int pointsNPC = 0, int pointsPlayer = 0, string cardsNPC = "none", string cardsPlayer = "none")
        {
            Console.Clear();
            view.MainView(ConsoleColor.Green, data.NameNPC, pointsNPC, cardsNPC);
            view.AdditionsView(bet);
            view.MainView(ConsoleColor.Yellow, data.NamePlayer, pointsPlayer, cardsPlayer, moneyPlayer);
        }

        private int GetBet()
        {
            Console.WriteLine();
            Console.WriteLine(dictTextByGame["rates"]);
            Console.WriteLine(dictTextByGame["putAbet"]);
            do
            {
                int temp = 0;
                string tempStr = Console.ReadLine();
                if (regex.IsMatch(tempStr))
                {
                    temp = Int32.Parse(tempStr);
                    if (temp == 5 || temp == 10 || temp == 25 || temp == 50 || temp == 100)
                    {
                        bet = temp;
                    }
                    else
                    {
                        Console.WriteLine(dictTextByGame["erroBet"]);
                    }
                }
            } while (bet == 0);

            return bet;
        }
    }
}
