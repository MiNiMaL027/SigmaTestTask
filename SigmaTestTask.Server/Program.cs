
using Azure.Storage.Blobs;
using Services.Filters;
using Services.Interfaces;
using Services.Services;

namespace SigmaTestTask.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();

            builder.Services.AddControllers(options => options.Filters.Add(typeof(NotImplExceptionFilterAttribute)));

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", policy =>
                {
                    policy.WithOrigins("https://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
        
            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.UseCors("AllowAngularApp");

            app.Run();
        }
    }
}
