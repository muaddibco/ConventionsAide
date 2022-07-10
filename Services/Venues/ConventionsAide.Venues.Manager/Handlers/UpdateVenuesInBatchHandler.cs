using AutoMapper;
using ConventionsAide.Core.Authentication;
using ConventionsAide.Core.Communication;
using ConventionsAide.Venues.Contracts;
using ConventionsAide.Venues.Domain;

namespace ConventionsAide.Venues.Manager.Handlers
{
    [AuthorizationScope("read:venues")]
    public class UpdateVenuesInBatchHandler : ApiBatchHandlerBase<UpdateVenueInBatchRequestDto>
    {
        private readonly IVenuesRepository _venuesRepository;
        private readonly IMapper _mapper;

        public UpdateVenuesInBatchHandler(IVenuesRepository venuesRepository, IMapper mapper)
        {
            _venuesRepository = venuesRepository;
            _mapper = mapper;
        }

        public override async Task HandleAsync(IEnumerable<CommandMessage<UpdateVenueInBatchRequestDto>> request)
        {
            var externalIds = request.Select(v => v.Payload.ExternalId);
            var existingVenues = await _venuesRepository.GetListAsync(v => externalIds.Any(e => e == v.ExternalId));
            existingVenues.ForEach(v =>
            {
                var input = request.First(i => i.Payload.ExternalId == v.ExternalId);
                v.Country = input.Payload.Country;
                v.State = input.Payload.State;
                v.City = input.Payload.City;
                v.Address1 = input.Payload.Address1;
                v.Address2 = input.Payload.Address2;
                v.Address3 = input.Payload.Address3;
                v.PostalCode = input.Payload.PostalCode;
                v.Latitude = input.Payload.Latitude;
                v.Longitude = input.Payload.Longitude;
                v.WebUrl = input.Payload.WebUrl;
                v.ContactPhone = input.Payload.ContactPhone;
            });

            await _venuesRepository.UpdateManyAsync(existingVenues);
            var newVenues = request
                .Where(i => existingVenues.Select(v => v.ExternalId).All(e => e != i.Payload.ExternalId))
                .Select(v => _mapper.Map<UpdateVenueInBatchRequestDto, Venue>(v.Payload));

            await _venuesRepository.InsertManyAsync(newVenues, true);
        }
    }
}
