using LawOf100.Habits;
using LawOf100.Users;
using Microsoft.EntityFrameworkCore;

namespace LawOf100._Plugins;

public class LawOf100Context : DbContext
{
    public DbSet<Habit> Habits => Set<Habit>();

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Reaction> Reactions => Set<Reaction>();

    public LawOf100Context(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Habit>().HasPartitionKey(x => x.UserId);
        modelBuilder.Entity<Reaction>().HasPartitionKey(x => x.UserId);
        modelBuilder.Entity<Account>().HasPartitionKey(x => x.UserId);
    }
}
