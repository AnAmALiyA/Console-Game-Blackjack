namespace Game_Blackjack.Interface
{
    public interface IDataGame
    {
        string NamePlayer { get; }
        string NameNPC { get; }
        int MoneyPlayer { get; set; }             
    }
}
