using System;
using System.Net;
using System.Threading.Tasks;

namespace Philes.Ftp.Core
{
    public class FtpMessageInvoker : IFtpMessageInvoker
    {
        private readonly IFtpMessageHandler _handler;

        public FtpMessageInvoker(IFtpMessageHandler handler)
        {
            _handler = handler;
        }

        public async Task<FtpWebResponse> CommandAsync(string method, string path = null,
                                                       string from = null,
                                                       string to = null,
                                                       Action<FtpWebResponse>
                                                           responseAction = null,
                                                       Action<FtpWebRequest>
                                                           requestAction = null)
        {
            var request = CreateRequest(method, path, from, to);
            requestAction?.Invoke(request);

            var response = await _handler.InvokeRequestAsync(request);
            responseAction?.Invoke(response);

            return response;
        }

        private FtpWebRequest CreateRequest(string method, string path, string from,
                                            string to)
        {
            switch (method) {
                case WebRequestMethods.Ftp.UploadFile:
                case WebRequestMethods.Ftp.AppendFile:
                    return _handler.CreateRequest(to, method);
                case WebRequestMethods.Ftp.DownloadFile:
                    return _handler.CreateRequest(from, method);
                default:
                    return _handler.CreateRequest(path, method);
            }
        }
    }
}