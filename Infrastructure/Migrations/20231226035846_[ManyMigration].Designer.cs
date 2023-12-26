﻿// <auto-generated />
using System;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(CleanApiMainContext))]
    [Migration("20231226035846_[ManyMigration]")]
    partial class ManyMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Models.Animal.AnimalModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("AnimalModel");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AnimalModel");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Models.Ownership", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AnimalId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "AnimalId");

                    b.HasIndex("AnimalId");

                    b.ToTable("Ownerships");
                });

            modelBuilder.Entity("Domain.Models.User.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Userpassword")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Models.Bird", b =>
                {
                    b.HasBaseType("Domain.Models.Animal.AnimalModel");

                    b.Property<bool>("CanFly")
                        .HasColumnType("tinyint(1)");

                    b.HasDiscriminator().HasValue("Bird");
                });

            modelBuilder.Entity("Domain.Models.Cat", b =>
                {
                    b.HasBaseType("Domain.Models.Animal.AnimalModel");

                    b.Property<bool>("LikesToPlay")
                        .HasColumnType("tinyint(1)");

                    b.HasDiscriminator().HasValue("Cat");
                });

            modelBuilder.Entity("Domain.Models.Dog", b =>
                {
                    b.HasBaseType("Domain.Models.Animal.AnimalModel");

                    b.HasDiscriminator().HasValue("Dog");
                });

            modelBuilder.Entity("Domain.Models.Ownership", b =>
                {
                    b.HasOne("Domain.Models.Animal.AnimalModel", "Animal")
                        .WithMany("Ownerships")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.User.UserModel", "User")
                        .WithMany("Ownerships")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.Animal.AnimalModel", b =>
                {
                    b.Navigation("Ownerships");
                });

            modelBuilder.Entity("Domain.Models.User.UserModel", b =>
                {
                    b.Navigation("Ownerships");
                });
#pragma warning restore 612, 618
        }
    }
}
