// Data/ApplicationDbContext.cs
using ecommerce_net.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ecommerce_net.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Seed default users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Administrator",
                    Email = "admin@toko.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "admin"
                },
                new User
                {
                    Id = 2,
                    Name = "User Demo",
                    Email = "user@toko.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("user123"),
                    Role = "user"
                }
            );
        }
    }
}