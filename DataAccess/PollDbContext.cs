using System.Data.Entity;
using NeilMichaelPortelliEPSolution.Domain;

namespace NeilMichaelPortelliEPSolution.DataAccess
{
    public class PollDbContext : DbContext
    {
        public PollDbContext() : base("name=DefaultConnection")
        {
        }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Poll>()
                .HasMany(p => p.Votes)
                .WithRequired(v => v.Poll)
                .HasForeignKey(v => v.PollId);
        }
    }
}
