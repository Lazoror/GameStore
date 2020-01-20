using System;
using GameStore.Domain.Models.SqlModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.EntityConfigurations
{
    public class PlatformConfiguration : IEntityTypeConfiguration<Platform>
    {
        public void Configure(EntityTypeBuilder<Platform> builder)
        {
            builder.ToTable("Platform");

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.HasIndex(x => x.Name).IsUnique();

            var platforms = new[]
            {
                new Platform{Id = Guid.NewGuid(), Name = "Desktop"},
                new Platform{Id = Guid.NewGuid(), Name = "Mobile"},
                new Platform{Id = Guid.NewGuid(), Name = "IOS"},
            };
        }
    }
}