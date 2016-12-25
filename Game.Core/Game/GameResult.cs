using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessGame.Core.Game
{
	public class GameResult
	{
		public GameResult()
		{
			Closest = new PlayerGuess();
		}

		public int RealWeight { get; set; }

		public string WinnerName { get; set; }

		public PlayerGuess Closest { get; set; }
	}

	public class PlayerGuess
	{
		public string Name { get; set; }

		public int Guess { get; set; }
	}
}
