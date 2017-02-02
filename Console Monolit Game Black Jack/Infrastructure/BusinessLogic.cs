using Console_Monolit_Game_Black_Jack.Entities;

namespace Console_Monolit_Game_Black_Jack.Infrastructure
{
    public class BusinessLogic
    {
        public void StartGame()
        {
            Game game = new Game();

            game.Screen();

            game.SeeRules();

            game.CreateUser();
           
            game.ExecuteFiveGames();

            game.ShowAllReport();
        }
    }
}
