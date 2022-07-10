namespace ConventionsAide.Venues.Integration.OpenBrewery.Manager.Configuration
{
    public class OpenBreweryOptions
    {
        public const string SectionName = "OpenBrewery";
        public string ListUri { get; set; }
        public int PerPage { get; set; }
    }
}
