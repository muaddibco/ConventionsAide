using ConventionsAide.Core.Common;
using ConventionsAide.Core.Common.Architecture;
using ConventionsAide.Core.Migrator;
using ConventionsAide.Core.Services;
using ConventionsAide.Venues.EntityFrameworkCore;

Runner<Bootstrapper, DbMigratorService<VenuesDbContext>> runner = new("Venues DB Migrator", EnvironmentConsts.UseAspCoreEnv);
runner.Start<Program>(args);