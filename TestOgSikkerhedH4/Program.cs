using Microsoft.EntityFrameworkCore;
using TestOgSikkerhedH4.Controllers.Models;
using System.Security.Cryptography.X509Certificates;

namespace TestOgSikkerhedH4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 🔹 Configure HTTPS Certificate
            var certPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                @".aspnet\https\mycert.pfx");

            var certPassword = File.Exists("password.txt")
                ? File.ReadAllText("password.txt").Trim()
                : "MyPassword"; // Default password

            var cert = new X509Certificate2(certPath, certPassword);

            // 🔹 Add services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 🔹 Database Connection
            var connectionString = builder.Configuration.GetConnectionString("Connection");

            builder.Services.AddDbContext<Dbcontext>(options =>
                options.UseSqlServer(connectionString));


            // 🔹 CORS Setup (Allow Angular)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy => policy.WithOrigins("https://localhost:4200") // 🔹 Secure HTTPS
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            var app = builder.Build();

            // 🔹 Enable Swagger in Development
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // 🔹 HTTPS Enforced
            app.UseHttpsRedirection();

            // 🔹 Secure CORS Policy
            app.UseCors("AllowFrontend");

            // 🔹 Authorization Middleware
            app.UseAuthorization();

            // 🔹 Map Controllers
            app.MapControllers();

            // 🔹 Use HTTPS Certificate (Kestrel)
            app.Use(async (context, next) =>
            {
                var scheme = context.Request.Scheme;
                if (scheme != "https")
                {
                    context.Response.Redirect("https://" + context.Request.Host + context.Request.Path);
                }
                await next();
            });

            app.Run();
        }
    }
}
