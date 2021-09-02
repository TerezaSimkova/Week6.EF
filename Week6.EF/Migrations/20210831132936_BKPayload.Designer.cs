﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Week6.EF.EF;

namespace Week6.EF.Migrations
{
    [DbContext(typeof(KnightsContext))]
    [Migration("20210831132936_BKPayload")]
    partial class BKPayload
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Week6.EF.Core.Models.Battle", b =>
                {
                    b.Property<int>("BattleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BattleId");

                    b.ToTable("Battles");
                });

            modelBuilder.Entity("Week6.EF.Core.Models.BattleKnight", b =>
                {
                    b.Property<int>("BattleId")
                        .HasColumnType("int");

                    b.Property<int>("KnightId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateJoined")
                        .HasColumnType("datetime2");

                    b.HasKey("BattleId", "KnightId");

                    b.HasIndex("KnightId");

                    b.ToTable("BattleKnight");
                });

            modelBuilder.Entity("Week6.EF.Core.Models.Knight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Knights");
                });

            modelBuilder.Entity("Week6.EF.Core.Models.Weapon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KnightId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KnightId");

                    b.ToTable("Weapons");
                });

            modelBuilder.Entity("Week6.EF.Core.Models.BattleKnight", b =>
                {
                    b.HasOne("Week6.EF.Core.Models.Battle", null)
                        .WithMany()
                        .HasForeignKey("BattleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Week6.EF.Core.Models.Knight", null)
                        .WithMany()
                        .HasForeignKey("KnightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Week6.EF.Core.Models.Weapon", b =>
                {
                    b.HasOne("Week6.EF.Core.Models.Knight", "Knight")
                        .WithMany("Weapons")
                        .HasForeignKey("KnightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Knight");
                });

            modelBuilder.Entity("Week6.EF.Core.Models.Knight", b =>
                {
                    b.Navigation("Weapons");
                });
#pragma warning restore 612, 618
        }
    }
}
