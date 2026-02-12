using Ahura.Application.Interfaces;
using Ahura.Application.Mappers;
using Ahura.Application.Services;
using Ahura.Infrastructure;
using Ahura.Persistence.Context;
using Ahura.Persistence.Interceptors;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Ahura.Web;

internal static class DependencyInjectionExtension
{
    internal static IServiceCollection InjectControllers(this IServiceCollection services) =>
        services.AddControllers().Services;

    internal static IServiceCollection InjectServices(this IServiceCollection services) =>
       services.AddScoped<IForgeServices, ForgeServices>();

    internal static IServiceCollection InjectUnitOfWork(this IServiceCollection services) =>
       services.AddScoped<IUnitOfWork, UnitOfWork>();

    internal static IServiceCollection InjectMapster(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(ForgeMapper).Assembly);
        config.Compile();

        return services;
    }

    internal static IServiceCollection InjectDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("MariaDb")!;

        services.AddScoped<SaveEntityInterceptor>();

        services.AddDbContext<AhuraDbContext>((sp, options) =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), mySqlOptions =>
            {
                mySqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null
                );

                mySqlOptions.CommandTimeout(30);
            });

            options.AddInterceptors(sp.GetRequiredService<SaveEntityInterceptor>());
        });

        return services;
    }



    internal static IServiceCollection InjectAddSwaggerGen(this IServiceCollection services) =>
       services.AddSwaggerGen(c =>
       {
           c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ahura", Version = "v1" });

           c.CustomSchemaIds(type => type.FullName);

           c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
           {
               Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
               Name = "Authorization",
               In = ParameterLocation.Header,
               Type = SecuritySchemeType.ApiKey,
               Scheme = "Bearer"
           });

           c.AddSecurityRequirement(new OpenApiSecurityRequirement
           {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
           });
       });
}
