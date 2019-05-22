using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityConfiguration.Configuration
{
	public class UserEntityConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd();

            builder.Property(u => u.FirstName)
	            .HasMaxLength(50)
	            .IsRequired(true);

            builder.Property(u => u.LastName)
	            .HasMaxLength(50)
	            .IsRequired(true);

            builder.Property(u => u.Password)
	            .HasMaxLength(300)
	            .IsRequired(true);

			builder.HasIndex(u => u.Email)
	            .IsUnique(true);

            builder.Property(u => u.Email)
                .IsRequired(true)
                .HasMaxLength(150);
		}
	}
}
