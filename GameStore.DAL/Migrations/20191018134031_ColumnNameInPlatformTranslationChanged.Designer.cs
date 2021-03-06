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
    [Migration("20191018134031_ColumnNameInPlatformTranslationChanged")]
    partial class ColumnNameInPlatformTranslationChanged
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

                    b.Property<Guid>("GameId");

                    b.Property<Guid>("LanguageId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("GameTranslation");
                });

            modelBuilder.Entity("GameStore.Domain.Models.LanguageModels.GenreTranslation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("GenreId");

                    b.Property<Guid>("LanguageId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.ToTable("GenreTranslation");
                });

            modelBuilder.Entity("GameStore.Domain.Models.LanguageModels.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Language");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e0aa65a1-524c-42fb-b79e-373cd5c74086"),
                            Code = "en",
                            Name = "English"
                        },
                        new
                        {
                            Id = new Guid("94f11f4a-ff83-4610-9611-ef838600b3da"),
                            Code = "ru",
                            Name = "Russian"
                        });
                });

            modelBuilder.Entity("GameStore.Domain.Models.LanguageModels.PlatformTranslation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("LanguageId");

                    b.Property<Guid>("PlatformId");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("PlatformId");

                    b.ToTable("PlatformTranslation");
                });

            modelBuilder.Entity("GameStore.Domain.Models.LanguageModels.PublisherTranslation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyName");

                    b.Property<string>("Description");

                    b.Property<string>("HomePage");

                    b.Property<Guid>("LanguageId");

                    b.Property<Guid>("PublisherId");

                    b.HasKey("Id");

                    b.HasIndex("PublisherId");

                    b.ToTable("PublisherTranslation");
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.AccountModels.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Role");
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
                });

            modelBuilder.Entity("GameStore.Domain.Models.SqlModels.OrderModels.Order", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("CustomerId");

                    b.Property<DateTime>("OrderDate");

                    b.Property<int>("OrderStatus");

                    b.Property<DateTime?>("ShippedDate");

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
                    b.HasOne("GameStore.Domain.Models.SqlModels.GameModels.Game")
                        .WithMany("Languages")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameStore.Domain.Models.LanguageModels.GenreTranslation", b =>
                {
                    b.HasOne("GameStore.Domain.Models.SqlModels.Genre")
                        .WithMany("Languages")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameStore.Domain.Models.LanguageModels.PlatformTranslation", b =>
                {
                    b.HasOne("GameStore.Domain.Models.SqlModels.Platform")
                        .WithMany("Languages")
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameStore.Domain.Models.LanguageModels.PublisherTranslation", b =>
                {
                    b.HasOne("GameStore.Domain.Models.SqlModels.Publisher")
                        .WithMany("Languages")
                        .HasForeignKey("PublisherId")
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
