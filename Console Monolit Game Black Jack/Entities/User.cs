using System.Collections.Generic;

namespace Console_Monolit_Game_Black_Jack.Entities
{
    public class User
    {
        public string Name { get; set; }
        public int Money { get; set; }
        public int Bet { get; set; }
        public int WinDrawLost { get; set; }
        public List<Card> Cards { get; set; }

        public User()
        {
            Cards = new List<Card>();
        }
    }
}
