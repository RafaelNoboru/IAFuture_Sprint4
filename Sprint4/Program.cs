
using Sprint4.Extensions;
using Sprint4.Configuration;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Sprint4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfiguration configuration = builder.Configuration;

            APPConfiguration appConfiguration = new APPConfiguration();

            configuration.Bind(appConfiguration);

            builder.Services.Configure<APPConfiguration>(configuration);

            // Add services to the container.

            builder.Services.AddControllers();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            
            builder.Services.AddEndpointsApiExplorer();
            
            builder.Services.AddSwagger(appConfiguration);

            builder.Services.AddDbContexts(appConfiguration);

            builder.Services.AddRepositories();

            builder.Services.AddHealthCheck(appConfiguration);


            var app = builder.Build();
            
            app.UseRouting();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health-check", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = HealthCheckExtensions.WriteResponse
                });
            });

            app.Run();
        }
    }
}
