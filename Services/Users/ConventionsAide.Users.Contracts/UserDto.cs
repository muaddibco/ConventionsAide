using ConventionsAide.Core.Contracts;
using ConventionsAide.Users.Domain.Shared;

namespace ConventionsAide.Users.Contracts
{
    public class UserDto : EntityDto<long>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ExternalId { get; set; }

        public UserTier Tier { get; set; }
    }
}
