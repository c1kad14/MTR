﻿// <auto-generated />
using System;
using MTR.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MTR.DAL.Migrations
{
    [DbContext(typeof(MTRContext))]
    partial class MTRContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("MTR.Domain.Action", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ActionType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Guid")
                        .HasColumnType("TEXT");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TurnId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasAlternateKey("Guid");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TurnId");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("MTR.Domain.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Rank")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Suit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cards");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Rank = "SIX",
                            Suit = "HEARTS"
                        },
                        new
                        {
                            Id = 2,
                            Rank = "SEVEN",
                            Suit = "HEARTS"
                        },
                        new
                        {
                            Id = 3,
                            Rank = "EIGHT",
                            Suit = "HEARTS"
                        },
                        new
                        {
                            Id = 4,
                            Rank = "NINE",
                            Suit = "HEARTS"
                        },
                        new
                        {
                            Id = 5,
                            Rank = "TEN",
                            Suit = "HEARTS"
                        },
                        new
                        {
                            Id = 6,
                            Rank = "JACK",
                            Suit = "HEARTS"
                        },
                        new
                        {
                            Id = 7,
                            Rank = "QUEEN",
                            Suit = "HEARTS"
                        },
                        new
                        {
                            Id = 8,
                            Rank = "KING",
                            Suit = "HEARTS"
                        },
                        new
                        {
                            Id = 9,
                            Rank = "ACE",
                            Suit = "HEARTS"
                        },
                        new
                        {
                            Id = 10,
                            Rank = "SIX",
                            Suit = "SPADED"
                        },
                        new
                        {
                            Id = 11,
                            Rank = "SEVEN",
                            Suit = "SPADED"
                        },
                        new
                        {
                            Id = 12,
                            Rank = "EIGHT",
                            Suit = "SPADED"
                        },
                        new
                        {
                            Id = 13,
                            Rank = "NINE",
                            Suit = "SPADED"
                        },
                        new
                        {
                            Id = 14,
                            Rank = "TEN",
                            Suit = "SPADED"
                        },
                        new
                        {
                            Id = 15,
                            Rank = "JACK",
                            Suit = "SPADED"
                        },
                        new
                        {
                            Id = 16,
                            Rank = "QUEEN",
                            Suit = "SPADED"
                        },
                        new
                        {
                            Id = 17,
                            Rank = "KING",
                            Suit = "SPADED"
                        },
                        new
                        {
                            Id = 18,
                            Rank = "ACE",
                            Suit = "SPADED"
                        },
                        new
                        {
                            Id = 19,
                            Rank = "SIX",
                            Suit = "DIAMONDS"
                        },
                        new
                        {
                            Id = 20,
                            Rank = "SEVEN",
                            Suit = "DIAMONDS"
                        },
                        new
                        {
                            Id = 21,
                            Rank = "EIGHT",
                            Suit = "DIAMONDS"
                        },
                        new
                        {
                            Id = 22,
                            Rank = "NINE",
                            Suit = "DIAMONDS"
                        },
                        new
                        {
                            Id = 23,
                            Rank = "TEN",
                            Suit = "DIAMONDS"
                        },
                        new
                        {
                            Id = 24,
                            Rank = "JACK",
                            Suit = "DIAMONDS"
                        },
                        new
                        {
                            Id = 25,
                            Rank = "QUEEN",
                            Suit = "DIAMONDS"
                        },
                        new
                        {
                            Id = 26,
                            Rank = "KING",
                            Suit = "DIAMONDS"
                        },
                        new
                        {
                            Id = 27,
                            Rank = "ACE",
                            Suit = "DIAMONDS"
                        },
                        new
                        {
                            Id = 28,
                            Rank = "SIX",
                            Suit = "CLUBS"
                        },
                        new
                        {
                            Id = 29,
                            Rank = "SEVEN",
                            Suit = "CLUBS"
                        },
                        new
                        {
                            Id = 30,
                            Rank = "EIGHT",
                            Suit = "CLUBS"
                        },
                        new
                        {
                            Id = 31,
                            Rank = "NINE",
                            Suit = "CLUBS"
                        },
                        new
                        {
                            Id = 32,
                            Rank = "TEN",
                            Suit = "CLUBS"
                        },
                        new
                        {
                            Id = 33,
                            Rank = "JACK",
                            Suit = "CLUBS"
                        },
                        new
                        {
                            Id = 34,
                            Rank = "QUEEN",
                            Suit = "CLUBS"
                        },
                        new
                        {
                            Id = 35,
                            Rank = "KING",
                            Suit = "CLUBS"
                        },
                        new
                        {
                            Id = 36,
                            Rank = "ACE",
                            Suit = "CLUBS"
                        });
                });

            modelBuilder.Entity("MTR.Domain.Cheat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CardId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("Guid")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAccounted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasAlternateKey("Guid");

                    b.HasIndex("ActionId");

                    b.HasIndex("CardId");

                    b.ToTable("Cheats");
                });

            modelBuilder.Entity("MTR.Domain.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Ended")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Guid")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Started")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasAlternateKey("Guid");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("MTR.Domain.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("MTR.Domain.MuckedCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CardId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("MuckedCards");
                });

            modelBuilder.Entity("MTR.Domain.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("GameId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("MTR.Domain.PlayerRemoved", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("TEXT");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerRemoved");
                });

            modelBuilder.Entity("MTR.Domain.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Ended")
                        .HasColumnType("TEXT");

                    b.Property<int>("GameId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("Guid")
                        .HasColumnType("TEXT");

                    b.Property<int>("Sequence")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Started")
                        .HasColumnType("TEXT");

                    b.Property<string>("Suit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasAlternateKey("Guid");

                    b.HasIndex("GameId");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("MTR.Domain.RoundCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CardId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("Guid")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoundId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasAlternateKey("Guid");

                    b.HasIndex("CardId");

                    b.HasIndex("RoundId");

                    b.ToTable("RoundCards");
                });

            modelBuilder.Entity("MTR.Domain.RoundResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("Guid")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("TEXT");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoundId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasAlternateKey("Guid");

                    b.HasIndex("PlayerId");

                    b.HasIndex("RoundId");

                    b.ToTable("RoundResults");
                });

            modelBuilder.Entity("MTR.Domain.Turn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("OppositePlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoundId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OppositePlayerId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("RoundId");

                    b.ToTable("Turns");
                });

            modelBuilder.Entity("MTR.Domain.TurnCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CardId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("Guid")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("TEXT");

                    b.Property<int?>("OppositeCardId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasAlternateKey("Guid");

                    b.HasIndex("ActionId");

                    b.HasIndex("CardId");

                    b.HasIndex("OppositeCardId");

                    b.ToTable("TurnCards");
                });

            modelBuilder.Entity("MTR.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Guid")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasAlternateKey("Guid");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MTR.Domain.UserDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ImageId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("UserId");

                    b.ToTable("UserDetails");
                });

            modelBuilder.Entity("MTR.Domain.Action", b =>
                {
                    b.HasOne("MTR.Domain.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MTR.Domain.Turn", "Turn")
                        .WithMany()
                        .HasForeignKey("TurnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Turn");
                });

            modelBuilder.Entity("MTR.Domain.Cheat", b =>
                {
                    b.HasOne("MTR.Domain.Action", "Action")
                        .WithMany("Cheats")
                        .HasForeignKey("ActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MTR.Domain.TurnCard", "Card")
                        .WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Action");

                    b.Navigation("Card");
                });

            modelBuilder.Entity("MTR.Domain.MuckedCard", b =>
                {
                    b.HasOne("MTR.Domain.RoundCard", "Card")
                        .WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");
                });

            modelBuilder.Entity("MTR.Domain.Player", b =>
                {
                    b.HasOne("MTR.Domain.Game", "Game")
                        .WithMany("Players")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MTR.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MTR.Domain.PlayerRemoved", b =>
                {
                    b.HasOne("MTR.Domain.Player", "Player")
                        .WithMany("Removed")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("MTR.Domain.Round", b =>
                {
                    b.HasOne("MTR.Domain.Game", "Game")
                        .WithMany("Rounds")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("MTR.Domain.RoundCard", b =>
                {
                    b.HasOne("MTR.Domain.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MTR.Domain.Round", "Round")
                        .WithMany()
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Round");
                });

            modelBuilder.Entity("MTR.Domain.RoundResult", b =>
                {
                    b.HasOne("MTR.Domain.Player", "Player")
                        .WithMany("Results")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MTR.Domain.Round", "Round")
                        .WithMany("RoundResults")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Round");
                });

            modelBuilder.Entity("MTR.Domain.Turn", b =>
                {
                    b.HasOne("MTR.Domain.Player", "OppositePlayer")
                        .WithMany()
                        .HasForeignKey("OppositePlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MTR.Domain.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MTR.Domain.Round", "Round")
                        .WithMany()
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OppositePlayer");

                    b.Navigation("Player");

                    b.Navigation("Round");
                });

            modelBuilder.Entity("MTR.Domain.TurnCard", b =>
                {
                    b.HasOne("MTR.Domain.Action", "Action")
                        .WithMany("Cards")
                        .HasForeignKey("ActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MTR.Domain.RoundCard", "Card")
                        .WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MTR.Domain.TurnCard", "OppositeCard")
                        .WithMany()
                        .HasForeignKey("OppositeCardId");

                    b.Navigation("Action");

                    b.Navigation("Card");

                    b.Navigation("OppositeCard");
                });

            modelBuilder.Entity("MTR.Domain.UserDetail", b =>
                {
                    b.HasOne("MTR.Domain.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.HasOne("MTR.Domain.User", "User")
                        .WithMany("Details")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MTR.Domain.Action", b =>
                {
                    b.Navigation("Cards");

                    b.Navigation("Cheats");
                });

            modelBuilder.Entity("MTR.Domain.Game", b =>
                {
                    b.Navigation("Players");

                    b.Navigation("Rounds");
                });

            modelBuilder.Entity("MTR.Domain.Player", b =>
                {
                    b.Navigation("Removed");

                    b.Navigation("Results");
                });

            modelBuilder.Entity("MTR.Domain.Round", b =>
                {
                    b.Navigation("RoundResults");
                });

            modelBuilder.Entity("MTR.Domain.User", b =>
                {
                    b.Navigation("Details");
                });
#pragma warning restore 612, 618
        }
    }
}
