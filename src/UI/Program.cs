using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI.Interfaces;
using UI.Services;

namespace UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("TimeTable.Api", client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            }).AddHttpMessageHandler<AuthorizationMessageHandler>();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
            builder.Services.AddTransient<AuthorizationMessageHandler>();
            builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("TimeTable.Api"));
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddOptions();      
            builder.Services.AddScoped<IAuthenticationHttpService, AuthenticationHttpService>();
            builder.Services.AddScoped<IUserHttpService, UserHttpService>();
            builder.Services.AddBlazoredToast();
            builder.Services.AddScoped<IHttpService, HttpService>();

            await builder.Build().RunAsync();
        }
    }
}
