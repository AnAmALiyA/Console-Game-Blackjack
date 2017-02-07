namespace Console_Monolit_Game_Black_Jack.Infrastructure
{
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
        Bet100 = 100,
        MinimumLimitOfMoney = 100,
        MaximumLimitOfMoney = 400
    }
}
