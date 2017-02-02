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
            dictTextByGame.Add("erroSplit", "With these cards you can not use split.");
            dictTextByGame.Add("erroRepeatBet", "You can not repeat rate.");
            dictTextByGame.Add("erroDouble", "You can not double down.");

            dictTextByGame.Add("getNPCcard", "NPC takes the card.");
            dictTextByGame.Add("distribution", "Distribution of cards.");
            dictTextByGame.Add("askGiveCard", "You will take a card?");
            dictTextByGame.Add("putAbet", "Put a bet.");
            dictTextByGame.Add("rates", "Possible rates: 5, 10, 25, 50 and 100.");
            dictTextByGame.Add("notMoney", "You don't have enough money.");
            dictTextByGame.Add("win", "You win!");
            dictTextByGame.Add("lose", "You lose!");
            dictTextByGame.Add("draw", "Draw!");
            dictTextByGame.Add("npc", "NPC course.");

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
    }
}
