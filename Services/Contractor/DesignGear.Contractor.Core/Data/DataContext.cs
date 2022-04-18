using DesignGear.Contractor.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace DesignGear.Contractor.Core.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Tariff> Tariffs { get; set; }

        public DbSet<UserInfo> Users { get; set; }
    }
}
