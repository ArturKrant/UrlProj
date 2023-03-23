using ShoritifierMVC.Configuration;
using ShoritifierMVC.Intrerfaces;
using ShoritifierMVC.Models;
using ShoritifierMVC.Services;

namespace ShoritifierMVC
{
    public class Program
    {
        //todo: admin user with its own claim and make log page authorization that suits this claim
        //todo: DTO models that show only relevant information
        //todo: login view
        //todo: signin view
        //todo: logout endpoint
        //todo: make the home/index view to be the Shortifier
        //todo: make view for the logged in user that lets him see his ComplexUrl - fullUrl - shortUrl - NumberOfUses
        //todo: make validations for inputs using dataAnottation and/or Regex [dataType.password] for example
        //todo: doesn't have to but maybe try to hash/encrypt the password
        //todo: make authentication cookie for the users
        //todo: profile page after you have logged on to see your user info and update it 
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(Config.CookieName).AddCookie(Config.CookieName, options =>
            {
                options.Cookie.Name = Config.CookieName;
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath= "/Account/AccessDenied";
            });

            builder.Services.AddAuthorization(options =>
                    options.AddPolicy("AdminOnly",
                    policy => policy.RequireClaim("Admin")
            ));

            builder.Services.AddSingleton<IUrlService>(_ = new UrlService(builder.Configuration.GetConnectionString("uri")!));
            builder.Services.AddSingleton<IUserService>(_ = new UserService(builder.Configuration.GetConnectionString("uri")!));
            builder.Services.AddSingleton<IUrlUsageLogService>(_ = new UrlUsageLogService(builder.Configuration.GetConnectionString("uri")!));

            builder.Services.AddAutoMapper(typeof(AutoMapperUserDto));

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}