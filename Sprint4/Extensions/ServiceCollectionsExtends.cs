using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Sprint4.Configuration;
using Sprint4.Database;
using Sprint4.Models;
using Sprint4.Repository;
using Sprint4.Repository.Interface;

namespace Sprint4.Extensions
{
    public static class ServiceCollectionsExtends
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, APPConfiguration configuration)
        {
            services.AddDbContext<OracleDbContext>(options =>
            {
                options.UseOracle(configuration.OracleDatabase.Connection);
            });

            services.AddDbContext<MongoDbContext>(options =>
            {
                options.UseMongoDB(configuration.ConnectionStrings.RM99948, "rafaelnoboru");
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Cliente>, MongoDbRepository<Cliente>>();
            services.AddScoped<IRepository<PlanoSaude>, MongoDbRepository<PlanoSaude>>();
            services.AddScoped<IRepository<RecomendacaoPlanoSaude>, MongoDbRepository<RecomendacaoPlanoSaude>>();

            return services;
        }
        public static IServiceCollection AddSwagger(this IServiceCollection services, APPConfiguration configuration)
        {

            services.AddSwaggerGen(swagger =>
            {
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] {}
                    }
                    });

                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = configuration.Swagger.Title,
                    Description = configuration.Swagger.Description,
                    Contact = new OpenApiContact()
                    {
                        Email = configuration.Swagger.Email,
                        Name = configuration.Swagger.Name
                    }
                });

            });

            return services;

        }
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, APPConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddOracle(configuration.OracleDatabase.Connection, name: configuration.OracleDatabase.Name)
                .AddMongoDb(configuration.ConnectionStrings.RM99948, name: "MONGODB");

            return services;
        }
    }
}

