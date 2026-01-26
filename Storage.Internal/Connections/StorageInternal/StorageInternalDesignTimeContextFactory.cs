using Microsoft.EntityFrameworkCore.Design;

namespace Storage.Core.Connections.StorageInternal;

public class StorageInternalDesignTimeContextFactory : IDesignTimeDbContextFactory<StorageInternalContext>
{
    public StorageInternalContext CreateDbContext(string[] args)
    {
        var serverArgs = new ServerArgs { Verbose = false };
        var contextFactory = new StorageInternalContextFactory(serverArgs);
        return contextFactory.CreateDbContext();
    }
}
