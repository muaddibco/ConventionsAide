using ConventionsAide.Core.Authentication;
using ConventionsAide.Core.Services;

[assembly: AuthorizationAudience("Venues")]

Runner runner = new("Venues Manager");
runner.Start<Program>(args);