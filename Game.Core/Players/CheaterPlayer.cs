using GuessGame.Core.Game;

namespace GuessGame.Core.Players
{
	public class CheaterPlayer : BasePlayer
	{
		public CheaterPlayer(string name) : base(name)
		{}

		public override int Guess()
		{
			int result;
			do
			{
				result = RandomService.Guess();
			}
			while (GuessGameClass.GuessedNumbers.Contains(result));
			Answers.Add(result);
			return result;
		}
	}
}
