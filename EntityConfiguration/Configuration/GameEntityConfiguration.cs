using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityConfiguration.Configuration
{
	public class GameEntityConfiguration : IEntityTypeConfiguration<Game<Guid>>
	{
		public void Configure(EntityTypeBuilder<Game<Guid>> builder)
		{
			
		}
	}
}
