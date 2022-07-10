using ConventionsAide.Venues.Domain;
using Microsoft.EntityFrameworkCore;

namespace ConventionsAide.Venues.EntityFrameworkCore;

public class VenuesDbContext : DbContext
{
    public VenuesDbContext() : base()
    {

    }

    public VenuesDbContext(DbContextOptions<VenuesDbContext> options)
        : base(options)
    {

    }

    public DbSet<Venue> Venues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureVenues();
    }
}