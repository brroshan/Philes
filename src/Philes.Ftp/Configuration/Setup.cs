using System;
using System.Net;
using System.Threading.Tasks;
using Philes.Ftp.Core;

namespace Philes.Ftp.Configuration
{
    public sealed class Setup
    {
        private static readonly Lazy<Setup> _setup =
            new Lazy<Setup>(() => new Setup());

        private Setup()
        { }

        internal static Setup Instance => _setup.Value;

        internal PhilesConfig Config { get; } = new PhilesConfig();

        public Setup MessageHandler<T>()
            where T : IFtpMessageHandler, new()
        {
            Config.MessageHandler = new T();
            return this;
        }

        public Setup AddBaseAddress(string uri)

        {
            Config.BaseAddress = uri;
            return this;
        }

        public Setup WithCredentials(ICredentials credentials)
        {
            Config.Credentials = credentials;
            return this;
        }

        public Setup OnStart(Func<PhilesContext, Task> onStart)
        {
            Config.OnStart = onStart;
            return this;
        }

        public Setup OnError(Func<PhilesContext, Task> onError)
        {
            Config.OnError = onError;
            return this;
        }

        public Setup OnEnded(Func<PhilesContext, Task> onEnded)
        {
            Config.OnEnded = onEnded;
            return this;
        }
    }
}
