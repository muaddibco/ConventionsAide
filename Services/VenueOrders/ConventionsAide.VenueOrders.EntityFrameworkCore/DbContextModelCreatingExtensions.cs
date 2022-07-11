using ConventionsAide.VenueOrders.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.VenueOrders.EntityFrameworkCore
{
    public static class DbContextModelCreatingExtensions
    {
        public static void ConfigureVenueOrders(this ModelBuilder builder)
        {
            builder.Entity<VenueOrder>(b =>
            {
                b.ToTable("VenueOrders");
                b.HasOne(v => v.VenuesConfirmationFlow).WithMany(v => v.VenueOrders).HasForeignKey(v => v.VenuesConfirmationFlowId);
            });

        }
        public static void ConfigureVenuesConfirmationFlows(this ModelBuilder builder)
        {
            builder.Entity<VenuesConfirmationFlow>(b =>
            {
                b.ToTable("VenuesConfirmationFlows");
            });
        }
    }
}
