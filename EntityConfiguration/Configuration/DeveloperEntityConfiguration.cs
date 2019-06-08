using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityConfiguration.Configuration
{
	public class DeveloperEntityConfiguration : IEntityTypeConfiguration<Developer>
	{
		public void Configure(EntityTypeBuilder<Developer> builder)
		{
			// Primary key
			builder.HasKey(d => d.Id);

            // 
            builder.HasIndex(d => d.Name)
                .IsUnique(true);

            builder.HasIndex(d => d.Website)
                .IsUnique(true);

			// Properties
			builder.Property(d => d.Name)
				.HasMaxLength(200)
				.IsRequired(true);

			builder.Property(d => d.HQ)
				.HasMaxLength(200)
				.IsRequired(true);

			builder.Property(d => d.Founded)
				.IsRequired(true);

			builder.Property(d => d.Website)
				.HasMaxLength(200)
				.IsRequired(true);

			// Relations
			builder.HasMany(d => d.Games)
				.WithOne(d => d.Developer)
				.HasPrincipalKey(d => d.Id)
				.HasForeignKey(g => g.DeveloperId);

		}
	}
}
