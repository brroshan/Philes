using System.Net;
using Philes.Ftp.Core;

namespace Philes.Ftp.Extensions
{
    public static class PhilesClientConfigExtensions
    {
        public static IPhilesClient WithMessageHandler<T>(this IPhilesClient client)
            where T : IFtpMessageHandler, new()
        {
            client.Config.MessageHandler = new T();
            return client;
        }

        public static IPhilesClient WithBaseAddress(this IPhilesClient client, string url)
        {
            client.Config.BaseAddress = url;
            return client;
        }

        public static IPhilesClient WithCredentials(this IPhilesClient client,
                                                    ICredentials cred)
        {
            client.Config.Credentials = cred;
            return client;
        }
    }
}