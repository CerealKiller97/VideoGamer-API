using System;
using Domain.Relations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityConfiguration.Configuration
{
	public class GamePlatformEntityConfiguration : IEntityTypeConfiguration<GamePlatform<Guid, Guid>>
	{
		public void Configure(EntityTypeBuilder<GamePlatform<Guid, Guid>> builder)
		{
            builder.HasKey(gp => new { gp.GameId, gp.PlatformId });
        }
	}
}
