using Microsoft.EntityFrameworkCore;
using NeilMichaelPortelliEPSolution.Domaian.EntityFrameworkCore;

namespace NeilMichaelportelliEPSolution
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<PollDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<PollRepository>();


            var app = builder.Build();
            app.Run();
        }
    }
}
