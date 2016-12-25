﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.Services.Implementation
{
    public class ConsoleService : IConsoleService
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
