using ConventionsAide.Core.Authentication;
using ConventionsAide.Core.Services;

[assembly: AuthorizationAudience("Registrations")]

Runner runner = new("Registrations Manager");
runner.Start<Program>(args);