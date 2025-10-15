using AppBlazor.Client;
//registro del servicio
using AppBlazor.Client.Services;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5180/") });

//crear el servicio
builder.Services.AddScoped<LibroService>();

//servicio del tipo de libro
builder.Services.AddScoped<TipoLibroService>();

//servicio del autor
builder.Services.AddScoped<AutorService>();

await builder.Build().RunAsync();
