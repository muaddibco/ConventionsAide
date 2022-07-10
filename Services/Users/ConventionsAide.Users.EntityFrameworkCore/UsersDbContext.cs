using ConventionsAide.Users.Domain;
using Microsoft.EntityFrameworkCore;

namespace ConventionsAide.Users.EntityFrameworkCore
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext() : base()
        {

        }

        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureUsers();
        }
    }
}