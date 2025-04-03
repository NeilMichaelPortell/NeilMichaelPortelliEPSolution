using Microsoft.EntityFrameworkCore;
using NeilMichaelportelliEPSolution.Domain;
namespace NeilMichaelPortelliEPSolution.Domaian.EntityFrameworkCore
{
    public class PollDbContext : DbContext
    {
        public PollDbContext(DbContextOptions<PollDbContext> options) : base(options)
        {
        }

        public DbSet<Poll> Polls { get; set; }
    }
}
