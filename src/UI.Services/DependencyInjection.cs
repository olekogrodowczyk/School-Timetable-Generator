using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Services.Interfaces;
using UI.Services.Services;

namespace UI.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<IAuthenticationHttpService, AuthenticationHttpService>()
                .AddScoped<IUserHttpService, UserHttpService>()
                .AddScoped<IHttpService, HttpService>()
                .AddScoped<IClassHttpService, ClassHttpService>()
                .AddScoped<ITimetableHttpService, TimetableHttpService>();
        }
    }
}
