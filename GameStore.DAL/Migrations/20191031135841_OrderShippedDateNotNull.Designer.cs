﻿// <auto-generated />
using System;
using GameStore.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameStore.DAL.Migrations
{
    [DbContext(typeof(GameContext))]
    [Migration("20191031135841_OrderShippedDateNotNull")]
    partial class OrderShippedDateNotNull
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GameStore.Domain.Models.LanguageModels.GameTranslation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<Guid>("EntityId");

                    b.Property<Guid>("LanguageId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.HasIndex("LanguageId");

                    b.ToTable("GameTranslation");
                });

            modelBuilder.Entity("GameStore.Domain.Models.LanguageModels.GenreTranslation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("EntityId");

                    b.Property<Guid>("LanguageId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.HasIndex("LanguageId");

                    b.ToTable("GenreTranslation");
                });

            modelBuilder.Entity("GameStore.Domain.Models.LanguageModels.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

                    b.ToTable("Language");

                    b.HasData(
                        new
                        {
                            Id = new Guid("df71808e-f968-478d-a0e2-6b8275693329"),
                            Code = "en",
                            Name = "English"
                        },
                        new
                        {
                            Id = new Guid("8a05c943-b3df-46a3-900a-8e176882247e"),
                            Code = "ru",
                            Name = "Russian"
                        });
                });

            modelBuilder.Entity("GameStore.Domain.Models.LanguageModels.PlatformTranslation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("EntityId");

                    b.Property<Guid>("LanguageId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.HasIndex("LanguageId");

                    b.ToTable("PlatformTranslation");
                });

            modelBuilder.Entity("GameStore.Domain.Models.LanguageModels.PublisherTranslation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyName");

                    b.Property<string>("Description");

                    b.Property<Guid>("EntityId");

                    b.Property<string>("HomePage");

                    b.Property<Guid>("LanguageId");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.HasIndex("LanguageId");

                    b.ToTable("PublisherTranslation");
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.AccountModels.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cec10fef-e41f-483f-b09d-51b2af8434a3"),
                            Name = "User"
                        },
                        new
                        {
                            Id = new Guid("725f9d04-a47a-4ffa-a896-fe005e052267"),
                            Name = "Moderator"
                        },
                        new
                        {
                            Id = new Guid("d4267e6e-0315-49c8-b7cd-1a11a7bdd4d2"),
                            Name = "Manager"
                        },
                        new
                        {
                            Id = new Guid("9dde4920-33b5-4ce2-afeb-4a333674e805"),
                            Name = "Administrator"
                        });
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.AccountModels.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("User");
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.AccountModels.UserRole", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.CommentModels.Comment", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("Body");

                    b.Property<Guid?>("GameId");

                    b.Property<Guid>("GameStateId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<Guid?>("ParentCommentId");

                    b.Property<string>("Quote");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("GameStateId");

                    b.HasIndex("ParentCommentId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.GameModels.Game", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime>("AddDate");

                    b.Property<string>("Description");

                    b.Property<bool>("Discontinued");

                    b.Property<Guid?>("GameStateId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Key");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price")
                        .HasColumnType("Money");

                    b.Property<DateTime>("PublishDate");

                    b.Property<Guid?>("PublisherId");

                    b.Property<short>("UnitsInStock")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("GameStateId");

                    b.HasIndex("Key")
                        .IsUnique()
                        .HasFilter("[Key] IS NOT NULL");

                    b.HasIndex("PublisherId");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.GameModels.GameGenre", b =>
                {
                    b.Property<Guid>("GameId");

                    b.Property<Guid>("GenreId");

                    b.HasKey("GameId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("GameGenre");
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.GameModels.GamePlatform", b =>
                {
                    b.Property<Guid>("GameId");

                    b.Property<Guid>("PlatformTypeId");

                    b.HasKey("GameId", "PlatformTypeId");

                    b.HasIndex("PlatformTypeId");

                    b.ToTable("GamePlatform");
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.GameModels.GameState", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GameKey");

                    b.Property<int>("ViewCount");

                    b.HasKey("Id");

                    b.ToTable("GameState");
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.Genre", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<Guid?>("ParentGenreId");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.HasIndex("ParentGenreId");

                    b.ToTable("Genre");

                    b.HasData(
                        new
                        {
                            Id = new Guid("94058fd9-862c-4cc2-a925-09e397628185"),
                            IsDeleted = false,
                            Name = "Strategy"
                        },
                        new
                        {
                            Id = new Guid("e8860073-9821-41ea-8218-85a7a6652c1f"),
                            IsDeleted = false,
                            Name = "RTS",
                            ParentGenreId = new Guid("94058fd9-862c-4cc2-a925-09e397628185")
                        },
                        new
                        {
                            Id = new Guid("ca929b54-993b-4232-bd53-72f8575defe3"),
                            IsDeleted = false,
                            Name = "TBS",
                            ParentGenreId = new Guid("94058fd9-862c-4cc2-a925-09e397628185")
                        },
                        new
                        {
                            Id = new Guid("aaad7c5f-944d-48ce-8257-e01c561950ef"),
                            IsDeleted = false,
                            Name = "RPG"
                        },
                        new
                        {
                            Id = new Guid("2768160c-877d-4fed-b82f-c88e4dcff6c3"),
                            IsDeleted = false,
                            Name = "Sports"
                        },
                        new
                        {
                            Id = new Guid("9a7c122a-eeca-46d1-8a88-b7d6b88221f5"),
                            IsDeleted = false,
                            Name = "Races"
                        },
                        new
                        {
                            Id = new Guid("3a9cad66-9bb9-44c8-92df-e95a7f326ebf"),
                            IsDeleted = false,
                            Name = "Rally",
                            ParentGenreId = new Guid("9a7c122a-eeca-46d1-8a88-b7d6b88221f5")
                        },
                        new
                        {
                            Id = new Guid("67fd3678-a02a-45af-a483-9068dd4c939a"),
                            IsDeleted = false,
                            Name = "Arcade",
                            ParentGenreId = new Guid("9a7c122a-eeca-46d1-8a88-b7d6b88221f5")
                        },
                        new
                        {
                            Id = new Guid("0a3d52f0-82d0-44c7-9ddd-990b25057820"),
                            IsDeleted = false,
                            Name = "Formula",
                            ParentGenreId = new Guid("9a7c122a-eeca-46d1-8a88-b7d6b88221f5")
                        },
                        new
                        {
                            Id = new Guid("e5a58777-8af5-4f43-a700-fa4fbce2dee7"),
                            IsDeleted = false,
                            Name = "Off-road",
                            ParentGenreId = new Guid("9a7c122a-eeca-46d1-8a88-b7d6b88221f5")
                        },
                        new
                        {
                            Id = new Guid("641a9e9e-a4e9-4672-addd-dda7d4941d86"),
                            IsDeleted = false,
                            Name = "Action"
                        },
                        new
                        {
                            Id = new Guid("661518c5-b421-45ea-a745-c5d5c4f3434e"),
                            IsDeleted = false,
                            Name = "FPS",
                            ParentGenreId = new Guid("641a9e9e-a4e9-4672-addd-dda7d4941d86")
                        },
                        new
                        {
                            Id = new Guid("65dea19a-53bf-4205-9a94-7d5a03c8f522"),
                            IsDeleted = false,
                            Name = "TPS",
                            ParentGenreId = new Guid("641a9e9e-a4e9-4672-addd-dda7d4941d86")
                        },
                        new
                        {
                            Id = new Guid("8bca534e-736c-4259-9563-3a29572de9b6"),
                            IsDeleted = false,
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = new Guid("b9f0dc99-9e7e-4f9e-b562-1f995f53a9ca"),
                            IsDeleted = false,
                            Name = "Puzzle & Skill"
                        },
                        new
                        {
                            Id = new Guid("a40b825d-351e-f2fc-715c-0e1405050505"),
                            IsDeleted = false,
                            Name = "Condiments"
                        },
                        new
                        {
                            Id = new Guid("a40b825d-351e-f2fc-715c-0e1505050505"),
                            IsDeleted = false,
                            Name = "Beverages"
                        },
                        new
                        {
                            Id = new Guid("a40b825d-351e-f2fc-715c-0e1605050505"),
                            IsDeleted = false,
                            Name = "Confections"
                        },
                        new
                        {
                            Id = new Guid("a40b825d-351e-f2fc-715c-0e1705050505"),
                            IsDeleted = false,
                            Name = "Grains/Cereals"
                        },
                        new
                        {
                            Id = new Guid("a40b825d-351e-f2fc-715c-0e1805050505"),
                            IsDeleted = false,
                            Name = "Dairy Products"
                        },
                        new
                        {
                            Id = new Guid("a40b825d-351e-f2fc-715c-0e1905050505"),
                            IsDeleted = false,
                            Name = "Meat/Poultry"
                        },
                        new
                        {
                            Id = new Guid("a40b825d-351e-f2fc-715c-0e1a05050505"),
                            IsDeleted = false,
                            Name = "Produce"
                        },
                        new
                        {
                            Id = new Guid("a40b825d-351e-f2fc-715c-0e1b05050505"),
                            IsDeleted = false,
                            Name = "Seafood"
                        });
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.OrderModels.Order", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("CustomerId");

                    b.Property<DateTime>("OrderDate");

                    b.Property<int>("OrderStatus");

                    b.Property<DateTime>("ShippedDate");

                    b.Property<string>("Shipper");

                    b.HasKey("Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.OrderModels.OrderDetail", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<float>("Discount")
                        .HasColumnType("Real");

                    b.Property<string>("GameKey")
                        .HasColumnType("nvarchar(40)");

                    b.Property<Guid>("OrderId");

                    b.Property<decimal>("Price")
                        .HasColumnType("Money");

                    b.Property<short>("Quantity")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.Platform", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Platform");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e462e441-19b7-407b-a5b7-c8486563c564"),
                            IsDeleted = false,
                            Name = "Desktop"
                        },
                        new
                        {
                            Id = new Guid("5e2658d2-02d9-4ca7-b539-232888904d0b"),
                            IsDeleted = false,
                            Name = "Mobile"
                        },
                        new
                        {
                            Id = new Guid("b4824060-bca4-4b87-83c2-02c99e14c148"),
                            IsDeleted = false,
                            Name = "IOS"
                        });
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.Publisher", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Description")
                        .HasColumnType("Ntext");

                    b.Property<string>("HomePage")
                        .HasColumnType("Ntext");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("Publisher");
                });

            modelBuilder.Entity("GameStore.Domain.Models.LanguageModels.GameTranslation", b =>
                {
                    b.HasOne("GameStore.Domain.Models.SqlModels.GameModels.Game", "Game")
                        .WithMany("Languages")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameStore.Domain.Models.LanguageModels.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameStore.Domain.Models.LanguageModels.GenreTranslation", b =>
                {
                    b.HasOne("GameStore.Domain.Models.SqlModels.Genre", "Genre")
                        .WithMany("Languages")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameStore.Domain.Models.LanguageModels.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameStore.Domain.Models.LanguageModels.PlatformTranslation", b =>
                {
                    b.HasOne("GameStore.Domain.Models.SqlModels.Platform", "Platform")
                        .WithMany("Languages")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameStore.Domain.Models.LanguageModels.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameStore.Domain.Models.LanguageModels.PublisherTranslation", b =>
                {
                    b.HasOne("GameStore.Domain.Models.SqlModels.Publisher", "Publisher")
                        .WithMany("Languages")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameStore.Domain.Models.LanguageModels.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.AccountModels.UserRole", b =>
                {
                    b.HasOne("GameStore.Domain.Models.SqlModels.AccountModels.Role", "Role")
                        .WithMany("Roles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameStore.Domain.Models.SqlModels.AccountModels.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.CommentModels.Comment", b =>
                {
                    b.HasOne("GameStore.Domain.Models.SqlModels.GameModels.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId");

                    b.HasOne("GameStore.Domain.Models.SqlModels.GameModels.GameState")
                        .WithMany("Comments")
                        .HasForeignKey("GameStateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameStore.Domain.Models.SqlModels.CommentModels.Comment", "ParentComment")
                        .WithMany()
                        .HasForeignKey("ParentCommentId");
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.GameModels.Game", b =>
                {
                    b.HasOne("GameStore.Domain.Models.SqlModels.GameModels.GameState", "GameState")
                        .WithMany()
                        .HasForeignKey("GameStateId");

                    b.HasOne("GameStore.Domain.Models.SqlModels.Publisher", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId");
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.GameModels.GameGenre", b =>
                {
                    b.HasOne("GameStore.Domain.Models.SqlModels.GameModels.Game", "Game")
                        .WithMany("GameGenres")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameStore.Domain.Models.SqlModels.Genre", "Genre")
                        .WithMany("GameGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.GameModels.GamePlatform", b =>
                {
                    b.HasOne("GameStore.Domain.Models.SqlModels.GameModels.Game", "Game")
                        .WithMany("GamePlatforms")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameStore.Domain.Models.SqlModels.Platform", "PlatformType")
                        .WithMany("GamePlatforms")
                        .HasForeignKey("PlatformTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.Genre", b =>
                {
                    b.HasOne("GameStore.Domain.Models.SqlModels.Genre", "ParentGenre")
                        .WithMany()
                        .HasForeignKey("ParentGenreId");
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.OrderModels.OrderDetail", b =>
                {
                    b.HasOne("GameStore.Domain.Models.SqlModels.OrderModels.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
