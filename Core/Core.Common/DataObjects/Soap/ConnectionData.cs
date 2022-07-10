using System;

namespace ConventionsAide.Core.Common.DataObjects.Soap
{
    public class ConnectionData
    {
        public string Url { get; set; }

        public TimeSpan? OpenTimeout { get; set; }

        public TimeSpan? CloseTimeout { get; set; }

        public TimeSpan? SendTimeout { get; set; }

        public TimeSpan? ReceiveTimeout { get; set; }

        public int? MaxBufferSize { get; set; }

        public int? MaxBufferPoolSize { get; set; }

        public int? MaxReceivedMessageSize { get; set; }
    }
}
