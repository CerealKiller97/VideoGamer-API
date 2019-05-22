using System;
using Domain;
using Domain.Relations;
using EntityConfiguration.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EntityConfiguration
{
    public class VideoGamerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Game<Guid>> Games { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<GamePlatform<Guid, Guid>> GamePlatforms { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GameGenre<Guid, Guid>> GameGenres { get; set; }

        public VideoGamerDbContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GameGenreEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GamePlatformEntityConfiguration());

		}

		public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.Entity is AbstractModel<Guid> item &&
                    entry.State == EntityState.Added &&
                    item.CreatedAt != default) item.CreatedAt = DateTime.UtcNow;
            }
            return base.SaveChanges();
        }
    }
}
