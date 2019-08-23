﻿// <auto-generated />
using System;
using Engineer.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Engineer.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Engineer.Domain.Entities.ToDo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<bool>("IsCompleted");

                    b.Property<string>("Task")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("Tasks");

                    b.HasData(
                        new { Id = new Guid("180d4b4d-439b-4356-bc21-d01e3dd651ca"), CreatedAt = new DateTimeOffset(new DateTime(2019, 8, 23, 5, 40, 28, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), IsCompleted = false, Task = "Seed Database" },
                        new { Id = new Guid("9879d7a2-4138-49ff-bbb7-46b48431fd2e"), CreatedAt = new DateTimeOffset(new DateTime(2019, 8, 23, 5, 40, 38, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), IsCompleted = false, Task = "Migrate Database" },
                        new { Id = new Guid("111d3da4-8c0d-46f4-bb5f-9d47896bdbd2"), CreatedAt = new DateTimeOffset(new DateTime(2019, 8, 23, 5, 40, 48, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), IsCompleted = false, Task = "Get Data" },
                        new { Id = new Guid("780f2cfb-1bcb-4765-9a7c-41d3fda1c14c"), CreatedAt = new DateTimeOffset(new DateTime(2019, 8, 23, 5, 40, 58, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), IsCompleted = false, Task = "Update Data" },
                        new { Id = new Guid("005123eb-7299-4845-8f7e-6b2b37f5bdbc"), CreatedAt = new DateTimeOffset(new DateTime(2019, 8, 23, 5, 41, 8, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), IsCompleted = false, Task = "Delete Data" }
                    );
                });
#pragma warning restore 612, 618
        }
    }
}
