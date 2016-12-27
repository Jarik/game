using Game.Core.Services;
using Game.Core.Services.Implementation;
using Game.Core.Game;
using Game.Core.Players;
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
            const string yesFlag = "y";

            Console.WriteLine("Sync game? y/n?");

            bool sync = Console.ReadLine() == yesFlag;

            var players = main.TestDataset();

            var game = new GameClass(players);

            var result = game.Start(sync);

            main.ConsoleService.Log(game.GameId, "Game is over ", sync);
            main.ConsoleService.Log(game.GameId, "Attempts: " + game.Attempts, sync);
            main.ConsoleService.Log(game.GameId, "RealWeight: " + result.RealWeight, sync);
            main.ConsoleService.Log(game.GameId, "Winner: " + result.WinnerName, sync);

            if (string.IsNullOrEmpty(result.WinnerName))
            {
                main.ConsoleService.Log(game.GameId, "Closest Name: " + result.Closest.Name, sync);
                main.ConsoleService.Log(game.GameId, "Closest Guess: " + result.Closest.Guess, sync);
            }

            Console.WriteLine("Restart game, y/n");

            if (Console.ReadLine() == yesFlag)
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
