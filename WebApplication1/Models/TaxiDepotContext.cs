using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public class TaxiDepotContext :DbContext
{
    public DbSet<Car> Cars { get; set; } = null!;
    public DbSet<TaxiDepot> TaxiDepots { get; set; } = null!;
    public DbSet<TaxiGroup> TaxiGroups { get; set; } = null!;

    public TaxiDepotContext()
    {
        Database.EnsureCreated();
    }
    
    public TaxiDepotContext(DbContextOptions<TaxiDepotContext> options)
    :base(options)
    {
        Database.EnsureCreated();
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlite(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TaxiDepot>().HasMany(u => u.TaxiGroups).
            WithOne(p => p.TaxiDepot_).OnDelete(DeleteBehavior.Cascade);
    }
}