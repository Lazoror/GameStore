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
    [Migration("20191003065734_PublisherNonRequiredGameEntity")]
    partial class PublisherNonRequiredGameEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GameStore.Models.Entities.Comment", b =>
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

            modelBuilder.Entity("GameStore.Models.Entities.Game", b =>
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

            modelBuilder.Entity("GameStore.Models.Entities.GameGenre", b =>
                {
                    b.Property<Guid>("GameId");

                    b.Property<Guid>("GenreId");

                    b.HasKey("GameId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("GameGenre");
                });

            modelBuilder.Entity("GameStore.Models.Entities.GamePlatform", b =>
                {
                    b.Property<Guid>("GameId");

                    b.Property<Guid>("PlatformTypeId");

                    b.HasKey("GameId", "PlatformTypeId");

                    b.HasIndex("PlatformTypeId");

                    b.ToTable("GamePlatform");
                });

            modelBuilder.Entity("GameStore.Models.Entities.Genre", b =>
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

            modelBuilder.Entity("GameStore.Models.Entities.Order", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("CustomerId");

                    b.Property<bool>("IsCompleted");

                    b.Property<DateTime?>("OrderDate");

                    b.Property<string>("Shipper");

                    b.HasKey("Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("GameStore.Models.Entities.OrderDetail", b =>
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

            modelBuilder.Entity("GameStore.Models.Entities.PlatformType", b =>
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

            modelBuilder.Entity("GameStore.Models.Entities.Publisher", b =>
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

            modelBuilder.Entity("GameStore.Models.Entities.Comment", b =>
                {
                    b.HasOne("GameStore.Models.Entities.Game", "Game")
                        .WithMany("Comments")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameStore.Models.Entities.Comment", "ParentComment")
                        .WithMany()
                        .HasForeignKey("ParentCommentId");
                });

            modelBuilder.Entity("GameStore.Models.Entities.Game", b =>
                {
                    b.HasOne("GameStore.Models.Entities.Publisher", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId");
                });

            modelBuilder.Entity("GameStore.Models.Entities.GameGenre", b =>
                {
                    b.HasOne("GameStore.Models.Entities.Game", "Game")
                        .WithMany("GameGenres")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameStore.Models.Entities.Genre", "Genre")
                        .WithMany("GameGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameStore.Models.Entities.GamePlatform", b =>
                {
                    b.HasOne("GameStore.Models.Entities.Game", "Game")
                        .WithMany("GamePlatforms")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameStore.Models.Entities.PlatformType", "PlatformType")
                        .WithMany("GamePlatforms")
                        .HasForeignKey("PlatformTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameStore.Models.Entities.Genre", b =>
                {
                    b.HasOne("GameStore.Models.Entities.Genre", "ParentGenre")
                        .WithMany()
                        .HasForeignKey("ParentGenreId");
                });

            modelBuilder.Entity("GameStore.Models.Entities.OrderDetail", b =>
                {
                    b.HasOne("GameStore.Models.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
