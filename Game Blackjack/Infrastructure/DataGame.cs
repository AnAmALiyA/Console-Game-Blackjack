using Game_Blackjack.Interface;

namespace Game_Blackjack.Infrastructure
{
    public class DataGame : IDataGame
    {
        public DataGame(string name, int money)
        {
            this.NamePlayer = name;
            this.NameNPC = "NPC";
            this.MoneyPlayer = money;            
        }

        public string NamePlayer { get; set; }
        public string NameNPC { get; set; }
        public int MoneyPlayer { get; set; }        
    }
}
