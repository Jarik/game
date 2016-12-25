using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.Players
{
    public class MemoryPlayer : BasePlayer
    {
        public List<int> WrongTryes { get; set; }

        public MemoryPlayer(string name) : base(name)
        {
            this.PlayerType = Enumerations.PlayerType.MemoryPlayer;

            this.WrongTryes = new List<int>();
        }

        public override int Guess()
        {
            int result = base.Guess();

            while (WrongTryes.Contains(result)) ;
            Guesses.Add(result);
            return result;

        }
    }
}
