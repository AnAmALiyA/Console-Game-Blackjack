using Console_Monolit_Game_Black_Jack.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace Console_Monolit_Game_Black_Jack.Entities
{
    public class Game
    {
        CardDeck CardDeck { get; set; }
        Validation Validation { get; set; }
        List<ReportGame> ListReportGame { get; set; }
        public List<User> UserGame { get; set; }
        int NumberCurrentGame { get; set; }
        int Bet { get; set; }

        bool ExitGame { get; set; }
        bool FinishCoursePlayer { get; set; }
        bool FinishCourseComputer { get; set; }
        bool RepeatBet { get; set; }
        bool NextCourseComputer { get; set; }

        enum RestrictionsEnum
        {
            Lost = -1,
            Draw,
            Win,
            TowCards,
            NoValues = 0,
            LimitForTakingNextCardNPC = 17,
            LimitPointForWin = 21,
            Bet5 = 5,
            Bet10 = 10,
            Bet25 = 25,
            Bet50 = 50,
            Bet100 = 100
        }

        public Game()
        {
            this.CardDeck = new CardDeck();
            this.Validation = new Validation();
            this.ListReportGame = new List<ReportGame>();
            this.UserGame = new List<User>();
            UserGame.Add(new User { Name = "NPC" });       
        }

        public void Screen()
        {
            ViewConsole.Title();
            ViewConsole.PreView();
            Thread.Sleep(1000);
        }

        public void SeeRules()
        {
            ViewConsole.Clear();
            ViewConsole.AskSeeRules();
            if (ViewConsole.ReadKey() == "F1")
            {
                ViewConsole.ShowRules();
            }
        }

        public void CreateUser()
        {
            User player = new User();

            ViewConsole.Clear();
            ViewConsole.EnterName();
            Validation.ExamineEnterName(player);

            ViewConsole.Clear();
            ViewConsole.EnterAmountOfMoney();
            Validation.ExamineEnterMoney(player);

            UserGame.Add(player);
        }
        
        public int SumPointsCards(User user)
        {
            int tempSum = 0;
            for (int i = 0; i < user.Cards.Count; i++)
            {
                tempSum += user.Cards[i].ValueCard;
            }
            return tempSum;
        }

        public void ViewDataUser()
        {
            ViewConsole.Clear();
            ViewConsole.AdditionsView(Bet);
            try
            {
            foreach (User user in UserGame)
            {
                    if (user.Cards.Count == 0)
                    {
                        if (user.Name == "NPC")
                        {
                            ViewConsole.MainView(user.Name, (int)RestrictionsEnum.NoValues, null);
                        }
                        else if (user.Money != 0)
                        {
                            ViewConsole.MainView(user.Name, (int)RestrictionsEnum.NoValues, null, user.Money);
                        }
                        else
                        {
                            ViewConsole.MainView(user.Name, (int)RestrictionsEnum.NoValues, null);
                        }
                    }
                    else
                    {
                        if (user.Name == "NPC")
                        {
                            ViewConsole.MainView(user.Name, user.Cards[0].ValueCard, user.Cards[0].NameCard + "-" + user.Cards[0].SuitCard + @", [Hidden cards.]");

                        }
                        else
                        {
                            ViewConsole.MainView(user.Name, SumPointsCards(user), ViewConsole.RowNamesCards(GetArrayNameCards(user), GetArraySuitCards(user)), user.Money);
                        }
                    }
            }
            }
            catch (Exception ex)
            {
                ViewConsole.Error(ex.Message);
            }
        }

        private string[] GetArraySuitCards(User user)
        {
            string[] tempList = new string[user.Cards.Count];
            for (int i = 0; i < user.Cards.Count; i++)
            {
                tempList[i] = user.Cards[i].SuitCard;
            }
            return tempList;
        }

        string[] GetArrayNameCards(User user)
        {
            string[] tempList = new string[user.Cards.Count];
            for (int i = 0; i < user.Cards.Count; i++)
            {
                tempList[i] = user.Cards[i].NameCard;
            }
            return tempList;
        }

        public void GetBetUser(User user)
        {
            if ((user.Money - (int)RestrictionsEnum.Bet5) < 0)
            {
                ViewConsole.ErrorNotEnoughMoney();
                ViewConsole.ReadKey();
                Environment.Exit(0);
            }
            else
            {
                ViewConsole.GetBet();

                string pattern = @"^[0-9]+$";
                Regex regex = new Regex(pattern);
                do
                {
                    int temp = 0;
                    string tempStr = ViewConsole.ReadLine();
                    if (regex.IsMatch(tempStr))
                    {
                        temp = Int32.Parse(tempStr);
                        if (temp == (int)RestrictionsEnum.Bet5 || temp == (int)RestrictionsEnum.Bet10 || temp == (int)RestrictionsEnum.Bet25 || temp == (int)RestrictionsEnum.Bet50 || temp == (int)RestrictionsEnum.Bet100)
                        {
                            Bet = temp;
                        }
                        else
                        {
                            ViewConsole.ErrorBet();
                        }
                    }

                    if (user.Money - Bet < 0)
                    {
                        ViewConsole.ErrorBetMoney();
                    }
                } while (Bet == 0 | (user.Money - Bet < 0));

                user.Money = user.Money - Bet;
                user.Bet = Bet;
            }
        }

        public void CreateCardDeckShuffle()
        {
            CardDeck.CreateCardDeck();
            CardDeck.ShuffleCardDeck();
        }

        public void DistributionCards()
        {
            ViewConsole.DistributionCards();
            Thread.Sleep(1000);
            
            for (int i = 0; i < UserGame.Count; i++)
            {
                GetCard(UserGame[i]);
                GetCard(UserGame[i]);
            }
        }

        public void GetCard(User user)
        {
            user.Cards.Add(CardDeck.ListCards[0]);
            CardDeck.ListCards.RemoveAt(0);
        }

        public void GetGame()
        {
            do
            {
                if (NextCourseComputer)
                {
                    ComputerCourse();
                    if (!ExitGame)
                    {
                        ViewConsole.ComputerCourse();
                        Thread.Sleep(1000);
                    }
                }
                NextCourseComputer = true;

                if (ExitGame)
                {
                    break;
                }
                ViewDataUser();

                if (!FinishCoursePlayer)
                {
                    ViewConsole.GiveCard();

                    switch (ViewConsole.ReadKey())
                    {
                        case "F1":
                            ViewConsole.ShowRules();
                            NextCourseComputer = false;
                            break;

                        case "F2":
                            PlayerCourse();
                            break;

                        case "F3":
                            FinishCoursePlayer = true;
                            break;

                        case "F4":
                            if (UserGame[1].Cards.Count == (int)RestrictionsEnum.TowCards & UserGame[1].Cards[0].NameCard == UserGame[1].Cards[1].NameCard)
                            {
                                if (GetRepeatBet(UserGame[1]))
                                {
                                    SplitCards(UserGame[1]);
                                }
                                else
                                {
                                    ViewConsole.ErrorRepeatBet();
                                    NextCourseComputer = false;
                                }
                            }
                            else
                            {
                                ViewConsole.errorSplit();
                                NextCourseComputer = false;
                            }                           
                            break;

                        case "F5":
                            if (!RepeatBet)
                            {
                                if (!GetRepeatBet(UserGame[1]))
                                {
                                    ViewConsole.ErrorRepeatBet();                                    
                                }
                                RepeatBet = true;
                                NextCourseComputer = false;
                            }
                            break;

                        case "F6":
                            if (UserGame[1].Cards.Count==(int)RestrictionsEnum.TowCards)
                            {
                                if (GetRepeatBet(UserGame[1]))
                                {
                                    FinishCoursePlayer = true;
                                    GetCard(UserGame[1]);
                                }
                                else
                                {
                                    ViewConsole.ErrorRepeatBet();
                                    NextCourseComputer = false;
                                }                               
                            }
                            break;

                        case "F7":
                            ExitGame = true;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    ViewConsole.ComputerCourse();
                    Thread.Sleep(1000);
                }

            } while (!ExitGame);
        }

        void PlayerCourse()
        {
            GetCard(UserGame[1]);
            AnalysisRuond();
        }

        void ComputerCourse()
        {
            if (SumPointsCards(UserGame[0]) <= (int)RestrictionsEnum.LimitForTakingNextCardNPC)
            {
                GetCard(UserGame[0]);
            }
            else
            {
                FinishCourseComputer = true;                
            }
            AnalysisRuond();
            RepeatBet = false;
        }

        void AnalysisRuond()
        {
            if (SumPointsCards(UserGame[1]) > (int)RestrictionsEnum.LimitPointForWin)
            {
                ViewConsole.End((int)RestrictionsEnum.Lost);
                UserGame[1].WinDrawLost = (int)RestrictionsEnum.Lost;                
                ExitGame = true;
            }

            if (SumPointsCards(UserGame[0]) > (int)RestrictionsEnum.LimitPointForWin)
            {
                ViewConsole.End((int)RestrictionsEnum.Win);
                UserGame[1].WinDrawLost = (int)RestrictionsEnum.Win;
                UserGame[1].Money += UserGame[1].Bet*2;
                ExitGame = true;
            }

            if (!ExitGame & FinishCourseComputer & FinishCoursePlayer)
            {
                if (SumPointsCards(UserGame[0]) == (int)RestrictionsEnum.LimitPointForWin & SumPointsCards(UserGame[1]) == (int)RestrictionsEnum.LimitPointForWin)
                {
                    if (UserGame[0].Cards.Count==(int)RestrictionsEnum.TowCards & UserGame[1].Cards.Count == (int)RestrictionsEnum.TowCards)
                    {
                        ViewConsole.End((int)RestrictionsEnum.Draw);
                        UserGame[1].WinDrawLost = (int)RestrictionsEnum.Draw;
                        UserGame[1].Money += UserGame[1].Bet;
                    }
                    else if (UserGame[0].Cards.Count == (int)RestrictionsEnum.TowCards)
                    {
                        ViewConsole.End((int)RestrictionsEnum.Lost);
                        UserGame[1].WinDrawLost = (int)RestrictionsEnum.Lost;
                    }
                    else if (UserGame[1].Cards.Count == (int)RestrictionsEnum.TowCards)
                    {
                        ViewConsole.End((int)RestrictionsEnum.Win);
                        UserGame[1].WinDrawLost = (int)RestrictionsEnum.Win;
                        UserGame[1].Money += UserGame[1].Bet * 2;
                    }
                    else
                    {
                        ViewConsole.End((int)RestrictionsEnum.Draw);
                        UserGame[1].WinDrawLost = (int)RestrictionsEnum.Draw;
                        UserGame[1].Money += UserGame[1].Bet;
                    }
                    ExitGame = true;
                }

                if (!ExitGame)
                {
                    if (SumPointsCards(UserGame[0]) == SumPointsCards(UserGame[1]))
                    {
                        ViewConsole.End((int)RestrictionsEnum.Draw);
                        UserGame[1].WinDrawLost = (int)RestrictionsEnum.Draw;
                        UserGame[1].Money += UserGame[1].Bet;
                    }
                    else if (SumPointsCards(UserGame[0]) > SumPointsCards(UserGame[1]))
                    {
                        ViewConsole.End((int)RestrictionsEnum.Lost);
                        UserGame[1].WinDrawLost = (int)RestrictionsEnum.Lost;
                    }
                    else
                    {
                        if (SumPointsCards(UserGame[1]) == (int)RestrictionsEnum.LimitPointForWin)
                        {
                            ViewConsole.End((int)RestrictionsEnum.Win);
                            UserGame[1].WinDrawLost = (int)RestrictionsEnum.Win;
                            double temp = UserGame[1].Bet * 1.5;
                            UserGame[1].Money += (int)temp;
                        }
                        else
                        {
                            ViewConsole.End((int)RestrictionsEnum.Win);
                            UserGame[1].WinDrawLost = (int)RestrictionsEnum.Win;
                            UserGame[1].Money += UserGame[1].Bet * 2;
                        }
                    }
                    ExitGame = true;
                }
            }

            if (ExitGame)
            {
                Thread.Sleep(1000);
            }
            else
            {
                ViewDataUser();
            }
        }

        bool GetRepeatBet(User user)
        {
            if ((user.Money - user.Bet) > 0)
            {
                user.Money = user.Money - user.Bet;
                Bet += user.Bet;
                return true;
            }
            else
            {

                return false;                
            }
        }

        void SplitCards(User user)
        {
            user.Cards.RemoveAt(1);
            GetCard(user);
        }

        public void GetReport()
        {
            NumberCurrentGame++;
            ViewConsole.Clear();            
            ViewConsole.NumberGame(NumberCurrentGame);

            foreach (User user in UserGame)
            {
                ReportGame report = new ReportGame();
                report.NumberGame = NumberCurrentGame;
                report.User = user;
                ViewConsole.ReportGame(user.Name, user.Money, user.WinDrawLost, user.Bet, SumPointsCards(user), ViewConsole.RowNamesCards(GetArrayNameCards(user), GetArraySuitCards(user)));
                ListReportGame.Add(report);
            }
            ViewConsole.LineInConsole();
        }

        public void ClearResult()
        {
            if (UserGame[1].Cards.Count != 0 )
            {
                List<User> tempUserGame = new List<User>();

                for (int i = 0; i < UserGame.Count; i++)
                {
                    tempUserGame.Add(new User { Name = UserGame[i].Name, Money = UserGame[i].Money });
                }
                UserGame = tempUserGame;
            }

            Bet = 0;
            ExitGame = false;
            FinishCoursePlayer = false;
            FinishCourseComputer = false;
            RepeatBet = false;
            NextCourseComputer = false;
        }

        public void ExecuteGames()
        {
                ClearResult();
                ViewDataUser();
                GetBetUser(UserGame[1]);
                ViewDataUser();
                CreateCardDeckShuffle();
                DistributionCards();
                ViewDataUser();
                GetGame();
                GetReport();
            ViewConsole.PressContinue();
            if (ViewConsole.ReadKey().ToLower() == "y")
            {
                ExecuteGames();
            }
        }

        public void ShowAllReport()
        {
            ViewConsole.Clear();
            foreach (ReportGame report in ListReportGame)
            {
                ViewConsole.NumberGame(report.NumberGame);
                ViewConsole.ReportGame(report.User.Name, report.User.Money, report.User.WinDrawLost, report.User.Bet, SumPointsCards(report.User), ViewConsole.RowNamesCards(GetArrayNameCards(report.User), GetArraySuitCards(report.User)));                
                ViewConsole.LineInConsole();
            }            
            ViewConsole.ReadKey();
        }
    }
}
