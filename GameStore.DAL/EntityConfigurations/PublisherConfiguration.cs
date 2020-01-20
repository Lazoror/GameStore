using GameStore.Domain.Models.SqlModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.EntityConfigurations
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.ToTable("Publisher");

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.CompanyName)
                .HasColumnType("nvarchar(40)");

            builder.Property(x => x.Description)
                .HasColumnType("Ntext");

            builder.Property(x => x.HomePage)
                .HasColumnType("Ntext");
        }
    }
}