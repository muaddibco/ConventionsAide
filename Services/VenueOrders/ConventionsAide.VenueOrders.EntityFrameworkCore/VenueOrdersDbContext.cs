using ConventionsAide.VenueOrders.Domain;
using Microsoft.EntityFrameworkCore;

namespace ConventionsAide.VenueOrders.EntityFrameworkCore
{
    public class VenueOrdersDbContext : DbContext
    {
        public VenueOrdersDbContext() : base()
        {

        }

        public VenueOrdersDbContext(DbContextOptions<VenueOrdersDbContext> options)
            : base(options)
        {

        }

        public DbSet<VenuesConfirmationFlow> VenuesConfirmationFlows { get; set; }
        public DbSet<VenueOrder> VenueOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureVenuesConfirmationFlows();
            modelBuilder.ConfigureVenueOrders();
        }
    }
}