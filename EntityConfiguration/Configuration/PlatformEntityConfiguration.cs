using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityConfiguration.Configuration
{
	public class PlatformEntityConfiguration : IEntityTypeConfiguration<Platform>
	{
		public void Configure(EntityTypeBuilder<Platform> builder)
		{
			builder.Property(p => p.Name)
				.HasConversion(x => x.ToString(),
					x => (Platforms) Enum.Parse(typeof(Platforms), x))
				.IsRequired(true);
		}
	}
}
