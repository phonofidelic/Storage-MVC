using Ivy.Connections;
using Ivy.Services;

namespace Storage.Core.Connections.StorageInternal;

public class StorageInternalConnection : IConnection, IHaveSecrets
{
    public string GetContext(string connectionPath)
    {
        var connectionFile = nameof(StorageInternalConnection) + ".cs";
        var contextFactoryFile = nameof(StorageInternalContextFactory) + ".cs";
        var files = System.IO.Directory.GetFiles(connectionPath, "*.*", System.IO.SearchOption.TopDirectoryOnly)
            .Where(f => !f.EndsWith(connectionFile) && !f.EndsWith(contextFactoryFile) && !f.EndsWith("EfmigrationsLock.cs"))
            .Select(System.IO.File.ReadAllText)
            .ToArray();
        return string.Join(System.Environment.NewLine, files);
    }

    public string GetName() => nameof(StorageInternal);

    public string GetNamespace() => typeof(StorageInternalConnection).Namespace ?? throw new Exception("Could not read namespace");

    public string GetConnectionType() => "EntityFramework.SqlServer";

    public ConnectionEntity[] GetEntities()
    {
        return typeof(StorageInternalContext)
            .GetProperties()
            .Where(e => e.PropertyType.IsGenericType && e.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
            .Where(e => e.PropertyType.GenericTypeArguments[0].Name != "EfmigrationsLock")
            .Select(e => new ConnectionEntity(e.PropertyType.GenericTypeArguments[0].Name, e.Name))
            .ToArray();
    }

    public void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<StorageInternalContextFactory>();
    }

   public Ivy.Services.Secret[] GetSecrets()
   {
       return
       [
           new("ConnectionStrings:StorageInternal")
       ];
   }
}
