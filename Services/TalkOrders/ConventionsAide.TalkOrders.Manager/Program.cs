using ConventionsAide.Core.Authentication;
using ConventionsAide.Core.Services;

[assembly: AuthorizationAudience("TalkOrders")]

Runner runner = new("Talk Orders Manager");
runner.Start<Program>(args);