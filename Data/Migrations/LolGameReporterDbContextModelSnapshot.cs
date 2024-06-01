﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LolGameReporter.Data.Migrations
{
    [DbContext(typeof(LolGameReporterDbContext))]
    partial class LolGameReporterDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LolGameReporter.Core.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChatId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedByName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<string>("DeletedByName")
                        .HasColumnType("text");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.ToTable("t_account", "main");
                });

            modelBuilder.Entity("LolGameReporter.Core.Models.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedByName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<string>("DeletedByName")
                        .HasColumnType("text");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("TelegramChatId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("t_chat", "main");
                });

            modelBuilder.Entity("LolGameReporter.Core.Models.Account", b =>
                {
                    b.HasOne("LolGameReporter.Core.Models.Chat", "Chat")
                        .WithMany("Accounts")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");
                });

            modelBuilder.Entity("LolGameReporter.Core.Models.Chat", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
