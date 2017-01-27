using System.Collections.Generic;

namespace Game_Blackjack.Infrastructure
{
    public class SpecificationGame
    {
        public Dictionary<string, string> dictTextByGame = new Dictionary<string, string>();

        public SpecificationGame()
        {
            dictTextByGame.Add("nameGame", "Black jack");
            dictTextByGame.Add("info", "Enter your Name and the amount of money that you want to have for this game.");
            dictTextByGame.Add("enterName", "Please enter your Name here: ");
            dictTextByGame.Add("enterMoney", "Please enter the amount of money that you want to have for this game(It must will be integer and at least 5): ");
            dictTextByGame.Add("continueGame", "Do you want to continue? Enter: Y - yes, any other button - no.");

            dictTextByGame.Add("erroName", "You have entered the incorrect name. Make sure that it is made up of characters.");
            dictTextByGame.Add("erroNumber", "You have entered the wrong number. Make sure that it is a positive integer and at least 5.");
            dictTextByGame.Add("erroFields", "The fields have empty values.");
            dictTextByGame.Add("erroBet", "You have entered an incorrect rate. Try it again.");
            dictTextByGame.Add("erroBetMoney", "Your bid spelled your money. Try it again.");

            dictTextByGame.Add("getNPCcard", "NPC takes the card.");
            dictTextByGame.Add("distribution", "Distribution of cards.");
            dictTextByGame.Add("askGiveCard", "You will take a card?");
            dictTextByGame.Add("putAbet", "Put a bet.");
            dictTextByGame.Add("rates", "Possible rates: 5, 10, 25, 50 and 100.");

            dictTextByGame.Add("continue", "You will play again? If yes, press - Y, or any other key - no.");

            dictTextByGame.Add("helpCardVal", "Card values:\n"
                + "\tAce(A) - 1 or 11 points\n"
                + "\tCards with pictures (J, Q, K) - 10 points\n"
                + "\tThe rest of the cards - in accordance with their dignity");
            dictTextByGame.Add("helpYouWin", "You win:\n"
                + "\tIf the sum of your cards is closer to 21 than the sum of the dealer's cards,\n"
                + "\tand in this case, your bet pays 1 to 1. If you have Black Jack\n"
                + "\tand the dealer does not have blackjack, your bet pays 3 to 2.");
            dictTextByGame.Add("helpDraw", "Draw:\n"
                + "\tIf the sum of your cards is equal to the sum of the values of the dealer's cards,\n"
                + "\tit is declared a draw, and you get your bet back. Remember that the combination \n"
                + "\tof Black Jack always beats the amount of cards 21, typed more than two cards.");
            dictTextByGame.Add("helpBust", "Bust:\n"
                + "\tIf the sum of your cards exceeds 21, you \"moved\".In this case, you lose your bet.");
            dictTextByGame.Add("helpSplit", "Split:\n"
                + "\tIf the first two cards received by you have the same advantage, you can split\n"
                + "\tthem into two \"arms\".To do this, you will need to click the \"Split\" button\n"
                + "\tto make an additional bet, equal in magnitude have made you (bet is made automatically).\n"
                + "\tYou can then dial the card as usual.However, if you split two aces, then each of them\n"
                + "\tyou will receive only one card. If, after the division of Aces to one of them you get ten,\n"
                + "\tthen this combination is not blackjack, but just 21 points. Remember that you can not\n"
                + "\tre-splitovat already shared maps.");
            dictTextByGame.Add("helpDoubling", "Doubling:\n"
                + "\tWhen you get your first two cards, and think that a third card may already bring you win,\n"
                + "\tyou can double your bet by clicking the \"Double\" button.In this case, your bet\n"
                + "\twill be doubled, and you will receive only one additional card. The same applies after the Split.");

            dictTextByGame.Add("helpControl", "Control keys are available in the game Black Jack\n"
                + "\tTo \tPress\n"
                + "\tRepeat Bet \tF1\n"
                + "\tPass Card \tF2\n"
                + "\tTake the card \tF3\n"
                + "\tTo declare \"Enough\" \tF4\n"
                + "\tDeclare \"Split\" \tF5\n"
                + "\tDouble the \tF6\n"
                + "\tOpen the \"Options\" \tF9\n"
                + "\tOpen the \"Help\" \tF10\n"
                + "\tExit \tF12");
        }
    }
}
