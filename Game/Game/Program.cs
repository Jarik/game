using Game.Core.Game;
using Game.Core.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Program
    {
        List<BasePlayer> TestMemberSet()
        {
            var playersCount = 3;
            var players = new List<BasePlayer>();

            for (int i = 0; i < playersCount; i++)
            {
                var random1 = new MemoryPlayer("MemoryPlayer_" + i);
                players.Add(random1);
            }

            var cheater = new CheaterPlayer("CheaterPlayer");
            players.Add(cheater);

            var thoroughCheater = new ThoroughCheaterPlayer("ThoroughCheater");
            players.Add(thoroughCheater);

            return players;
        }

        static void Main(string[] args)
        {
            var main = new Program();

            var players = main.TestMemberSet();

            var game = new GuessGame(players);

            var result = game.Start();

            Console.WriteLine("Game is over ");
            Console.WriteLine("Attempts: " + game.Attempts);
            Console.WriteLine("RealWeight: " + result.RealWeight);
            Console.WriteLine("Winner: " + result.WinnerName);

            if (string.IsNullOrEmpty(result.WinnerName))
            {
                Console.WriteLine("Closest Name: " + result.Clossest.Name);
                Console.WriteLine("Closest Guess: " + result.Clossest.Guess);
            }

            Console.ReadKey();
        }
    }
}
