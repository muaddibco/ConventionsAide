using ConventionsAide.Core.Contracts;

namespace ConventionsAide.TalkOrders.Contracts
{
    public class TalkOrderDto : EntityDto<long>
    {
        public string Topic { get; set; }
        public string Description { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<TalkInvitationDto> TalkInvitations { get; set; }
}
}
