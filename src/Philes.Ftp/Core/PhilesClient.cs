using System;
using System.Net;
using System.Threading.Tasks;
using Philes.Ftp.Configuration;
using static System.String;
using static Philes.Ftp.Configuration.PhilesStartup;

namespace Philes.Ftp.Core
{
    public class PhilesClient : IPhilesClient
    {
        private readonly ICredentials _credentials;
        private IFtpMessageInvoker _invoker;
        private IFtpMessageHandler _handler;

        public PhilesClient()
        {
            Config = PhilesStartup.Setup.Config.Clone;
        }

        public PhilesClient(string baseAddress = null, ICredentials credentials = null)
            : this()
        {
            if (credentials != null) {
                _credentials = Config.Credentials = credentials;
            }

            if (!IsNullOrWhiteSpace(baseAddress))
                Config.BaseAddress = baseAddress;
        }

        public PhilesConfig Config { get; set; }

        public IFtpMessageHandler MessageHandler
        {
            get
            {
                if (_handler == null) {
                    _handler = Config.MessageHandler;
                    _handler.Credentials = _credentials ?? Config.Credentials;
                }
                return _handler;
            }
        }

        public IFtpMessageInvoker MessageInvoker => _invoker ?? 
            (_invoker = new FtpMessageInvoker(MessageHandler));

        public async Task<FtpWebResponse> CommandAsync(
            string method, string path = null,
            string remote = null, Action<FtpWebResponse> responseAction = null,
            Action<FtpWebRequest> requestAction = null)
        {
            FtpWebResponse response = null;

            try {
                response = await MessageInvoker.CommandAsync(
                    method, path, remote, responseAction,
                    requestAction);

                Context = MessageHandler.Context;
            }
            catch when (Context.HasHandledException) {
                return response;
            }

            return response;
        }
    }
}