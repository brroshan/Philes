using System;
using System.Net;
using System.Threading.Tasks;
using Philes.Ftp.Configuration;
using static Philes.Ftp.Configuration.PhilesStartup;

namespace Philes.Ftp.Core
{
    public class FtpMessageHandler : IFtpMessageHandler
    {
        public ICredentials Credentials { get; set; }
        public PhilesContext Context { get; set; }

        public FtpWebRequest CreateRequest(string uri, string method)
        {
            var request = (FtpWebRequest)WebRequest.Create(uri);

            request.Credentials = Credentials;
            request.Method = method;
            
            Context = new PhilesContext(new Uri(uri));
            return Context.Request = request;
        }

        public async Task<FtpWebResponse> InvokeRequestAsync(FtpWebRequest request)
        {
            PhilesStartup.Context = Context;
            try {
                await Emit(Event.Start, Context);
                await TryGetResponseAsync(request);
                
                return Context.Response;
            }
            catch (Exception ex) {
                Context.Error = ex;
                await Emit(Event.Error, Context);
                throw;
            }
            finally {
                await Emit(Event.Ended, Context);
            }
        }

        private async Task TryGetResponseAsync(WebRequest request)
        {
            try {
                var response = await request.GetResponseAsync().ConfigureAwait(false);
                Context.Response = (FtpWebResponse)response;
            }
            catch (Exception ex) {
                throw new PhilesException(Context, ex);
            }
        }
    }
}