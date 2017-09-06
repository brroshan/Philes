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
            var config = Setup.Config;

            switch (on) {
                case Event.Start:
                    if (config.OnStart == null) return TaskCompleted;
                    return config.OnStart(context);
                case Event.Error:
                    if (config.OnError == null) return TaskCompleted;
                    return config.OnError(context);
                case Event.Ended:
                    if (config.OnEnded == null) return TaskCompleted;
                    return config.OnEnded(context);
                default:
                    return TaskCompleted;
            }
        }

        public static Task TaskCompleted => Task.FromResult<object>(null);
    }
}