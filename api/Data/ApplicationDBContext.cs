using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext :IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        :base(dbContextOptions)
        {
            
        }

        public DbSet<AppUser> AppUser{get;set;}
        public DbSet<Reservation> Reservation{get;set;}
        public DbSet<Service>Service{get;set;}


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
             List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id="Admin",
                    Name="Admin",
                    NormalizedName="ADMIN"
                },
                new IdentityRole
                {
                    Id="User",
                    Name="User",
                    NormalizedName="USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

        }
    }
}