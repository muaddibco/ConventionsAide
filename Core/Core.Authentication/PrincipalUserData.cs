using System;

namespace ConventionsAide.Core.Authentication
{
    public class PrincipalUserData
    {
        public string[] Roles { get; set; }

        public Guid? AntiForgeryToken { get; set; }
    }
}
