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
        Inspection Inspect { get; set; }
        List<ReportGame> ListReportGame { get; set; }
        public List<User> UserGame { get; set; }
        int Bet { get; set; }
        ConsoleColor[] Color { get; set; }

        bool ExitGame { get; set; }
        bool FinishCoursePlayer { get; set; }
        bool FinishCourseComputer { get; set; }
        bool RepeatBet { get; set; }
        bool NextCourseComputer { get; set; }

        enum RestrictionsEnum
        {
            NoValues = 0,
            Win = 1,
            Draw = 0,
            Lost =-1,
            TowCards = 2,
            ExecuteFiveGames = 5,
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
            this.Inspect = new Inspection();
            this.ListReportGame = new List<ReportGame>();
            this.UserGame = new List<User>();
            UserGame.Add(new User { Name = "NPC" });            
            this.Color = new ConsoleColor[] { ConsoleColor.Green, ConsoleColor.Yellow };            
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
            if (Console.ReadKey().Key == ConsoleKey.F1)
            {
                ViewConsole.ShowRules();
            }
        }

        public void CreateUser()
        {
            User player = new User();

            ViewConsole.Clear();
            ViewConsole.EnterName();
            Inspect.ExamineEnterName(player);

            ViewConsole.Clear();
            ViewConsole.EnterAmountOfMoney();
            Inspect.ExamineEnterMoney(player);

            UserGame.Add(player);
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
                            ViewConsole.MainView(ConsoleColor.Green, user.Name, (int)RestrictionsEnum.NoValues, null);
                        }
                        else if(user.Money!=0)   
                        {
                            ViewConsole.MainView(ConsoleColor.Yellow, user.Name, (int)RestrictionsEnum.NoValues, null, user.Money);
                        }
                        else
                        {
                            ViewConsole.MainView(ConsoleColor.Yellow, user.Name, (int)RestrictionsEnum.NoValues, null);
                        }
                    }
                    else
                    {
                        if (user.Name == "NPC")
                        {
                            ViewConsole.MainView(ConsoleColor.Green, user.Name, user.Cards[0].ValueCard, user.Cards[0].NameCard + "-" + user.Cards[0].SuitCard + @", [Hidden cards.]");

                        }
                        else
                        {
                            ViewConsole.MainView(ConsoleColor.Yellow, user.Name, user.SumPointsCards(), user.RowNamesCards(), user.Money);
                        }
                    }
            }
            }
            catch (Exception ex)
            {
                ViewConsole.Error(ex.Message);
            }
        }

        public void GetBetUser(User user)
        {
            if ((user.Money - (int)RestrictionsEnum.Bet5) < 0)
            {
                ViewConsole.ErrorNotEnoughMoney();
                Console.ReadKey();
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
                    string tempStr = Console.ReadLine();
                    if (regex.IsMatch(tempStr))
                    {
                        temp = Int32.Parse(tempStr);
                        if (temp == (int)RestrictionsEnum.Bet5 || temp == (int)RestrictionsEnum.Bet10 || temp == (int)RestrictionsEnum.Bet25 || temp == (int)RestrictionsEnum.Bet50 || temp == (int)RestrictionsEnum.Bet100)
                        {
                            Bet = temp;
                        }
                        else
                        {
                            ViewConsole.ErroBet();
                        }
                    }

                    if (user.Money - Bet < 0)
                    {
                        ViewConsole.ErroBetMoney();
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
            user.Cards.Add(CardDeck.listCards[0]);
            CardDeck.listCards.RemoveAt(0);
        }

        public void GetGame()
        {
            ReportGame report = new ReportGame();
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

                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.F1:
                            ViewConsole.ShowRules();
                            NextCourseComputer = false;
                            break;

                        case ConsoleKey.F2:
                            PlayerCourse();
                            break;

                        case ConsoleKey.F3:
                            FinishCoursePlayer = true;
                            break;

                        case ConsoleKey.F4:
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
                                ViewConsole.ErrorSplit();
                                NextCourseComputer = false;
                            }                           
                            break;

                        case ConsoleKey.F5:
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

                        case ConsoleKey.F6:
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

                        case ConsoleKey.F7:
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
            if (UserGame[0].SumPointsCards() <= (int)RestrictionsEnum.LimitForTakingNextCardNPC)
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
            if (UserGame[1].SumPointsCards() > (int)RestrictionsEnum.LimitPointForWin)
            {
                ViewConsole.End((int)RestrictionsEnum.Lost);
                UserGame[1].WinLost = (int)RestrictionsEnum.Lost;
                ExitGame = true;
            }

            if (UserGame[0].SumPointsCards() > (int)RestrictionsEnum.LimitPointForWin)
            {
                ViewConsole.End((int)RestrictionsEnum.Win);
                UserGame[1].WinLost = (int)RestrictionsEnum.Win;
                ExitGame = true;
            }

            if (!ExitGame & FinishCourseComputer & FinishCoursePlayer)
            {
                if (UserGame[0].SumPointsCards() == (int)RestrictionsEnum.LimitPointForWin & UserGame[1].SumPointsCards() == (int)RestrictionsEnum.LimitPointForWin)
                {
                    if (UserGame[0].Cards.Count==(int)RestrictionsEnum.TowCards & UserGame[1].Cards.Count == (int)RestrictionsEnum.TowCards)
                    {
                        ViewConsole.End((int)RestrictionsEnum.Draw);
                        UserGame[1].WinLost = (int)RestrictionsEnum.Draw;
                    }
                    else if (UserGame[0].Cards.Count == (int)RestrictionsEnum.TowCards)
                    {
                        ViewConsole.End((int)RestrictionsEnum.Lost);
                        UserGame[1].WinLost = (int)RestrictionsEnum.Lost;
                    }
                    else if (UserGame[1].Cards.Count == (int)RestrictionsEnum.TowCards)
                    {
                        ViewConsole.End((int)RestrictionsEnum.Win);
                        UserGame[1].WinLost = (int)RestrictionsEnum.Win;
                    }
                    else
                    {
                        ViewConsole.End((int)RestrictionsEnum.Draw);
                        UserGame[1].WinLost = (int)RestrictionsEnum.Draw;
                    }
                    ExitGame = true;
                }

                if (!ExitGame)
                {
                    if (UserGame[0].SumPointsCards()>UserGame[1].SumPointsCards())
                    {
                        ViewConsole.End((int)RestrictionsEnum.Lost);
                        UserGame[1].WinLost = (int)RestrictionsEnum.Lost;
                    }
                    else
                    {
                        ViewConsole.End((int)RestrictionsEnum.Win);
                        UserGame[1].WinLost = (int)RestrictionsEnum.Win;
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
            ReportGame report = new ReportGame();
            report.NumberGame = ListReportGame.Count + 1;

            ViewConsole.Clear();
            ViewConsole.NumberGame(report.NumberGame);
            foreach (User user in UserGame)
            {
                report.User = user;
                ViewConsole.ReportGame(user.Name, user.Money, user.WinLost, user.Bet, user.SumPointsCards(), user.RowNamesCards());                
            }            
            ListReportGame.Add(report);

            ViewConsole.LineConsole();
            ViewConsole.PressContinue();
            Console.ReadKey();
        }

        public void ClearResult()
        {
            for (int i = 0; i < UserGame.Count; i++)
            {
                if (UserGame[i].Cards.Count != 0)
                {
                    UserGame[i].Cards.RemoveRange(0, UserGame[i].Cards.Count);
                }                
            }

            Bet = 0;
            ExitGame = false;
            FinishCoursePlayer = false;
            FinishCourseComputer = false;
            RepeatBet = false;
            NextCourseComputer = false;
        }

        public void ExecuteFiveGames()
        {
            for (int i = 0; i < (int)RestrictionsEnum.ExecuteFiveGames; i++)
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
            }           
        }

        public void ShowAllReport()
        {
            ViewConsole.Clear();
            foreach (ReportGame report in ListReportGame)
            {
                foreach (User user in UserGame)
                {
                    ViewConsole.NumberGame(report.NumberGame);
                    report.User = user;
                    ViewConsole.ReportGame(user.Name, user.Money, user.WinLost, user.Bet, user.SumPointsCards(), user.RowNamesCards());
                }
                ViewConsole.LineConsole();
            }
            ViewConsole.PressContinue();
            Console.ReadKey();
        }
    }
}
