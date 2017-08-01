using System;
using System.Net;
using System.Threading.Tasks;
using Philes.Ftp.Core;

namespace Philes.Ftp.Configuration
{
    public class PhilesConfig
    {
        private string _baseAddress;

        public string BaseAddress
        {
            get => _baseAddress;
            set => _baseAddress = value.EndsWith("/") ? value : $"{value}/";
        }

        public ICredentials Credentials { get; set; }
        public IFtpMessageHandler MessageHandler { get; set; }
        public PhilesConfig Clone => (PhilesConfig)MemberwiseClone();

        public Func<PhilesContext,Task> OnStart { get; set; }
        public Func<PhilesContext,Task> OnError { get; set; }
        public Func<PhilesContext,Task> OnEnded { get; set; }
    }
}