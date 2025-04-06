using Microsoft.AspNetCore.Authentication.Cookies;
using NeilMichaelPortelliEPSolution.DataAccess;
using NeilMichaelPortelliEPSolution.Domain.Repositories;
using PollDbContext = NeilMichaelPortelliEPSolution.DataAccess.PollDbContext;

namespace NeilMichaelPortelliEPSolution
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Register repositories
            builder.Services.AddScoped<IPollRepository, PollRepository>();

            // Add authentication services
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";  // Path to login page
                    options.AccessDeniedPath = "/Account/AccessDenied";  // Path to access denied page
                });

            // Add authorization services
            builder.Services.AddAuthorization();

            // Add MVC with correct Razor options
            builder.Services.AddControllersWithViews()
                .AddRazorOptions(options =>
                {
                    options.ViewLocationFormats.Add("/Views/{1}/{0}.cshtml");
                    options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
                });

            var app = builder.Build();

            // Configure HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Use authentication and authorization middleware
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // Define the default route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Poll}/{action=Index}/{id?}");

            await app.RunAsync();
        }
    }
}
