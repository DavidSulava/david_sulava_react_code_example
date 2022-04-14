using DesignGear.Contracts.Models.Contractor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Models
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
