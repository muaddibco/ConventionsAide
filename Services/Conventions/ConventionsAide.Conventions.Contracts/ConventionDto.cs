namespace ConventionsAide.Conventions.Contracts
{
    public class ConventionDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? VenuesConfirmationFlowId { get; set; }
        public IEnumerable<long>? TopicIds { get; set; }
        public long? GuestsInvitationFlowId { get; set; }
        public long? TalkersInvitationFlowId { get; set; }
        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
    }
}