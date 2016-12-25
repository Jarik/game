
namespace GuessGame.Core.Players
{
	public class RandomPlayer : BasePlayer
	{
		public RandomPlayer(string name) : base(name)
		{}

		public override int Guess()
		{
            int result = RandomService.Guess();
            Answers.Add(result);
			return result;
		}
	}
}
