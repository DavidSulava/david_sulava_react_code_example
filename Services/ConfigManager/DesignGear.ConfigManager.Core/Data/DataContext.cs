using DesignGear.ConfigManager.Core.Data.Entity;
using DesignGear.ConfigManager.Core.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DesignGear.ConfigManager.Core.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=DATABASE3\\DEV2019;Database=DesignGearConfigMgr;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParameterDefinition>()
                .HasOne(x => x.Configuration)
                .WithMany(y => y.ParameterDefinitions)
                .OnDelete(DeleteBehavior.Restrict);

            //base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<AppBundle> AppbBundles { get; set; }

        public virtual DbSet<ComponentDefinition> ComponentDefinitions { get; set; }

        public virtual DbSet<Configuration> Configurations { get; set; }

        public virtual DbSet<ConfigurationInstance> ConfigurationInstances { get; set; }

        public virtual DbSet<ParameterDefinition> ParameterDefinitions { get; set; }

        public virtual DbSet<ValueOption> ValueOptions { get; set; }        

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
