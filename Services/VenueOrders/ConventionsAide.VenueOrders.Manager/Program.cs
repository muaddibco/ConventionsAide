using ConventionsAide.Core.Authentication;
using ConventionsAide.Core.Services;

[assembly: AuthorizationAudience("VenueOrders")]

Runner runner = new("Venue Orders Manager");
runner.Start<Program>(args);