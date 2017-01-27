using Game_Blackjack.Interface;

namespace Game_Blackjack.Infrastructure
{
    public class DataGame : IDataGame
    {
        private string name, npc;
        private int moneyPlayer, moneyNPC;

        public DataGame(string name, int money)
        {
            this.name = name;
            this.NameNPC = "NPC";
            this.moneyPlayer = money;
            this.moneyNPC = money;
        }

        public string NamePlayer { get; }
        public string NameNPC { get; }
        public int MoneyPlayer { get; set; }
        public int MoneyNPC { get; set; }
    }
}
