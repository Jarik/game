using Game.Core.Services;
using Game.Core.Services.Implementation;
using System;
using System.Collections.Generic;

namespace GuessGame.Core.Players
{
	public abstract class BasePlayer
	{
        protected IRandomService RandomService = new RandomService();

        public List<int> Answers;

		public BasePlayer(string name)
		{
			Name = name;
			Answers = new List<int>();
		}

		public string Name { get; set; }

		public DateTime? LastGuess { get; set; }
		public int WaitTime { get; set; }

		public void AddWaitTime(int timeToWait)
		{
			LastGuess = DateTime.Now;
			WaitTime = timeToWait;
		}

		public bool IsWaiting
		{
			get
			{
				if (LastGuess.HasValue)
				{
					var isReady = DateTime.Now < LastGuess.Value.AddMilliseconds(WaitTime);
					if (isReady)
					{
						LastGuess = null;
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