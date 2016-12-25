using GuessGame.Core.Game;
using GuessGame.Core.Players;
using System;
using System.Collections.Generic;

namespace GuessWeightGame
{
	class Program
	{
		List<BasePlayer> TestDataset()
		{
			const int playersCount = 2;
			var players = new List<BasePlayer>();

			for (int i = 0; i < playersCount; i++)
			{
				var random1 = new MemoryPlayer($"MemoryPlayer-{i}");
				players.Add(random1);
			}

			var cheater = new CheaterPlayer("CheaterPlayer");
			players.Add(cheater);

			var thoroughCheater = new ThoroughCheaterPlayer("ThoroughCheaterPlayer");
			players.Add(thoroughCheater);

			return players;
		}

		static void Main(string[] args)
		{
			var main = new Program();

			var players = main.TestDataset();

            var game = new GuessGameClass(players);

			var result = game.Start();

			Console.WriteLine("Game is over ");
			Console.WriteLine("Attempts: " + game.Attempts);
			Console.WriteLine("RealWeight: " + result.RealWeight);
			Console.WriteLine("Winner: " + result.WinnerName);

			if (string.IsNullOrEmpty(result.WinnerName))
			{
				Console.WriteLine("Closest Name: " + result.Closest.Name);
				Console.WriteLine("Closest Guess: " + result.Closest.Guess);
			}

			Console.ReadKey();
		}
	}
}
