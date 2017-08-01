using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Philes.Ftp.Extensions
{
    public static class FtpWebResponseExtensions
    {
        public static async Task<string> ReceiveAsString(this Task<FtpWebResponse> source)
            => await ReadStreamAsync(source);

        public static async Task WriteToFile(this Task<FtpWebResponse> source,
                                             string path)
        {
            var contents = await ReadStreamAsync(source);
            File.WriteAllText(path, contents);
        }

        public static async Task AppendToFile(this Task<FtpWebResponse> source,
                                              string path)
        {
            var contents = await ReadStreamAsync(source);
            File.AppendAllText(path, contents);
        }

        private static async Task<string> ReadStreamAsync(Task<FtpWebResponse> source)
        {
            var response = await source.ConfigureAwait(false);
            using (var stream = response?.GetResponseStream()) {
                if (stream != null)
                    using (var sr = new StreamReader(stream))
                        return await sr.ReadToEndAsync().ConfigureAwait(false);
            }

            return null;
        }
    }
}