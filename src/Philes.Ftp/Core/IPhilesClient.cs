using System;
using System.Net;
using System.Threading.Tasks;
using Philes.Ftp.Configuration;

namespace Philes.Ftp.Core
{
    public interface IPhilesClient
    {
        IFtpMessageHandler MessageHandler { get; }
        IFtpMessageInvoker MessageInvoker { get; }
        
        PhilesConfig Config { get; set; }

        Task<FtpWebResponse> CommandAsync(string method, string path = null,
                                          string local = null,
                                          string remote = null,
                                          Action<FtpWebResponse>
                                              responseAction = null,
                                          Action<FtpWebRequest>
                                              requestAction = null);
    }
}