using LawOf100.Features.Habits.Entities;
using Microsoft.EntityFrameworkCore;

namespace LawOf100.Features._Plugins
{
    public class LawOf100Context : DbContext
    {
        public DbSet<Habit> Habits { get; set; }
        
        public LawOf100Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Habit>().HasPartitionKey(x => x.UserId);
        }
    }
}
