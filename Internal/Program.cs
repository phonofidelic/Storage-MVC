using Ivy;

var server = new Server();
server.UseHotReload();
server.AddAppsFromAssembly();
server.UseChrome();

await server.RunAsync();