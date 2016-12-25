using Game.Core.Players;
using Game.Core.Services;
using Game.Core.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game.Core.Game
{
    public class GuessGame
    {
        //ILogger _logger;
        private IRandomService RandomService = new RandomService();
        private IConsoleService ConsoleService = new ConsoleService();

        public GuessGame(List<BasePlayer> players)
        {
            _isGuessed = false;
            _isGameEnded = false;
            Players = players;
            BasketWeight = RandomService.GetRandomNumber();

            ConsoleService.Log($"Basket Weight - {BasketWeight}");

            GuessedNumbers = new List<int>();

            TimeForGame = StaticDeclarations.NumberConstants.GameTime;
        }

        private static DateTime GameStarted { get; set; }
        private static int TimeForGame { get; set; }
        public int BasketWeight { get; set; }

        public List<BasePlayer> Players { get; set; }
        public volatile int Attempts;
        public BasePlayer Winner { get; set; }

        private static bool _isGuessed;
        private static bool _isGameEnded;
        public static bool _isTimeEnded;

        private static bool Exit()
        {
            _isTimeEnded = (DateTime.Now > GameStarted.AddMilliseconds(TimeForGame));
            return (_isGuessed || _isGameEnded || _isTimeEnded);
        }

        public static List<int> GuessedNumbers { get; set; }

        public bool IsGuessed(int number, BasePlayer player)
        {
            GuessedNumbers.Add(number);

            _isGuessed = BasketWeight == number;
            Attempts++;

            _isGameEnded = Attempts == 100;

            if (_isGuessed)
            {
                if (!_isGameEnded)
                {
                    Winner = player;
                }
            }
            else
            {
                var timeToWait = WaitTime(number);
                player.AddWaitTime(timeToWait);

                ConsoleService.Log($"player {player.Name}, waits - {player.WaitTime}");
            }

            return _isGuessed;
        }

        public int WaitTime(int number)
        {
            return Math.Abs(BasketWeight - number);
        }

        public GameResult Start()
        {
            GameStarted = DateTime.Now;

            while (!Exit())
            {
                foreach (var player in Players)
                {
                    if (!player.IsWaiting)
                    {
                        var num = player.Guess();

                        if (Exit())
                        {
                            break;
                        }

                        ConsoleService.Log($"player {player.Name} atttempts - {num}");

                        IsGuessed(num, player);
                    }
                }
            }

            var result = GetResult();
            return result;
        }

        private GameResult GetResult()
        {
            var result = new GameResult();
            result.RealWeight = BasketWeight;

            if (Winner != null)
            {
                result.WinnerName = Winner.Name;
            }

            var allNearestNumbers = new List<PlayerGuess>();

            foreach (var item in Players)
            {
                if (item.Guesses.Count == 0)
                {
                    continue;
                }

                int closestGuess = item.Guesses.Aggregate(
                    (x, y) => Math.Abs(x - BasketWeight) < Math.Abs(y - BasketWeight) ? x : y);

                var playerGuess = new PlayerGuess();
                playerGuess.Guess = closestGuess;
                playerGuess.Name = item.Name;

                allNearestNumbers.Add(playerGuess);
            }

            if (allNearestNumbers.Count > 0)
            {
                var closest = allNearestNumbers.Aggregate(
                    (x, y) => Math.Abs(x.Guess - BasketWeight) < Math.Abs(y.Guess - BasketWeight) ? x : y);

                result.Clossest = closest;
            }

            return result;
        }
    }
}
