using ConventionsAide.Venues.Domain;
using Microsoft.EntityFrameworkCore;

namespace ConventionsAide.Venues.EntityFrameworkCore
{
    public static class DbContextModelCreatingExtensions
    {
        public static void ConfigureVenues(this ModelBuilder builder)
        {
            builder.Entity<Venue>(b =>
            {
                b.ToTable("Venues");
                b.Property(v => v.Name).IsRequired();
                b.Property(v => v.ExternalId).IsRequired();
                b.HasIndex(v => v.ExternalId).IsUnique();
            });
        }
    }
}
