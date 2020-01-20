using System;
using GameStore.Domain.Models.SqlModels.AccountModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.EntityConfigurations.Account
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");

            builder.HasKey(x => new { x.UserId, x.RoleId });

            var userRoles = new UserRole[]
            {
                new UserRole()
                {
                    RoleId = new Guid("8b611f34-a353-4f12-b61c-4b07d34a5ad8"),
                    UserId = new Guid("8f24c5d5-5150-4ba6-84ff-e4b044f34071")
                },
                new UserRole()
                {
                    RoleId = new Guid("754b99dd-ce1c-4ea4-b7c2-d9e65135fa38"),
                    UserId = new Guid("487eac9c-1810-440b-b228-45151340d013")
                },
                new UserRole()
                {
                    RoleId = new Guid("8b611f34-a353-4f12-b61c-4b07d34a5ad8"),
                    UserId = new Guid("8463a866-2602-4805-9430-3dc0cf17d46a")
                },
                new UserRole()
                {
                    RoleId = new Guid("48632ad4-d1b8-43f2-8f59-e11ca453f326"),
                    UserId = new Guid("7de39dbf-f656-41f1-932d-b2c7835c20a9")
                }
            };

            builder.HasData(userRoles);
        }
    }
}