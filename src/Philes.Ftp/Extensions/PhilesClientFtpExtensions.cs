using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Philes.Ftp.Core;
using static System.Net.WebRequestMethods.Ftp;

namespace Philes.Ftp.Extensions
{
    public static class PhilesClientFtpExtensions
    {
        public static async Task<FtpWebResponse> DownloadAsync(
            this IPhilesClient c, string local, string remote)
        {
            var url = remote.AppendTo(c.Config.BaseAddress);
            return await c.CommandAsync(DownloadFile, remote: url,
                responseAction:
                async response =>
                {
                    using (var stream = response.GetResponseStream())
                    using (var sr = new StreamReader(stream, Encoding.UTF8)) {
                        var content = await sr
                            .ReadToEndAsync().ConfigureAwait(false);
                        File.WriteAllText(local, content);
                    }
                });
        }

        public static async Task<FtpWebResponse> AppendAsync(
            this IPhilesClient c, string local, string remote)
        {
            var url = remote.AppendTo(c.Config.BaseAddress);
            return await c.CommandAsync(AppendFile, remote: url,
                requestAction:
                async request => { await AddToRequestStream(local, request); });
        }

        public static async Task<FtpWebResponse> UploadAsync(
            this IPhilesClient c, string local, string remote)
        {
            var url = remote.AppendTo(c.Config.BaseAddress);
            return await c.CommandAsync(UploadFile, remote: url,
                requestAction:
                async request => { await AddToRequestStream(local, request); });
        }

        public static async Task<FtpWebResponse>
            DeleteAsync(this IPhilesClient c, string path)
        {
            var url = path.AppendTo(c.Config.BaseAddress);
            return await c.CommandAsync(DeleteFile, url);
        }

        public static async Task<FtpWebResponse>
            MakeDirectoryAsync(this IPhilesClient c, string path)
        {
            var url = path.AppendTo(c.Config.BaseAddress);
            return await c.CommandAsync(MakeDirectory, url);
        }

        public static async Task<FtpWebResponse>
            DeleteDirectoryAsync(this IPhilesClient c, string path)
        {
            var url = path.AppendTo(c.Config.BaseAddress);
            return await c.CommandAsync(RemoveDirectory, url);
        }

        public static async Task<FtpWebResponse>
            GetDirectoryContentsAsync(this IPhilesClient c, string path)
        {
            var url = path.AppendTo(c.Config.BaseAddress);
            return await c.CommandAsync(ListDirectory, url);
        }

        public static async Task<FtpWebResponse>
            GetDirectoryDetailsAsync(this IPhilesClient c, string path)
        {
            var url = path.AppendTo(c.Config.BaseAddress);
            return await c.CommandAsync(ListDirectoryDetails, url);
        }

        public static async Task<FtpWebResponse> RenameAsync(
            this IPhilesClient c, string path, string name)
        {
            var url = path.AppendTo(c.Config.BaseAddress);
            return await c.CommandAsync(Rename, url, requestAction:
                request => { request.RenameTo = name; });
        }

        private static async Task AddToRequestStream(string from, WebRequest request)
        {
            byte[] contents;
            using (var stream = new StreamReader(from))
                contents =
                    Encoding.UTF8.GetBytes(
                        await stream
                            .ReadToEndAsync()
                            .ConfigureAwait(false));

            using (var stream =
                await request
                    .GetRequestStreamAsync()
                    .ConfigureAwait(false))
                stream.Write(contents, 0, contents.Length);
        }
    }
}