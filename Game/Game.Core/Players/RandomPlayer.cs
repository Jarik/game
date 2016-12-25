using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.Players
{
    public class RandomPlayer : BasePlayer
    {
        public RandomPlayer(string name) : base(name)
        {
            this.PlayerType = Enumerations.PlayerType.RandomPlayer;
        }

        public override int Guess()
        {
            int result = base.Guess(); 
            Guesses.Add(result);
            return result;
        }
    }
}
