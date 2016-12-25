using GuessGame.Core.Game;
using System.Linq;

namespace GuessGame.Core.Players
{
	public class ThoroughCheaterPlayer : ThoroughPlayer
	{
		public ThoroughCheaterPlayer(string name) : base(name)
		{ }

		public override int Guess()
		{
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
			while (GuessGameClass.GuessedNumbers.Distinct().Contains(LastGuessed));

			Answers.Add(LastGuessed);
			return LastGuessed;
		}
	}
}
