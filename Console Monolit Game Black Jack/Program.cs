using Console_Monolit_Game_Black_Jack.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Monolit_Game_Black_Jack
{
    class Program
    {
        static void Main(string[] args)
        {
            BusinessLogic logic = new BusinessLogic();
            logic.StartGame();
        }
    }
}
