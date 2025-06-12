using Chirp.Services.Services;
using Chirp.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Chirp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                                .ReadFrom.Configuration(builder.Configuration)
                                .CreateLogger();

            builder.Host.UseSerilog();

            // Add services to the container.
            builder.Services.AddDbContext<ChirpContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

            builder.Services.AddControllers();

            builder.Services.AddScoped<IChirpsService, JanChirpsService>();

            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAllOrigins",
            //        builder => builder.AllowAnyOrigin()
            //                          .AllowAnyMethod()
            //                          .AllowAnyHeader());
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            //app.UseAuthorization();

            //app.UseCors("AllowAllOrigins");

            app.MapControllers();

            app.Run();
        }
    }
}
