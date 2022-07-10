using ConventionsAide.Core.Domain.Entities;

namespace ConventionsAide.Conventions.Domain
{
    public class Convention : Entity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long? VenueSelectionProcessId { get; set; }
        public IEnumerable<long>? TopicIds { get; set; }
        public long GuestsInvitationId { get; set; }
        public long TalkersInvitationId { get; set; }
        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
    }
}