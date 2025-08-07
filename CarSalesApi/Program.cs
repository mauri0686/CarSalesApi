using CarSalesApi.Application.Services;
using CarSalesApi.Domain.Repositories;
using CarSalesApi.Infrastructure.Repositories;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace CarSalesApi
{
    public class Program
    {
        [RequiresUnreferencedCode()]
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
   
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}