using Game.Core.Services;
using Game.Core.Services.Implementation;
using Game.Core.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Core.Game
{
    public class GameClass
    {
        IConsoleService ConsoleService = new ConsoleService();
        IRandomService RandomService = new RandomService();

        private static bool _isGuessed;
        private static bool _isGameEnded;

        private bool _syncFlag = false;

        List<Task> _tasks = new List<Task>();

        public GameClass(List<BasePlayer> players)
        {
            _isGuessed = false;
            _isGameEnded = false;

            this.GameId = Guid.NewGuid();

            ConsoleService.Log(this.GameId, $"Game id:  {this.GameId.ToString()}");

            Players = players;
            BasketWeight = RandomService.Guess();
            ConsoleService.Log(this.GameId, $"Basket weight : {BasketWeight}");
            GuessedNumbers = new List<int>();
            TimeForGame = Constants.GameTime;

            foreach (var player in Players)
            {
                _tasks.Add(new Task(() => DoPlayerStuff(player)));
            }
        }

        private static DateTime GameStarted { get; set; }

        private static int TimeForGame { get; set; }

        public int BasketWeight { get; set; }

        public Guid GameId { get; set; }

        public List<BasePlayer> Players { get; set; }

        public volatile int Attempts;

        public BasePlayer Winner { get; set; }

        public static bool _isTimeEnded;

        private static bool Exit()
        {
            _isTimeEnded = (DateTime.Now > GameStarted.AddMilliseconds(TimeForGame));

            if (_isTimeEnded)
            {
                Console.WriteLine("Time is up");
            }

            return (_isGuessed || _isGameEnded || _isTimeEnded);
        }

        public static List<int> GuessedNumbers { get; set; }

        public bool IsGuessed(int number, BasePlayer player)
        {
            const int attemptsCount = 100;

            GuessedNumbers.Add(number);

            _isGuessed = BasketWeight == number;
            Attempts++;

            _isGameEnded = Attempts == attemptsCount;

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

                ConsoleService.Log(this.GameId, $"player {player.Name}  waits : {player.WaitingTime}", this._syncFlag);
            }

            return _isGuessed;
        }

        public int WaitTime(int number)
        {
            return Math.Abs(BasketWeight - number);
        }

        private void DoPlayerStuff(BasePlayer player)
        {
            if (Exit())
            {
                return;
            }

            while (!Exit())
            {
                var num = player.Guess();

                ConsoleService.Log(this.GameId, $"player {player.Name} tries: " + num, this._syncFlag);
                IsGuessed(num, player);
            }
        }

        public GameResult Start(bool sync)
        {
            GameStarted = DateTime.Now;

            this._syncFlag = sync;

            if (sync)
            {
                while (!Exit())
                {
                    foreach (var player in Players)
                    {
                        DoPlayerStuff(player);
                    }
                }
            }
            else
            {
                foreach (var task in this._tasks)
                {
                    task.Start();
                }

                Task.WaitAll(this._tasks.ToArray());
            }

            return GetResult();
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
                if (!item.Answers.Any())
                {
                    continue;
                }

                int closestGuess
                    = item.Answers.Aggregate((x, y) =>
                            Math.Abs(x - BasketWeight) < Math.Abs(y - BasketWeight) ? x : y);

                var playerGuess = new PlayerGuess();
                playerGuess.Guess = closestGuess;
                playerGuess.Name = item.Name;

                allNearestNumbers.Add(playerGuess);
            }

            if (allNearestNumbers.Any())
            {
                var closest
                    = allNearestNumbers.Aggregate((x, y) =>
                        Math.Abs(x.Guess - BasketWeight) < Math.Abs(y.Guess - BasketWeight) ? x : y);

                result.Closest = closest;
            }

            return result;
        }
    }
}