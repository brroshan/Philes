using System;
using System.Net;
using System.Threading.Tasks;

namespace Philes.Ftp.Core
{
    public interface IFtpMessageInvoker
    {
        Task<FtpWebResponse> CommandAsync(string method, string path = null,
                                          string from = null, string to = null,
                                          Action<FtpWebResponse>
                                              responseAction = null,
                                          Action<FtpWebRequest>
                                              requestAction = null);
    }
}