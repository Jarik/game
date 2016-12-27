using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.Services
{
    public interface IConsoleService
    {
        void Log(Guid gameId, string message, bool syncVersion = true);
    }
}
