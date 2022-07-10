using ConventionsAide.Core.Communication;
using ConventionsAide.Users.Contracts;

namespace ConventionsAide.Users.Client
{
    public static class CommunicationServiceExtensions
    {
        private const string _apiName = "Users";

        public static async Task<UserDto> CreateUserAsync(this ICommunicationService communicationService, CreateUserRequestDto request)
        {
            return await communicationService.SendRequest<CreateUserRequestDto, UserDto>(request, _apiName);
        }
    }
}