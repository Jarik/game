using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.Players
{
	public class ThoroughPlayer : BasePlayer
	{
		public int Start { get; set; }
		public int End { get; set; }
		public int LastGuessed { get; set; }

		public ThoroughPlayer(string name) : base(name)
		{
			Start = Constants.MinBasketWeight;
			End = Constants.MaxBasketWeight;
		}

		public override int Guess()
		{
            this.Sleep();

            if (LastGuessed == 0)
			{
				LastGuessed = Start;
			}
			else if (LastGuessed < End)
			{
				LastGuessed++;
			}

			Answers.Add(LastGuessed);

			return LastGuessed;
		}
	}
}
