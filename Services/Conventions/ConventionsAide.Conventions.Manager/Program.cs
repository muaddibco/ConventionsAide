using ConventionsAide.Core.Common;
using ConventionsAide.Core.Services;

Runner runner = new("Conventions Manager", EnvironmentConsts.UseAspCoreEnv);
runner.Start<Program>(args);