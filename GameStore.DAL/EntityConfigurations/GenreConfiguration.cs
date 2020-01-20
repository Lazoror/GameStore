using System;
using GameStore.Domain.Models.SqlModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.EntityConfigurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            Guid strategyId = Guid.NewGuid();
            Guid racesId = Guid.NewGuid();
            Guid actionId = Guid.NewGuid();

            var genres = new[]
            {
                new Genre {Name = "Strategy", Id = strategyId},
                new Genre {Name = "RTS", Id = Guid.NewGuid(), ParentGenreId = strategyId},
                new Genre {Name = "TBS", Id = Guid.NewGuid(), ParentGenreId = strategyId},
                new Genre {Name = "RPG", Id = Guid.NewGuid()},
                new Genre {Name = "Sports", Id = Guid.NewGuid()},
                new Genre {Name = "Races", Id = racesId},
                new Genre {Name = "Rally", Id = Guid.NewGuid(), ParentGenreId = racesId},
                new Genre {Name = "Arcade", Id = Guid.NewGuid(), ParentGenreId = racesId},
                new Genre {Name = "Formula", Id = Guid.NewGuid(), ParentGenreId = racesId},
                new Genre {Name = "Off-road", Id = Guid.NewGuid(), ParentGenreId = racesId},
                new Genre {Name = "Action", Id = actionId},
                new Genre {Name = "FPS", Id = Guid.NewGuid(), ParentGenreId = actionId},
                new Genre {Name = "TPS", Id = Guid.NewGuid(), ParentGenreId = actionId},
                new Genre {Name = "Adventure", Id = Guid.NewGuid()},
                new Genre {Name = "Puzzle & Skill", Id = Guid.NewGuid()},
                new Genre {Name = "Condiments", Id = new Guid("a40b825d-351e-f2fc-715c-0e1405050505")},
                new Genre {Name = "Beverages", Id = new Guid("a40b825d-351e-f2fc-715c-0e1505050505")},
                new Genre {Name = "Confections", Id = new Guid("a40b825d-351e-f2fc-715c-0e1605050505")},
                new Genre {Name = "Grains/Cereals", Id = new Guid("a40b825d-351e-f2fc-715c-0e1705050505")},
                new Genre {Name = "Dairy Products", Id = new Guid("a40b825d-351e-f2fc-715c-0e1805050505")},
                new Genre {Name = "Meat/Poultry", Id = new Guid("a40b825d-351e-f2fc-715c-0e1905050505")},
                new Genre {Name = "Produce", Id = new Guid("a40b825d-351e-f2fc-715c-0e1a05050505")},
                new Genre {Name = "Seafood", Id = new Guid("a40b825d-351e-f2fc-715c-0e1b05050505")},
            };


            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.HasIndex(x => x.Name).IsUnique();

            builder.ToTable("Genre");
        }
    }
}