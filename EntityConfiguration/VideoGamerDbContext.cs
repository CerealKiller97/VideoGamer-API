using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.Relations;
using EntityConfiguration.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EntityConfiguration
{
    public class VideoGamerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Genre> Genres { get; set; }
		public DbSet<Developer> Developers { get; set; } 
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<GamePlatform> GamePlatforms { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }

        public VideoGamerDbContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GameEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PlatformEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GenreEntityConfiguration()); 
            modelBuilder.ApplyConfiguration(new DeveloperEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GamePlatformEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GameGenreEntityConfiguration());
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.Entity is AbstractModel item && entry.State == EntityState.Added && item.CreatedAt == default)
                {
                    item.CreatedAt = DateTime.Now;
                }

            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.Entity is AbstractModel item && entry.State == EntityState.Added && item.CreatedAt == default)
                {
                    item.CreatedAt = DateTime.Now;
                }

            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.Entity is AbstractModel item && entry.State == EntityState.Added && item.CreatedAt == default)
                {
                    item.CreatedAt = DateTime.Now;
                } else if (entry.Entity is AbstractModel x && entry.State == EntityState.Modified)
				{
					x.UpdatedAt = DateTime.Now;
				}

            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
