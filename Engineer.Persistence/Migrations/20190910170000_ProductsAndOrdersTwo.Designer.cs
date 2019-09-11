﻿// <auto-generated />
using System;
using Engineer.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Engineer.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190910170000_ProductsAndOrdersTwo")]
    partial class ProductsAndOrdersTwo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Engineer.Domain.Entities.EngineerUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<DateTimeOffset>("LastLoginTimestamp");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int>("Role");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Engineer.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("Email");

                    b.Property<DateTimeOffset>("EndDate");

                    b.Property<string>("Name");

                    b.Property<string>("OrderNumber");

                    b.Property<DateTimeOffset>("StartDate");

                    b.Property<string>("UniversityId");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Engineer.Domain.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("OrderId");

                    b.Property<Guid?>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<int>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("Engineer.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new { Id = new Guid("180d4b4d-439b-4356-bc21-d01e3dd651ca"), CreatedAt = new DateTimeOffset(new DateTime(2019, 8, 23, 5, 40, 28, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), Description = "Funny little item 0", Image = "", Name = "Item 0", Price = 100m },
                        new { Id = new Guid("181d4b4d-439b-4356-bc21-d01e3dd651ca"), CreatedAt = new DateTimeOffset(new DateTime(2019, 8, 23, 5, 57, 8, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), Description = "Funny little item 1", Image = "", Name = "Item 1", Price = 110m },
                        new { Id = new Guid("182d4b4d-439b-4356-bc21-d01e3dd651ca"), CreatedAt = new DateTimeOffset(new DateTime(2019, 8, 23, 6, 13, 48, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), Description = "Funny little item 2", Image = "", Name = "Item 2", Price = 120m },
                        new { Id = new Guid("183d4b4d-439b-4356-bc21-d01e3dd651ca"), CreatedAt = new DateTimeOffset(new DateTime(2019, 8, 23, 6, 30, 28, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), Description = "Funny little item 3", Image = "", Name = "Item 3", Price = 130m },
                        new { Id = new Guid("184d4b4d-439b-4356-bc21-d01e3dd651ca"), CreatedAt = new DateTimeOffset(new DateTime(2019, 8, 23, 6, 47, 8, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), Description = "Funny little item 4", Image = "", Name = "Item 4", Price = 140m },
                        new { Id = new Guid("185d4b4d-439b-4356-bc21-d01e3dd651ca"), CreatedAt = new DateTimeOffset(new DateTime(2019, 8, 23, 7, 3, 48, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), Description = "Funny little item 5", Image = "", Name = "Item 5", Price = 150m },
                        new { Id = new Guid("186d4b4d-439b-4356-bc21-d01e3dd651ca"), CreatedAt = new DateTimeOffset(new DateTime(2019, 8, 23, 7, 20, 28, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), Description = "Funny little item 6", Image = "", Name = "Item 6", Price = 160m },
                        new { Id = new Guid("187d4b4d-439b-4356-bc21-d01e3dd651ca"), CreatedAt = new DateTimeOffset(new DateTime(2019, 8, 23, 7, 37, 8, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), Description = "Funny little item 7", Image = "", Name = "Item 7", Price = 170m },
                        new { Id = new Guid("188d4b4d-439b-4356-bc21-d01e3dd651ca"), CreatedAt = new DateTimeOffset(new DateTime(2019, 8, 23, 7, 53, 48, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), Description = "Funny little item 8", Image = "", Name = "Item 8", Price = 180m },
                        new { Id = new Guid("189d4b4d-439b-4356-bc21-d01e3dd651ca"), CreatedAt = new DateTimeOffset(new DateTime(2019, 8, 23, 8, 10, 28, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), Description = "Funny little item 9", Image = "", Name = "Item 9", Price = 190m }
                    );
                });

            modelBuilder.Entity("Engineer.Domain.Entities.RefreshToken", b =>
                {
                    b.Property<string>("Token")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Expiration");

                    b.Property<string>("UserId");

                    b.HasKey("Token");

                    b.ToTable("RefreshTokens");
                });

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Engineer.Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("Engineer.Domain.Entities.Order", "Order")
                        .WithMany("Products")
                        .HasForeignKey("OrderId");

                    b.HasOne("Engineer.Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Engineer.Domain.Entities.EngineerUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Engineer.Domain.Entities.EngineerUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Engineer.Domain.Entities.EngineerUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Engineer.Domain.Entities.EngineerUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
