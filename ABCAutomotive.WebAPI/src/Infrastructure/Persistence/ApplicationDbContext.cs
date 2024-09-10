using ABCAutomotive.WebAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABCAutomotive.WebAPI.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Sale> Sales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>().Property(p => p.CarType).HasConversion<string>();
    }
}