﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Entities;

namespace WebApi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Journal> Journals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<IdentityRole> roles = SeedRoles(modelBuilder);
            SeedUsers(modelBuilder, roles);
        }

        private static List<IdentityRole> SeedRoles(ModelBuilder modelBuilder)
        {
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }
            };

            roles.ForEach(role => modelBuilder.Entity<IdentityRole>().HasData(role));

            return roles;
        }

        private static void SeedUsers(ModelBuilder modelBuilder, List<IdentityRole> roles)
        {
            var hasher = new PasswordHasher<IdentityUser>();

            var johnDoe = new IdentityUser()
            {
                Id = "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                UserName = "johndoe",
                NormalizedUserName = "JOHNDOE",
                Email = "john.doe@nomail.com",
                NormalizedEmail = "JOHN.DOE@NOMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "0707-12345",
                PasswordHash = hasher.HashPassword(null, "Password#123"),
            };

            modelBuilder.Entity<IdentityUser>().HasData(johnDoe);

            var administrator = roles.FirstOrDefault(x => x.Name == "Administrator");

            if (administrator != null)
            {
                modelBuilder.Entity<IdentityUserRole<string>>()
                    .HasData(new IdentityUserRole<string>
                    {
                        UserId = johnDoe.Id,
                        RoleId = administrator.Id
                    });
            }
        }
    }
}
