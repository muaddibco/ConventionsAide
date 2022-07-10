using ConventionsAide.Core.Communication;
using ConventionsAide.Venues.Integration.OpenBrewery.Contracts;

namespace ConventionsAide.Venues.Integration.OpenBrewery.Client
{
    public static class CommunicationServiceExtensions
    {
        private const string _apiName = "Integration.OpenBrewery";
        public static async Task<SyncBrewerysResponseDto> SyncBrewerysAsync(this ICommunicationService communicationService, SyncBrewerysRequestDto request)
        {
            var response = await communicationService.SendRequest<SyncBrewerysRequestDto, SyncBrewerysResponseDto>(request, _apiName, TimeSpan.FromSeconds(600));

            return response;
        }
    }
}