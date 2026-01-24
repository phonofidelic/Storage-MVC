using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Storage.Core.Connections.StorageInternal;

public class StorageInternalContextFactory(ServerArgs args) : IDbContextFactory<StorageInternalContext>
{
    public StorageInternalContext CreateDbContext()
    {
        var configuration = new ConfigurationBuilder()
           .AddEnvironmentVariables()
           .AddUserSecrets(Assembly.GetExecutingAssembly())
           .Build();

        var optionsBuilder = new DbContextOptionsBuilder<StorageInternalContext>();

        var connectionString = configuration.GetConnectionString("StorageInternal");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Database connection string 'StorageInternal' is not set.");
        }

        optionsBuilder.UseSqlServer(connectionString, conf =>
        {
            conf.EnableRetryOnFailure(5, TimeSpan.FromSeconds(2), null);
        });

        if (args.Verbose)
        {
            optionsBuilder
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information);
        }

        return new StorageInternalContext(optionsBuilder.Options);
    }
}
