using Markdig;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Portfolio;
using Portfolio.Common;
using Portfolio.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddSingleton<IconRegistry>();
builder.Services.AddSingleton<PhotoViewer.Service>();
builder.Services.AddSingleton(new MarkdownPipelineBuilder()
    .UsePipeTables()
    .UseAutoLinks()
    .Build()
);

var host = builder.Build();

await host.Services.GetRequiredService<IconRegistry>().LoadAsync();

await host.RunAsync();
