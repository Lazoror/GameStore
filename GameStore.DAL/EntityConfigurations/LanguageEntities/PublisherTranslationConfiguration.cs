using GameStore.Domain.Models.LanguageModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.EntityConfigurations.LanguageEntities
{
    public class PublisherTranslationConfiguration : IEntityTypeConfiguration<PublisherTranslation>
    {
        public void Configure(EntityTypeBuilder<PublisherTranslation> builder)
        {
            builder.ToTable("PublisherTranslation");

            builder.HasOne(x => x.Publisher).WithMany(x => x.Languages)
                .HasForeignKey(k => k.EntityId);
        }
    }
}