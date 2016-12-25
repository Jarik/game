using Game.Core.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.Players
{
    public class ThoroughPlayer : BasePlayer
    {
        public int LastGuessed { get; set; }
        public int Start { get; set; }
        public int End { get; set; }

        public ThoroughPlayer(string name) : base(name)
        {
            this.PlayerType = Enumerations.PlayerType.ThoroughPlayer;

            this.Start = StaticDeclarations.NumberConstants.MinBasketWeight;
            this.End = StaticDeclarations.NumberConstants.MaxBasketWeight;
        }

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
            while (GuessGame.GuessedNumbers.Distinct().Contains(LastGuessed));

            Guesses.Add(LastGuessed);
            return LastGuessed;
        }
    }
}
