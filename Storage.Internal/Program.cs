using Storage.Core.Apps.Publishing;
using Storage.Core.Apps.Publishing.Categories;
using Storage.Core.Apps.Publishing.Products;
using Storage.Core.Services;
using Storage.Internal;
using Storage.Internal.Apps.Publishing;

var chromeSettings = new ChromeSettings()
    .WallpaperApp<Home>()
    .Header(
        Layout.Vertical().Gap(2)
        | Text.Lead("hello")
    )
    .DefaultApp<ProductsPublishingApp>()
    .UseTabs(preventDuplicates: true);

CultureInfo systemCulture = new("sv-SE");
CultureInfo.DefaultThreadCurrentCulture = systemCulture;
CultureInfo.DefaultThreadCurrentUICulture = systemCulture;

var server = new Server();
server.UseHotReload();
server.AddAppsFromAssembly();
server.AddConnectionsFromAssembly();
server.UseChrome(() => new DefaultSidebarChrome(chromeSettings));

var storageInternalConnection = new StorageInternalConnection();
storageInternalConnection.RegisterServices(server.Services);
server.Services.AddScoped<IPublishingRepository, PublishingRepository>();
server.Services.AddScoped<IProductsPublishingService, ProductsPublishingService>();
server.Services.AddScoped<ICategoriesPublishingService, CategoriesPublishingService>();

await server.RunAsync();


