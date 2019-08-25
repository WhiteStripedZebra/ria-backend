using System;
using Engineer.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.ValueGeneration;

namespace Engineer.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<EngineerUser>
    {
        public DbSet<ToDo> Tasks { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
    
        
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
