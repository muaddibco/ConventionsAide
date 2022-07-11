using ConventionsAide.Core.Authentication;
using ConventionsAide.Core.Services;

[assembly: AuthorizationAudience("Users")]

Runner runner = new("Users Manager");
runner.Start<Program>(args);