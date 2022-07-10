namespace ConventionsAide.Venues.Contracts
{
    public class VenueDto
    {
        public long Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string VenueType { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public string ContactPhone { get; set; }
        public string WebUrl { get; set; }
    }
}
