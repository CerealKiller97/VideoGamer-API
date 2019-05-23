using System;
using Domain.Relations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityConfiguration.Configuration
{
	public class GamePlatformEntityConfiguration : IEntityTypeConfiguration<GamePlatform>
	{
		public void Configure(EntityTypeBuilder<GamePlatform> builder)
		{
            builder.HasKey(gp => new { gp.GameId, gp.PlatformId });

            builder
                .HasOne(gp => gp.Game)
                .WithMany(g => g.GamePlatforms)
                .OnDelete(DeleteBehavior.Cascade)
                .HasPrincipalKey(g => g.Id)
                .HasForeignKey(gp => gp.GameId)
                .IsRequired(true);

            builder
                .HasOne(gp => gp.Platform)
                .WithMany(p => p.GamePlatforms)
				.OnDelete(DeleteBehavior.Cascade)
                .HasPrincipalKey(p => p.Id)
                .HasForeignKey(gp => gp.PlatformId)
                .IsRequired(true);
        }
	}
}
