using Game.Core;
using System;

namespace Game.Core.Services.Implementation
{
    public class RandomService : IRandomService
    {
        private static Random _random = new Random();

        public int Guess()
        {
            return _random.Next(Constants.MinBasketWeight, Constants.MaxBasketWeight + 1);
        }
    }
}
