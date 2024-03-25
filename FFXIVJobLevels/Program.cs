var builder = WebAssemblyHostBuilder.CreateDefault(args);
var services = builder.Services;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

services
    .AddMudServices()
    .AddScoped(sp => new HttpClient
    {
        BaseAddress = new Uri("https://xivapi.com/")
    });

await builder.Build().RunAsync();
