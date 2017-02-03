using System;
using System.Collections.Generic;

namespace Console_Monolit_Game_Black_Jack.Infrastructure
{
    public static class ViewConsole
    {
        static Dictionary<string, string> dictTextByGame = new Dictionary<string, string>();
        static ViewConsole()
        {
            dictTextByGame.Add("nameGame", "Black jack");
            dictTextByGame.Add("screen", "\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t\tBlack jack");
            dictTextByGame.Add("info", "Enter your Name and the amount of money that you want to have for this game.");
            dictTextByGame.Add("enterName", "Please enter your Name here: ");
            dictTextByGame.Add("enterMoney", "Please enter the amount of money that you want to have for this game(It must be an integer and at least 100 and not more than 400): ");
            dictTextByGame.Add("continueGame", "Please press key \"y\" to continue game.");

            dictTextByGame.Add("errorName", "\nYou have entered the incorrect name. Make sure that it is made up of characters.\n");
            dictTextByGame.Add("errorNumber", "\nYou have entered the wrong number. Make sure that it is a positive integer and at least 100 and not more than 400)\n");
            dictTextByGame.Add("errorFields", "The fields have empty values.");
            dictTextByGame.Add("errorBet", "\nYou have entered an incorrect rate. Try it again.");
            dictTextByGame.Add("errorBetMoney", "\nYou do not have the money to bet. Try it again.");
            dictTextByGame.Add("errorNotEnoughMoney", "\nYou do not have the money. You LOST.");
            dictTextByGame.Add("errorSplit", "With these cards you can not use split.");
            dictTextByGame.Add("errorepeatBet", "You can not repeat rate.");
            dictTextByGame.Add("errorDouble", "You can not double down.");

            dictTextByGame.Add("didBet", "\nBet: {0}\n\n");
            dictTextByGame.Add("nameMoneyUser", "{0}\t\t\t\t\tSum: {1}\n");
            dictTextByGame.Add("nameMoneyNPC", "{0}\t\t\t\t\t");
            dictTextByGame.Add("pointsCards", "Points:{0}\tCards: {1}\n");

            dictTextByGame.Add("getNPCcard", "NPC takes the card.");
            dictTextByGame.Add("distribution", "Distribution of cards.");
            dictTextByGame.Add("askGiveCard", "\nYou will take a card?");
            dictTextByGame.Add("putAbet", "Put a bet.");
            dictTextByGame.Add("rates", "\nPossible rates: 5, 10, 25, 50 and 100.");
            dictTextByGame.Add("notMoney", "You don't have enough money.");
            dictTextByGame.Add("win", "\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t\tYou win!");
            dictTextByGame.Add("lose", "\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t\tYou lose!");
            dictTextByGame.Add("draw", "\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t\tDraw!");
            dictTextByGame.Add("npc", "\nNPC course.");

            dictTextByGame.Add("numberGame", "\n\nNumber game:{0}");
            dictTextByGame.Add("namePlayer", "Name player - {0}");
            dictTextByGame.Add("winPlayer", "\t\tYou - WIN.");
            dictTextByGame.Add("lostPlayer", "\t\tYou - LOST.");
            dictTextByGame.Add("drawPlayer", "\t\tYou - DRAW.");
            dictTextByGame.Add("haveMoneyBet", "You have money: {0}\t\tBet: {1}");
            dictTextByGame.Add("pointsReport", "Your point: {0}");
            dictTextByGame.Add("cardsReport", "Your cards: {0}");

            dictTextByGame.Add("outOnce", "The output result of the game");
            dictTextByGame.Add("outPut", "The output of all the game results");
            dictTextByGame.Add("continueRound", "You will play new round? If yes, press - Y, or any other key - no.");
            dictTextByGame.Add("continue", "You will play again? If yes, press - Y, or any other key - no.");

            dictTextByGame.Add("seeRules", "\nYou may see rules if you press F1 or any key to continue.\n");
            dictTextByGame.Add("presKey", "\n\nTo exit, press any key.");
            dictTextByGame.Add("helpCardVal", "Card values:\n"
                + "\tAce(A) - 1 or 11 points\n"
                + "\tCards with pictures (J, Q, K) - 10 points\n"
                + "\tThe rest of the cards - in accordance with their dignity");
            dictTextByGame.Add("helpYouWin", "You win:\n"
                + "\tIf the sum of your cards is closer to 21 than the sum of the dealer's\n"
                + "\t cards, and in this case, your bet pays 1 to 1. If you have Black Jack\n"
                + "\tand the dealer does not have blackjack, your bet pays 3 to 2.");
            dictTextByGame.Add("helpDraw", "Draw:\n"
                + "\tIf the sum of your cards is equal to the sum of the values of the\n"
                + "\t dealer's cards, it is declared a draw, and you get your bet back.\n"
                + "\t Remember that the combination of Black Jack always beats the amount\n"
                + "\tof cards 21, typed more than two cards.");
            dictTextByGame.Add("helpBust", "Bust:\n"
                + "\tIf the sum of your cards exceeds 21, you \"moved\".In this case, you\n"
                + "\tlose your bet.");
            dictTextByGame.Add("helpSplit", "Split:\n"
                + "\tIf the first two cards received by you have the same advantage, you\n"
                + "\tcan split them into two \"arms\".To do this, you will need to click\n"
                + "\tthe \"Split\" button to make an additional bet, equal in magnitude\n"
                + "\thave made you (bet is made automatically). You can then dial the card\n"
                + "\tas usual.However, if you split two aces, then each of them you will\n"
                + "\treceive only one card. If, after the division of Aces to one of them\n"
                + "\tyou get ten, then this combination is not blackjack, but just 21 points.\n"
                + "\tRemember that you can notre-splitovat already shared maps.");
            dictTextByGame.Add("helpDoubling", "Doubling:\n"
                + "\tWhen you get your first two cards, and think that a third card may\n"
                + "\talready bring you win, you can double your bet by clicking the \"Double\"\n"
                + "\t button.In this case, your bet will be doubled, and you will receive\n"
                + "\tonly tone additional card. The same applies after the Split.");

            dictTextByGame.Add("helpControl", "Control keys are available in the game Black Jack\n"
                + "\n\tTo \t\t\tPress\n"
                + "\tOpen the \"Help\" \tF1\n"
                + "\tTake the card \t\tF2\n"
                + "\tTo declare \"Enough\" \tF3\n"
                + "\tDeclare \"Split\" \tF4\n"
                + "\tRepeat Bet \t\tF5\n"
                + "\tDouble the Bet \t\tF6\n"
                + "\tExit the round \tF7");
        }

        public static void PreView()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(dictTextByGame["screen"]);            
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void errorSplit()
        {
            Console.WriteLine(dictTextByGame["errorSplit"]);
        }

        public static void Title()
        {
            Console.Title = dictTextByGame["nameGame"];
        }
        
        public static void AskSeeRules()
        {
            Console.WriteLine(dictTextByGame["seeRules"]);
        }

        public static void ShowRules()
        {
            Console.WriteLine(dictTextByGame["helpCardVal"]);
            Console.WriteLine(dictTextByGame["helpYouWin"]);
            Console.WriteLine(dictTextByGame["helpDraw"]);
            Console.WriteLine(dictTextByGame["helpBust"]);
            Console.WriteLine(dictTextByGame["helpSplit"]);
            Console.WriteLine(dictTextByGame["helpDoubling"]);
            Console.WriteLine(dictTextByGame["helpControl"]);
            Console.WriteLine(dictTextByGame["presKey"]);
            Console.ReadKey();
        }

        public static void ErrorMoney()
        {
            Console.WriteLine(dictTextByGame["errorNumber"]);
        }

        public static void EnterAmountOfMoney()
        {
            Console.Write(dictTextByGame["enterMoney"]);
        }
        
        public static void EnterName()
        {
            Console.WriteLine(dictTextByGame["enterName"]);
        }

        public static void ErrorName()
        {
            Console.WriteLine(dictTextByGame["errorName"]);            
        }

        public static void Error(string text)
        {
            Console.WriteLine(text);
        }

        public static void Clear()
        {
            Console.Clear();
        }

        public static string ReadKey()
        {
            return Console.ReadKey().Key.ToString();
        }

        public static string ReadLine()
        {
            return Console.ReadLine();
        }

        public static string RowNamesCards(string[] nameCard, string[] suitCard)
        {
            string tempStr = string.Empty;
            if (nameCard.Length == suitCard.Length)
            {
                for (int i = 0; i < nameCard.Length; i++)
                {
                    if (i == 0)
                    {
                        tempStr += nameCard[i] + "-" + suitCard[i];
                    }
                    else
                    {
                        tempStr += ", " + nameCard[i] + "-" + suitCard[i];
                    }
                }
            }
            return tempStr;
        }

        public static void MainView(string nameUser, int pointsCards, string namesCards, int money = 0)
        {
            Console.WriteLine(new string('-', 70));
            
            if (nameUser == "NPC")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(dictTextByGame["nameMoneyNPC"], nameUser);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(dictTextByGame["nameMoneyUser"], nameUser, money);
            }
            Console.ForegroundColor = ConsoleColor.White;

            if (namesCards == null)
            {
                Console.WriteLine(dictTextByGame["pointsCards"], pointsCards, "none");
            }
            else
            {
                Console.WriteLine(dictTextByGame["pointsCards"], pointsCards, namesCards);
            }
           
            Console.WriteLine(new string('-', 70));
        }

        public static void AdditionsView(int bet)
        {
            Console.Write(dictTextByGame["didBet"], bet);
        }

        public static void GetBet()
        {
            Console.WriteLine(dictTextByGame["rates"]);
            Console.WriteLine(dictTextByGame["putAbet"]);
        }

        public static void ErrorBet()
        {
            Console.WriteLine(dictTextByGame["errorBet"]);
        }

        public static void ErrorBetMoney()
        {
            Console.WriteLine(dictTextByGame["errorBetMoney"]);
        }

        public static void ErrorNotEnoughMoney()
        {
            Console.WriteLine(dictTextByGame["errorNotEnoughMoney"]);
        }

        public static void DistributionCards()
        {
            Console.WriteLine(dictTextByGame["distribution"]);
        }

        public static void GiveCard()
        {
            Console.WriteLine(dictTextByGame["askGiveCard"]);
        }

        public static void ComputerCourse()
        {
            Console.WriteLine(dictTextByGame["npc"]);
        }

        public static void End(int end)
        {
            Clear();
            if (end == 1)
            {                
                Console.WriteLine(dictTextByGame["win"]);               
            }
            else if (end == -1)
            {
                Console.WriteLine(dictTextByGame["lose"]);
            }
            else
            {
                Console.WriteLine(dictTextByGame["draw"]);                
            }
        }

        public static void ErrorRepeatBet()
        {
            Console.WriteLine(dictTextByGame["notMoney"]);
        }

        public static void NumberGame(int numberGame)
        {
            Console.WriteLine(dictTextByGame["numberGame"], numberGame);
        }

        public static void ReportGame(string nameUser, int money, int winLost, int bet, int points, string cards)
        {
            Console.WriteLine();
            if (nameUser != "NPC")
            {
                if (winLost == -1)
                {
                    Console.WriteLine(dictTextByGame["namePlayer"] + dictTextByGame["lostPlayer"], nameUser);
                }
                else if (winLost == 1)
                {
                    Console.WriteLine(dictTextByGame["namePlayer"] + dictTextByGame["winPlayer"], nameUser);
                }
                else
                {
                    Console.WriteLine(dictTextByGame["namePlayer"] + dictTextByGame["drawPlayer"], nameUser);
                }
            }
            else
            {
                Console.WriteLine(dictTextByGame["namePlayer"], nameUser);
            }

            if (nameUser != "NPC")
            {
                Console.WriteLine(dictTextByGame["haveMoneyBet"], money, bet);
            }            
            Console.WriteLine(dictTextByGame["pointsReport"], points);
            Console.WriteLine(dictTextByGame["cardsReport"], cards);            
        }

        public static void PressContinue()
        {
            Console.WriteLine(dictTextByGame["continueGame"]);
        }

        public static void LineInConsole()
        {
            Console.WriteLine(new string('-', 70));
        }
    }
}
