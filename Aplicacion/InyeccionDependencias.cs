﻿using Aplicacion.Helper.Comportamietos;
using Aplicacion.Infraestructura.Persistencia.Comunes;
using Aplicacion.Infraestructura.Persistencia;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Infraestructura.EnviarEmail.Interfaces;
using Aplicacion.Infraestructura.EnviarEmail.Implementaciones;

namespace Aplicacion
{
    public static class InyeccionDependencias
    {
        public static IServiceCollection AgregarAplicacion(this IServiceCollection services, IConfiguration configuration)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddScoped<InterceptorEntidadAuditable>();
            services.AddDbContext<ContextoDB>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
                options.UseNpgsql(Environment.GetEnvironmentVariable("URI_DB"),
                    (a) => a.MigrationsAssembly("API_GestionDominios"));
            },
            ServiceLifetime.Transient);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Validacion<,>));

            services.AddTransient<IEnviarEmail, EnviarEmailConGmail>();
            return services;
        }
    }
}
