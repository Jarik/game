using Game.Core.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.Players
{
    public class CheaterPlayer : BasePlayer
    {
        public CheaterPlayer(string name) : base(name)
        {
            this.PlayerType = Enumerations.PlayerType.CheaterPlayer;
        }

        public override int Guess()
        {
            int result = base.Guess();

            while (GuessGame.GuessedNumbers.Contains(result)) ;
            Guesses.Add(result);

            return result;
        }
    }
}
