using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityConfiguration.Configuration
{
	public class PublisherEntityConfiguration : IEntityTypeConfiguration<Publisher>
	{
		public void Configure(EntityTypeBuilder<Publisher> builder)
		{
			// Primary key
			builder.HasKey(p => p.Id);

			builder.Property(p => p.Id)
				.ValueGeneratedOnAdd();

            // Indexes
            builder.HasIndex(p => p.Name)
                .IsUnique(true);

            builder.HasIndex(p => p.ISIN)
                .IsUnique(true);

            builder.HasIndex(p => p.Website)
                .IsUnique(true);

            // Properties
            builder.Property(p => p.Name)
				.HasMaxLength(100)
				.IsRequired(true);

			builder.Property(p => p.HQ)
				.HasMaxLength(200)
				.IsRequired(true);

			builder.Property(p => p.Founded)
				.IsRequired(true);

			builder.Property(p => p.ISIN)
				.HasMaxLength(12)
				.IsRequired(true);

			builder.Property(p => p.Website)
				.HasMaxLength(250)
				.IsRequired(true);

			// Relations
			builder.HasMany(p => p.Games)
				.WithOne(g => g.Publisher)
				.HasPrincipalKey(p => p.Id)
				.HasForeignKey(g => g.PublisherId);
		}
	}
}
