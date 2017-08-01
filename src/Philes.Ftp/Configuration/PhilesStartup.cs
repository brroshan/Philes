using System.Threading.Tasks;
using Philes.Ftp.Core;

namespace Philes.Ftp.Configuration
{
    public static class PhilesStartup
    {
        public static Setup Setup { get; } = Setup.Instance;
        internal static PhilesContext Context { get; set; }

        internal static Task Emit(Event on, PhilesContext context)
        {
            switch (on) {
                case Event.Start:
                    return Setup.Config.OnStart(context);
                case Event.Error:
                    return Setup.Config.OnError(context);
                case Event.Ended:
                    return Setup.Config.OnEnded(context);
                default:
                    return Task.FromResult<object>(null);
            }
        }
    }
}