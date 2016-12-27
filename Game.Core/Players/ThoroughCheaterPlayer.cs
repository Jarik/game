using Game.Core.Game;
using System.Linq;

namespace Game.Core.Players
{
	public class ThoroughCheaterPlayer : ThoroughPlayer
	{
		public ThoroughCheaterPlayer(string name) : base(name)
		{ }

		public override int Guess()
		{
            this.Sleep();

			do
			{
				if (LastGuessed == 0)
				{
					LastGuessed = Start;
				}
				else if (LastGuessed < End)
				{
					LastGuessed++;
				}
			}
			while (GameClass.GuessedNumbers.Distinct().Contains(LastGuessed));

			Answers.Add(LastGuessed);
			return LastGuessed;
		}
	}
}
