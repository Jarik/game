
namespace Game.Core.Players
{
	public class RandomPlayer : BasePlayer
	{
		public RandomPlayer(string name) : base(name)
		{}

		public override int Guess()
		{
            this.Sleep();

            int result = RandomService.Guess();
            Answers.Add(result);
			return result;
		}
	}
}
