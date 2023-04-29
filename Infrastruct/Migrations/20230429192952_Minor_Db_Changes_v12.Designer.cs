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
    [Migration("20230429192952_Minor_Db_Changes_v12")]
    partial class Minor_Db_Changes_v12
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CardDeck", b =>
                {
                    b.Property<int>("CardsId")
                        .HasColumnType("int");

                    b.Property<int>("DecksId")
                        .HasColumnType("int");

                    b.HasKey("CardsId", "DecksId");

                    b.HasIndex("DecksId");

                    b.ToTable("CardDeck");
                });

            modelBuilder.Entity("CardPlayer", b =>
                {
                    b.Property<int>("HandId")
                        .HasColumnType("int");

                    b.Property<int>("PlayersId")
                        .HasColumnType("int");

                    b.HasKey("HandId", "PlayersId");

                    b.HasIndex("PlayersId");

                    b.ToTable("CardPlayer");
                });

            modelBuilder.Entity("CardStack", b =>
                {
                    b.Property<int>("CardsId")
                        .HasColumnType("int");

                    b.Property<int>("StacksId")
                        .HasColumnType("int");

                    b.HasKey("CardsId", "StacksId");

                    b.HasIndex("StacksId");

                    b.ToTable("CardStack");
                });

            modelBuilder.Entity("Domain.Entities.CardEntities.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Suit")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("Domain.Entities.CardEntities.Deck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoomId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoomId")
                        .IsUnique();

                    b.ToTable("Decks");
                });

            modelBuilder.Entity("Domain.Entities.CardEntities.Stack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Mode")
                        .HasColumnType("int");

                    b.Property<string>("RoomId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoomId")
                        .IsUnique();

                    b.ToTable("Stacks");
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

                    b.Property<string>("PlayerMessage")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

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

                    b.Property<bool>("IsCardDrewFromDeck")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCardThrownToStack")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCardsDrewFromStack")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPlayerRound")
                        .HasColumnType("bit");

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

                    b.Property<int>("CurrentPlayerRoundIndex")
                        .HasColumnType("int");

                    b.Property<bool>("IsGameStarted")
                        .HasColumnType("bit");

                    b.Property<int>("MaxPlayerRoundIndex")
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

            modelBuilder.Entity("CardDeck", b =>
                {
                    b.HasOne("Domain.Entities.CardEntities.Card", null)
                        .WithMany()
                        .HasForeignKey("CardsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.CardEntities.Deck", null)
                        .WithMany()
                        .HasForeignKey("DecksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CardPlayer", b =>
                {
                    b.HasOne("Domain.Entities.CardEntities.Card", null)
                        .WithMany()
                        .HasForeignKey("HandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.PlayerEntities.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CardStack", b =>
                {
                    b.HasOne("Domain.Entities.CardEntities.Card", null)
                        .WithMany()
                        .HasForeignKey("CardsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.CardEntities.Stack", null)
                        .WithMany()
                        .HasForeignKey("StacksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.CardEntities.Deck", b =>
                {
                    b.HasOne("Domain.Entities.RoomEntities.Room", "Room")
                        .WithOne("Deck")
                        .HasForeignKey("Domain.Entities.CardEntities.Deck", "RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Domain.Entities.CardEntities.Stack", b =>
                {
                    b.HasOne("Domain.Entities.RoomEntities.Room", "Room")
                        .WithOne("Stack")
                        .HasForeignKey("Domain.Entities.CardEntities.Stack", "RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
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

                    b.Navigation("Deck")
                        .IsRequired();

                    b.Navigation("Players");

                    b.Navigation("Stack")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
