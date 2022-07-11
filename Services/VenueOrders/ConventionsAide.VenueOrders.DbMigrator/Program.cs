using ConventionsAide.Core.Common;
using ConventionsAide.Core.Common.Architecture;
using ConventionsAide.Core.Migrator;
using ConventionsAide.Core.Services;
using ConventionsAide.VenueOrders.EntityFrameworkCore;

Runner<Bootstrapper, DbMigratorService<VenueOrdersDbContext>> runner = new("Venue Orders DB Migrator", EnvironmentConsts.UseAspCoreEnv);
runner.Start<Program>(args);