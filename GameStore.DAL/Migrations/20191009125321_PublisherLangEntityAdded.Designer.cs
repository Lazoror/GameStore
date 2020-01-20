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
    [Migration("20191009125321_PublisherLangEntityAdded")]
    partial class PublisherLangEntityAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GameStore.Models.Models.Comment", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("Body");

                    b.Property<Guid>("GameId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<Guid?>("ParentCommentId");

                    b.Property<string>("Quote");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("ParentCommentId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("GameStore.Models.Models.Game", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime>("AddDate");

                    b.Property<string>("Description");

                    b.Property<bool>("Discontinued");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Key");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price")
                        .HasColumnType("Money");

                    b.Property<DateTime>("PublishDate");

                    b.Property<Guid?>("PublisherId");

                    b.Property<short>("UnitsInStock")
                        .HasColumnType("smallint");

                    b.Property<int>("ViewCount");

                    b.HasKey("Id");

                    b.HasIndex("Key")
                        .IsUnique()
                        .HasFilter("[Key] IS NOT NULL");

                    b.HasIndex("PublisherId");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("GameStore.Models.Models.GameGenre", b =>
                {
                    b.Property<Guid>("GameId");

                    b.Property<Guid>("GenreId");

                    b.HasKey("GameId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("GameGenre");
                });

            modelBuilder.Entity("GameStore.Models.Models.GamePlatform", b =>
                {
                    b.Property<Guid>("GameId");

                    b.Property<Guid>("PlatformTypeId");

                    b.HasKey("GameId", "PlatformTypeId");

                    b.HasIndex("PlatformTypeId");

                    b.ToTable("GamePlatform");
                });

            modelBuilder.Entity("GameStore.Models.Models.Genre", b =>
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

            modelBuilder.Entity("GameStore.Models.Models.Language", b =>
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
                            Id = new Guid("cf9fb8b5-88c1-4eee-84e4-1a210a1eb32c"),
                            Code = "en",
                            Name = "English"
                        },
                        new
                        {
                            Id = new Guid("02abd65f-c833-446f-942a-29fc498ebc2f"),
                            Code = "ru",
                            Name = "Russian"
                        });
                });

            modelBuilder.Entity("GameStore.Models.Models.LanguageModels.PublisherLang", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyName");

                    b.Property<string>("Description");

                    b.Property<string>("HomePage");

                    b.Property<Guid>("LanguageId");

                    b.Property<Guid>("PublisherId");

                    b.HasKey("Id");

                    b.ToTable("PublisherLang");
                });

            modelBuilder.Entity("GameStore.Models.Models.Order", b =>
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

            modelBuilder.Entity("GameStore.Models.Models.OrderDetail", b =>
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

            modelBuilder.Entity("GameStore.Models.Models.PlatformType", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("Type")
                        .IsUnique()
                        .HasFilter("[Type] IS NOT NULL");

                    b.ToTable("PlatformType");
                });

            modelBuilder.Entity("GameStore.Models.Models.Publisher", b =>
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

            modelBuilder.Entity("GameStore.Models.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("GameStore.Models.Models.User", b =>
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

            modelBuilder.Entity("GameStore.Models.Models.UserRole", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("GameStore.Models.Models.Comment", b =>
                {
                    b.HasOne("GameStore.Models.Models.Game", "Game")
                        .WithMany("Comments")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameStore.Models.Models.Comment", "ParentComment")
                        .WithMany()
                        .HasForeignKey("ParentCommentId");
                });

            modelBuilder.Entity("GameStore.Models.Models.Game", b =>
                {
                    b.HasOne("GameStore.Models.Models.Publisher", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId");
                });

            modelBuilder.Entity("GameStore.Models.Models.GameGenre", b =>
                {
                    b.HasOne("GameStore.Models.Models.Game", "Game")
                        .WithMany("GameGenres")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameStore.Models.Models.Genre", "Genre")
                        .WithMany("GameGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameStore.Models.Models.GamePlatform", b =>
                {
                    b.HasOne("GameStore.Models.Models.Game", "Game")
                        .WithMany("GamePlatforms")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameStore.Models.Models.PlatformType", "PlatformType")
                        .WithMany("GamePlatforms")
                        .HasForeignKey("PlatformTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameStore.Models.Models.Genre", b =>
                {
                    b.HasOne("GameStore.Models.Models.Genre", "ParentGenre")
                        .WithMany()
                        .HasForeignKey("ParentGenreId");
                });

            modelBuilder.Entity("GameStore.Models.Models.OrderDetail", b =>
                {
                    b.HasOne("GameStore.Models.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameStore.Models.Models.UserRole", b =>
                {
                    b.HasOne("GameStore.Models.Models.Role", "Role")
                        .WithMany("Roles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameStore.Models.Models.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
