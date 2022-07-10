namespace ConventionsAide.Core.Authentication
{
    public class AuthOptions
    {
        public const string Name = "AuthSettings";

        public bool? IsM2M { get; set; }
        public string Authority { get; set; }
        public string Audience { get; set; }
        public string UsernameClaimType { get; set; }
        public string SiteIdClaimType { get; set; }
    }
}
