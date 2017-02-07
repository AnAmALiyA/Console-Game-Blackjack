using Console_Monolit_Game_Black_Jack.Infrastructure;

namespace Console_Monolit_Game_Black_Jack.Entities
{
    public class Card
    {
        public EnumCards NameCard { get; set; }
        public EnumSuitCards SuitCard { get; set; }
        public int ValueCard { get; set; }
    }
}
