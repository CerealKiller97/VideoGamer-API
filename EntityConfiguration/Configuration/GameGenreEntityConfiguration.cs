using System;
using Domain.Relations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityConfiguration.Configuration
{
	public class GameGenreEntityConfiguration : IEntityTypeConfiguration<GameGenre<Guid, Guid>>
	{
		public void Configure(EntityTypeBuilder<GameGenre<Guid, Guid>> builder)
		{
			builder.HasKey(gg => new {gg.GameId, gg.GenreId});
		}
	}
}
