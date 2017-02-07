using Console_Monolit_Game_Black_Jack.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace Console_Monolit_Game_Black_Jack.Entities
{
    public class Game
    {
        public List<User> UserGame { get; set; }

        private CardDeck CardDeck { get; set; }
        private Validation Validation { get; set; }
        private List<ReportGame> ListReportGame { get; set; }

        private int NumberCurrentGame { get; set; }
        private int Bet { get; set; }

        private bool ExitGame { get; set; }
        private bool FinishCoursePlayer { get; set; }
        private bool FinishCourseComputer { get; set; }
        private bool RepeatBet { get; set; }
        private bool NextCourseComputer { get; set; }
        
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
            OutputDataRepresentation.Title();
            OutputDataRepresentation.PreView();
            Thread.Sleep(1000);
        }

        public void SeeRules()
        {
            OutputDataRepresentation.Clear();
            OutputDataRepresentation.AskSeeRules();
            if (OutputDataRepresentation.GetActionUser() == "F1")
            {
                OutputDataRepresentation.ShowRules();
            }
        }

        public void CreateUser()
        {
            User player = new User();

            OutputDataRepresentation.Clear();
            OutputDataRepresentation.EnterName();
            Validation.ExamineEnterName(player);

            OutputDataRepresentation.Clear();
            OutputDataRepresentation.EnterAmountOfMoney();
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
            OutputDataRepresentation.Clear();
            OutputDataRepresentation.AdditionsView(Bet);
            try
            {
                foreach (User user in UserGame)
                {
                    if (user.Cards.Count == 0)
                    {
                        if (user.Name == "NPC")
                        {
                            OutputDataRepresentation.MainView(user.Name, (int)RestrictionsEnum.NoValues, null);
                            continue;
                        }

                        if (user.Money != 0)
                        {
                            OutputDataRepresentation.MainView(user.Name, (int)RestrictionsEnum.NoValues, null, user.Money);
                            continue;
                        }

                        OutputDataRepresentation.MainView(user.Name, (int)RestrictionsEnum.NoValues, null);
                        continue;
                    }

                    if (user.Name == "NPC")
                    {
                        OutputDataRepresentation.MainView(user.Name, user.Cards[0].ValueCard, user.Cards[0].NameCard + "-" + user.Cards[0].SuitCard + @", [Hidden cards.]");
                        continue;
                    }

                    OutputDataRepresentation.MainView(user.Name, SumPointsCards(user), OutputDataRepresentation.RowNamesCards(GetArrayNameCards(user), GetArraySuitCards(user)), user.Money);
                }
            }
            catch (Exception ex)
            {
                OutputDataRepresentation.Error(ex.Message);
            }
        }

        private string[] GetArraySuitCards(User user)
        {
            string[] tempListSuitCards = new string[user.Cards.Count];
            for (int i = 0; i < user.Cards.Count; i++)
            {
                tempListSuitCards[i] = user.Cards[i].SuitCard.ToString();
            }
            return tempListSuitCards;
        }

        string[] GetArrayNameCards(User user)
        {
            string[] tempListNameCards = new string[user.Cards.Count];
            for (int i = 0; i < user.Cards.Count; i++)
            {
                tempListNameCards[i] = user.Cards[i].NameCard.ToString();
            }
            return tempListNameCards;
        }

        public void GetBetUser(User user)
        {
            if ((user.Money - (int)RestrictionsEnum.Bet5) < 0)
            {
                ErrorNotEnoughMoney();
            }
            else
            {
                ActionGetBetUser(user);
            }
        }

        private void ErrorNotEnoughMoney()
        {
            OutputDataRepresentation.ErrorNotEnoughMoney();
            OutputDataRepresentation.GetActionUser();
            Environment.Exit(0);
        }

        private void ActionGetBetUser(User user)
        {
            OutputDataRepresentation.GetBet();

            string pattern = @"^[0-9]+$";
            Regex regex = new Regex(pattern);
            do
            {
                try
                {
                    int temp = 0;
                    string tempStr = OutputDataRepresentation.GetInputUser();
                    if (regex.IsMatch(tempStr))
                    {
                        temp = Int32.Parse(tempStr);
                        if (temp == (int)RestrictionsEnum.Bet5 || temp == (int)RestrictionsEnum.Bet10 || temp == (int)RestrictionsEnum.Bet25 || temp == (int)RestrictionsEnum.Bet50 || temp == (int)RestrictionsEnum.Bet100)
                        {
                            Bet = temp;
                        }
                        else
                        {
                            OutputDataRepresentation.ErrorBet();
                        }
                    }

                    if (user.Money - Bet < 0)
                    {
                        OutputDataRepresentation.ErrorBetMoney();
                    }
                }
                catch (Exception ex)
                {
                    OutputDataRepresentation.Error(ex.Message);
                }
            } while (Bet == 0 | (user.Money - Bet < 0));

            user.Money = user.Money - Bet;
            user.Bet = Bet;
        }

        public void CreateCardDeckShuffle()
        {
            CardDeck.CreateCardDeck();
            CardDeck.ShuffleCardDeck();
        }

        public void DistributionCards()
        {
            OutputDataRepresentation.DistributionCards();
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
                        OutputDataRepresentation.ComputerCourse();
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
                    OutputDataRepresentation.GiveCard();
                    ActionPlayer();
                }
                else
                {
                    OutputDataRepresentation.ComputerCourse();
                    Thread.Sleep(1000);
                }

            } while (!ExitGame);
        }

        void ActionPlayer()
        {
            switch (OutputDataRepresentation.GetActionUser())
            {
                case "F1":
                    OutputDataRepresentation.ShowRules();
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
                            break;
                        }
                        OutputDataRepresentation.ErrorRepeatBet();
                        NextCourseComputer = false;
                        break;
                    }
                    OutputDataRepresentation.errorSplit();
                    NextCourseComputer = false;
                    break;

                case "F5":
                    if (!RepeatBet)
                    {
                        if (!GetRepeatBet(UserGame[1]))
                        {
                            OutputDataRepresentation.ErrorRepeatBet();
                        }
                        RepeatBet = true;
                        NextCourseComputer = false;
                    }
                    break;

                case "F6":
                    if (UserGame[1].Cards.Count == (int)RestrictionsEnum.TowCards)
                    {
                        if (GetRepeatBet(UserGame[1]))
                        {
                            FinishCoursePlayer = true;
                            GetCard(UserGame[1]);
                            break;
                        }
                        OutputDataRepresentation.ErrorRepeatBet();
                        NextCourseComputer = false;
                    }
                    break;

                case "F7":
                    ExitGame = true;
                    break;
                default:
                    break;
            }
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
                OutputDataRepresentation.End((int)RestrictionsEnum.Lost);
                UserGame[1].WinDrawLost = (int)RestrictionsEnum.Lost;                
                ExitGame = true;
            }

            if (SumPointsCards(UserGame[0]) > (int)RestrictionsEnum.LimitPointForWin)
            {
                OutputDataRepresentation.End((int)RestrictionsEnum.Win);
                UserGame[1].WinDrawLost = (int)RestrictionsEnum.Win;
                UserGame[1].Money += UserGame[1].Bet*2;
                ExitGame = true;
            }

            if (!ExitGame & FinishCourseComputer & FinishCoursePlayer)
            {
                GetFinalResult();
                if (!ExitGame)
                {
                    GetFinalResultPressedExit();
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

        void GetFinalResult()
        {
            if (SumPointsCards(UserGame[0]) == (int)RestrictionsEnum.LimitPointForWin & SumPointsCards(UserGame[1]) == (int)RestrictionsEnum.LimitPointForWin)
            {
                ExitGame = true;

                if (UserGame[0].Cards.Count == (int)RestrictionsEnum.TowCards & UserGame[1].Cards.Count == (int)RestrictionsEnum.TowCards)
                {
                    OutputDataRepresentation.End((int)RestrictionsEnum.Draw);
                    UserGame[1].WinDrawLost = (int)RestrictionsEnum.Draw;
                    UserGame[1].Money += UserGame[1].Bet;
                    return;
                }

                if (UserGame[0].Cards.Count == (int)RestrictionsEnum.TowCards)
                {
                    OutputDataRepresentation.End((int)RestrictionsEnum.Lost);
                    UserGame[1].WinDrawLost = (int)RestrictionsEnum.Lost;
                    return;
                }

                if (UserGame[1].Cards.Count == (int)RestrictionsEnum.TowCards)
                {
                    OutputDataRepresentation.End((int)RestrictionsEnum.Win);
                    UserGame[1].WinDrawLost = (int)RestrictionsEnum.Win;
                    UserGame[1].Money += UserGame[1].Bet * 2;
                    return;
                }

                OutputDataRepresentation.End((int)RestrictionsEnum.Draw);
                UserGame[1].WinDrawLost = (int)RestrictionsEnum.Draw;
                UserGame[1].Money += UserGame[1].Bet;
            }
            
        }

        void GetFinalResultPressedExit()
        {
            ExitGame = true;

            if (SumPointsCards(UserGame[0]) == SumPointsCards(UserGame[1]))
            {
                OutputDataRepresentation.End((int)RestrictionsEnum.Draw);
                UserGame[1].WinDrawLost = (int)RestrictionsEnum.Draw;
                UserGame[1].Money += UserGame[1].Bet;
                return;
            }

            if (SumPointsCards(UserGame[0]) > SumPointsCards(UserGame[1]))
            {
                OutputDataRepresentation.End((int)RestrictionsEnum.Lost);
                UserGame[1].WinDrawLost = (int)RestrictionsEnum.Lost;
                return;
            }

            if (SumPointsCards(UserGame[1]) == (int)RestrictionsEnum.LimitPointForWin)
            {
                OutputDataRepresentation.End((int)RestrictionsEnum.Win);
                UserGame[1].WinDrawLost = (int)RestrictionsEnum.Win;
                double temp = UserGame[1].Bet * 1.5;
                UserGame[1].Money += (int)temp;
                return;
            }

            OutputDataRepresentation.End((int)RestrictionsEnum.Win);
            UserGame[1].WinDrawLost = (int)RestrictionsEnum.Win;
            UserGame[1].Money += UserGame[1].Bet * 2;
            return;
        }

        bool GetRepeatBet(User user)
        {
            if ((user.Money - user.Bet) > 0)
            {
                user.Money = user.Money - user.Bet;
                Bet += user.Bet;
                return true;
            }
            return false;
        }

        void SplitCards(User user)
        {
            user.Cards.RemoveAt(1);
            GetCard(user);
        }

        public void GetReport()
        {
            NumberCurrentGame++;
            OutputDataRepresentation.Clear();            
            OutputDataRepresentation.NumberGame(NumberCurrentGame);

            foreach (User user in UserGame)
            {
                ReportGame report = new ReportGame();
                report.NumberGame = NumberCurrentGame;
                report.User = user;
                OutputDataRepresentation.ReportGame(user.Name, user.Money, user.WinDrawLost, user.Bet, SumPointsCards(user), OutputDataRepresentation.RowNamesCards(GetArrayNameCards(user), GetArraySuitCards(user)));
                ListReportGame.Add(report);
            }
            OutputDataRepresentation.LineInConsole();
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
            OutputDataRepresentation.PressContinue();
            if (OutputDataRepresentation.GetActionUser().ToLower() == "y")
            {
                ExecuteGames();
            }
        }

        public void ShowAllReport()
        {
            OutputDataRepresentation.Clear();
            foreach (ReportGame report in ListReportGame)
            {
                OutputDataRepresentation.NumberGame(report.NumberGame);
                OutputDataRepresentation.ReportGame(report.User.Name, report.User.Money, report.User.WinDrawLost, report.User.Bet, SumPointsCards(report.User), OutputDataRepresentation.RowNamesCards(GetArrayNameCards(report.User), GetArraySuitCards(report.User)));                
                OutputDataRepresentation.LineInConsole();
            }            
            OutputDataRepresentation.GetActionUser();
        }
    }
}
