using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EntityConfiguration.Configuration
{
	public class GameEntityConfiguration : IEntityTypeConfiguration<Game>
	{
		public void Configure(EntityTypeBuilder<Game> builder)
		{
            // Primary Key 
            builder.HasKey(g => g.Id);

            // Indexes
			builder.HasIndex(g => g.Name)
				.IsUnique(true);
            builder.HasIndex(g => g.Engine);
            builder.HasIndex(g => g.AgeLabel);
            builder.HasIndex(g => g.ReleaseDate);

			// Properties
            builder.Property(g => g.Name)
				.HasMaxLength(200)
				.IsRequired(true);

            builder.Property(g => g.Engine)
				.HasMaxLength(200)
				.IsRequired(true);
            
			// Enum conversion
            builder.Property(g => g.AgeLabel)
                .HasMaxLength(50)
                .HasConversion(new EnumToNumberConverter<PegiAgeRating, int>())
                .IsRequired(true);

            builder.Property(g => g.GameMode)
	            .HasMaxLength(50)
	            .HasConversion(new EnumToNumberConverter<GameModes, int>())
	            .IsRequired(true);

            builder.Property(g => g.Path)
                .IsRequired(true)
                .HasMaxLength(255);

			// Relations

            builder.HasOne(g => g.Developer)
                .WithMany(d => d.Games)
                .OnDelete(DeleteBehavior.Cascade)
                .HasPrincipalKey(d => d.Id)
                .HasForeignKey(g => g.DeveloperId)
                .IsRequired(false);

            builder.HasOne(g => g.Publisher)
                .WithMany(p => p.Games)
                .OnDelete(DeleteBehavior.Cascade)
                .HasPrincipalKey(p => p.Id)
                .HasForeignKey(g => g.PublisherId)
                .IsRequired(false);

            builder.HasOne(g => g.User)
	            .WithMany(u => u.Games)
	            .OnDelete(DeleteBehavior.Cascade)
	            .HasPrincipalKey(p => p.Id)
	            .HasForeignKey(g => g.PublisherId)
	            .IsRequired(true);

            builder.HasMany(g => g.GameGenres)
	            .WithOne(gg => gg.Game)
	            .HasPrincipalKey(g => g.Id)
	            .HasForeignKey(gg => gg.GameId);

            builder.HasMany(g => g.GamePlatforms)
	            .WithOne(gg => gg.Game)
	            .HasPrincipalKey(g => g.Id)
	            .HasForeignKey(gg => gg.GameId);
		}
	}
}
