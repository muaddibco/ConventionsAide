using System;

namespace ConventionsAide.Core.Authentication
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class AuthorizationScopeAttribute : Attribute
    {
        public AuthorizationScopeAttribute(string scope)
        {
            this.Scope = scope;
        }

        public string Scope { get; }
    }
}
