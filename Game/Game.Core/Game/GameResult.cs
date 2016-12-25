using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.Game
{
    public class GameResult
    {
        public GameResult()
        {
            Clossest = new PlayerGuess();
        }

        public int RealWeight { get; set; }
        public string WinnerName { get; set; }

        public PlayerGuess Clossest { get; set; }
    }

    public class PlayerGuess
    {
        public string Name { get; set; }
        public int Guess { get; set; }
    }
}
