using ConventionsAide.Core.Communication;
using ConventionsAide.Venues.Contracts;

namespace ConventionsAide.Venues.Client
{
    public static class CommunicationServiceExtensions
    {
        private const string _apiName = "Venues";
        
        public static async Task UpdateVenuesInBatchAsync(this ICommunicationService communicationService, UpdateVenueInBatchRequestDto request)
        {
             await communicationService.Publish(() => request, _apiName);
        }

        public static async Task<GetVenuesListResponseDto> GetVenuesList(this ICommunicationService communicationService, GetVenuesListRequestDto request)
        {
            return await communicationService.SendRequest<GetVenuesListRequestDto, GetVenuesListResponseDto>(request, _apiName);
        }
    }
}