using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TeamPlayers.Core.Services;
using TeamPlayers.Services;

namespace System
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddEntitiesServices(this IServiceCollection services)
        {
            services.AddTransient<IEstadoServices, EstadoServices>();
            services.AddTransient<IEquipoServices, EquipoServices>();
            services.AddTransient<IJugadorServices, JugadorServices>();

            return services;
        }
    }
}
