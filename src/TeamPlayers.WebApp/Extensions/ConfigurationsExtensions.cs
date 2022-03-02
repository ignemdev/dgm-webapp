using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TeamPlayers.Data;

namespace System
{
    public static class ConfigurationsExtensions
    {
        public static IServiceCollection AddMvcConfigurations(this IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddNewtonsoftJson(x =>
                {
                    x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    x.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    };
                });

            return services;
        }

        public static IServiceCollection AddDatabaseConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("TeamPlayersConnection");
            services.AddDbContext<TeamPlayersContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
