using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Contracts.Services;
using Services.Services;
using System;

namespace Services
{
    public static class ServiceConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            Data.ConnectionConfiguration.Configure(services, configuration);

            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
