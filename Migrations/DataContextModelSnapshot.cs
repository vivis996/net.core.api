﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using net.core.api.Data;

namespace net.core.api.Migrations
{
  [DbContext(typeof(DataContext))]
  partial class DataContextModelSnapshot : ModelSnapshot
  {
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
      modelBuilder
          .HasAnnotation("ProductVersion", "5.0.8");

      modelBuilder.Entity("CharacterSkill", b =>
          {
            b.Property<int>("CharactersId")
                      .HasColumnType("INTEGER");

            b.Property<int>("SkillsId")
                      .HasColumnType("INTEGER");

            b.HasKey("CharactersId", "SkillsId");

            b.HasIndex("SkillsId");

            b.ToTable("CharacterSkill");
          });

      modelBuilder.Entity("net.core.api.Models.Character", b =>
          {
            b.Property<int>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("INTEGER");

            b.Property<int>("Class")
                      .HasColumnType("INTEGER");

            b.Property<int>("Defeats")
                      .HasColumnType("INTEGER");

            b.Property<int>("Defense")
                      .HasColumnType("INTEGER");

            b.Property<int>("Fights")
                      .HasColumnType("INTEGER");

            b.Property<int>("HitPoints")
                      .HasColumnType("INTEGER");

            b.Property<int>("Intelligence")
                      .HasColumnType("INTEGER");

            b.Property<string>("Name")
                      .HasColumnType("TEXT");

            b.Property<int>("Strength")
                      .HasColumnType("INTEGER");

            b.Property<int?>("UserId")
                      .HasColumnType("INTEGER");

            b.Property<int>("Victories")
                      .HasColumnType("INTEGER");

            b.HasKey("Id");

            b.HasIndex("UserId");

            b.ToTable("Characters");
          });

      modelBuilder.Entity("net.core.api.Models.Skill", b =>
          {
            b.Property<int>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("INTEGER");

            b.Property<int>("Damage")
                      .HasColumnType("INTEGER");

            b.Property<string>("Name")
                      .HasColumnType("TEXT");

            b.HasKey("Id");

            b.ToTable("Skills");

            b.HasData(
                      new
                  {
                    Id = 1,
                    Damage = 100,
                    Name = "Force"
                  },
                      new
                  {
                    Id = 2,
                    Damage = 110,
                    Name = "Dark Rays"
                  },
                      new
                  {
                    Id = 3,
                    Damage = 80,
                    Name = "Push"
                  });
          });

      modelBuilder.Entity("net.core.api.Models.User", b =>
          {
            b.Property<int>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("INTEGER");

            b.Property<byte[]>("PasswordHash")
                      .HasColumnType("BLOB");

            b.Property<byte[]>("PasswordSalt")
                      .HasColumnType("BLOB");

            b.Property<string>("Role")
                      .IsRequired()
                      .ValueGeneratedOnAdd()
                      .HasColumnType("TEXT")
                      .HasDefaultValue("Player");

            b.Property<string>("Username")
                      .HasColumnType("TEXT");

            b.HasKey("Id");

            b.ToTable("Users");
          });

      modelBuilder.Entity("net.core.api.Models.Weapon", b =>
          {
            b.Property<int>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("INTEGER");

            b.Property<int>("CharacterId")
                      .HasColumnType("INTEGER");

            b.Property<int>("Damage")
                      .HasColumnType("INTEGER");

            b.Property<string>("Name")
                      .HasColumnType("TEXT");

            b.HasKey("Id");

            b.HasIndex("CharacterId")
                      .IsUnique();

            b.ToTable("Weapons");
          });

      modelBuilder.Entity("CharacterSkill", b =>
          {
            b.HasOne("net.core.api.Models.Character", null)
                      .WithMany()
                      .HasForeignKey("CharactersId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.HasOne("net.core.api.Models.Skill", null)
                      .WithMany()
                      .HasForeignKey("SkillsId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();
          });

      modelBuilder.Entity("net.core.api.Models.Character", b =>
          {
            b.HasOne("net.core.api.Models.User", "User")
                      .WithMany("Characters")
                      .HasForeignKey("UserId");

            b.Navigation("User");
          });

      modelBuilder.Entity("net.core.api.Models.Weapon", b =>
          {
            b.HasOne("net.core.api.Models.Character", "Character")
                      .WithOne("Weapon")
                      .HasForeignKey("net.core.api.Models.Weapon", "CharacterId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.Navigation("Character");
          });

      modelBuilder.Entity("net.core.api.Models.Character", b =>
          {
            b.Navigation("Weapon");
          });

      modelBuilder.Entity("net.core.api.Models.User", b =>
          {
            b.Navigation("Characters");
          });
#pragma warning restore 612, 618
    }
  }
}
