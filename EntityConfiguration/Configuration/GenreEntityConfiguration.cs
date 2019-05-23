using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityConfiguration.Configuration
{
	public class GenreEntityConfiguration : IEntityTypeConfiguration<Genre>
	{
		public void Configure(EntityTypeBuilder<Genre> builder)
		{
			// Primary Key
			builder.HasKey(g => g.Id);

			// Property
			builder.Property(g => g.Name)
				.HasMaxLength(100)
				.IsRequired(true);

			// Relations
			builder.HasMany(g => g.GameGenre)
				.WithOne(gg => gg.Genre)
				.HasPrincipalKey(g => g.Id)
				.HasForeignKey(gg => gg.GenreId);
		}
	}
}
