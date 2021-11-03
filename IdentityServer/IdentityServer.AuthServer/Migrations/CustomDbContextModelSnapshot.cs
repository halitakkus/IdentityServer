﻿// <auto-generated />
using IdentityServer.AuthServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IdentityServer.AuthServer.Migrations
{
    [DbContext(typeof(CustomDbContext))]
    partial class CustomDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IdentityServer.AuthServer.Models.CustomUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CustomUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "halitakkus03@gmail.com",
                            Password = "password"
                        },
                        new
                        {
                            Id = 2,
                            Email = "test@gmail.com",
                            Password = "password"
                        },
                        new
                        {
                            Id = 3,
                            Email = "test2@gmail.com",
                            Password = "password"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}