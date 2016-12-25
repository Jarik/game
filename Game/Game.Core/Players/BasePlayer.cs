using Game.Core.Enumerations;
using Game.Core.Services;
using Game.Core.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.Players
{
    public abstract class BasePlayer
    {
        public IRandomService RandomService = new RandomService();

        public string Name { get; set; }

        public PlayerType PlayerType { get; set; }

        public List<int> Guesses { get; set; }

        public DateTime? TryiedGuess { get; set; }
        public int WaitTime { get; set; }

        public BasePlayer(string name)
        {
            this.Name = name;
            this.Guesses = new List<int>();
        }

        public void AddWaitTime(int timeToWait)
        {
            this.TryiedGuess = DateTime.Now;
            this.WaitTime = timeToWait;
        }

        public bool IsWaiting
        {
            get
            {
                if (this.TryiedGuess.HasValue)
                {
                    var isReady = DateTime.Now < this.TryiedGuess.Value.AddMilliseconds(this.WaitTime);

                    if (isReady)
                    {
                        TryiedGuess = null;
                        WaitTime = 0;
                    }

                    return isReady;
                }

                return false;
            }
        }

        public virtual int Guess()
        {
            return RandomService.GetRandomNumber();
        }
    }
}
