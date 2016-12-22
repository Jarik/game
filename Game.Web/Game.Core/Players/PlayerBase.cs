using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.Players
{
    public abstract class PlayerBase
    {
        public List<int> MyGuesses { get; set; };

        public PlayerBase(string name)
        {
            Name = name;
            MyGuesses = new List<int>();
        }

        public string Name { get; set; }
        

        PlayerType _playerType;
        public PlayerType PlayerType
        {
            get
            {
                return _playerType;
            }

            set
            {
                _playerType = value;
            }
        }

        public DateTime? TryiedGuess { get; set; }
        public int WaitTime { get; set; }

        public void AddWaitTime(int timeToWait)
        {
            TryiedGuess = DateTime.Now;
            WaitTime = timeToWait;
        }

        public bool IsWaiting
        {
            get
            {
                if (TryiedGuess.HasValue)
                {
                    var isReady = DateTime.Now < TryiedGuess.Value.AddMilliseconds(WaitTime);
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

        public abstract int Guess();
    }
}
