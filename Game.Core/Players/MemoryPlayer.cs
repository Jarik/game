using System.Collections.Generic;

namespace GuessGame.Core.Players
{
	public class MemoryPlayer : BasePlayer
	{
		public List<int> WrongTryes { get; set; }

		public MemoryPlayer(string name) : base(name)
		{
			WrongTryes = new List<int>();
		}

		public override int Guess()
		{
            this.Sleep();

            int result;
			do
			{
				result = RandomService.Guess();
			}
			while (WrongTryes.Contains(result));
			Answers.Add(result);
			return result;
		}
	}
}
