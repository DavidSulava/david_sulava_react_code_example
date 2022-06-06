using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contractor.Core.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DesignGear.Contractor.Core.Data
{
    public class GuidToNullConverter : ValueConverter<Guid?, Guid?>
    {
        public GuidToNullConverter(ConverterMappingHints mappingHints = null) :
            base(x => x == Guid.Empty ? default : x, x => x, mappingHints)
        { }
    }

    public class DataContext : DbContext
    {
        public DataContext() : base()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=DESKTOP-04NOTM8\\MSSQLSERVER01;Database=DesignGearCloud;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductVersion>()
                .HasOne(x => x.CurrentVersionProduct)
                .WithOne(y => y.CurrentProductVersion)
                .HasForeignKey<Product>(x => x.CurrentVersionId);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Organization> Organizations { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductVersion> ProductVersions { get; set; }

        public virtual DbSet<Tariff> Tariffs { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserAssignment> UserAssignments { get; set; }

        public virtual DbSet<ProductVersionPreview> ProductVersionPreviews { get; set; }

        private void BeforeSaveChanges()
        {
            var milliseconds = 0;
            //Modifier modifier = null;
            foreach (var entry in ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged))
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is ICreated created)
                    {
                        if (created.Created == DateTime.MinValue)
                        {
                            created.Created = DateTime.UtcNow.AddMilliseconds(milliseconds++);
                        }
                    }

                    if (entry.Entity is IGenerateUid generateUid)
                    {
                        if (generateUid.Id == Guid.Empty)
                        {
                            generateUid.Id = Guid.NewGuid();
                        }
                    }
                }
            }
        }

        public override int SaveChanges()
        {
            BeforeSaveChanges();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            BeforeSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            BeforeSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
