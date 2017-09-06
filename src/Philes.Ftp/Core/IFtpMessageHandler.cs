using System.Net;
using System.Threading.Tasks;

namespace Philes.Ftp.Core
{
    public interface IFtpMessageHandler
    {
        ICredentials Credentials { get; set; }
        PhilesContext Context { get; set; }
        FtpWebRequest CreateRequest(string uri, string method);
        Task<FtpWebResponse> RequestAsync(FtpWebRequest request);
    }
}