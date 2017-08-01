using System;

namespace Philes.Ftp.Core
{
    public class PhilesException : Exception
    {
        public PhilesContext Context { get; }

        public PhilesException(PhilesContext context, string message, Exception inner)
            : base(message, inner)
        {
            Context = context;
        }

        public PhilesException(PhilesContext context, Exception inner)
            : this(context, GetMessage(context, inner), inner)
        { }

        private static string GetMessage(PhilesContext context, Exception inner)
        {
            return context.Completed
                ? $"The request to {context.Uri} ended with the statuscode{context.Response.StatusCode} {context.Response.StatusDescription} {inner.GetBaseException().Message}"
                : $"The request to {context.Uri} ended without a response from the remote machine.{inner?.Message}";
        }
    }
}