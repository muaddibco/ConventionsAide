using ConventionsAide.Core.Common.Architecture;

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
        ConsumerPrincipal User { get; set; }
    }
}
