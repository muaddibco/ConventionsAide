using AutoMapper;
using ConventionsAide.Core.Authentication;
using ConventionsAide.Core.Communication;
using ConventionsAide.Venues.Contracts;
using ConventionsAide.Venues.Domain;

namespace ConventionsAide.Venues.Manager.Handlers
{
    [AuthorizationScope("read:venues")]
    public class GetVenuesListHandler : ApiHandlerBase<GetVenuesListRequestDto, GetVenuesListResponseDto>
    {
        private readonly IVenuesRepository _venuesRepository;
        private readonly IMapper _mapper;

        public GetVenuesListHandler(IVenuesRepository venuesRepository, IMapper mapper)
        {
            _venuesRepository = venuesRepository;
            _mapper = mapper;
        }

        public override async Task<GetVenuesListResponseDto> HandleAsync(CommandMessage<GetVenuesListRequestDto> message, CancellationToken cancellationToken)
        {
            var (venues, count) = await _venuesRepository.GetListAsync(message.Payload.SkipCount, message.Payload.MaxCount, message.Payload.Sorting, message.Payload.Filter);

            return new GetVenuesListResponseDto
            {
                TotalCount = count,
                Items = _mapper.Map<List<Venue>, List<VenueDto>>(venues)
            };
        }
    }
}
