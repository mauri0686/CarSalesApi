using CarSalesApi.Api;
using CarSalesApi.Application;
using CarSalesApi.Infrastructure;
using Microsoft.OpenApi.Models;

namespace CarSalesApi.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure services
            ConfigureServices(builder);

            var app = builder.Build();

            // Configure HTTP pipeline
            Configure(app);

            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Add controllers
            builder.Services.AddControllers();

            // Add Swagger for API documentation
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarSalesApi", Version = "v1" });
            });

            // Register dependencies
            builder.Services.AddScoped<ILogger<ExecutionTimeFilter>, Logger<ExecutionTimeFilter>>();
            builder.Services.AddScoped<ExecutionTimeFilter>();
            builder.Services.AddSingleton<ISaleRepository, InMemorySaleRepository>();
            builder.Services.AddScoped<ISaleService, SaleService>();
        }

        private static void Configure(WebApplication app)
        {
            // Enable Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarSalesApi v1");
            });

            // Middleware de errores
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            
            // Configure HTTP pipeline
            app.UseRouting();

            // Add authorization middleware
            app.UseAuthorization();

            // Map controllers
            app.MapControllers();
        }
    }
}