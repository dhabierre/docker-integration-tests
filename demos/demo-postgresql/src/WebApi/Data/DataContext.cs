namespace WebApi.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using WebApi.Entities;

    public class DataContext : DbContext
    {
        private readonly string connectionString = null;

        public DataContext(string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (this.connectionString != null)
            {
                optionsBuilder.UseSqlServer(this.connectionString);
            }

            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<SampleEntity> Sample { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SampleEntity>()
                .ToTable("Sample")
                .HasKey(a => a.Id);

            builder.Entity<SampleEntity>()
                .Property(a => a.IsCritical);

            builder.Entity<SampleEntity>()
                .Property(a => a.Message);

            builder.Entity<SampleEntity>()
                .Property(a => a.Timestamp);

            base.OnModelCreating(builder);
        }
    }
}