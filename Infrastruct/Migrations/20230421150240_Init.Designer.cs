﻿// <auto-generated />
using System;
using Infrastruct.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastruct.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230421150240_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.CardEntities.Card", b =>
                {
                    b.Property<int>("Suit")
                        .HasColumnType("int");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("Domain.Entities.CardEntities.Deck", b =>
                {
                    b.ToTable("Deck");
                });

            modelBuilder.Entity("Domain.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("PlayerMessage")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RoomId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("RoomId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Domain.Entities.PlayerEntities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConnectionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GameRoomId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("UserScore")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameRoomId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Domain.Entities.RoomEntities.Room", b =>
                {
                    b.Property<string>("RoomId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("NumberOfRounds")
                        .HasColumnType("int");

                    b.Property<int>("RoundsPlayed")
                        .HasColumnType("int");

                    b.HasKey("RoomId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("UserScore")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.Message", b =>
                {
                    b.HasOne("Domain.Entities.PlayerEntities.Player", "Author")
                        .WithMany("Messages")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.RoomEntities.Room", "Room")
                        .WithMany("Chat")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Domain.Entities.PlayerEntities.Player", b =>
                {
                    b.HasOne("Domain.Entities.RoomEntities.Room", "GameRoom")
                        .WithMany("Players")
                        .HasForeignKey("GameRoomId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("GameRoom");
                });

            modelBuilder.Entity("Domain.Entities.PlayerEntities.Player", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Domain.Entities.RoomEntities.Room", b =>
                {
                    b.Navigation("Chat");

                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
