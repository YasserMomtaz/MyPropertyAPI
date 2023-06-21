using DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Context
{
    public class MyProperyContext:IdentityDbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Appartment> Appartments { get; set; }
        public DbSet<Broker> Broker { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Photo> Photo { get; set; }

        public DbSet<Searched> Searched { get; set; }

        public DbSet<UserApartement> UserAppartement { get; set; }
        public DbSet<SoldAppartement> SoldAppartement { get; set; }

        public MyProperyContext(DbContextOptions<MyProperyContext> options) : base(options)
        {

        }

        public MyProperyContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        builder.Entity<UserApartement>()
            .HasKey(app => new { app.UserId, app.ApartementId });


            builder.Entity<Broker>().HasMany(o => o.Appartment)
                .WithOne(o=>o.Broker).HasForeignKey(o=>o.BrokerId).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Admin>().HasMany(o => o.Appartment)
                .WithOne(o => o.Admin).HasForeignKey(o=>o.AdminId).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>().HasMany(o => o.Appartment)
                .WithOne(o => o.User).HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Broker>().HasMany(o => o.SoldAppartment)
            .WithOne(o => o.Broker).HasForeignKey(o => o.BrokerId).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Admin>().HasMany(o => o.SoldAppartment)
                .WithOne(o => o.Admin).HasForeignKey(o => o.AdminId).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>().HasMany(o => o.SoldAppartment)
                .WithOne(o => o.User).HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.NoAction);

           builder.Entity<Appartment>()
                        .Property(e => e.BrokerId)
                        .IsRequired(false);       
            builder.Entity<Appartment>()
                        .Property(e => e.AdminId)
                        .IsRequired(false);


        }
  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
