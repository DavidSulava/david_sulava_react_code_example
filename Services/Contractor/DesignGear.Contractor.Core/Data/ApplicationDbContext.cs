using DesignGear.Contractor.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace DesignGear.Contractor.Core.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Tariff> Tariffs { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
