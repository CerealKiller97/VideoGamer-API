using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityConfiguration.Configuration
{
	public class PublisherEntityConfiguration : IEntityTypeConfiguration<Publisher>
	{
		public void Configure(EntityTypeBuilder<Publisher> builder)
		{
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
		}
	}
}
