using ConventionsAide.Users.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.Users.EntityFrameworkCore
{
    public static class DbContextModelCreatingExtensions
    {
        public static void ConfigureUsers(this ModelBuilder builder)
        {
            builder.Entity<User>(b =>
            {
                b.ToTable("Users");
                b.Property(v => v.Email).IsRequired();
                b.Property(v => v.FirstName).IsRequired();
                b.Property(v => v.LastName).IsRequired();
                b.Property(v => v.ExternalId).IsRequired();
                b.HasIndex(v => v.ExternalId).IsUnique();
                b.Property(v => v.Tier).IsRequired().HasDefaultValue(UserTier.Tier1);
                b.HasIndex(v => v.Tier);
            });
        }
    }
}
