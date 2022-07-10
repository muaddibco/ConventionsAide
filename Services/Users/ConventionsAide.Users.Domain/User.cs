using ConventionsAide.Core.Domain.Entities;

namespace ConventionsAide.Users.Domain
{
    public class User: Entity<long>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ExternalId { get; set; }
        public UserTier Tier { get; set; }
    }
}