using Game.Core.Services;
using Game.Core.Services.Implementation;
using GuessGame.Core.Game;
using GuessGame.Core.Players;
using System;
using System.Collections.Generic;
using System.IO;

namespace GuessWeightGame
{
    class Program
    {
        public IConsoleService ConsoleService = new ConsoleService();

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

        private void BreakOutput()
        {
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("");
        }

        private void PlayGame(Program main)
        {
            const string repeatGameFlag = "y";

            var players = main.TestDataset();

            var game = new GuessGameClass(players);

            var result = game.Start();

            main.ConsoleService.Log(game.GameId, "Game is over ");
            main.ConsoleService.Log(game.GameId, "Attempts: " + game.Attempts);
            main.ConsoleService.Log(game.GameId, "RealWeight: " + result.RealWeight);
            main.ConsoleService.Log(game.GameId, "Winner: " + result.WinnerName);

            if (string.IsNullOrEmpty(result.WinnerName))
            {
                main.ConsoleService.Log(game.GameId, "Closest Name: " + result.Closest.Name);
                main.ConsoleService.Log(game.GameId, "Closest Guess: " + result.Closest.Guess);
            }

            Console.WriteLine("Restart game, y/n");

            if (Console.ReadLine() == repeatGameFlag)
            {
                main.BreakOutput();

                main.PlayGame(main);
            }
        }

        static void Main(string[] args)
        {
            CheckGamesFolderExistence();

            var main = new Program();

            main.PlayGame(main);
        }

        static void CheckGamesFolderExistence()
        {
            if (!Directory.Exists(@"Games"))
            {
                Directory.CreateDirectory(@"Games");
            }
        }
    }
}
