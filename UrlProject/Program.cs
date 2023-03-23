
using API.DAL;
using API.Services;
using Microsoft.EntityFrameworkCore;
using System;
using UrlProject.Services;

namespace UrlProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DataContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("ShortifierDb")));

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<ComplexUrlService>();
            builder.Services.AddScoped<UrlUsageLogService>();

            builder.Services.AddCors(options => {
                options.AddPolicy(
                    "aaa",
                    policy => {
                        policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            //migrate
            await using (var scope = app.Services.CreateAsyncScope())
            {
                using var db = scope.ServiceProvider.GetService<DataContext>();
                await db!.Database.MigrateAsync();
            }


                    // Configure the HTTP request pipeline.
                    if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("aaa");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}