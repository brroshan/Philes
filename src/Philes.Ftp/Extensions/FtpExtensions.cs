using System;

namespace Philes.Ftp.Extensions
{
    internal static class FtpExtensions
    {
        public static string AppendTo(this string url, string baseAddress)
        {
            if (string.IsNullOrEmpty(url))
                throw new Exception("You must specify a resource or absolute uri.");

            if (url.StartsWith("/"))
                url = url.Substring(1);

            var uri = baseAddress + url;

            if (!Uri.TryCreate(uri, UriKind.Absolute, out Uri _))
                throw new Exception(
                    $"Unable to invoke an ftp request with the uri '{uri}'.");

            return uri;
        }
    }
}