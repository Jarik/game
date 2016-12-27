using Game.Core.Game;

namespace Game.Core.Players
{
	public class CheaterPlayer : BasePlayer
	{
		public CheaterPlayer(string name) : base(name)
		{}

		public override int Guess()
		{
            this.Sleep();

            int result;
			do
			{
				result = RandomService.Guess();
			}
			while (GameClass.GuessedNumbers.Contains(result));
			Answers.Add(result);
			return result;
		}
	}
}
