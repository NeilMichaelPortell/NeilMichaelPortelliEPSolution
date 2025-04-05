using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NeilMichaelPortelliEPSolution.Domain.EntityFrameworkCore;
using NeilMichaelPortelliEPSolution.Domain.Repositories;

namespace NeilMichaelPortelliEPSolution
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddDbContext<PollDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register repositories
            builder.Services.AddScoped<IPollRepository, PollRepository>();

            builder.Services.AddControllersWithViews()
                .AddRazorOptions(options =>
                {
                    options.ViewLocationFormats.Add("Presentation/Views/{1}/{0}.cshtml");
                    options.ViewLocationFormats.Add("Presentation/Views/Shared/{0}.cshtml");
                });

            var app = builder.Build();

            // Apply migrations automatically
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<PollDbContext>();
                dbContext.Database.Migrate();
            }

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

            // Redirect root URL ("/") to Poll/Index
            app.MapGet("/", async context =>
            {
                context.Response.Redirect("/Poll/Index");
            });

            // Define the default route to Poll/Index
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Poll}/{action=Index}/{id?}");

            await app.RunAsync();
        }
    }
}
