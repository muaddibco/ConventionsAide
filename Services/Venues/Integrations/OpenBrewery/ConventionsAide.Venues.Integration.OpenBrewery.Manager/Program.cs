﻿using ConventionsAide.Core.Authentication;
using ConventionsAide.Core.Common;
using ConventionsAide.Core.Services;

[assembly: AuthorizationAudience("VenuesIntegration")]

Runner runner = new("OpenBrewery Integration Manager", EnvironmentConsts.UseAspCoreEnv);
runner.Start<Program>(args);