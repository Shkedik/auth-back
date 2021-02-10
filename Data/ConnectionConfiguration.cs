using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;
using System;

namespace Data
{
    public class ConnectionConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbDataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<User>(options => { options.SignIn.RequireConfirmedAccount = false; })
                .AddEntityFrameworkStores<DbDataContext>();
        }
    }
}
