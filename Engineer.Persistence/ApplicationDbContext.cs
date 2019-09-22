using System;
using System.Collections.Generic;
using Engineer.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.ValueGeneration;

namespace Engineer.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<EngineerUser>
    {
        public DbSet<Club> Clubs { get; set; }
        public DbSet<ToDo> Tasks { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var products = new List<Product>();

            for (var i = 0; i < 10; i++)
            {
                products.Add(new Product
                {
                    Id = Guid.Parse($"18{i}d4b4d-439b-4356-bc21-d01e3dd651ca"),
                    CreatedAt = DateTimeOffset.FromUnixTimeSeconds(1566538828 + (1000 * i)),
                    Name = $"Item {i}",
                    Description = $"Funny little item {i}",
                    Image = "",
                    Price = 100 + (10*i)
                });
            }

            modelBuilder.Entity<Product>().HasData(products.ToArray());

            modelBuilder.Entity<ToDo>().HasData(
                new ToDo
                {
                    Id = Guid.Parse("180d4b4d-439b-4356-bc21-d01e3dd651ca"),
                    CreatedAt = DateTimeOffset.FromUnixTimeSeconds(1566538828),
                    IsCompleted = false,
                    Task = "Seed Database"
                },
                new ToDo
                {
                    Id = Guid.Parse("9879d7a2-4138-49ff-bbb7-46b48431fd2e"),
                    CreatedAt = DateTimeOffset.FromUnixTimeSeconds(1566538838),
                    IsCompleted = false,
                    Task = "Migrate Database"
                },
                new ToDo
                {
                    Id = Guid.Parse("111d3da4-8c0d-46f4-bb5f-9d47896bdbd2"),
                    CreatedAt = DateTimeOffset.FromUnixTimeSeconds(1566538848),
                    IsCompleted = false,
                    Task = "Get Data"
                },
                new ToDo
                {
                    Id = Guid.Parse("780f2cfb-1bcb-4765-9a7c-41d3fda1c14c"),
                    CreatedAt = DateTimeOffset.FromUnixTimeSeconds(1566538858),
                    IsCompleted = false,
                    Task = "Update Data"
                },
                new ToDo
                {
                    Id = Guid.Parse("005123eb-7299-4845-8f7e-6b2b37f5bdbc"),
                    CreatedAt = DateTimeOffset.FromUnixTimeSeconds(1566538868),
                    IsCompleted = false,
                    Task = "Delete Data"
                });

                

               base.OnModelCreating(modelBuilder);
        }
       
    }
}
