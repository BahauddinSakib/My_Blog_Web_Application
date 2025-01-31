﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "49f32a1d-721b-45dc-b40f-39795bc3cd09";
            var writerRoleId = "891db9bd-de7b-4317-a988-bdd78af30b0e";

            //Create reader and writer role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            //seed the role
            builder.Entity<IdentityRole>().HasData(roles);

            //Create an Admin User
            var adminUserId = "3d7492d9-3737-4828-99d1-f7ecaa153c3b";

            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@codepulse.com",
                Email = "admin@codepulse.com",
                NormalizedEmail = "admin@codepulse.com".ToUpper(),
                NormalizedUserName = "admin@codepulse.com".ToUpper()
            };
            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

            builder.Entity<IdentityUser>().HasData(admin);


            //Give roles to admin

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                  UserId = adminUserId,
                  RoleId = readerRoleId
                },
                 new()
                {
                  UserId = adminUserId,
                  RoleId = writerRoleId
                }

            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
