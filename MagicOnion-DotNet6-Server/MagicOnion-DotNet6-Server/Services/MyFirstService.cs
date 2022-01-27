using MagicOnion;
using MagicOnion.Server;
using MagicOnionDotNet6.Shared;

namespace MagicOnionDotNet6.Server.Services
{
    public class MyFirstService : ServiceBase<IMyFirstService>, IMyFirstService
    {
        public async UnaryResult<int> SumAsync(int x, int y)
        {
            Console.WriteLine($"Received: {x}, {y}");
            return x + y;
        }
    }
}
