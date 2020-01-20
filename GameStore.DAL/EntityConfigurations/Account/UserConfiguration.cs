using System;
using GameStore.Domain.Models.SqlModels.AccountModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.EntityConfigurations.Account
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasIndex(x => x.Email).IsUnique();

            var users = new[]
            {
                new User{Email = "admin@gmail.com", Id = new Guid("7de39dbf-f656-41f1-932d-b2c7835c20a9"), IsDeleted = false, Name = "admin@gmail.com", Password = "AD7dkD/M0O7pJlubwB5k7pIqHxJZMkhsFA81fwwSrx+8CWxFyHktakivcLDhIdGOuQ=="},
                new User{Email = "manager@gmail.com", Id = new Guid("0c0ba235-c522-4f52-8db7-173f228e7570"), IsDeleted = false, Name = "manager@gmail.com", Password = "AJygD2xp0RFZhleqdD9TJIkSUuJk5ka73mtMe+fJTwLCyc8Gqyc1rhZC+wxfIKeAnw=="},
                new User{Email = "moder@gmail.com", Id = new Guid("487eac9c-1810-440b-b228-45151340d013"), IsDeleted = false, Name = "moder@gmail.com", Password = "AJG2sO4hVwYH8yEwX5/kRlVANijpsr9pmADQDvm0h7UpLVk2C2bG8ogfNNnJlXulog=="},
                new User{Email = "user@gmail.com", Id = new Guid("8f24c5d5-5150-4ba6-84ff-e4b044f34071"), IsDeleted = false, Name = "user@gmail.com", Password = "AInfVp2rb2LuQhe0iB3N1WfexKNK4M0MTKsinlX6TSmDwGK2R0cAAtiv4zKKCJm+bw=="},
            };

            builder.HasData(users);
        }
    }
}