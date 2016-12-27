using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.Services.Implementation
{
    public class ConsoleService : IConsoleService
    {
        public void Log(Guid gameId, string message, bool syncVersion = true)
        {
            if (syncVersion)
            {
                FileStream fs = new FileStream($@"Games\Game-{gameId.ToString()}.txt", FileMode.Append);

                TextWriter consoleOutput = Console.Out;

                StreamWriter fileOutput = new StreamWriter(fs);

                Console.SetOut(fileOutput);
                Console.WriteLine(message);

                Console.SetOut(consoleOutput);
                Console.WriteLine(message);

                fileOutput.Close();
            }
            else
            {
                Console.WriteLine(message);
            }
        }
    }
}
