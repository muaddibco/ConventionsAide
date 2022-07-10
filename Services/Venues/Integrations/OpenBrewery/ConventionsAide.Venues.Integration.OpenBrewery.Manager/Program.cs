using ConventionsAide.Core.Common;
using ConventionsAide.Core.Services;

Runner runner = new("OpenBrewery Integration Manager", EnvironmentConsts.UseAspCoreEnv);
runner.Start<Program>(args);