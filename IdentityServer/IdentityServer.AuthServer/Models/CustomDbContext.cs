using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.AuthServer.Models
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions opts) : base(opts)
        {

        }

        public DbSet<CustomUser> CustomUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomUser>().HasData(
           new CustomUser
            {
                Id = 1,
                Email = "halitakkus03@gmail.com",
                Password = "password"
            },
           new CustomUser
           {
               Id = 2,
               Email = "test@gmail.com",
               Password = "password"
           },
           new CustomUser
           {
               Id = 3,
               Email = "test2@gmail.com",
               Password = "password"
           }
           );

            base.OnModelCreating(modelBuilder);
        }
    }
}
