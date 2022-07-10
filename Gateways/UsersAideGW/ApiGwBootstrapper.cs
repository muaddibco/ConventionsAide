using ConventionsAide.Core.Common.Architecture;

namespace UsersAideGW;

public class ApiGwBootstrapper : Bootstrapper
{
    protected override IEnumerable<string> EnumerateCatalogItems(string rootFolder)
    {
        return base.EnumerateCatalogItems(rootFolder).Concat(Directory.EnumerateFiles(rootFolder, "UsersAideGW.dll").Select(f => new FileInfo(f).Name));
    }
}
