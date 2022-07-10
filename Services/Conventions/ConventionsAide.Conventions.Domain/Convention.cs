using ConventionsAide.Core.Domain.Entities;

namespace ConventionsAide.Conventions.Domain
{
    public class Convention : Entity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long? VenuesConfirmationFlowId { get; set; }
        public IEnumerable<long>? TopicIds { get; set; }
        public long? GuestsInvitationFlowId { get; set; }
        public long? TalksConfirmationFlowId { get; set; }
        public DateOnly From { get; set; }
        public DateOnly To { get; set; }

        public List<ConventionTalk> Talks { get; set; }
    }
}