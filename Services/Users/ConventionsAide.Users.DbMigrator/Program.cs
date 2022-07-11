using ConventionsAide.Core.Common;
using ConventionsAide.Core.Common.Architecture;
using ConventionsAide.Core.Migrator;
using ConventionsAide.Core.Services;
using ConventionsAide.Users.EntityFrameworkCore;

Runner<Bootstrapper, DbMigratorService<UsersDbContext>> runner = new("Users DB Migrator", EnvironmentConsts.UseAspCoreEnv);
runner.Start<Program>(args);