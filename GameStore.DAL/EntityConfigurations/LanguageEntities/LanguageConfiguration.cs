using System;
using GameStore.Domain.Models.LanguageModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.EntityConfigurations.LanguageEntities
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable("Language");

            builder.HasIndex(x => x.Code).IsUnique();

            var languages = new Language[]
            {
                new Language
                {
                    Id = Guid.NewGuid(),
                    Code = "en",
                    Name = "English"
                },
                new Language
                {
                    Id = Guid.NewGuid(),
                    Code = "ru",
                    Name = "Russian"
                },
            };

        }
    }
}