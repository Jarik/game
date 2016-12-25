using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.Services.Implementation
{
    public class RandomService : IRandomService
    {
        public int GetRandomNumber()
        {
            return StaticDeclarations.Random.Next(
                StaticDeclarations.NumberConstants.MinBasketWeight, 
                StaticDeclarations.NumberConstants.MaxBasketWeight + 1);
        }
    }
}
