using System;
using GameStore.Domain;
using GameStore.Domain.Models.SqlModels.AccountModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.EntityConfigurations.Account
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");

            builder.HasIndex(x => x.Name).IsUnique();

            var roles = new Role[]
            {
                new Role {Id = new Guid("8b611f34-a353-4f12-b61c-4b07d34a5ad8"), Name = RoleName.User},
                new Role {Id = new Guid("754b99dd-ce1c-4ea4-b7c2-d9e65135fa38"), Name = RoleName.Moderator},
                new Role {Id = new Guid("8463a866-2602-4805-9430-3dc0cf17d46a"), Name = RoleName.Manager},
                new Role {Id = new Guid("48632ad4-d1b8-43f2-8f59-e11ca453f326"), Name = RoleName.Admin},
            };

            builder.HasData(roles);
        }
    }
}