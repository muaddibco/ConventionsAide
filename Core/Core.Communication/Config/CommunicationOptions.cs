namespace ConventionsAide.Core.Communication.Config
{
    public class CommunicationOptions
    {
        public const string Name = "Communication";

        public string Host { get; set; }
        public string VirtualHost { get; set; }
        public string Username { get; set; }
        // TODO: Need to adjust according to secured settings reading
        public string Password { get; set; }
        public int MessageLimit { get; set; }
        public int TimeLimitMs { get; set; }
        public int ConcurrencyLimit { get; set; }
    }
}
