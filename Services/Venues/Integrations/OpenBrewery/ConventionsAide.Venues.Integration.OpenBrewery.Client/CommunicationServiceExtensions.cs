using ConventionsAide.Core.Communication;
using ConventionsAide.Venues.Integration.OpenBrewery.Contracts;

namespace ConventionsAide.Venues.Integration.OpenBrewery.Client
{
    public static class CommunicationServiceExtensions
    {
        public static async Task<SyncBrewerysResponseDto> SyncBrewerysAsync(this ICommunicationService communicationService, SyncBrewerysRequestDto request)
        {
            var response = await communicationService.SendRequest<SyncBrewerysRequestDto, SyncBrewerysResponseDto>(request, TimeSpan.FromSeconds(600));

            return response;
        }
    }
}