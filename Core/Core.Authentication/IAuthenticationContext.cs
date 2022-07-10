using ConventionsAide.Core.Common.Architecture;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Authentication
{
    /// <summary>
    /// Provides authentication context for the request.
    /// </summary>
    [ServiceContract]
    public interface IAuthenticationContext
    {
        /// <summary>
        /// Gets or sets the user identity.
        /// </summary>
        ConsumerPrincipal User { get; }

        void SetUserFromHeader(byte[] content);

        Task<string> FetchApiToken(string apiName);

        void StoreApiToken(string apiToken);

        void ValidateApiToken(string audience, string scope);
    }
}
