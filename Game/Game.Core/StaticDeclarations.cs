using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public static class StaticDeclarations
    {
        public static Random Random = new Random();

        public static class NumberConstants
        {
            public static int MaxAttemptCount = 100;

            public static int MinBasketWeight = 40;

            public static int MaxBasketWeight = 140;

            public static int GameTime = 1500;
        }
    }
}
