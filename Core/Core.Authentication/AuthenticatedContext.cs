using ConventionsAide.Core.Common.Architecture;

namespace ConventionsAide.Core.Authentication
{
    /// <summary>
    /// A default implementation for the authentication context.
    /// </summary>
    [RegisterService(typeof(IAuthenticationContext), Lifetime = LifetimeManagement.Scoped)]
    public class AuthenticatedContext : IAuthenticationContext
    {
        /// <inheritdoc />
        public ConsumerPrincipal User { get; set; }
    }
}
