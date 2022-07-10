using ConventionsAide.Core.Contracts;
using ConventionsAide.Users.Domain.Shared;

namespace ConventionsAide.Users.Contracts
{
    public class UpdateUserTierRequestDto : EntityDto<long>
    {
        public UserTier Tier { get; set; }
    }
}
