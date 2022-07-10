using ConventionsAide.Core.Communication;
using ConventionsAide.Venues.Contracts;

namespace ConventionsAide.Venues.Client
{
    public static class CommunicationServiceExtensions
    {
        public static async Task UpdateVenuesInBatchAsync(this ICommunicationService communicationService, UpdateVenueInBatchRequestDto request)
        {
             await communicationService.Publish(() => request);
        }

        public static async Task<GetVenuesListResponseDto> GetVenuesList(this ICommunicationService communicationService, GetVenuesListRequestDto request)
        {
            return await communicationService.SendRequest<GetVenuesListRequestDto, GetVenuesListResponseDto>(request);
        }
    }
}