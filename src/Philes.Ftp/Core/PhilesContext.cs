using System;
using System.Net;

namespace Philes.Ftp.Core
{
    public class PhilesContext
    {
        internal PhilesContext(Uri uri)
        {
            Uri = uri;
        }

        public Uri Uri { get; internal set; }
        public FtpWebRequest Request { get; internal set; }
        public FtpWebResponse Response { get; internal set; }
        public Exception Error { get; internal set; }

        public bool Completed => Response != null;
        public bool Succeeded => Completed && IsSuccessStatusRange;

        private bool IsSuccessStatusRange => 
            Completed && Response.StatusDescription != null && Response.StatusDescription.StartsWith("2");

        public bool HasHandledException { get; set; }
    }
}