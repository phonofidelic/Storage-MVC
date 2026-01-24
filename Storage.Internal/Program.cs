using Ivy;
using Ivy.Chrome;
using Ivy.Views;
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

var server = new Server();
server.UseHotReload();
server.AddAppsFromAssembly();
// server.UseDefaultApp(typeof(Demo));
server.UseChrome(() => new DefaultSidebarChrome(chromeSettings));

await server.RunAsync();


