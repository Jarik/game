using Game.Core.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.Players
{
    public class ThoroughCheaterPlayer : ThoroughPlayer
    {
        public ThoroughCheaterPlayer(string name) : base(name)
        {
            this.PlayerType = Enumerations.PlayerType.ThoroughCheaterPlayer;
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
