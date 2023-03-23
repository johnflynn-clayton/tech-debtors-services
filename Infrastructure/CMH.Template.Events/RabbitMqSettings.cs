namespace CMH.MobileHomeTracker.Events
{
    public class RabbitMqSettings
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ExchangeName { get; set; }
        public string EventSource { get; set; }
        public string EventVersion { get; set; }
        public int Port { get; set; }
        public bool SslEnabled { get; set; }
        public string VirtualHost { get; set; }
        public uint PrefetchSize { get; set; }
        public ushort PrefetchCount { get; set; }
        public double NetworkRecoveryIntervalMilliseconds { get; set; }
        public double ContinuationTimeoutMilliseconds { get; set; }
        public int MaxNumberOfRetries { get; set; }
        public int RecoveryWaitTimeMilliseconds { get; set; }
    }
}
