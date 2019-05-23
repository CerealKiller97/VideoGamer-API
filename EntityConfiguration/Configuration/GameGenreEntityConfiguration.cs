using System;
using Domain.Relations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityConfiguration.Configuration
{
	public class GameGenreEntityConfiguration : IEntityTypeConfiguration<GameGenre>
	{
		public void Configure(EntityTypeBuilder<GameGenre> builder)
		{
			builder.HasKey(gg => new {gg.GameId, gg.GenreId});

			builder
				.HasOne(gg => gg.Game)
				.WithMany(g => g.GameGenres)
				.OnDelete(DeleteBehavior.Cascade)
				.HasPrincipalKey(g => g.Id)
				.HasForeignKey(gg => gg.GameId)
				.IsRequired(true);

			builder
				.HasOne(gg => gg.Genre)
				.WithMany(g => g.GameGenre)
				.OnDelete(DeleteBehavior.Cascade)
				.HasPrincipalKey(g => g.Id)
				.HasForeignKey(gg => gg.GenreId)
				.IsRequired(true);
		}	
	}
}
