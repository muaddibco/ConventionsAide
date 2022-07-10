using ConventionsAide.Core.Common.Architecture;

namespace ConventionsAideGW;

public class ApiGwBootstrapper : Bootstrapper
{
    protected override IEnumerable<string> EnumerateCatalogItems(string rootFolder)
    {
        return base.EnumerateCatalogItems(rootFolder).Concat(Directory.EnumerateFiles(rootFolder, "ConventionsAideGW.dll").Select(f => new FileInfo(f).Name));
    }
}
