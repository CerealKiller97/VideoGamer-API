using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EntityConfiguration.Configuration
{
	public class PlatformEntityConfiguration : IEntityTypeConfiguration<Platform>
	{
		public void Configure(EntityTypeBuilder<Platform> builder)
		{
			// Primary key
			builder.HasKey(p => p.Id);

			// Properties
			builder.Property(p => p.Id)
				.ValueGeneratedOnAdd();

			builder.Property(p => p.Name)
				.HasConversion(new EnumToNumberConverter<Platforms, int>())
				.IsRequired(true);

			// Relations
			builder.HasMany(p => p.GamePlatforms)
				.WithOne(gp => gp.Platform)
				.HasPrincipalKey(p => p.Id)
				.HasForeignKey(gp => gp.PlatformId);

		}
	}
}
