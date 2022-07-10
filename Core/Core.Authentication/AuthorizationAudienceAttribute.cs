using System;

namespace ConventionsAide.Core.Authentication;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
public sealed class AuthorizationAudienceAttribute : Attribute
{

    // This is a positional argument
    public AuthorizationAudienceAttribute(string audience)
    {
        Audience = audience;
        throw new NotImplementedException();
    }

    public string Audience { get; }
}
